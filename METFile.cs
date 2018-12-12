using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace warmf {
	class METFile {
		public string filename;
		public int version;
		public double latitude, longitude;

		public List<DateTime> date;
		public List<string> clockTime;
		public List<double> precip;
		public List<double> minTemp;
		public List<double> maxTemp;
		public List<double> cloudCover;
		public List<double> dewPointTemp;
		public List<double> airPressure;
		public List<double> windSpeed;
		public List<string> comment;

		public struct METGraphLabels {
			public string key;
			public string xaxis;
			public string yaxis;
		}

		public static readonly METGraphLabels[] labels = new METGraphLabels[] {
				new METGraphLabels() { key="precip", xaxis="Time", yaxis="Precipitation (cm)"},
				new METGraphLabels() { key="mintemp", xaxis="Time", yaxis="Minimum Temperature (C)"},
				new METGraphLabels() { key="maxtemp", xaxis="Time", yaxis="Maximum Temperature (C)"},
				new METGraphLabels() { key="cloud", xaxis="Time", yaxis="Cloud Cover"},
				new METGraphLabels() { key="dewpoint", xaxis="Time", yaxis="Dew Point Temperature (C)"},
				new METGraphLabels() { key="airpressure", xaxis="Time", yaxis="Air Pressure (mbar)"},
				new METGraphLabels() { key="windspeed", xaxis="Time", yaxis="Wind Speed (meters/sec)"},
		};

		// methods

		public METFile(string fname) {
			filename = fname;
		}

		public bool readMETFile() {
			STechStreamReader sr = null;

			try {
				int intRes;
				double dblRes;
				string line;
				int day, month, year;
				sr = new STechStreamReader(filename);

				line = sr.ReadLine();
				if (line.StartsWith("VERSION"))
					version = Int32.TryParse(line.Substring(8, 8), out intRes) ? intRes : 0;
				else {
					Debug.WriteLine("Error in MET file.  Version number is missing. Continuing.");
					version = -1;
				}

				line = sr.ReadLine();
				latitude = Double.TryParse(line.Substring(9, 10), out dblRes) ? dblRes : 0;
				longitude = Double.TryParse(line.Substring(30, 10), out dblRes) ? dblRes : 0;
				date = new List<DateTime>();
				clockTime = new List<string>();
				
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
					date.Add(new DateTime(year, month, day));
					clockTime.Add(line.Substring(8, 5));
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
				Debug.WriteLine("Error in MET file.  Badly formatted data at line = " + sr.LineNum);
				return false;
			}
			return true;
		}

		public bool writeMETfile(DataGridView grid) {
			
			return true;
		}
	}
}
