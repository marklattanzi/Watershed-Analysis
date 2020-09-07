using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace warmf
{
    class FLOFile : DataFile
    {
        // methods
        public FLOFile(string fname)
        {
            filename = fname;
        }

        public override bool IsRegularInterval()
        {
            return false;
        }

        public override bool ReadParameters(ref STechStreamReader SR)
        {
            try
            {
                string flowFortranCode = "MFLO";
                string line = SR.ReadLine();
                Int32.TryParse(line.Substring(5, 8), out NumParameters);
                for (int ii = 0; ii < NumParameters; ii++)
                {
                    string nameString = line.Substring(13 + 8 * ii, 8);
                    if (String.IsNullOrWhiteSpace(nameString))
                        ParameterCodes.Add(flowFortranCode + "    ");
                    else
                        ParameterCodes.Add(nameString);

                    // Remove trailing spaces from the name of the diversion
                    if (nameString.Contains(flowFortranCode))
                        nameString = "";
                    else
                        nameString = nameString.Trim() + " ";
                    ParameterNames.Add(nameString + Global.coe.GetParameterNameAndUnitsFromCode(flowFortranCode));
                }
            }
            catch (Exception e)
            {
                if (SR != null)
                    Debug.WriteLine("Error in FLO file.  Badly formatted data at line = " + SR.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for data file " + filename);
                return false;
            }

            return true;
        }
    }
}

