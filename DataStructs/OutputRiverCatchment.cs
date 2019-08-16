using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace warmf {
    public class Output {
        public int numSimDays, timeStepPerDay, numConstits, numEntities, numCatchOutputs;
        public int startDateDay, startDateMonth, startDateYear;
        public List<int> constituentNumbers;
        public List<string> constituentNameUnits = new List<string>();
        public List<int> entityNumbers;
        public List<List<double>> output = new List<List<double>>();

        //Output methods
        //Read the .CAT File
        public bool ReadCAT(string fileName, int catchID)
        {
            try
            {
                int tempParameterNumber, parameterNumber;
                string nameUnits;
                int outputID;
                List<double>timestepOutput = new List<double>();
                long outputPosition = -1;

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

                        //get list of parameters in CAT
                        for (int i = 0; i < numConstits; i++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
                        }

                        numCatchOutputs = reader.ReadInt32();
                        for (int i = 0; i < numCatchOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            outputID = outputID % 65536;
                            if (outputID == catchID && outputID > 0)
                            {
                                outputPosition = i+1; //combined output for now
                            }
                        }

                        for (int i = 0; i < (numSimDays*timeStepPerDay); i++)
                        {
                            reader.ReadInt32(); //Day number
                            for (int j = 0; j < numCatchOutputs; j++)//loop through catchment outputs until you find the right position
                            {
                                reader.ReadInt32();
                                if (j==outputPosition)
                                {
                                    for (int k = 0; k < numConstits; k++)
                                    {
                                        timestepOutput.Add(reader.ReadDouble());
                                    }
                                    output.Add(timestepOutput);
                                    timestepOutput.Clear();//cant actually do this...How to solve?
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