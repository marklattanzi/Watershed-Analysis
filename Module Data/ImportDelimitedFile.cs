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
        public DialogImportDelimitedFile()
        {
            InitializeComponent();
            NumHeaderLines = 0;
        }

        // Reads the last data import linkages used and puts them in the data import spreadsheet
        public void SelectPreviousImport(string InputFileName, List<string> TheHeaders)
        {
/*            int i, j, k, l;
            char readLine[999];
            CharStarArray fileTypes;
            ReString dataSourceString(*SystemWindow->GetApplication(), IDS_DATASOURCE);
            TB.GetFileTypes(fileTypes, SystemWindow->GetApplication());
            stifstream inputFile(InputFileName);
            while (inputFile.good() && !inputFile.eof())
            {
                inputFile.get(readLine, 998, '\n');
                inputFile.ignore(1000000, '\n');
                if (!strlen(readLine))
                    break;

                // Check through the header fields looking for perfect match
                for (j = 0; j < TB.TheSpread->NumRows; j++)
                {
                    char* field = readLine;
                    for (i = 0; i < NumHeaderLines; i++)
                    {
                        char* tab;
                        char fieldCopy[999];
                        strcpy(fieldCopy, field);

                        if (field)
                        {
                            tab = strchr(fieldCopy, '|');
                            if (tab)
                                *tab = '\0';
                            // Compare field in input file with those in the dialog
                            char* spreadText = TB.TheSpread->GetText(j, i);
                            if (!spreadText || strcmp(fieldCopy, spreadText))
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

                        TB.TheSpread->SetCombo(j, NumHeaderLines, fileTypes, field);
                        int inputFileType = fileTypes.FindString(field);
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
            string[] fileTypes = new string[] {"(not used)",
            "Meterology", "Air Quality", "Observed Hydrology", "Observed Water Quality", "Managed Flow", "Point Sources" };
            DataGridViewComboBoxColumn inputType = new DataGridViewComboBoxColumn();
            inputType.HeaderText = "Input Type";
            inputType.MaxDropDownItems = 7;
            for (i = 0; i < inputType.MaxDropDownItems; i++)
                inputType.Items.Add(fileTypes[i]);
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
            STechStreamReader linkageFile = new STechStreamReader("delimited.inp");

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
            SelectPreviousImport(FileName, theHeaders);
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

        // Called when grid cell content has been changed
        private void ImportDelimitedDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int i;

            // First column after headers: file type
            if (e.ColumnIndex == NumHeaderLines)
            {
                string selection = (string)ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (selection.Contains("Meteorology"))
                {
                    // Fill the combo box one cell to the right with the names of the meteorology files
                    DataGridViewComboBoxCell theCell = new DataGridViewComboBoxCell();
                    for (i = 0; i < Global.coe.numMETFiles; i++)
                        theCell.Items.Add(ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]);
                    ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = theCell;
                }
            }
            ImportDelimitedDataGrid.Invalidate();
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
                    DataGridViewComboBoxCell theCell = new DataGridViewComboBoxCell();
                    for (i = 0; i < Global.coe.numMETFiles; i++)
                        theCell.Items.Add(ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]);
                    ImportDelimitedDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex + 1] = theCell;
                }
            }
        }
    }
}
