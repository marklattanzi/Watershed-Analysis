using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf
{
    // The DataLine class stores all the information on one line of a time series data file
    public class DataLine
    {
        public DateTime Date { get; set; }
        public List<double> Values;
        public string Source;

        public bool ParseString(string TheString, int NumValues)
        {
            int intRes, day, month, year, hour, minute;

            day = Int32.TryParse(TheString.Substring(0, 2), out intRes) ? intRes : 0;
            month = Int32.TryParse(TheString.Substring(2, 2), out intRes) ? intRes : 0;
            year = Int32.TryParse(TheString.Substring(4, 4), out intRes) ? intRes : 0;
            hour = Int32.TryParse(TheString.Substring(9, 2), out intRes) ? intRes : 0;
            minute = Int32.TryParse(TheString.Substring(11, 2), out intRes) ? intRes : 0;
            Date = new DateTime(year, month, day, hour, minute, 0);

            double dblRes;
            Values = new List<double>();
            for (int i = 0; i < NumValues; i++)
            {
                Values.Add(Double.TryParse(TheString.Substring(13 + 8 * i, 8), out dblRes) ? dblRes : 0);
            }
            Source = TheString.Substring(69);

            return true;
        }
    }

    // The DataFile class stores the entire contents of a time series data file
    // Derived classes are used for different types of data files
    public class DataFile
    {
        public int NumLines;
        public int NumParameters;
        public int NumGroups;

        public string filename { get; set; }
        public string shortName { get; set; }
        public int version { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<DateTime> date;
        public List<string> comment;
        public List<DataLine> TheData;

        public DataFile() { NumLines = 0; NumParameters = 0; NumGroups = 1; TheData = new List<DataLine>(); }

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

        public bool ReadHeader(ref STechStreamReader SR)
        {
            return ReadVersionLatLongName(ref SR);
        }

        

    }
}
