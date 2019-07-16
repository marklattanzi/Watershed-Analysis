using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace warmf {
    
    class METFile : DataFile {
		public List<double> precip;
		public List<double> minTemp;
		public List<double> maxTemp;
		public List<double> cloudCover;
		public List<double> dewPointTemp;
		public List<double> airPressure;
		public List<double> windSpeed;

		// labels for graph in array; may be an easier way --MRL
		public static readonly GraphLabels[] labels = new GraphLabels[] {
				new GraphLabels() { key="precip", xaxis="Time", yaxis="Precipitation (cm)"},
				new GraphLabels() { key="mintemp", xaxis="Time", yaxis="Minimum Temperature (C)"},
				new GraphLabels() { key="maxtemp", xaxis="Time", yaxis="Maximum Temperature (C)"},
				new GraphLabels() { key="cloud", xaxis="Time", yaxis="Cloud Cover"},
				new GraphLabels() { key="dewpoint", xaxis="Time", yaxis="Dew Point Temperature (C)"},
				new GraphLabels() { key="airpressure", xaxis="Time", yaxis="Air Pressure (mbar)"},
				new GraphLabels() { key="windspeed", xaxis="Time", yaxis="Wind Speed (meters/sec)"},
		};

		// methods
		public METFile(string fname) { filename = fname; NumParameters = 7;}

		public bool ReadMETFile() {
			STechStreamReader sr = null;

			try {
				int intRes;
				double dblRes;
				string line;
				int day, month, year, hour, minute;
                DataLine thisDataLine;
                sr = new STechStreamReader(filename);
                
                ReadHeader(ref sr);

				date = new List<DateTime>();
				precip = new List<double>();
				minTemp = new List<double>();
				maxTemp = new List<double>();
				cloudCover = new List<double>();
				dewPointTemp = new List<double>();
				airPressure = new List<double>();
				windSpeed = new List<double>();
				comment = new List<string>();
				line = sr.ReadLine();
				while (line != null) {
                    thisDataLine = new DataLine();
                    thisDataLine.ParseString(line, NumParameters);
                    TheData.Add(thisDataLine);
                    date.Add(TheData[TheData.Count - 1].Date);
                    precip.Add(TheData[TheData.Count - 1].Values[0]);
                    minTemp.Add(TheData[TheData.Count - 1].Values[1]);
                    maxTemp.Add(TheData[TheData.Count - 1].Values[2]);
                    cloudCover.Add(TheData[TheData.Count - 1].Values[3]);
                    dewPointTemp.Add(TheData[TheData.Count - 1].Values[4]);
                    airPressure.Add(TheData[TheData.Count - 1].Values[5]);
                    windSpeed.Add(TheData[TheData.Count - 1].Values[6]);
                    comment.Add(TheData[TheData.Count - 1].Source);
                    line = sr.ReadLine();
				}
			}
			catch (Exception e) {
				if (sr != null)
					Debug.WriteLine("Error in MET file.  Badly formatted data at line = " + sr.LineNum);
					else
					Debug.WriteLine("Error opening StreamReader for MET file " + filename);
				return false;
			}
			return true;
		}

		public bool WriteMETFile() {
			STechStreamWriter sw = null;
			try {
				sw = new STechStreamWriter(filename, false);
                WriteVersionLatLongName(ref sw);
				for (int ii=0; ii < TheData.Count(); ii++) {
                    sw.WriteLine("{0:ddMMyyyy HHmm}{1,8:0.###}{2,8:0.#}{3,8:0.#}{4,8:0.##}{5,8:0.#}{6,8:0.#}{7,8:0.#}{8}",
                        Convert.ToDateTime(date[ii].ToString()),
                        TheData[ii].Values[0],
                        TheData[ii].Values[1],
                        TheData[ii].Values[2],
                        TheData[ii].Values[3],
                        TheData[ii].Values[4],
                        TheData[ii].Values[5],
                        TheData[ii].Values[6],
                        TheData[ii].Source);
                }
                sw.Close();
				return true;
			}
			catch (Exception e) {
				if (sw != null)
					Debug.WriteLine("Error writing MET file {0} at line {1} ", filename, sw.LineNum);
				else
					Debug.WriteLine("Error writing MET file {0}.  Problem with file creation.", filename);
				return true;
			}
		}
	}
}
