using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using warmf.Module_Data;

namespace warmf
{
    public partial class FormData : Form {
		FormMain parent;
		DataFile activeData;
        bool needToSave;
		string fileInTable;
        static readonly string xAxisLabel = "Time";

		public static readonly string[] PlotFileTypes = new string[] {
			"Meteorology", "Air Quality", "Observed Hydrology", "Observed Water Quality", "Managed Flow", "Point Sources", "Pictures"
		};
        public static readonly int FileTypeMeteorology = 0;
        public static readonly int FileTypeAirQuality = 1;
        public static readonly int FileTypeObservedHydrology = 2;
        public static readonly int FileTypeObservedWaterQuality = 3;
        public static readonly int FileTypeManagedFlow = 4;
        public static readonly int FileTypePointSource = 5;
        public static readonly int FileTypePictures = 6;

        // FormData constructor
        public FormData(FormMain par) {
			InitializeComponent();
			this.parent = par;
			needToSave = false;
			toolDataGrid.Hide();
			toolGraph.Show();

			// populate "Type of Data" combo box
			for (int ii = 0; ii < 7; ii++)
				cboxTypeOfFile.Items.Add(PlotFileTypes[ii]);

			this.Load += new System.EventHandler(this.FormData_Load);
			this.ResizeEnd += new EventHandler(DataForm_ResizeEnd);
			this.toolDataGrid.CellValueChanged += toolGrid_CellChanged;
			this.tboxLatitude.Leave += new System.EventHandler(this.tbox_Leave);
			this.tboxLongitude.Leave += new System.EventHandler(this.tbox_Leave);

		}

		// nothing in here at the moment
		private void FormData_Load(object sender, EventArgs e) { }

