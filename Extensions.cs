﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warmf {
	class Extensions {
		public static double StdDev(List<double> dnums) {
			double mean = dnums.Average();
			return Math.Sqrt(dnums.Sum(x => Math.Pow(x - mean, 2)) / (dnums.Count()));
		}

		public static int GetMinimum(List<double> dnums) {
			double range = dnums.Max() - dnums.Min();
			if (range <= 1) return 0;
			int min = (int)(dnums.Min() - range * 0.1);  // range - 10%
			return min / 10 * 10;
		}

		public static int GetMaximum(List<double> dnums) {
			double range = dnums.Max() - dnums.Min();
			if (range <= 1) return 1;
			int max = (int)(dnums.Max() + range * 0.2);  // range + 20%
			
			// round to nearest 10
			if (max < 10) 
				return 10;
			else
				return (max / 10) * 10;
		}
	}
}
