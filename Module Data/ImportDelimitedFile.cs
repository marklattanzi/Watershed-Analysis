using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            NumHeaderLines = 0;
        }

        // Reads the last data import linkages used and puts them in the data import spreadsheet
        public void SelectPreviousImport(string InputFileName, int NumHeaderLines, List<string> TheHeaders)
        {
            int i, j, k, l;
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
/*                for (j = 0; j < numHeaders; j++)
                {
                    string field = readLine;
                    if (field.IndexOf())
                    for (i = 0; i < NumHeaderLines; i++)
                    {
                        string tab;
                        string fieldCopy = field;
                        int delimiterIndex;

                        if (!string.IsNullOrEmpty(field))
                        {
                            tab = strchr(fieldCopy, '|');
                            delimiterIndex = field.IndexOf(linkageDelimiter);
                            if (delimiterIndex >= 0)
                                fieldCopy = field.Substring(0, delimiterIndex);
                            if (tab)
                                *tab = '\0';
                            // Compare field in input file with those in the dialog
                            if (strcmp(fieldCopy, spreadText))
                                break;
                        }
                        else
                            break;

                        tab = strchr(field, '|');
                        if (tab)
                            field = tab + 1;
                        else
                            field = NULL;
                    }

                    // If we made it all the way through the above for loop, we have a match
                    if (i == NumHeaderLines && field)
                    {
                        // Give the field an end
                        char* tab = strchr(field, '|');
                        if (tab)
                            *tab = '\0';

                        TB.TheSpread->SetCombo(j, NumHeaderLines, FormData.PlotFileTypes, field);
                        int inputFileType = FormData.PlotFileTypes.FindString(field);
                        if (inputFileType > 0 && tab)
                        {
                            // Get to the file name field
                            field = tab + 1;
                            tab = strchr(field, '|');
                            if (tab)
                                *tab = '\0';

                            // List of parameters for the penultimate column
                            CharStarArray parameterList;
                            char nameAndUnits[256];
                            IWMMSystemCoeffs & systemCoeffs = (IWMMSystemCoeffs &) GetSystemCoeffs();
                            IWMMSimControlCoeffs & simControlCoeffs = (IWMMSimControlCoeffs &) GetSimControlCoeffs();
                            // Meteorology
                            if (inputFileType == 1)
                            {
                                TB.TheSpread->SetCombo(j, NumHeaderLines + 1, simControlCoeffs.FileNames.Meteorology, field);

                                for (i = 0; i < 7; i++)
                                {
                                    ReString aString(*SystemWindow->GetApplication(), IDS_METPARAM + i);
                parameterList.AddString((char*)(const char*) aString);
            }
        }
                           // Managed flow
                           if (inputFileType == 2)
                           {
                            TB.TheSpread->SetCombo(j, NumHeaderLines + 1, simControlCoeffs.FileNames.ManagedFlow.FileNames, field);

        int paramNum = systemCoeffs.Constituents.FindFortranCode("MFLO");
                                    if (paramNum >= 0)
                                {
                                stParameter* aParameter = systemCoeffs.Constituents.Pointers[paramNum];
        char nameAndUnits[256];
        aParameter->GetNameAndUnits(nameAndUnits);
        parameterList.AddString(nameAndUnits);
                               }
}
                           // Point source
                           else if (inputFileType == 3)
                           {
                                TB.TheSpread->SetCombo(j, NumHeaderLines + 1, simControlCoeffs.FileNames.PointSources, field);

PointSourceStruct pd;
int version;
stifstream ifs(field);
// Only parameters in point source file can be substituted or total composite parameters
pd.ReadHeader(ifs, version);
                                    // Get string for each parameter
                                for (k = 0; k<pd.ColumnHeader.GetNumber(); k++)
                                   {
                                    int paramNum = systemCoeffs.Constituents.FindFortranCode(pd.ColumnHeader.GetString(k));
                                   if (paramNum >= 0)
                                      {
                                        stParameter* aParameter = systemCoeffs.Constituents.Pointers[paramNum];
aParameter->GetNameAndUnits(nameAndUnits);
parameterList.AddString(nameAndUnits);
                                   }
                                }
                              // Add applicable total composite parameters
                                    int first = systemCoeffs.Constituents.GetFirstOfType(typeid(stChemical), true);
int firstTotal = systemCoeffs.Constituents.GetFirstOfType(typeid(stTotalParameter), true);
                              for (k = firstTotal; k<systemCoeffs.Constituents.Number; k++)
                              {
                                    stTotalParameter* totalParameter = (stTotalParameter*)systemCoeffs.Constituents.Pointers[k];
                                 for (l = 0; l<totalParameter->ComponentMultipliers.Number; l++)
                                    if (totalParameter->ComponentMultipliers.Values[l] > 0. &&
                                        pd.ColumnHeader.FindString(systemCoeffs.Constituents.Pointers[first + l]->FortranCode) >= 0)
                                    {
                                        totalParameter->GetNameAndUnits(nameAndUnits);
parameterList.AddString(nameAndUnits);
                                       break;
                                    }
                              }
                           }
                           // Observed Data
                           else if (inputFileType == 4 || inputFileType == 5)
                           {
                            // Compile a list of observed data files
                              CharStarArray obsDataFiles = GetObservedDataFileList((inputFileType == 4), (inputFileType == 5));

// Put the list of observed data files in the combo box
TB.TheSpread->SetCombo(j, NumHeaderLines + 1, obsDataFiles, field);

// Put the list of parameters for the selected file into the next column
ObservedDataStruct od;
int version;
stifstream ifs(field);
// Only parameters in observed data file
od.ReadHeader(ifs, version);
                                    // Get string for each parameter
                                for (k = 0; k<od.ColumnHeader.GetNumber(); k++)
                                   {
                                    int paramNum = systemCoeffs.Constituents.FindFortranCode(od.ColumnHeader.GetString(k));
                                   if (paramNum >= 0)
                                      {
                                        stParameter* aParameter = systemCoeffs.Constituents.Pointers[paramNum];
aParameter->GetNameAndUnits(nameAndUnits);
parameterList.AddString(nameAndUnits);
                                   }
                                }
                                }

                           // Select parameter in final column
                           field = tab + 1;
                           tab = strchr(field, '|');
                           if (tab)
                               * tab = '\0';

// Data Source
parameterList.AddString((char*) (const char*) dataSourceString);

                                // Put in the appropriate parameter list with selected field
                           TB.TheSpread->SetCombo(j, NumHeaderLines + 2, parameterList, field);

// Add in the unit conversion
field = tab + 1;
                           TB.TheSpread->SetDouble(j, NumHeaderLines + 3, atof(field));
                        }

                        // Found a spreadsheet line matching the line in the input file,
                        // so go on to the next line in the input file
                        break;
                     }
                  }
               }
               inputFile.close();*/
        }

        // Fills the dialog with data from delimited file
        public void Populate(string FileName, char Delimiter, int nIgnoreLines, int nHeaderLines)
        {
            int i, lineCount, headerCount;

            NumHeaderLines = nHeaderLines;
            
            // Label the column headers
            //            ImportDelimitedDataGrid.ColumnCount = NumHeaderLines + 4;
            // No headers for columns above file headers
            for (lineCount = 0; lineCount < NumHeaderLines; lineCount++)
            {
                DataGridViewTextBoxColumn headerColumn = new DataGridViewTextBoxColumn();
                headerColumn.HeaderText = "";
                ImportDelimitedDataGrid.Columns.Add(headerColumn);
            }

            // Input file types column
            DataGridViewComboBoxColumn inputType = new DataGridViewComboBoxColumn();
            inputType.HeaderText = "Input Type";
            inputType.MaxDropDownItems = 7;
            inputType.Items.Add("(not used)");
            for (i = 0; i < 6; i++)
                inputType.Items.Add(FormData.PlotFileTypes[i]);
            ImportDelimitedDataGrid.Columns.Add(inputType);

            // Input file names column - choices not set until file type is chosen
            DataGridViewComboBoxColumn inputFile = new DataGridViewComboBoxColumn();
            inputFile.HeaderText = "Input File Name";
            ImportDelimitedDataGrid.Columns.Add(inputFile);

            // Input parameter column - choices not set until file name is chosen
            DataGridViewComboBoxColumn inputParameter = new DataGridViewComboBoxColumn();
            inputParameter.HeaderText = "Parameter";
            ImportDelimitedDataGrid.Columns.Add(inputParameter);

            // Unit conversion column
            DataGridViewTextBoxColumn unitConversion = new DataGridViewTextBoxColumn();
            unitConversion.HeaderText = "Unit Conversion";
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