		// menu item handlers
		private void miExit_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("engineering");
		}
		private void miData_Click(object sender, EventArgs e) {
			parent.ShowForm("data");
		}
		private void miKnowledge_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("knowledge");
		}
		private void miManager_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("manager");
		}
		private void miTMDL_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("tmdl");
		}
		private void miConsensus_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("consensus");
		}
		private void miEngineering_Click(object sender, EventArgs e) {
			SaveFormData();
			parent.ShowForm("engineering");
		}

		// graph form resize handler 
		private void DataForm_ResizeEnd(Object sender, EventArgs r) {
			if (radioGraph.Checked) PlotGraph();
		}

		// PlotGraph is a relic - calls to it can be changed to PlotData
		private void PlotGraph() {
            PlotData();
    }

    // populate Filename and Data selectors based on Type of Data File picked
    private void PopulateSelectors(int dataIdx) {
            // Clear existing lists of files and data parameters
            cboxFilename.Items.Clear();
            cboxData.Items.Clear();

            switch (dataIdx) {
				case 0: // MET
					for (int ii = 0; ii < Global.coe.numMETFiles; ii++)
						cboxFilename.Items.Add(Global.coe.METFilename[ii]);
					break;
				case 1: // AIR QUALITY
                    for (int ii = 0; ii < Global.coe.numAIRFiles; ii++)
                        cboxFilename.Items.Add(Global.coe.AIRFilename[ii]);
                    break;
				case 2: // OBSERVED HYDROLOGY
                    List<string> obsHydFiles = Global.coe.GetAllObservedHydrologyFiles();
                    for (int ii = 0; ii < obsHydFiles.Count; ii++)
                        cboxFilename.Items.Add(obsHydFiles[ii]);
                    break;
				case 3: // OBSERVED WATER QUALITY
                    List<string> obsWQFiles = Global.coe.GetAllObservedWaterQualityFiles();
                    for (int ii = 0; ii < obsWQFiles.Count; ii++)
                        cboxFilename.Items.Add(obsWQFiles[ii]);
                    break;
				case 4: // MANAGED FLOW
                    for (int ii = 0; ii < Global.coe.numDIVFiles; ii++)
                        cboxFilename.Items.Add(Global.coe.DIVData[ii].filename);
                    break;
                case 5: // POINT SOURCES
                    for (int ii = 0; ii < Global.coe.numPTSFiles; ii++)
                        cboxFilename.Items.Add(Global.coe.PTSFilename[ii]);
                    break;
                case 6: // PICTURES
					break;

            }
            // Initially have no file selected
            cboxFilename.SelectedIndex = -1;
            cboxData.SelectedIndex = -1;
        }

        // Type of Data File selector handler
        private void cboxTypeOfFile_SelectedIndexChanged(object sender, EventArgs e) {
			SaveFormData();

            // Clear existing strings from file names and parameter combobox
            cboxFilename.Items.Clear();
            cboxData.Items.Clear();

            PopulateSelectors(cboxTypeOfFile.SelectedIndex);
		}

		// Filename selector handler
		private void cboxFilename_SelectedIndexChanged(object sender, EventArgs e) {
            SaveFormData();
            string selectedString = cboxFilename.Text;

            // Clear the combo box with the parameters
            cboxData.Items.Clear();

            if (!String.IsNullOrEmpty(selectedString))
            {
                string filename;

                switch (cboxTypeOfFile.SelectedIndex)
                {
                    case 0: // MET
                        filename = Global.DIR.MET + Global.coe.METFilename[cboxFilename.SelectedIndex];
                        activeData = new METFile(filename);
                        break;
                    case 1: // AIR QUALITY
                        filename = Global.DIR.AIR + Global.coe.AIRFilename[cboxFilename.SelectedIndex];
                        activeData = new AIRFile(filename);
                        break;
                    case 2: // OBSERVED HYDROLOGY
                        filename = Global.DIR.ORH + selectedString;
                        activeData = new ObservedFile(filename);
                        break;
                    case 3: // OBSERVED WATER QUALITY
                        filename = Global.DIR.ORC + selectedString;
                        activeData = new ObservedFile(filename);
                        break;
                    case 4: // MANAGED FLOW
                        filename = Global.DIR.FLO + selectedString;
                        activeData = new FLOFile(filename);
                        break;
                    case 5: // POINT SOURCES
                        filename = Global.DIR.PTS + selectedString;
                        activeData = new PTSFile(filename);
                        break;
                    case 6: // PICTURES
                        break;
                }

                // Enable/disable Edit menu items
                columnsToolStripMenuItem.Enabled = activeData.FlexibleColumns;
                sortByToolStripMenuItem.Enabled = activeData.Sortable;
                fillMissingDataToolStripMenuItem.Enabled = activeData.Fillable;

                if (activeData.ReadFile())
                 {
                    ShowHeaderData();
                    for (int ii = 0; ii < activeData.NumParameters; ii++)
                        cboxData.Items.Add(activeData.ParameterNames[ii]);
                    if (radioGraph.Checked)
                        PlotData();
                    else
                        FillTable();
                }
            }
        }

		// Data element selector handler
		private void cboxData_SelectedIndexChanged(object sender, EventArgs e) {
			if (radioGraph.Checked) PlotGraph();
		}

		// Average or Std Dev handler
		private void chkboxAverage_CheckedChanged(object sender, EventArgs e) {
			if (radioGraph.Checked) PlotGraph();
		}
		private void chkboxStdDev_CheckedChanged(object sender, EventArgs e) {
			if (radioGraph.Checked) PlotGraph();
		}

		// Data grid handlers
		private void toolGrid_CellChanged(object sender, DataGridViewCellEventArgs e) {
            needToSave = true;
            Double.TryParse(toolDataGrid[e.ColumnIndex, e.RowIndex].Value.ToString(), out double dblRes);

            // Date changed
            if (e.ColumnIndex == 1)
                activeData.TheData[e.RowIndex].Date = Convert.ToDateTime(toolDataGrid[e.ColumnIndex, e.RowIndex].Value + " " + toolDataGrid[e.RowIndex, e.ColumnIndex + 1].Value);
            // Time changed
            else if (e.ColumnIndex == 2)
                activeData.TheData[e.RowIndex].Date = Convert.ToDateTime(toolDataGrid[e.ColumnIndex, e.RowIndex - 1].Value + " " + toolDataGrid[e.RowIndex, e.ColumnIndex].Value);
            // Data source
            else if (e.ColumnIndex == activeData.NumParameters + 3)
                activeData.TheData[e.RowIndex].Source = toolDataGrid[e.ColumnIndex, e.RowIndex].Value.ToString();
            // Data values
            else if (e.ColumnIndex > 2)
                activeData.TheData[e.RowIndex].Values[e.ColumnIndex - 3] = dblRes;
		}

		// changed name text box
		private void tboxName_TextChanged(object sender, EventArgs e) {
			needToSave = true;
			activeData.shortName = tboxName.Text;
		}

		// validate text boxes
		private void validateTextBoxes() {
            if (Double.TryParse(tboxLatitude.Text, out double dblRes))
            {
                activeData.latitude = dblRes;
            }
            else
            {
                tboxLatitude.Text = activeData.latitude.ToString();
                WMDialog dialog = new WMDialog("Data Error", "Error in latitude data.  Reverting to file data.", false);
                dialog.ShowDialog();
            }

            if (Double.TryParse(tboxLongitude.Text, out dblRes)) {
				activeData.longitude = dblRes;
			}
			else {
				tboxLongitude.Text = activeData.longitude.ToString();
				WMDialog dialog = new WMDialog("Data Error", "Error in longitude data.  Reverting to file data.", false);
				dialog.ShowDialog();
			}
		}

		// changed name/lat/long text box
		private void tbox_TextChanged(object sender, EventArgs e) {
			needToSave = true;
		}

		// leave lat/long text box
		private void tbox_Leave(object sender, EventArgs e) {
			needToSave = true;
			validateTextBoxes();	// check now for coutesy, but it gets validated before saving
		}

		private void radioTableGraph_CheckedChanged(object sender, EventArgs e) {
			if (radioTable.Checked) {
				toolDataGrid.Show();
				toolGraph.Hide();
				FillTable();
			}
			else {
				toolDataGrid.Hide();
				toolGraph.Show();
				PlotGraph();
			}
		}

		// ask to save data if changed
		private bool SaveFormData() {
			if (needToSave) {
				validateTextBoxes();
				int result = WriteMETFile();
				if (result != -1) { // user didn't cancel
					needToSave = false;
					return true;    // all good, so leave grid whether saved or not
				}
				return false;   // user canceled operation
			}
			return true;    // don't need to save				
		}

		// ***************************** MET FILE handlers *******************************

		// write out MET file data
		private int WriteMETFile() {
			WMDialog dialog = new WMDialog("MET File Write Confirmation", "There is unsaved data for this MET file.\nDo you wish to save it?", true, true);
			dialog.setLabels("Save", "Discard");
			dialog.ShowDialog();
			if (dialog.Result == 1) {
				activeData.WriteFile();
			}
			return dialog.Result;
		}

		// display METfile header data on form
		private void ShowHeaderData() {
			tboxName.Text = activeData.shortName;
			tboxLatitude.Text = activeData.latitude.ToString();
			tboxLongitude.Text = activeData.longitude.ToString();
			needToSave = false;
		}

		// plots file data
		private void PlotData() {
			if (cboxData.SelectedIndex != -1) {
                // Copy data for the selected parameter to a List of double
                List<double> data = new List<double>();
                if (cboxData.SelectedIndex < activeData.NumParameters)
                {
                    for (int ii = 0; ii < activeData.TheData.Count(); ii++)
                        data.Add(activeData.TheData[ii].Values[cboxData.SelectedIndex]);
                }
                // For some reason, the selection in the combo box exceeds the number of parameters (shouldn't happen)
                else
                {
                    for (int ii = 0; ii < activeData.TheData.Count(); ii++)
                        data.Add(-999);
                }

//                string dataName = METFile.labels[cboxData.SelectedIndex].key;

				toolGraph.Titles.Clear();
				toolGraph.ChartAreas[0].AxisY.StripLines.Clear();

				try {
					tboxAverage.Text = data.Average().ToString("0.00000");
					tboxStdDev.Text = Extensions.StdDev(data).ToString("0.00000");
				}
				catch (Exception e) {
                    MessageBox.Show(e.Message, "Exception/Error", MessageBoxButtons.OK);
				}

				Series series = toolGraph.Series["data"];
				series.Points.Clear();
				series.XValueType = ChartValueType.Date;
				series.IsVisibleInLegend = false;

				int len = (int)data.LongCount();    // get number of points to plot

				// set marker size based on graph size
				series.MarkerSize = toolGraph.Size.Width * 15 / len;
				if (series.MarkerSize < 1) series.MarkerSize = 1;
				if (series.MarkerSize > 7) series.MarkerSize = 7;

                toolGraph.Titles.Add(activeData.ParameterNames[cboxData.SelectedIndex] + " vs. " + xAxisLabel);
                toolGraph.Titles[0].Font = new Font(toolGraph.Titles[0].Font.Name, 12, FontStyle.Bold);
				toolGraph.Palette = ChartColorPalette.Berry;
				for (int ii = 0; ii < len; ii++)
					series.Points.AddXY(activeData.TheData[ii].Date.ToString("yyyy"), data[ii]);
				toolGraph.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
				toolGraph.ChartAreas[0].AxisX.Interval = 12;

				toolGraph.ChartAreas[0].AxisY.Minimum = Extensions.GetMinimum(data);
				toolGraph.ChartAreas[0].AxisY.Maximum = Extensions.GetMaximum(data);
				toolGraph.ChartAreas[0].AxisY.LabelStyle.Angle = -90;
                toolGraph.ChartAreas[0].AxisX.Title = xAxisLabel;
                toolGraph.ChartAreas[0].AxisY.Title = activeData.ParameterNames[cboxData.SelectedIndex];
                toolGraph.ChartAreas[0].AxisX.TitleFont = new Font(toolGraph.ChartAreas[0].AxisX.TitleFont.Name, 12, FontStyle.Bold);
				toolGraph.ChartAreas[0].AxisY.TitleFont = new Font(toolGraph.ChartAreas[0].AxisY.TitleFont.Name, 12, FontStyle.Bold);

				// plot average
				if (chkboxAverage.Checked) {
                    StripLine line = new StripLine
                    {
                        StripWidth = 0.01,
                        BackColor = Color.Green,
                        //line.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
                        IntervalOffset = Double.TryParse(tboxAverage.Text, out double dblRes) ? dblRes : 0
                    };
                    toolGraph.ChartAreas[0].AxisY.StripLines.Add(line);
				}

				// plot std dev
				if (chkboxStdDev.Checked) {
                    double average = Double.TryParse(tboxAverage.Text, out double dblRes) ? dblRes : 0;
                    StripLine line = new StripLine
                    {
                        StripWidth = 0.01,
                        BackColor = Color.Orange,
                        //line.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
                        IntervalOffset = average + (Double.TryParse(tboxStdDev.Text, out dblRes) ? dblRes : 0)
                    };
                    toolGraph.ChartAreas[0].AxisY.StripLines.Add(line);

                    StripLine line2 = new StripLine
                    {
                        StripWidth = 0.01,
                        BackColor = Color.Orange,
                        //line2.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
                        IntervalOffset = average - (Double.TryParse(tboxStdDev.Text, out dblRes) ? dblRes : 0)
                    };
                    toolGraph.ChartAreas[0].AxisY.StripLines.Add(line2);
				}
			}
		}

		// fills file data table
		private void FillTable() {
			if (needToSave) return;
			if (toolDataGrid.RowCount != 0 && activeData.filename == fileInTable) return;

			toolDataGrid.Rows.Clear();
			if (cboxFilename.SelectedIndex != -1) {
				toolDataGrid.ColumnCount = activeData.NumParameters + 4;
				toolDataGrid.Columns[0].Name = "Line Num";
				toolDataGrid.Columns[1].Name = "Date";
				toolDataGrid.Columns[2].Name = "Time";
                for (int ii = 0; ii < activeData.NumParameters; ii++)
                    toolDataGrid.Columns[3 + ii].Name = activeData.ParameterNames[ii];
				toolDataGrid.Columns[activeData.NumParameters + 3].Name = "Data Source";

                // Row number and date columns
                toolDataGrid.Columns[0].Width = 70;
                toolDataGrid.Columns[1].Width = 70;
                // Time column
                toolDataGrid.Columns[2].Width = 50;
                // Data columns and data source column
                for (int ii = 3; ii <= 3 + activeData.NumParameters; ii++)
                {
					toolDataGrid.Columns[ii].Width = 120;
				}

                // Load data values into spreadsheet
                // Each row is an array of strings
                // Compile each row as one string and then split it into an array
                char[] charSeparators = new char[] { '\n' };
                for (int ii = 0; ii < activeData.TheData.Count(); ii++)
                {
                    // Row number, date, and time
                    string unsplitRow = ii.ToString() + "\n" + activeData.TheData[ii].Date.ToString("MM/dd/yyyy") + "\n" + activeData.TheData[ii].Date.ToString("HH:mm");
                    // Data values
                    for (int jj = 0; jj < activeData.NumParameters; jj++)
                        unsplitRow = unsplitRow + "\n" + activeData.TheData[ii].Values[jj].ToString();
                    // Data source
                    unsplitRow = unsplitRow + "\n" + activeData.TheData[ii].Source;
                    string[] row = unsplitRow.Split(charSeparators);
					toolDataGrid.Rows.Add(row);
				}
				fileInTable = activeData.filename;
			}
		}

        private void columnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Called from Edit / Sort by... / Data Source in the Data Module menu
        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i, j;
            int sortIncrement = 6000;

            // This is the maximum number of things to sort at once without crashing
            int numIterations = activeData.NumLines / sortIncrement;

            // Initial sort - the only one needed if there are less than "SORTINCREMENT" lines
            for (i = 0; i < activeData.NumLines - 1; i += sortIncrement)
                activeData.SortByDate(i, Math.Min(i + sortIncrement, activeData.NumLines) - 1);

            for (j = 0; j < numIterations; j++)
            {
                // Split each sorted block in half
                for (i = sortIncrement / 2; i < activeData.NumLines - sortIncrement; i += sortIncrement)
                    activeData.SortByDate(i, i + sortIncrement - 1);

                // Sort again the regular way
                for (i = 0; i < activeData.NumLines; i += sortIncrement)
                    activeData.SortByDate(i, Math.Min(i + sortIncrement, activeData.NumLines) - 1);
            }

        }

        // Called from Edit / Sort by... / Data Source in the Data Module menu
        private void dataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Called from Edit / Fill Missing Data in the Data Module menu
        private void fillMissingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Called from Edit / Extrapolate in the Data Module menu
        private void extrapolateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Called from Edit / Truncate in the Data Module menu
        private void truncateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Called from Edit / Import Delimited in the Data Module menu
        private void importDelimitedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the name of the comma delimited file
            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Global.DIR.ROOT,
                FileName = "",
                DefaultExt = ".csv",
                Filter = "Comma Delimited File (.csv)|*.csv"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the delimiter and number of ignore lines & header lines
                DialogImportFileFormat myDialog = new DialogImportFileFormat();
                myDialog.Populate(openDialog.FileName);
                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    char delimiter = myDialog.GetDelimiter();
                    int numIgnoreLines = myDialog.GetNumberOfIgnoreLines();
                    int numHeaderLines = myDialog.GetNumberOfHeaderLines();

                    DialogImportDelimitedFile importDialog = new DialogImportDelimitedFile();
                    importDialog.Populate(openDialog.FileName, delimiter, numIgnoreLines, numHeaderLines);

                    if (importDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get linkages from the dialog and save them to the linkage file
                        importDialog.GetLinkages(openDialog.FileName, ref parent.scenarioChanged);
                        // Modify data files with imported data
                    }
                }
            }
        }

        // Called from Edit / Import HEC-DSS in the Data Module menu
        private void importHECDSSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //override form's OnFormClosing method to hide the form, not dispose of it
            if (e.CloseReason == CloseReason.WindowsShutDown
                || e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                return;
            }
            e.Cancel = true;
            
            this.Hide();
        }
    }
}