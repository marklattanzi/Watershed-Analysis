using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warmf.Module_Data
{
    public partial class DialogImportDelimitedFile : Form
    {

        static readonly string LinkageFileName = "delimited.inp";

        public DialogImportDelimitedFile()
        {
            InitializeComponent();
            NumIgnoreLines = 0;
            NumHeaderLines = 0;
        }

        // Reads the last data import linkages used and puts them in the data import spreadsheet
        public void SelectPreviousImport(string InputFileName, int NumHeaderLines, List<string> TheHeaders)
        {
            int i, j;
            char linkageDelimiter = '|';
            int numHeaders = TheHeaders.Count / NumHeaderLines;

            // Compile the headers of the import file in the form seen in the linkage file
            List<string> combinedHeaders = new List<string>();
            for (i = 0; i < numHeaders; i++)
            {
                string tempString = TheHeaders[i];
                for (j = 1; j < NumHeaderLines; j++)
                    tempString = tempString + linkageDelimiter + TheHeaders[j * numHeaders + i];
                combinedHeaders.Add(tempString);
            }

            STechStreamReader linkageFile = new STechStreamReader(LinkageFileName);
            string readLine;

            while (!linkageFile.EndOfStream)
            {
                readLine = linkageFile.ReadLine();
                if (string.IsNullOrEmpty(readLine))
                    break;

                // Check through the header fields looking for perfect match
                // If so, this loop is ended early
                for (i = 0; i < numHeaders; i++)
                {
                    // Determine if the header in the import file lines up with the beginning of the 
                    // header in the linkage file and lines up with the end of the field
                    if (readLine.IndexOf(combinedHeaders[i]) == 0 &&
                        (readLine.Length == combinedHeaders[i].Length || readLine[combinedHeaders[i].Length] == linkageDelimiter))
                    {
                        string lineSubstring = readLine.Substring(combinedHeaders[i].Length + 1);
                        // Set the remaining columns of the spreadsheet on this row from the linkage file
                        for (j = 0; j < 4; j++)
                        {
                            // Get the field from the linkage file
                            // Find the next delimiter
                            int delimiterIndex = lineSubstring.IndexOf(linkageDelimiter);

                            // Set the field between delimiters or after the last delimiter
                            string field;
                            if (delimiterIndex >= 0)
                            {
                                field = lineSubstring.Substring(0, delimiterIndex);
                                // Set the next substring to find the next field
                                lineSubstring = lineSubstring.Substring(field.Length + 1);
                            }
                            else
                                field = lineSubstring;

                            // Set the spreadsheet cell 
                            ImportDelimitedDataGrid.Rows[i].Cells[NumHeaderLines + j].Value = field;
                            ImportDelimitedDataGrid_ProcessCellChanged(i, NumHeaderLines + j);
                        }

                        // Stop looking for a match since we found one and move on to the next line in the linkage file
                        break;
                    }
                }
            }

            linkageFile.Close();
        }

        // Fills the dialog with data from delimited file
        public void Populate(string FileName, char Delimiter, int nIgnoreLines, int nHeaderLines)
        {
            int i, lineCount, headerCount;

            NumIgnoreLines = nIgnoreLines;
            NumHeaderLines = nHeaderLines;
            
            // Label the column headers
            //            ImportDelimitedDataGrid.ColumnCount = NumHeaderLines + 4;
            // No headers for columns above file headers
            for (lineCount = 0; lineCount < NumHeaderLines; lineCount++)
            {
                DataGridViewTextBoxColumn headerColumn = new DataGridViewTextBoxColumn
                {
                    HeaderText = ""
                };
                ImportDelimitedDataGrid.Columns.Add(headerColumn);
            }

            // Input file types column
            DataGridViewComboBoxColumn inputType = new DataGridViewComboBoxColumn
            {
                HeaderText = "Input Type",
                MaxDropDownItems = 7
            };
            inputType.Items.Add("(not used)");
            for (i = 0; i < 6; i++)
                inputType.Items.Add(FormData.PlotFileTypes[i]);
            ImportDelimitedDataGrid.Columns.Add(inputType);

            // Input file names column - choices not set until file type is chosen
            DataGridViewComboBoxColumn inputFile = new DataGridViewComboBoxColumn
            {
                HeaderText = "Input File Name"
            };
            ImportDelimitedDataGrid.Columns.Add(inputFile);

            // Input parameter column - choices not set until file name is chosen
            DataGridViewComboBoxColumn inputParameter = new DataGridViewComboBoxColumn
            {
                HeaderText = "Parameter"
            };
            ImportDelimitedDataGrid.Columns.Add(inputParameter);

            // Unit conversion column
            DataGridViewTextBoxColumn unitConversion = new DataGridViewTextBoxColumn
            {
                HeaderText = "Unit Conversion"
            };
            ImportDelimitedDataGrid.Columns.Add(unitConversion);

            // Open the file to be imported and the file with the linkage between import headers and WARMF file
            STechStreamReader importFile = new STechStreamReader(FileName);

            // Ignore the ignore lines at the top of the import file
            for (lineCount = 0; lineCount < nIgnoreLines; lineCount++)
                importFile.ReadLine();

            // Read the header lines and enter the info in the spreadsheet
            // This one-dimentional array holds the header information for multiple lines in the order
            // line 1 header 1, line 1 header 2, ... line 2 header 1, line 2 header 2, line 2 header 3 etc.
            List<string> theHeaders = new List<string>();
            for (lineCount = 0; lineCount < NumHeaderLines; lineCount++)
            {
                bool endOfLine = false;
                while (!importFile.EndOfStream && !endOfLine)
                {
                    theHeaders.Add(importFile.ReadDelimitedField(Delimiter, ref endOfLine));
                }
            }
            importFile.Close();

            int numHeaders = 0;
            if (NumHeaderLines > 0)
                numHeaders = theHeaders.Count / NumHeaderLines;
            ImportDelimitedDataGrid.RowCount = numHeaders;
            for (headerCount = 0; headerCount < numHeaders; headerCount++)
            {
                for (i = 0; i < NumHeaderLines; i++)
                {
                    ImportDelimitedDataGrid.Rows[headerCount].Cells[i].Value = theHeaders[numHeaders * i + headerCount];
                }
            }
            SelectPreviousImport(FileName, NumHeaderLines, theHeaders);
        }

        // Removes duplicate entries in the linkage file
        public void CondenseLinkageFile(string fileName)
        {
            int i, j;

            // Read the import file
            string inputLine;
            List<string> fileContents = new List<string>();
            STechStreamReader linkageFile = new STechStreamReader(LinkageFileName);
            while (!linkageFile.EndOfStream)
            {
                inputLine = linkageFile.ReadLine();
                if (inputLine.Length > 0)
                    fileContents.Add(inputLine);
            }
            linkageFile.Close();

            // Work from end to beginning looking for an earlier match of the header lines
            for (i = fileContents.Count - 1; i >= 0; i--)
            {
                // Create a string with only the header information
                inputLine = fileContents[i];

                int delimiterIndex = 0;
                for (j = 0; j < NumHeaderLines; j++)
                {
                    if (delimiterIndex >= 0)
                    {
                        delimiterIndex = inputLine.IndexOf('|', delimiterIndex + 1);
                    }
                }

                // Check if any of the import fields have been left blank
                bool removeMainString = (delimiterIndex < 0);
                if (!removeMainString)
                    for (j = delimiterIndex; j < inputLine.Length - 1; j++)
                        if (inputLine[j] == '|' && inputLine[j + 1] == '|')
                        {
                            removeMainString = true;
                            break;
                        }

                // Successfully skipped across the header info
                if (delimiterIndex >= 0 && !removeMainString)
                {
                    // Truncate at the tab after the header info
                    inputLine = inputLine.Substring(0, delimiterIndex);

                    // Search through previous lines for the same header info
                    for (j = 0; j < i; j++)
                        if (fileContents[j].StartsWith(inputLine))
                        {
                            // Cut out the duplicate
                            fileContents.RemoveAt(j);

                            // Don't remove the main string yet to keep things simple
                            //            	   removeMainString = false;
                            break;
                        }
                }

                if (removeMainString)
                    fileContents.RemoveAt(i);
            }

            // Write back to file
            STechStreamWriter sw = new STechStreamWriter(LinkageFileName);
            for (i = 0; i < fileContents.Count; i++)
                sw.WriteLine(fileContents[i]);
            sw.Close();
        }

        // Gets linkages from spreadsheet
        public void GetLinkages(string csvFileName, ref bool scenarioChanged)
        {
            int lineCount, headerCount;

            // Get the file name suffix
            string suffix = FileNameSuffix.Text;

            // Save settings to an input file
            // Append to save previous imports then condense to remove duplicates
            STechStreamWriter linkageFile = new STechStreamWriter(LinkageFileName, true);

            // Save the order of everything in the headers
            List <int> modifiedTypes = new List<int>();
            List<string> modifiedFiles = new List<string>();
            List <int> modifiedParameters = new List<int>();

            // Save list of observed data files
            List <string> observedFiles;

            // Get information from transfer buffer
            for (lineCount = 0; lineCount < ImportDelimitedDataGrid.Rows.Count; lineCount++)
            {
                string spreadText;

                // Write to input file
                for (headerCount = 0; headerCount < ImportDelimitedDataGrid.Columns.Count; headerCount++)
                {
                    if (ImportDelimitedDataGrid.Rows[lineCount].Cells[headerCount].Value != null)
                        spreadText = ImportDelimitedDataGrid.Rows[lineCount].Cells[headerCount].Value.ToString();
                    else
                        spreadText = "";

                    // Add the delimiter to all but the last entry on a line
                    if (headerCount < ImportDelimitedDataGrid.Columns.Count - 1)
                        spreadText = spreadText + "|";
                    linkageFile.Write(spreadText);
                }
                linkageFile.WriteLine();

                // Get the cases where any data type was selected
                if (ImportDelimitedDataGrid.Rows[lineCount].Cells[NumHeaderLines].Value != null)
                    spreadText = ImportDelimitedDataGrid.Rows[lineCount].Cells[NumHeaderLines].Value.ToString();
                else
                    spreadText = "";

                // Data import linkage found
                int selectedFileType = Array.IndexOf(FormData.PlotFileTypes, spreadText);
                if (selectedFileType > 0)
                {
                    modifiedTypes.Add(selectedFileType);

                    string oldFileName = "";

                    for (headerCount = NumHeaderLines; headerCount < NumHeaderLines + 3; headerCount++)
                    {
                        spreadText = ImportDelimitedDataGrid.Rows[lineCount].Cells[headerCount].Value.ToString();
//                        linkageFile.Write(spreadText);
//                        if (headerCount < NumHeaderLines + 2)
//                            linkageFile.Write("|");

                        // Save the file name
                        if (headerCount == NumHeaderLines + 1)
                        {
                            oldFileName =  spreadText;
                            modifiedFiles.Add(spreadText);
                        }

                        // Save the parameter number
                        if (headerCount == NumHeaderLines + 2)
                        {
                            int parameterIndex = Global.coe.GetParameterNumberFromNameAndUnits(spreadText);
                            modifiedParameters.Add(parameterIndex);
                        }
                    }

                    // Copy the files if a suffix is added
                    if (suffix.Length > 0)
                    {
                        // Compile the modified file name
                        string newFileName = Path.GetFileNameWithoutExtension(oldFileName) + suffix + Path.GetExtension(oldFileName);

                        // See if it a meteorology file
                        int fileNumber = Global.coe.METFilename.IndexOf(oldFileName);
                        if (fileNumber >= 0)
                        {
                            // Copy the file
                            METFile mf = new METFile(oldFileName);
                            if (mf.ReadFile())
                            {
                                // Change the file used by WARMF in the master record
                                Global.coe.METFilename[fileNumber] = newFileName;
                                mf.filename = newFileName;
                                scenarioChanged = true;

                                // Copy the data to the new file
                                mf.WriteFile();
                            }
                        }

                        // See if it a managed flow file
                        fileNumber = Global.coe.GetFLONumberFromName(oldFileName);
                        if (fileNumber >= 0)
                        {
                            // Copy the file
                            FLOFile ff = new FLOFile(oldFileName);
                            if (ff.ReadFile())
                            {
                                // Change the file used by WARMF in the master record
                                Global.coe.DIVData[fileNumber].filename = newFileName;
                                ff.filename = newFileName;
                                scenarioChanged = true;

                                // Copy the data to the new file
                                ff.WriteFile();
                            }
                        }

                        // See if it is a point source file
                        fileNumber = Global.coe.PTSFilename.IndexOf(oldFileName);
                        if (fileNumber >= 0)
                        {
                            // Copy the file
                            PTSFile pf = new PTSFile(oldFileName);
                            if (pf.ReadFile())
                            {
                                // Change the file used by WARMF in the master record
                                Global.coe.PTSFilename[fileNumber] = newFileName;
                                pf.filename = newFileName;
                                scenarioChanged = true;

                                // Copy the data to the new file 
                                pf.WriteFile();
                            }
                        }

                        // See if it is an observed data file
                        List<string> allObservedFiles = Global.coe.GetAllObservedHydrologyFiles();
                        allObservedFiles.AddRange(Global.coe.GetAllObservedWaterQualityFiles());
                        fileNumber = allObservedFiles.IndexOf(oldFileName);
                        if (fileNumber >= 0)
                        {
                            // Copy the file
                            ObservedFile of = new ObservedFile(oldFileName);
                            if (of.ReadFile())
                            {
                                // Find all instances of the observed data file and
                                // change them all to the new file name
                                Global.coe.ChangeObservedFileName(oldFileName, newFileName);
                                of.filename = newFileName;
                                scenarioChanged = true;

                                // Write to the new file name
                                of.WriteFile();
                            }
                        }

                        // Modify the string in the set of modified files
                        int modifiedIndex = modifiedFiles.IndexOf(oldFileName);
                        if (modifiedIndex >= 0)
                        {
                            modifiedFiles[modifiedIndex] = newFileName;
                        }
                    }
                }
                else
                {
                    modifiedTypes.Add(-999);
                    modifiedFiles.Add("");
                    modifiedParameters.Add(-999);
                }
                linkageFile.WriteLine();
            }

            // Close the input file
            linkageFile.Close();

            // Remove duplicated entries
            CondenseLinkageFile(LinkageFileName);

            // Data structure to hold imported data
            DataFile newData = new DataFile();
            newData.NumGroups = 1;
            newData.NumLines = 0;
            // NumParameters in this case is each column of data in the import file
            newData.NumParameters = ImportDelimitedDataGrid.RowCount;

            // Structure to hold potential data source text
            // Each row has a data source array of the fields in each column
            List<List<string>> dataSourceFields = new List<List<string>>();

            // Get the data from the text file
            STechStreamReader csvFile = new STechStreamReader(csvFileName);

            // Read past the ignore lines and the headers
            for (lineCount = 0; lineCount < NumIgnoreLines + NumHeaderLines; lineCount++)
                csvFile.ReadLine();

            lineCount = 0;
            double dblRes;
            while (!csvFile.EndOfStream)
            {
                bool endOfLine = false;
                DataLine theDataLine = new DataLine();
                // Get the date (assumed first on each line)
                theDataLine.Date = Convert.ToDateTime(csvFile.ReadDelimitedField(',', ref endOfLine));

                // Read each column of the file
                theDataLine.Values = new List<double>();
                List<string> theseDataSourceFields = new List<string>();
                for (headerCount = 1; headerCount < ImportDelimitedDataGrid.RowCount; headerCount++)
                {
                    // Get the field as a double precision floating point number
                    string field = csvFile.ReadDelimitedField(',', ref endOfLine); 
                    if (field.Length > 0)
                        theDataLine.Values.Add(Double.TryParse(field, out dblRes) ? dblRes : 0);
                    else
                        theDataLine.Values.Add(-999);

                    // Save the field as a string in case it is a data source
                    theseDataSourceFields.Add(field);
                }

                newData.TheData.Add(theDataLine);
            }
            csvFile.Close();
            
            // Get the conversion from current flow units to cms
            double flowConvert = 1;
            int paramNum = Global.coe.GetParameterNumberFromCode("MFLO");
            if (paramNum >= 0 && Global.coe.AllConstits[paramNum].units.IndexOf("cfs") == 0)
                flowConvert = 0.028318;

            List<double> replaceVariable = new List<double>();

            // Get data structures for all the selected files
            for (headerCount = 0; headerCount < ImportDelimitedDataGrid.RowCount; headerCount++)
            {
                if (modifiedTypes[headerCount] > 0)
                {
                    // Clear the replacement variables
                    for (int k = 0; k < replaceVariable.Count; k++)
                        replaceVariable[k] = -999;

                    // Fill in the data source
//                    if (headerCount > 0)
//                        for (lineCount = 0; lineCount < newData.TheData.Count; lineCount++)
//                            newData.TheData[lineCount].Source = dataSourceFields[lineCount][headerCount - 1];

                    // Open the appropriate file type
                    // Meteorology
                    if (modifiedTypes[headerCount] == 1)
                    {
                        replaceVariable[modifiedParameters[headerCount]] = 1;

                        METFile mf = new METFile(modifiedFiles[headerCount]);
                        mf.ReadFile();
                        mf.ReplaceData(replaceVariable, newData, headerCount, true, flowConvert);
                        mf.WriteFile();
                    }
                    // Managed flow
                    else if (modifiedTypes[headerCount] == 2)
                    {
                        replaceVariable[modifiedParameters[headerCount]] = 1;

                        FLOFile of = new FLOFile(modifiedFiles[headerCount]);
                        of.ReadFile();
                        of.ReplaceData(replaceVariable, newData, headerCount, true, flowConvert);
                        of.WriteFile();
                    }
                    // Point source
                    else if (modifiedTypes[headerCount] == 3)
                    {
                        PTSFile pf = new PTSFile(modifiedFiles[headerCount]);
                        pf.ReadFile();

                        if (modifiedParameters[headerCount] < pf.NumParameters)
                            replaceVariable[modifiedParameters[headerCount]] = 1;
                        else
                        {
                            // Recreate the list of total parameters which would have been in the list
                            int totalCounter = 0;
                            int k;
                            for (k = 0; k < Global.coe.compositeConstits.Count; k++)
                            {
                                for (int l = 0; l < Global.coe.compositeConstits[k].componentTotalMass.Count; l++)
                                {
                                    if (Global.coe.compositeConstits[k].componentTotalMass[l] > 0 &&
                                        pf.ParameterCodes.IndexOf(Global.coe.compositeConstits[k].fortranCode) >= 0)
                                    {
                                        totalCounter++;
                                        break;
                                    }
                                }

                                if (totalCounter > modifiedParameters[headerCount] - pf.NumParameters)
                                    break;
                            }

                            if (k < Global.coe.compositeConstits.Count && totalCounter == modifiedParameters[headerCount] - pf.NumParameters + 1)
                            {
                                for (int l = 0; l < Global.coe.compositeConstits[k].componentTotalMass.Count; l++)
                                {
                                    if (Global.coe.compositeConstits[k].componentTotalMass[l] > 0)
                                    {
                                        // Alias for the component which makes up a part of the total parameter
                                        PhysicalConstits thePhysical = Global.coe.AllConstits[Global.coe.numHydrologyParams + l] as PhysicalConstits;
                                        if (thePhysical != null)
                                        {
                                            // Index of the component in the list of point source parameters
                                            int psIndex = pf.ParameterCodes.IndexOf(thePhysical.fortranCode);
                                            if (psIndex >= 0)
                                                replaceVariable[psIndex] = Global.coe.compositeConstits[k].componentTotalMass[l] *
                                                        Global.coe.compositeConstits[k].massEquivalent / thePhysical.massEquivalent;
                                        }
                                    }
                                }
                            }
                        }

                        pf.ReplaceData(replaceVariable, newData, headerCount, true, flowConvert);
                        pf.WriteFile();
                    }
                    // Observed hydrology and observed water quality
                    else if (modifiedTypes[headerCount] == 4 || modifiedTypes[headerCount] == 5)
                    {

                        replaceVariable[modifiedParameters[headerCount]] = 1;

                        ObservedFile of = new ObservedFile(modifiedFiles[headerCount]);
                        of.ReadFile();
                        of.ReplaceData(replaceVariable, newData, headerCount, true, flowConvert);
                        of.WriteFile();
                    }
                }
            }
        }

        private void ImportFileFormatHelp_Click(object sender, EventArgs e)
        {

        }

        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        void ImportDelimitedDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (ImportDelimitedDataGrid.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                ImportDelimitedDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Called either through event manager or manually to do the tasks needed when a cell changes
        private void ImportDelimitedDataGrid_ProcessCellChanged(int RowIndex, int ColumnIndex)
        {
            int i;

            // First column after headers: file type
            if (ColumnIndex == NumHeaderLines)
            {
                // Get the text which is selected in the combo box which has been changed
                string selection = (string)ImportDelimitedDataGrid.Rows[RowIndex].Cells[ColumnIndex].Value;
                // Get the combo box one cell to the right
                DataGridViewComboBoxCell theCell = (DataGridViewComboBoxCell)ImportDelimitedDataGrid.Rows[RowIndex].Cells[ColumnIndex + 1];
                theCell.Items.Clear();
                // Meteorology
                if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeMeteorology]) >= 0)
                {
                    // Fill the combo box with the names of the meteorology files
                    for (i = 0; i < Global.coe.numMETFiles; i++)
                        theCell.Items.Add(Global.coe.METFilename[i]);
                }
                // Air / Rain Chemistry
                else if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeAirQuality]) >= 0)
                {
                    // Fill the combo box with the names of the air quality files
                    for (i = 0; i < Global.coe.numAIRFiles; i++)
                        theCell.Items.Add(Global.coe.AIRFilename[i]);
                }
                // Observed hydrology
                else if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeObservedHydrology]) >= 0)
                {
                    List<string> obsFiles = Global.coe.GetAllObservedHydrologyFiles();

                    // Fill the combo box with the names of the point source files
                    for (i = 0; i < obsFiles.Count; i++)
                        theCell.Items.Add(obsFiles[i]);
                }
                // Observed water quality
                else if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeObservedWaterQuality]) >= 0)
                {
                    List<string> obsFiles = Global.coe.GetAllObservedWaterQualityFiles();

                    // Fill the combo box with the names of the point source files
                    for (i = 0; i < obsFiles.Count; i++)
                        theCell.Items.Add(obsFiles[i]);
                }
                // Managed flow
                else if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeManagedFlow]) >= 0)
                {
                    // Fill the combo box with the names of the managed flow files
                    for (i = 0; i < Global.coe.numDIVFiles; i++)
                        theCell.Items.Add(Global.coe.DIVData[i].filename);
                }
                // Point sources
                else if (selection.IndexOf(FormData.PlotFileTypes[FormData.FileTypePointSource]) >= 0)
                {
                    // Fill the combo box with the names of the point source files
                    for (i = 0; i < Global.coe.numPTSFiles; i++)
                        theCell.Items.Add(Global.coe.PTSFilename[i]);
                }
            }
            // File name changed
            else if (ColumnIndex == NumHeaderLines + 1)
            {
                // Get the selected file type
                string fileTypeSelection = (string)ImportDelimitedDataGrid.Rows[RowIndex].Cells[NumHeaderLines].Value;

                // Get the selected file name
                string fileNameSelection = (string)ImportDelimitedDataGrid.Rows[RowIndex].Cells[ColumnIndex].Value;

                // Get the combo box one cell to the right
                DataGridViewComboBoxCell theCell = (DataGridViewComboBoxCell)ImportDelimitedDataGrid.Rows[RowIndex].Cells[ColumnIndex + 1];
                theCell.Items.Clear();

                try
                {
                    // Get a data file structure for the chosen data type
                    DataFile df;
                    // Meteorology
                    if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeMeteorology]) >= 0)
                    {
                        // Meteorology data file structure to get the names of the parameters
                        fileNameSelection = Global.DIR.MET + fileNameSelection;
                        df = new METFile(fileNameSelection);

                    }
                    // Air Quality
                    else if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeAirQuality]) >= 0)
                    {
                        // Read the header of the air/rain chemistry data file structure to get the parameters within it
                        fileNameSelection = Global.DIR.AIR + fileNameSelection;
                        df = new AIRFile(fileNameSelection);
                    }
                    // Observed Hydrology
                    else if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeObservedHydrology]) >= 0)
                    {
                        // Read the header of the observed data file structure to get the parameters within it
                        fileNameSelection = Global.DIR.ORH + fileNameSelection;
                        df = new ObservedFile(fileNameSelection);
                    }
                    // Observed Water Quality
                    else if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeObservedWaterQuality]) >= 0)
                    {
                        // Read the header of the observed data file structure to get the parameters within it
                        fileNameSelection = Global.DIR.ORC + fileNameSelection;
                        df = new ObservedFile(fileNameSelection);
                    }
                    // Managed Flow
                    else if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypeManagedFlow]) >= 0)
                    {
                        // Read the header of the managed flow data file structure to get the parameters within it
                        fileNameSelection = Global.DIR.FLO + fileNameSelection;
                        df = new FLOFile(fileNameSelection);
                    }
                    // Point Source
                    else if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypePointSource]) >= 0)
                    {
                        // Read the header of the point source data file structure to get the parameters within it
                        fileNameSelection = Global.DIR.PTS + fileNameSelection;
                        df = new PTSFile(fileNameSelection);
                    }
                    else
                    {
                        df = new DataFile();
                    }
                    // Add the parameters for the data file to the combo box
                    STechStreamReader sr = new STechStreamReader(fileNameSelection);
                    if (df.ReadHeader(ref sr))
                    {
                        for (i = 0; i < df.ParameterNames.Count; i++)
                            theCell.Items.Add(df.ParameterNames[i]);
                        // Add applicable composite parameters for point sources
                        if (fileTypeSelection.IndexOf(FormData.PlotFileTypes[FormData.FileTypePointSource]) >= 0)
                        {
                            for (i = 0; i < Global.coe.numCompositeParams; i++)
                            {
                                for (int j = 0; j < Global.coe.compositeConstits[i].componentTotalMass.Count; j++)
                                {
                                    if (Global.coe.compositeConstits[i].componentTotalMass[j] > 0 &&
                                       df.FindParameterCode(Global.coe.AllConstits[Global.coe.hydroConstits.Count + j].fortranCode) >= 0)
                                    {
                                        string nameAndUnits = Global.coe.compositeConstits[i].fullName + ", " + Global.coe.compositeConstits[i].units;
                                        theCell.Items.Add(nameAndUnits);
                                        break;
                                    }

                                }
                            }
                        }

                        // Add Data Source
                        theCell.Items.Add("Data Source");
                        // Select the first item
                        if (df.ParameterNames.Count > 0)
                            theCell.Value = df.ParameterNames[0];
                    }
                }
                catch
                {
                }
                // Set the unit conversion to 1 by default
                DataGridViewTextBoxCell unitConversion = (DataGridViewTextBoxCell)ImportDelimitedDataGrid.Rows[RowIndex].Cells[NumHeaderLines + 3];
                unitConversion.Value = "1";
            }
            ImportDelimitedDataGrid.Invalidate();
        }

        // Called when grid cell content has been changed
        private void ImportDelimitedDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ImportDelimitedDataGrid_ProcessCellChanged(e.RowIndex, e.ColumnIndex);
        }        
        
        // Called when grid cell content has been changed
        private void ImportDelimitedDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            
            // First column after headers: file type
            if (e.ColumnIndex == NumHeaderLines)
            {
                string selection = (string) ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (selection.Contains("Meteorology"))
                {
                    // Fill the combo box one cell to the right with the names of the meteorology files
                    DataGridViewComboBoxCell theCell = (DataGridViewComboBoxCell) ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
                    theCell.Items.Clear();
                    for (i = 0; i < Global.coe.numMETFiles; i++)
                        theCell.Items.Add(ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]);
//                    ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = theCell;
                }
            }
        }

        private void ImportDelimitedDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }
    }
}
