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
            FlexibleColumns = true;
            Fillable = true;
        }

        // Reads the data in AIR file format
        public override bool ReadData(ref STechStreamReader SR)
        {
            try
            {
                double dblRes;
                string line;
                DataLine thisDataLine;
                line = SR.ReadLine();
                while (line != null)
                {
                    // First line has date and data source
                    thisDataLine = new DataLine();
                    thisDataLine.ParseDate(line);
                    thisDataLine.Source = line.Substring(13);

                    // second line has rain quality data
                    line = SR.ReadLine();
                    thisDataLine.Values = new List<double>();
                    for (int i = 0; i < NumParameters; i++)
                    {
                        thisDataLine.Values.Add(Double.TryParse(line.Substring(8 + 8 * i, 8), out dblRes) ? dblRes : 0);
                    }

                    // third line has air quality data
                    line = SR.ReadLine();
                    thisDataLine.SecondaryValues = new List<double>();
                    for (int i = 0; i < NumParameters; i++)
                    {
                        thisDataLine.SecondaryValues.Add(Double.TryParse(line.Substring(8 + 8 * i, 8), out dblRes) ? dblRes : 0);
                    }
                    TheData.Add(thisDataLine);
                    line = SR.ReadLine();
                }
            }
            catch (Exception e)
            {
                if (SR != null)
                    Debug.WriteLine("Error in AIR file.  Badly formatted data at line = " + SR.LineNum);
                else
                    Debug.WriteLine("Error opening StreamReader for AIR file " + filename);
                return false;
            }
            return true;

        }

            // Writes all the lines of data
        public override bool WriteData(ref STechStreamWriter SW)
        {
            for (int ii = 0; ii < TheData.Count(); ii++)
            {
                // Write the date and time using a 24-hour clock
                SW.Write("{0}", TheData[ii].Date.ToString("ddMMyyyy HHmm"));
                SW.WriteLine(TheData[ii].Source);
                // Rain quality data
                SW.Write("RAIN--MG");
                for (int jj = 0; jj < NumParameters; jj++)
                {
                    SW.WriteDouble(TheData[ii].Values[jj]);
                }
                SW.WriteLine("");
                // Air quality data
                SW.Write("AIRQ--MG");
                for (int jj = 0; jj < NumParameters; jj++)
                {
                    SW.WriteDouble(TheData[ii].SecondaryValues[jj]);
                }
                SW.WriteLine("");
            }

            return true;
        }

    }
}
