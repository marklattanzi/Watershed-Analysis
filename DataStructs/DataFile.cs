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
        public List<double> SecondaryValues;
        public string Source;

        public bool ParseDate(string TheString)
        {
            int intRes;

            int day = Int32.TryParse(TheString.Substring(0, 2), out intRes) ? intRes : 0;
            int month = Int32.TryParse(TheString.Substring(2, 2), out intRes) ? intRes : 0;
            int year = Int32.TryParse(TheString.Substring(4, 4), out intRes) ? intRes : 0;
            int hour = Int32.TryParse(TheString.Substring(9, 2), out intRes) ? intRes : 0;
            int minute = Int32.TryParse(TheString.Substring(11, 2), out intRes) ? intRes : 0;
            Date = new DateTime(year, month, day, hour, minute, 0);

            return true;
        }

        public bool ParseString(string TheString, int NumValues)
        {
            //int intRes, day, month, year, hour, minute;

            ParseDate(TheString);
/*            day = Int32.TryParse(TheString.Substring(0, 2), out intRes) ? intRes : 0;
            month = Int32.TryParse(TheString.Substring(2, 2), out intRes) ? intRes : 0;
            year = Int32.TryParse(TheString.Substring(4, 4), out intRes) ? intRes : 0;
            hour = Int32.TryParse(TheString.Substring(9, 2), out intRes) ? intRes : 0;
            minute = Int32.TryParse(TheString.Substring(11, 2), out intRes) ? intRes : 0;
            Date = new DateTime(year, month, day, hour, minute, 0);*/

            double dblRes;
            Values = new List<double>();
            for (int i = 0; i < NumValues; i++)
            {
                Values.Add(Double.TryParse(TheString.Substring(13 + 8 * i, 8), out dblRes) ? dblRes : 0);
            }
            Source = TheString.Substring(13 + 8 * NumValues);

            return true;
        }

        public bool WriteDataLine(ref STechStreamWriter SW)
        {
            // Write the date and time using a 24-hour clock
            SW.Write("{0}", Date.ToString("ddMMyyyy HHmm"));
            for (int ii = 0; ii < Values.Count(); ii++)
            {
                SW.WriteDouble(Values[ii]);
            }
            SW.WriteLine(Source);

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
        public List<string> ParameterNames;
        public List<string> ParameterCodes;

        public string filename { get; set; }
        public string shortName { get; set; }
        public int version { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<DataLine> TheData;

        public DataFile()
        {
            NumLines = 0;
            NumParameters = 0;
            NumGroups = 1;
            ParameterNames = new List<string>();
            ParameterCodes = new List<string>();
            TheData = new List<DataLine>();
        }

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

        public virtual bool ReadHeader(ref STechStreamReader SR)
        {
            ReadVersionLatLongName(ref SR);

            return ReadParameters(ref SR);
        }

        // Reads the number and codes of parameters
        public virtual bool ReadParameters(ref STechStreamReader SR)
        {
            try
            { 
                string line = SR.ReadLine();
                Int32.TryParse(line.Substring(5, 8), out NumParameters);
                for (int ii = 0; ii < NumParameters; ii++)
                {
                    ParameterCodes.Add(line.Substring(13 + 8 * ii, 8));
                    ParameterNames.Add(Global.coe.GetParameterNameAndUnitsFromCode(ParameterCodes[ii]));
                }
            }
            catch (Exception e)
            {
                if (SR != null)
                    Debug.WriteLine("Error in data file.  Badly formatted data at line = " + SR.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for data file " + filename);
                return false;
            }

            return true;
        }

        // Reads the lines of data
        public virtual bool ReadData(ref STechStreamReader SR)
        {
            DataLine thisDataLine;
            string line = SR.ReadLine();
            while (line != null)
            {
                thisDataLine = new DataLine();
                thisDataLine.ParseString(line, NumParameters);
                TheData.Add(thisDataLine);
                line = SR.ReadLine();
            }

            return true;
        }

        public virtual bool ReadFile()
        {
            STechStreamReader sr = null;

            try
            {
                string line;               
                DataLine thisDataLine;
                sr = new STechStreamReader(filename);

                ReadHeader(ref sr);
                ReadData(ref sr);
            }
            catch (Exception e)
            {
                if (sr != null)
                    Debug.WriteLine("Error in data file.  Badly formatted data at line = " + sr.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for data file " + filename);
                return false;
            }
            return true;

        }

        public bool WriteVersionLatLongName(ref STechStreamWriter SW)
        {
            try
            {
                SW.WriteLine("VERSION {0, 8}", version);
                SW.WriteLine("Latitude:{0, 10:F4} Longitude:{1,10:F4}{2}", latitude, longitude, shortName);
            }
            catch (Exception e)
            {
                if (SW != null)
                    Debug.WriteLine("Error writing MET file {0} at line {1} ", filename, SW.LineNum);
                else
                    Debug.WriteLine("Error writing MET file {0}.  Problem with file creation.", filename);
                return true;
            }

            return true;
        }

        public virtual bool WriteParameters(ref STechStreamWriter SW)
        {
            SW.Write("     ");
            SW.WriteInt(NumParameters);
            for (int ii = 0; ii < NumParameters; ii++)
                SW.Write(ParameterCodes[ii]);
            SW.Write("\n");

            return true;
        }

        public virtual bool WriteHeader(ref STechStreamWriter SW)
        {
            WriteVersionLatLongName(ref SW);
            return WriteParameters(ref SW);
        }

        // Writes all the lines of data
        public virtual bool WriteData(ref STechStreamWriter SW)
        {
            for (int ii = 0; ii < TheData.Count(); ii++)
            {
                TheData[ii].WriteDataLine(ref SW);
            }

            return true;
        }

        // Writes the entire file
        public bool WriteFile()
        {
            STechStreamWriter sw = null;
            try
            {
                sw = new STechStreamWriter(filename, false);
                WriteHeader(ref sw);
                WriteData(ref sw);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                if (sw != null)
                    Debug.WriteLine("Error writing data file {0} at line {1} ", filename, sw.LineNum);
                else
                    Debug.WriteLine("Error writing data file {0}.  Problem with file creation.", filename);
                return true;
            }

        }

    }
}
