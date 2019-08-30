using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace warmf
{
    public class Output
    {
        public int numSimDays, timeStepPerDay, numConstits, numEntities, numOutputs, numFeatureOutputs;
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
                int catchNumber = Global.coe.GetCatchmentNumberFromID(catchID);


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

                        //dimension and initialize the catchment output array
                        numFeatureOutputs = Global.coe.catchments[catchNumber].numSoilLayers + 2;
                        output = new List<float>[numFeatureOutputs, numConstits];
                        for (int i = 0; i < numFeatureOutputs; i++)
                        {
                            for (int j = 0; j < numConstits; j++)
                            {
                                output[i, j] = new List<float>();
                            }
                        }

                        //get list of parameters in CAT
                        for (int j = 0; j < numConstits; j++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
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
                                outputPosition = i;
                                positionSet = true;
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++)
                        {
                            int i2 = 0;
                            int temp = reader.ReadInt32(); //Day number (not used for anything)
                            for (int j = 0; j < numOutputs; j++)//loop through catchment outputs until you find the right position
                            {
                                for (int k = 0; k < numConstits; k++)
                                {
                                    bytes = reader.ReadBytes(4);
                                    if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
                                    {
                                        output[i2, k].Add(BitConverter.ToSingle(bytes, 0));
                                    }
                                }
                                if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
                                    i2 = i2 + 1;
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

        //Read the .RIV File
        public bool ReadRIV(string fileName, int riverID)
        {
            try
            {
                int tempParameterNumber, parameterNumber;
                string nameUnits;
                int outputID;
                long outputPosition = -1;
                byte[] bytes;
                Boolean positionSet;
                int riverNumber = Global.coe.GetRiverNumberFromID(riverID);

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
                        numFeatureOutputs = 2; //two outputs per river segment: water column and bed sediment

                        //dimension and initialize the river output array
                        output = new List<float>[numFeatureOutputs, numConstits];
                        for (int i = 0; i < numFeatureOutputs; i++)
                        {
                            for (int j = 0; j < numConstits; j++)
                            {
                                output[i, j] = new List<float>();
                            }
                        }

                        //get list of parameters in RIV
                        for (int j = 0; j < numConstits; j++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
                        }

                        //find the position of the first desired output within the RIV file
                        //(first output is water column, then bed sediment)
                        numOutputs = reader.ReadInt32();
                        positionSet = false;
                        for (int i = 0; i < numOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            outputID = outputID % 65536;
                            if (outputID == riverID && outputID > 0 && positionSet == false)
                            {
                                outputPosition = i;
                                positionSet = true;
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++)
                        {
                            int i2 = 0;
                            int temp = reader.ReadInt32(); //Day number (not used for anything)
                            for (int j = 0; j < numOutputs; j++)//loop through river outputs until you find the right position
                            {
                                for (int k = 0; k < numConstits; k++)
                                {
                                    bytes = reader.ReadBytes(4);
                                    if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
                                    {
                                        output[i2, k].Add(BitConverter.ToSingle(bytes, 0));
                                    }
                                }
                                if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
                                    i2 = i2 + 1;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("RIV file read failure: " + e.ToString());
                return false;
            }
        }

        //Read the .RIV or .CAT File
        public bool ReadOutput(string fileName, int entityID)
        {
            try
            {
                int tempParameterNumber, parameterNumber;
                string nameUnits;
                int outputID;
                long outputPosition = -1;
                byte[] bytes;
                Boolean positionSet;
                int entityNumber;

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
                        if (fileName.Contains(".CAT") == true) //catchment output
                        {
                            entityNumber = Global.coe.GetCatchmentNumberFromID(entityID);
                            numFeatureOutputs = Global.coe.catchments[entityNumber].numSoilLayers + 2;
                        }
                        else //river output
                        {
                            entityNumber = Global.coe.GetRiverNumberFromID(entityID);
                            numFeatureOutputs = 2; //two outputs per river segment: water column and bed sediment
                        }

                        //dimension and initialize the river output array
                        output = new List<float>[numFeatureOutputs, numConstits];
                        for (int i = 0; i < numFeatureOutputs; i++)
                        {
                            for (int j = 0; j < numConstits; j++)
                            {
                                output[i, j] = new List<float>();
                            }
                        }

                        //get list of parameters in output file
                        for (int j = 0; j < numConstits; j++)
                        {
                            parameterNumber = reader.ReadInt32();
                            nameUnits = Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                            constituentNameUnits.Add(nameUnits);
                        }

                        //find the position of the first desired output within the output file
                        numOutputs = reader.ReadInt32();
                        positionSet = false;
                        for (int i = 0; i < numOutputs; i++)
                        {
                            outputID = reader.ReadInt32();
                            outputID = outputID % 65536;
                            if (outputID == entityNumber && outputID > 0 && positionSet == false)
                            {
                                outputPosition = i;
                                positionSet = true;
                            }
                        }

                        for (int i = 0; i < (numSimDays * timeStepPerDay); i++)
                        {
                            int i2 = 0;
                            int temp = reader.ReadInt32(); //Day number (not used for anything)
                            for (int j = 0; j < numOutputs; j++)//loop through outputs until you find the right position
                            {
                                for (int k = 0; k < numConstits; k++)
                                {
                                    bytes = reader.ReadBytes(4);
                                    if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
                                    {
                                        output[i2, k].Add(BitConverter.ToSingle(bytes, 0));
                                    }
                                }
                                if (j >= outputPosition && j < (outputPosition + numFeatureOutputs))
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
                }
                else //river output
                {
                    Debug.WriteLine("RIV file read failure: " + e.ToString());
                }
                return false;
            }
        }
    }
}