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
            public static string ROOT = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\";
            
            //data, input, output directories 
            public static string DATA = System.IO.Path.Combine(ROOT,"data\\");
            public static string INPUT = System.IO.Path.Combine(DATA, "input\\");
            public static string OUTPUT = System.IO.Path.Combine(DATA, "output\\");

            //input directories
            public static string AIR = System.IO.Path.Combine(INPUT, "air\\");
            public static string COE = System.IO.Path.Combine(INPUT, "coe\\");
            public static string CPA = System.IO.Path.Combine(INPUT, "air\\");
            public static string FLO = System.IO.Path.Combine(INPUT, "flo\\");
            public static string MET = System.IO.Path.Combine(INPUT, "met\\");
            public static string OLC = System.IO.Path.Combine(INPUT, "orc\\");
            public static string OLH = System.IO.Path.Combine(INPUT, "orh\\");
            public static string ORC = System.IO.Path.Combine(INPUT, "orc\\");
            public static string ORH = System.IO.Path.Combine(INPUT, "orh\\");
            public static string PTS = System.IO.Path.Combine(INPUT, "pts\\");
            public static string SHP = System.IO.Path.Combine(INPUT, "shp\\");
            public static string NPT = System.IO.Path.Combine(INPUT, "npt\\");

            //output directories
            public static string RIV = System.IO.Path.Combine(OUTPUT, "riv\\");
            public static string CAT = System.IO.Path.Combine(OUTPUT, "cat\\");
            public static string LAK = System.IO.Path.Combine(OUTPUT, "lak\\");
            public static string PSM = System.IO.Path.Combine(OUTPUT, "psm\\");
            public static string QWQ = System.IO.Path.Combine(OUTPUT, "qwq\\");
            public static string WST = System.IO.Path.Combine(OUTPUT, "wst\\");
        }

        public static Coefficients coe = new Coefficients(); // so we can access coefficients from anywhere - may not be necessary...  MRL
        public static Simulation simulation = new Simulation();
    }
}
