using System;
using System.Collections.Generic;
using System.IO;

namespace warmf {
    public class OutputRiverCatchment
    {
        public int numSimDays, timeStepPerDay, numConstits, numEntities;
        public DateTime startDate;
        public List<int> constituentNumbers;
        public List<int> entityNumbers;
        public List<List<double>> output;

        public void readCAT(string fileName)
        {
            int tempParameterNumber, parameterNumber;

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    tempParameterNumber = Global.coe.GetParameterNumberFromCode("MTEMP");
                    numSimDays = reader.ReadInt32();
                    timeStepPerDay = reader.ReadInt32();
                    startDate = reader.ReadInt32();
                    monthStartDate = reader.ReadInt32();
                    yearStartDate = reader.ReadInt32();
                    numConstits = reader.ReadInt32();



                    for (int i = 0; i < numConstits; i++)
                    {
                        parameterNumber = reader.ReadInt32();
                        Global.coe.GetParameterNameAndUnitsFromNumber(parameterNumber + tempParameterNumber);
                    }
                }


            }

        }
    }

}