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

        public override bool ReadHeader(ref STechStreamReader SR)
        {
            base.ReadHeader(ref SR);

            try
            {
                string line = SR.ReadLine();
                Int32.TryParse(line.Substring(5, 8), out NumParameters);
                for (int ii = 0; ii < NumParameters; ii++)
                {
                    ParameterCodes.Add("MFLO");
                    ParameterNames.Add(Global.coe.GetParameterNameFromCode(ParameterCodes[ii]));
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

