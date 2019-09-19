using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace warmf
{
    public class Output
    {
        public int numSimDays, timeStepPerDay, numConstits, numEntities, numOutputs, numFeatureOutputs;
        public int startDateDay, startDateMonth, startDateYear;
        public List<int> constituentNumbers;
        public List<string> constituentNameUnits = new List<string>();
        public List<string> constituentFortranCode = new List<string>();
        public List<int> entityNumbers;
        public List<float>[,] output; //2d array of lists - one list for each output (combined, surface, soil layer 1, etc) - constituent combination

        //Output methods
        //Read the .RIV or .CAT File
        public bool ReadOutput(string fileName, int entityID)
        {
            try
            {
                int tempParameterNumber, parameterNumber;
                string nameUnits, fortranCode;
                int outputID, segmentID, outputFraction, fractionCount;
                long outputPosition = -1;
                float result;
                Boolean positionSet;
                
                if (File.Exists(fileName))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                    {
                        tempParameterNumber = Global.coe.GetParameterNumberFromCode("MTEMP");
                        numSimDays = reader.ReadInt32();
                        timeStepPerDay = reader.ReadInt32();
                        startDateDay = reader.ReadInt32();
                        startDateMonth = reader.ReadInt32();
                        startDateYear = reader.ReadInt32();
                        numConstits = reader.ReadInt32();

                    

                        //get lists of name and units, and Fortran code for each parameter in output file
                        for (int j = 0; j < numConstits; j++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
                            fortranCode = Global.coe.GetParameterCodeFromNumber(parameterNumber + tempParameterNumber);
                            constituentFortranCode.Add(fortranCode);
                        }

                        //find the position of the first desired output within the output file
                        numOutputs = reader.ReadInt32();
                        positionSet = false;
                        fractionCount = 0;
                        for (int i = 0; i < numOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            segmentID = outputID % 65536;
                            outputFraction = outputID / 65536;
                            if (segmentID == entityID)
                            {
                                if (positionSet==false) //first fraction for segment
                                {
                                    outputPosition = i;
                                }
                                positionSet = true;
                                fractionCount = fractionCount + 1;
                            }
                        }

                        //dimension and initialize the output array
                        output = new List<float>[fractionCount, numConstits];
                        for (int i = 0; i < fractionCount; i++)
                        {
                            for (int j = 0; j < numConstits; j++)
                            {
                                output[i, j] = new List<float>();
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++) //for each timestep
                        {
                            int i2 = 0;
                            int temp = reader.ReadInt32(); //Day number (not used for anything)
                            for (int j = 0; j < numOutputs; j++) //loop through outputs
                            {
                                for (int k = 0; k < numConstits; k++) //loop through constituents
                                {
                                    result = reader.ReadSingle();
                                    if (j >= outputPosition && j < (outputPosition + fractionCount))
                                    {
                                        output[i2, k].Add(result);
                                    }
                                }
                                if (j >= outputPosition && j < (outputPosition + fractionCount))
                                    i2 = i2 + 1;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                if (fileName.Contains(".CAT") == true) //catchment output
                {
                    Debug.WriteLine("CAT file read failure: " + e.ToString());
                    MessageBox.Show("CAT file read Failure: " + e.ToString());
                }
                else //river output
                {
                    Debug.WriteLine("RIV file read failure: " + e.ToString());
                    MessageBox.Show("RIV file read failure: " + e.ToString());
                }
                return false;
            }
        }
    }
}