using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace warmf
{
    class AIRFile : DataFile
    {
        // methods
        public AIRFile(string fname)
        {
            filename = fname;
        }

        public override bool ReadHeader(ref STechStreamReader SR)
        {
            base.ReadHeader(ref SR);

            return ReadParameters(ref SR);
        }

        public override bool ReadFile()
        {
            STechStreamReader sr = null;

            try
            {
                double dblRes;
                string line;
                DataLine thisDataLine;
                sr = new STechStreamReader(filename);

                ReadHeader(ref sr);

                line = sr.ReadLine();
                while (line != null)
                {
                    // First line has date and data source
                    thisDataLine = new DataLine();
                    thisDataLine.ParseDate(line);
                    thisDataLine.Source = line.Substring(13);

                    // second line has rain quality data
                    line = sr.ReadLine();
                    thisDataLine.Values = new List<double>();
                    for (int i = 0; i < NumParameters; i++)
                    {
                        thisDataLine.Values.Add(Double.TryParse(line.Substring(8 + 8 * i, 8), out dblRes) ? dblRes : 0);
                    }

                    // third line has air quality data
                    line = sr.ReadLine();
                    thisDataLine.SecondaryValues = new List<double>();
                    for (int i = 0; i < NumParameters; i++)
                    {
                        thisDataLine.SecondaryValues.Add(Double.TryParse(line.Substring(8 + 8 * i, 8), out dblRes) ? dblRes : 0);
                    }
                    TheData.Add(thisDataLine);
                    line = sr.ReadLine();
                }
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
    }
}
