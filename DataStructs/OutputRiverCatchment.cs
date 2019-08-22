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
        public List<float>[] output; //array of lists - one list for each output constituent

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
                        output = new List<float>[numConstits];

                        //get list of parameters in CAT and initialize each list in output array
                        for (int i = 0; i < numConstits; i++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
                            output[i] = new List<float>();
                        }

                        numCatchOutputs = reader.ReadInt32();
                        for (int i = 0; i < numCatchOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            outputID = outputID % 65536;
                            if (outputID == catchID && outputID > 0)
                            {
                                outputPosition = i + 1; //combined output for now
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++)
                        {
                            int temp = reader.ReadInt32(); //Day number
                            for (int j = 0; j < numCatchOutputs; j++)//loop through catchment outputs until you find the right position
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