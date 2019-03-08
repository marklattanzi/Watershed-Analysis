using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf {
	static class Global {
		public const string checkmark = "✓";
        //Directory Structures
        public struct DIR
        {
            public static string ROOT = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            public const string DATA = "data\\";
            public static string AIR = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "air\\");
            public static string COE = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "coe\\");
            public static string CPA = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "cpa\\");
            public static string FLO = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "flo\\");
            public static string MET = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "met\\");
            public static string OLC = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "olc\\");
            public static string OLH = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "olh\\");
            public static string ORC = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "orc\\");
            public static string ORH = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "orh\\");
            public static string PTS = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "pts\\");
            public static string SHP = System.IO.Path.Combine(Global.DIR.ROOT, Global.DIR.DATA, "shp\\");
        }
        public static Coefficients coe = new Coefficients();	// so we can access coefficients from anywhere - may not be necessary...  MRL
	}
}
