﻿using System;
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
		public METFile(string fname) { filename = fname; }

		public bool ReadMETFile() {
			STechStreamReader sr = null;

			try {
				int intRes;
				double dblRes;
				string line;
				int day, month, year, hour, minute;
				sr = new STechStreamReader(filename);

                ReadVersionLatLongName(ref sr);

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
					day = Int32.TryParse(line.Substring(0, 2), out intRes) ? intRes : 0;
					month = Int32.TryParse(line.Substring(2, 2), out intRes) ? intRes : 0;
					year = Int32.TryParse(line.Substring(4, 4), out intRes) ? intRes : 0;
					hour = Int32.TryParse(line.Substring(9, 2), out intRes) ? intRes : 0;
					minute = Int32.TryParse(line.Substring(11, 2), out intRes) ? intRes : 0;
					date.Add(new DateTime(year, month, day, hour, minute, 0));
					precip.Add(Double.TryParse(line.Substring(13, 8), out dblRes) ? dblRes : 0);
					minTemp.Add(Double.TryParse(line.Substring(21, 8), out dblRes) ? dblRes : 0);
					maxTemp.Add(Double.TryParse(line.Substring(29, 8), out dblRes) ? dblRes : 0);
					cloudCover.Add(Double.TryParse(line.Substring(37, 8), out dblRes) ? dblRes : 0);
					dewPointTemp.Add(Double.TryParse(line.Substring(45, 8), out dblRes) ? dblRes : 0);
					airPressure.Add(Double.TryParse(line.Substring(53, 8), out dblRes) ? dblRes : 0);
					windSpeed.Add(Double.TryParse(line.Substring(61, 8), out dblRes) ? dblRes : 0);
					comment.Add(line.Substring(69));
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
				sw.WriteLine("VERSION {0, 8}", version);
				sw.WriteLine("Latitude:{0, 10:F4} Longitude:{1,10:F4}{2}", latitude, longitude, shortName);
				for (int ii=0; ii < date.Count(); ii++) {
					sw.WriteLine("{0:ddMMyyyy HHmm}{1,8:0.###}{2,8:0.#}{3,8:0.#}{4,8:0.##}{5,8:0.#}{6,8:0.#}{7,8:0.#}{8}",
						Convert.ToDateTime(date[ii].ToString()),
						precip[ii], 
						minTemp[ii], 
						maxTemp[ii], 
						cloudCover[ii], 
						dewPointTemp[ii], 
						airPressure[ii], 
						windSpeed[ii], 
						comment[ii]);
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
