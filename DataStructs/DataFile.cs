using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf
{
    class DataFile
    {
        public string filename { get; set; }
        public string shortName { get; set; }
        public int version { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<DateTime> date;
        public List<string> comment;

        public struct GraphLabels
        {
            public string key;
            public string xaxis;
            public string yaxis;
        }

        public bool ReadVersionLatLongName(ref STechStreamReader SR)
        {
            int intRes;
            double dblRes;
            string line;

            line = SR.ReadLine();
            if (line.StartsWith("VERSION"))
                version = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
            else
            {
                Debug.WriteLine("Error in data file.  Version number is missing. Continuing.");
                version = -1;

                return false;
            }

            line = SR.ReadLine();
            latitude = Double.TryParse(line.Substring(9, 10), out dblRes) ? dblRes : 0;
            longitude = Double.TryParse(line.Substring(30, 10), out dblRes) ? dblRes : 0;
            shortName = line.Substring(40);

            return true;
        }

    }
}
