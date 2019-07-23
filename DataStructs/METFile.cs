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
		public METFile(string fname)
        {
            filename = fname;
            NumParameters = 7;
/*            ParameterNames.Add(new string("Precipitation, cm"));
            ParameterNames.Add("Min. Temperature, C");
            ParameterNames.Add("Max. Temperature, C");
            ParameterNames.Add("Cloud Cover");
            ParameterNames.Add("Dew Point Temperature, C");
            ParameterNames.Add("Air Pressure, mbar");
            ParameterNames.Add("Wind Speed, m/s");*/
        }

    }
}
