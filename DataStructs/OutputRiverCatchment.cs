using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace warmf {
    public class Output {
        public int numSimDays, timeStepPerDay, numConstits, numEntities, numOutputs, numCatchOutputs;
        public int startDateDay, startDateMonth, startDateYear;
        public List<int> constituentNumbers;
        public List<string> constituentNameUnits = new List<string>();
        public List<int> entityNumbers;
        public List<float>[,] output; //2d array of lists - one list for each output (combined, surface, soil layer 1, etc) - constituent combination

        //Output methods
        //Read the .CAT File
        public bool ReadCAT(string fileName, int catchID)
        {
            try
            {
                int tempParameterNumber, parameterNumber;
                string nameUnits;
                int outputID;
                long outputPosition = -1;
                byte[] bytes;
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

                        //dimension the output array
                        numCatchOutputs = Global.coe.catchments[catchID].numSoilLayers + 2;
                        output = new List<float>[numCatchOutputs,numConstits];

                        //get list of parameters in CAT and initialize each list in output array
                        for (int i = 0; i < numCatchOutputs; i++)
                        {
                            for (int j = 0; j < numConstits; j++)
                            {
                                parameterNumber = reader.ReadInt32();
                                nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                                constituentNameUnits.Add(nameUnits);
                                output[i,j] = new List<float>();
                            }
                        }
                        
                        //find the position of the first desired output within the CAT file
                        //(first output is surface runoff, then combined output, then each soil layer)
                        
                        numOutputs = reader.ReadInt32();
                        positionSet = false;
                        for (int i = 0; i < numOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            outputID = outputID % 65536;
                            if (outputID == catchID && outputID > 0 && positionSet == false)
                            {
                                outputPosition = i; //combined output for now
                                positionSet = true;
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++)
                        {
                            int temp = reader.ReadInt32(); //Day number (not used for anything)
                            for (int j = 0; j < numOutputs; j++)//loop through catchment outputs until you find the right position
                            {
                                for (int k = 0; k < numConstits; k++)
                                {
                                    bytes = reader.ReadBytes(4);
                                    if (j == outputPosition)
                                    {
                                        output[k].Add(BitConverter.ToSingle(bytes, 0));
                                    } 
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("CAT file read failure: " + e.ToString());
                return false;
            }
        }   
    }   
}