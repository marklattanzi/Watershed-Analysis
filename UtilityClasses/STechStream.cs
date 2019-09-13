using System.IO;

// File stream reader and writer that keep track of line nums
namespace warmf {
    public class STechStreamReader : StreamReader {
        public int LineNum { get; private set; }

		public STechStreamReader(string filename) : base(filename) { LineNum = 0; }

        public override string ReadLine() {
            LineNum++;
            return base.ReadLine();
        }
    }

	public class STechStreamWriter : StreamWriter {
		public int LineNum { get; private set; }

		public STechStreamWriter(string filename) : base(filename) { LineNum = 0; }
		public STechStreamWriter(string filename, bool append) : base(filename, append) { }

		public override void WriteLine() {
			LineNum++;
			base.WriteLine();
		}

        // Writes an integer in an 8 character field
        public void WriteInt(int Value)
        {
            string intString = Value.ToString("0");

            // Fill the number with preceding spaces so it will be 8 characters right justified
            intString = intString.PadLeft(8);
            Write("{0}", intString);
        }

        // Writes a double precision floating point number in an 8 character field
        public void WriteDouble(double Value)
        {
            string doubleString;

            // Force exponential notation when there isn't room to show the number in 8 spaces
            // Value > 1.0E10: 4 sig figs, 2 unsigned digits after the exponential
            if (Value >= 1.0E10)
                doubleString = Value.ToString("0.###E00");
            // 1.0E7 <= Value < 1.0E10: 5 sig figs, 1 unsigned digits for exponential
            else if (Value >= 1.0E7)
                doubleString = Value.ToString("0.####E0");
            // 1 <= Value < 1.0E7: standard notation, 7 significant figures
            else if (Value >= 1)
                doubleString = Value.ToString("G7");
            // 0.1 <= Value < 1: standard notation, 6 significant figures
            else if (Value >= 0.1)
                doubleString = Value.ToString("G6");
            // 0.01 <= Value < 0.1: standard notation, 5 significant figures
            else if (Value >= 0.01)
                doubleString = Value.ToString("G5");
            // 0.001 <= Value < 0.01: standard notation, 4 significant figures
            else if (Value >= 0.001)
                doubleString = Value.ToString("G4");
            // 1E-9 <= Value < 0.001: 4 sig figs, 1 signed digit for exponential
            else if (Value >= 1.0E-9)
                doubleString = Value.ToString("0.###E+0");
            // 0 < Value < 1E-9: 3 sig figs, 2 signed digits for exponential
            else if (Value > 0)
                doubleString = Value.ToString("0.##E+00");
            // Zero
            else if (Value == 0)
                doubleString = Value.ToString("0");
            // 0 > Value > -1E-9: 2 sig figs, 2 signed digits for exponential
            else if (Value > -1.0E-9)
                doubleString = Value.ToString("0.#E+00");
            // -1E-9 > Value > -1E-3: 3 sig figs, 1 signed digits for exponential
            else if (Value > -1.0E-3)
                doubleString = Value.ToString("0.##E+0");
            // -0.001 >= Value > -0.01: 3 sig figs, standard notation
            else if (Value > -0.01)
                doubleString = Value.ToString("G3");
            // -0.01 >= Value > -0.1: 4 sig figs, standard notation
            else if (Value > -0.1)
                doubleString = Value.ToString("G4");
            // -0.1 >= Value > -1: 5 sig figs, standard notation
            else if (Value > -1)
                doubleString = Value.ToString("G5");
            // -1 >= Value > -1E7: 6 sig figs, standard notation
            else if (Value > -1E6)
                doubleString = Value.ToString("G6");
            // -1.0E7 >= Value > -1.0E10: 4 sig figs, 1 unsigned digits for exponential
            else if (Value > -1.0E10)
                doubleString = Value.ToString("0.###E0");
            // Value < -1.0E10: 3 sig figs, 2 unsigned digits for exponential
            else
                doubleString = Value.ToString("0.##E00");
            // Fill in spaces left of the number if it is less than 8 digits
            doubleString = doubleString.PadLeft(8);
            Write("{0}", doubleString);
        }

        public void WriteOnOffSwitch(bool Value)
        {
            if (Value)
                Write("{0}", "ON      ");
            else
                Write("{0}", "OFF     ");
        }
    }
}
