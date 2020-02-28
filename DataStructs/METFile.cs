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
		// methods
		public METFile(string fname)
        {
            filename = fname;
            NumParameters = 7;

            ParameterNames.Add("Precipitation, cm");
            ParameterNames.Add("Min. Temperature, C");
            ParameterNames.Add("Max. Temperature, C");
            ParameterNames.Add("Cloud Cover");
            ParameterNames.Add("Dew Point Temperature, C");
            ParameterNames.Add("Air Pressure, mbar");
            ParameterNames.Add("Wind Speed, m/s");

            Fillable = true;
        }

        // Overridden ReadParameters routine to do nothing
        public override bool ReadParameters(ref STechStreamReader SW)
        {
            return true;
        }

        // Overridden WriteParameters routine to do nothing
        public override bool WriteParameters(ref STechStreamWriter SW)
        {
            return true;
        }
    }
}
