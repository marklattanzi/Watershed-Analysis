using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace warmf.Module_Data
{
    public partial class DialogImportFileFormat : Form
    {
        public DialogImportFileFormat()
        {
            InitializeComponent();
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Guesses the delimiter in a text file
        public char GetTextFileDelimiter(string FileName, bool Guess, ref int IgnoreLines, ref int HeaderLines)
        {
            // Defaults
            int theDelimiter = 0;
            IgnoreLines = 0;
            HeaderLines = 1;

            // Hard wired for specific 3 delimiters
            int numPossibleDelimiters = 3;
            string possibleDelimiters = ",\t ";
            // delimitersPerLine is single dimensional array with two dimensions within it
            // elements 0-2 are the number of delimiters for each possible delimiter on the first line, 
            // 3 -5 are the number of each possible delimiter on the second line etc.
            List<int> delimitersPerLine = new List<int>();

            if (Guess)
            {
                int i, j;
                STechStreamReader sr = new STechStreamReader(FileName);
                int lineNumber = 0;
                for (i = 0; i < numPossibleDelimiters; i++)
                    delimitersPerLine.Add(0);
                int firstLineStartingWithNumber = 0;

                // Search through each character in the file
                char thisChar = '\0';
                while (!sr.EndOfStream)
                {
                    char nextChar = (char)sr.Read();
                    if (nextChar == '\n')
                    {
                        // New line
                        for (i = 0; i < numPossibleDelimiters; i++)
                            delimitersPerLine.Add(0);
                        lineNumber++;

                        // See if the first character of the next line is a number (part of date)
                        // to find where the header ends
                        char peekChar = (char)sr.Peek();
                        if (firstLineStartingWithNumber == 0 && peekChar >= '0' && peekChar <= '9')
                            firstLineStartingWithNumber = lineNumber;
                    }
                    else if (nextChar != thisChar)
                    {
                        // Check for delimiters
                        for (i = 0; i < numPossibleDelimiters; i++)
                            if (nextChar == possibleDelimiters[i])
                                delimitersPerLine[lineNumber * numPossibleDelimiters + i]++;
                    }

                    thisChar = nextChar;
                }
                sr.Close();

                // Calculate mean of delimiters and numbers per line
                List<double> meanDelimitersPerLine = new List<double>();
                if (lineNumber > 1)
                {
                    // Calculate means
                    for (j = 0; j < lineNumber; j++)
                    {
                        // Mean occurrence of each possible delimiter
                        for (i = 0; i < numPossibleDelimiters; i++)
                            meanDelimitersPerLine.Add(delimitersPerLine[j * numPossibleDelimiters + i] / (double)lineNumber);
                    }

                    // Assume delimiter is a comma if this is a CSV file
                    if (FileName.LastIndexOf(".csv") > 0 || FileName.LastIndexOf(".CSV") > 0)
                        theDelimiter = 0;
                    else
                    {
                        // Assume the delimiter is the one which occurs most frequently
                        // Find the highest mean
                        for (i = 0; i < numPossibleDelimiters; i++)
                            if (meanDelimitersPerLine[i] > meanDelimitersPerLine[theDelimiter])
                                theDelimiter = i;
                    }
                }

                // Assume the number of ignore lines is the number of lines at the top which
                // have less than half the average number of delimiters
                while (IgnoreLines < lineNumber && delimitersPerLine[IgnoreLines * numPossibleDelimiters + theDelimiter] + 0.001 < 0.5 * meanDelimitersPerLine[theDelimiter])
                {
                    IgnoreLines++;
                }

                HeaderLines = Math.Max(0, firstLineStartingWithNumber - IgnoreLines);
            }

            return possibleDelimiters[theDelimiter];
        }

        // Fills the dialog's controls
        public void Populate(string FileName)
        {
            int ignoreLines = 0;
            int headerLines = 1;

            char delimiter = GetTextFileDelimiter(FileName, true, ref ignoreLines, ref headerLines);
            NumberOfLinesToIgnore.Text = Convert.ToString(ignoreLines);
            NumberOfHeaderLines.Text = Convert.ToString(headerLines);
            if (delimiter == ',')
                RadioComma.Checked = true;
            else if (delimiter == '\t')
                RadioTab.Checked = true;
            else if (delimiter == ' ')
                RadioSpace.Checked = true;

            // Set the OK button to be enabled/disabled
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Returns the number of header lines
        public char GetDelimiter()
        {
            if (RadioTab.Checked)
                return '\t';
            if (RadioSpace.Checked)
                return ' ';
            if (RadioOther.Checked && OtherDelimiter.Text.Length > 0)
                return OtherDelimiter.Text[0];

            // Either comma radio button is checked or default if nothing is checked
            return ',';
        }

        // Returns the number of header lines
        public int GetNumberOfHeaderLines()
        {
            return Convert.ToInt32(NumberOfHeaderLines.Text);
        }

        // Returns the number of header lines
        public int GetNumberOfIgnoreLines()
        {
            return Convert.ToInt32(NumberOfLinesToIgnore.Text);
        }

        // Check to see if "Other" has been selected and if so, then if there is a character in the other box
        private bool OKEnable()
        {
            // Nothing in the Number of Lines to Ignore text box
            if (NumberOfLinesToIgnore.Text.Length == 0)
                return false;

            // Nothing in the Number of Header Lines text box
            if (NumberOfHeaderLines.Text.Length == 0)
                return false;

            // Other delimiter checked but nothing in the Other delimiter text box
            if (RadioOther.Checked && OtherDelimiter.Text.Length == 0)
                return false;

            return true;
        }

        // Enable function called when the text box with delimiter character is changed
        private void OtherDelimiter_TextChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Parses a string to see if it is an integer
        private bool ValidateInteger(char TheChar)
        {
            // Accept backspace
            if (TheChar == '\b')
                return true;

            // Integer characters
            return (TheChar >= '0' && TheChar <= '9');
        }

        // Checks to see if a character entered is an integer and therefore is valid
        private void NumberOfLinesToIgnore_KeyPress(object sender, KeyPressEventArgs e)
        {
            // True means we don't want the character processed, false means go ahead and display the character
            e.Handled = !ValidateInteger(e.KeyChar);
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Checks if the OK button should be enabled after any change has been made
        private void NumberOfLinesToIgnore_TextChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Checks to see if a character entered is an integer and therefore is valid
        private void NumberOfHeaderLines_KeyPress(object sender, KeyPressEventArgs e)
        {
            // True means we don't want the character processed, false means go ahead and display the character
            e.Handled = !ValidateInteger(e.KeyChar);
            ImportFileFormatOK.Enabled = OKEnable();
        }

        // Checks if the OK button should be enabled after any change has been made
        private void NumberOfHeaderLines_TextChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        private void RadioComma_CheckedChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        private void RadioTab_CheckedChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        private void RadioSpace_CheckedChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }

        private void RadioOther_CheckedChanged(object sender, EventArgs e)
        {
            ImportFileFormatOK.Enabled = OKEnable();
        }
    }


    // Get basic information about the file header format
    /*       stImportHeaderDialog* headerDialog = new stImportHeaderDialog(SystemWindow, "ImportHeader");
        headerDialog->SetTransferBuffer(&csvInfoTB);
           if (headerDialog->Execute() == IDOK)
           {
            // Ignore and header lines
              IgnoreLines = csvInfoTB.IgnoreLines;
              HeaderLines = csvInfoTB.HeaderLines;

            // Get the delimiter
              if (csvInfoTB.Comma)
                return ',';
              else if (csvInfoTB.Tab)
                return '\t';
              else if (csvInfoTB.Space)
                return ' ';
              else if (csvInfoTB.Other && strlen(csvInfoTB.OtherCharacter))
                return csvInfoTB.OtherCharacter[1];
           }
           else
            return '\0';

           return ',';

                }

                // Fills the dialog with guesses based on reading a file
                public void Populate()
                {
                    // Get the name of the comma delimited file
                    using (OpenFileDialog theDialog = new OpenFileDialog())
                    {
                        theDialog.InitialDirectory = Global.DIR.ROOT;
                        theDialog.FileName = "";
                        theDialog.DefaultExt = ".csv";
                        theDialog.Filter = "Comma Delimited File (.csv)|*.csv";
                        if (theDialog.ShowDialog() == DialogResult.OK)
                        {
                                                csvInfoTB.IgnoreLines = 0;
                                                csvInfoTB.HeaderLines = 1;
                                                csvInfoTB.Comma = 1;
                                                csvInfoTB.Tab = 0;
                                                csvInfoTB.Space = 0;
                                                csvInfoTB.Other = 0;
                                                strcpy(csvInfoTB.OtherCharacter, "");

                                                // Get basic information about the file header format
                                                stImportHeaderDialog* headerDialog = new stImportHeaderDialog(this, "ImportHeader");
                                                headerDialog->SetTransferBuffer(&csvInfoTB);
                                                if (headerDialog->Execute() == IDOK)
                                                {
                                                    // Get the delimiter
                                                    char delimiter = ',';
                                                    if (csvInfoTB.Comma)
                                                        delimiter = ',';
                                                    else if (csvInfoTB.Tab)
                                                        delimiter = '\t';
                                                    else if (csvInfoTB.Space)
                                                        delimiter = ' ';
                                                    else if (csvInfoTB.Other && strlen(csvInfoTB.OtherCharacter))
                                                        delimiter = csvInfoTB.OtherCharacter[1];

                                                    int lineCount;

                                                    // Get the number of headers and records in the file
                                                    stifstream csvFile(FilenameData.FileName);
                                                    int numRecords = 0;
                                                    int numHeaders = 1;
                                                    // Pass the ignore lines
                                                    for (lineCount = 0; lineCount < csvInfoTB.IgnoreLines; lineCount++)
                                                        csvFile.ignore(1000000, '\n');
                                                    // Get the number of headers
                                                    char nextChar;
                                                    do
                                                    {
                                                        nextChar = csvFile.get();
                                                        if (nextChar == delimiter)
                                                            numHeaders++;
                                                    } while (!csvFile.eof() && nextChar != '\n');
                                                    // Pass the rest of the header lines
                                                    for (lineCount = 1; lineCount < csvInfoTB.HeaderLines; lineCount++)
                                                        csvFile.ignore(1000000, '\n');

                                                    while (!csvFile.eof())
                                                    {
                                                        char tempLine[256];
                                                        csvFile.get(tempLine, 255, '\n');
                                                        csvFile.ignore(1000000, '\n');
                                                        // Check to make sure there is something on the line
                                                        if (strpbrk(tempLine, "0123456789"))
                                                            numRecords++;
                                                    }
                                                    csvFile.close();

                                                }

                                            }
                                        }
                    }
         */
}
