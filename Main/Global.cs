using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf {


	static class Global {
		public const string checkmark = "✓";
		public const string DATA_DIR = "data\\";

		public static Coefficients coe = new Coefficients();	// so we can access coefficients from anywhere - may not be necessary...  MRL
	}
}
