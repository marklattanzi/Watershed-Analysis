using System;
using System.IO;
using System.Collections.Generic;

// File stream reader and writer that keep track of line nums
namespace warmf {
    public class STechStreamReader : StreamReader {
        public int LineNum { get; private set; }

		public STechStreamReader(string filename) : base(filename) { LineNum = 0; }

        public override string ReadLine() {
            LineNum++;
            return base.ReadLine();
        }

        // Determines if a character read in is the end of the stream, end of the line, or the file delimiter
        bool IsEndOfField(int ReadChar, char Delimiter, ref bool EndOfLine)
        {
            // End of file or end of line, therefore end of field
            if (EndOfStream || ReadChar == '\n')
            {
                EndOfLine = true;
                return true;
            }
            EndOfLine = false;

            // End of field if read character is a comma and we are not in the middle of quotes
            if (ReadChar == Delimiter)
                return true;

            // If none of the above conditions are met, it is not the end of a field
            return false;
        }

        // Gets the characters between commas in a CSV or other delimited file and reads past the trailing delimiter
        public string ReadDelimitedField(char Delimiter, ref bool EndOfLine)
        {
            // Local variables
            string field = "";
            int charCount = 0;
            char tempDelimiter = Delimiter;

            // Check to see if the first character of the field is double quote marks
            char readChar = Convert.ToChar(Peek());
            bool inQuotes = (readChar == '\"');
            // If the field is in quotes, skip past the quote marks and then make quote marks the delimiter
            if (inQuotes)
            {
                tempDelimiter = '\"';
                Read();
            }

            // Read one character at a time to check for various ways of ending the field
            while (!EndOfStream)
            {
                readChar = Convert.ToChar(Read());
                if (!IsEndOfField(readChar, tempDelimiter, ref EndOfLine))
                    field = field + Convert.ToString(readChar);
                else
                    break;

                charCount++;
            }

            // Remove whitespace characters from beginning and end
            field = field.Trim();

            // If the last character read was ending quote marks, read past the subsequent comma in the stream
            if (readChar == '\"')
                Read();

            return field;
        }
    }

	public class STechStreamWriter : StreamWriter {
		public int LineNum { get; private set; }

		public STechStreamWriter(string filename) : base(filename) { LineNum = 0; }
		public STechStreamWriter(string filename, bool append) : base(filename, append) { }

		public override void WriteLine()
        {
			LineNum++;
			base.WriteLine();
		}

        public override void WriteLine(string value)
        {
            LineNum++;
            base.WriteLine(value);
        }

        // Writes a left-justified string, and pads if less than 8 characters
        public void WriteString(string Value)
        {
            if (Value.Length < 8)
            {
                Value = Value.PadRight(8);
            }
            Write(Value);
        }

        // Writes a left-justified string, and pads if less than 8 characters
        public void WriteString16(string Value)
        {
            if (Value.Length < 16)
            {
                Value = Value.PadRight(16);
            }
            Write(Value);
        }

        // Writes a boolean as 1 or 0
        public void WriteBool(bool Value)
        {
            if (Value)
                WriteInt(1);
            else
                WriteInt(0);
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

        // writes doubles from a list- 9 per line
        public void WriteDoubleData(string lineAbbrev, List<double> dblValues)
        {
            double dblRes;
            int numValues, linesToWrite, i;

            numValues = dblValues.Count;
            dblRes = (double)numValues / 9;
            linesToWrite = Convert.ToInt16(Math.Ceiling(dblRes));

            i = 0;

            for (int j = 0; j < linesToWrite; j++)
            {
                if (j < (linesToWrite - 1)) //all full lines
                {
                    WriteString(lineAbbrev);
                    for (int k = 0; k < 9; k++)
                    {
                        WriteDouble(dblValues[i]);
                        i++;
                    }
                    WriteLine();
                }
                else //last line
                {
                    WriteString(lineAbbrev);
                    while (i < dblValues.Count)
                    {
                        WriteDouble(dblValues[i]);
                        i++;
                    }
                    WriteLine();
                    return;
                }
            }
        }

        // Writes On (true) and Off (false)
        public void WriteOnOffSwitch(bool Value)
        {
            if (Value)
                Write("{0}", "ON      ");
            else
                Write("{0}", "OFF     ");
        }

        // Writes 1 (true) and 0 (false)
        public void WriteOnOffas1or0(bool Value)
        {
            if (Value)
                WriteInt(1);
            else
                WriteInt(0);
        }
    }
}
