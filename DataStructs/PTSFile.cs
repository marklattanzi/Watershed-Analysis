using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace warmf
{
    class PTSFile : DataFile
    {
        // data
        public bool swInternal;
        public int unspecified;
        public double outElevation;
        public double outWidth;
        public string npdesPermit;

        // methods
        public PTSFile(string fname) { filename = fname; }

        public bool ReadHeader()
        {
            string line;
            int intRes;
            double dblRes;

            STechStreamReader sr = null;
            try
            {
                sr = new STechStreamReader(Global.DIR.PTS + filename);
                ReadHeader(ref sr);
            }
            catch (Exception e)
            {
                if (sr != null)
                    Debug.WriteLine("Error in PTS file.  Badly formatted data at line = " + sr.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for PTS file " + filename);
                return false;
            }
            return true;
        }

        public override bool ReadHeader(ref STechStreamReader SR)
        {
            ReadVersionLatLongName(ref SR);
            try
            {
                int intRes;
                double dblRes;
                string line = SR.ReadLine();
                swInternal = line.Substring(0, 8).Contains("0");
                unspecified = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : -1;
                outElevation = Double.TryParse(line.Substring(16, 8), out dblRes) ? dblRes : -1;
                outWidth = Double.TryParse(line.Substring(24, 8), out dblRes) ? dblRes : -1;
                npdesPermit = line.Substring(32);
            }
            catch (Exception e)
            {
                if (SR != null)
                    Debug.WriteLine("Error in PTS file.  Badly formatted data at line = " + SR.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for PTS file " + filename);
                return false;
            }

            return ReadParameters(ref SR);
        }

        public override bool WriteHeader(ref STechStreamWriter SW)
        {
            WriteVersionLatLongName(ref SW);

            // Write parameters specific to point source files
            SW.WriteBool(swInternal);
            SW.WriteInt(unspecified);
            SW.WriteDouble(outElevation);
            SW.WriteDouble(outWidth);
            SW.WriteLine(npdesPermit);

            return base.WriteParameters(ref SW);
        }
    }
}
