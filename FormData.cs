using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace warmf {
	public partial class FormData : Form {

		FormMain parent;
		METFile met;
		bool needToSave;

		public static readonly string[] PlotFileTypes = new string[] {
			"Meterology", "Air Quality", "Observed Hydrology", "Observed Water Quality", "Managed Flow", "Point Sources", "Pictures"
		};

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
		}

		// nothing in here at the moment
		private void FormData_Load(object sender, EventArgs e) {
		}

		// menu item handlers
		private void miDataEngr_Click(object sender, EventArgs e) {
			parent.ShowForm("engr");
		}
		private void miDataKnow_Click(object sender, EventArgs e) {
			parent.ShowForm("know");
		}

		// graph form resize handler 
		private void DataForm_ResizeEnd(Object sender, EventArgs r) {
			if (radioGraph.Checked) plotGraph();
		}

		// these routines need to be updated for other file types (than MET) --MRL
		// pick data to graph
		private void plotGraph() {
			switch (cboxTypeOfFile.SelectedIndex) {
				case 0: // MET
					plotMETData();
					break;
				case 1: // AIR QUALITY
					break;
				case 2: // OBSERVED HYDROLOGY
					break;
				case 3: // OBSERVED WATER QUALITY
					break;
				case 4: // MANAGED FLOW
					break;
				case 5: // POINT SOURCES
					break;
				case 6: // PICTURES
					break;
			}
		}
		// pick data to display in grid
		private void fillGrid() {
			switch (cboxTypeOfFile.SelectedIndex) {
				case 0: // MET
					fillMETGrid();
					break;
				case 1: // AIR QUALITY
					break;
				case 2: // OBSERVED HYDROLOGY
					break;
				case 3: // OBSERVED WATER QUALITY
					break;
				case 4: // MANAGED FLOW
					break;
				case 5: // POINT SOURCES
					break;
				case 6: // PICTURES
					break;
			}
		}
		// populate Filename and Data selectors based on Type of Data File picked
		private void populateSelectors(int dataIdx) {
			switch (dataIdx) {
				case 0: // MET
					for (int ii = 0; ii < Global.coe.numMETFiles; ii++)
						cboxFilename.Items.Add(Global.coe.METFilename[ii]);
					for (int ii = 0; ii < 7; ii++)
						cboxData.Items.Add(METFile.labels[ii].yaxis);

					cboxFilename.SelectedIndex = -1;
					cboxData.SelectedIndex = -1;
					break;
				case 1: // AIR QUALITY
					break;
				case 2: // OBSERVED HYDROLOGY
					break;
				case 3: // OBSERVED WATER QUALITY
					break;
				case 4: // MANAGED FLOW
					break;
				case 5: // POINT SOURCES
					break;
				case 6: // PICTURES
					break;
			}
		}

		// Type of Data File selector handler
		private void cboxTypeOfFile_SelectedIndexChanged(object sender, EventArgs e) {
			populateSelectors(cboxTypeOfFile.SelectedIndex);
		}

		// Filename selector handler
		private void cboxFilename_SelectedIndexChanged(object sender, EventArgs e) {
			switch (cboxTypeOfFile.SelectedIndex) {
				case 0: // MET
					if (cboxFilename.SelectedIndex != -1) {
						string filename = "data/met/" + Global.coe.METFilename[cboxFilename.SelectedIndex];
						met = new METFile(filename);
						if (met.ReadMETFile()) {
							if (radioGraph.Checked)
								plotMETData();
							else
								fillMETGrid();
						}
					}
					break;
				case 1: // AIR QUALITY
					break;
				case 2: // OBSERVED HYDROLOGY
					break;
				case 3: // OBSERVED WATER QUALITY
					break;
				case 4: // MANAGED FLOW
					break;
				case 5: // POINT SOURCES
					break;
				case 6: // PICTURES
					break;
			}
		}

		// Data element selector handler
		private void cboxData_SelectedIndexChanged(object sender, EventArgs e) {
			if (radioGraph.Checked)	plotGraph();
		}

		// Average or Std Dev handler
		private void chkboxAverage_CheckedChanged(object sender, EventArgs e) {
			if (radioGraph.Checked)	plotGraph();
		}
		private void chkboxStdDev_CheckedChanged(object sender, EventArgs e) {
			if (radioGraph.Checked)	plotGraph();
		}

		// Data grid handlers
		private void toolGrid_CellChanged(object sender, DataGridViewCellEventArgs e) {
			needToSave = true;
		}

		// ask to save data if changed
		private bool saveGridData() {
			if (needToSave) {
				writeMETFile();
				needToSave = false;
			}
			return true;
		}
		// chart or graph handler
		private void ChartOrGraph() {
			if (radioChart.Checked) {
				toolDataGrid.Show();
				toolGraph.Hide();
				fillGrid();
			}
			else {
				if (saveGridData()) {
					toolDataGrid.Hide();
					needToSave = false;
					toolGraph.Show();
					plotGraph();
				}
			}

		}
		private void radioChart_CheckedChanged(object sender, EventArgs e) { ChartOrGraph(); }
		private void radioGraph_CheckedChanged(object sender, EventArgs e) { ChartOrGraph(); }
		
		// ***************************** MET FILE handlers *******************************

		// write out MET file data
		private void writeMETFile() {
			MessageBox.Show("Saving MET file "+met.filename);  // change to ask user if they want to save
			met.WriteMETFile(toolDataGrid);
		}

		// plots MET file data
		private void plotMETData() {
			List<double> data = null;

			if (cboxData.SelectedIndex != -1) {
				string dataName = METFile.labels[cboxData.SelectedIndex].key;
				switch (dataName) {
					case "precip": data = met.precip; break;
					case "mintemp": data = met.minTemp; break;
					case "maxtemp": data = met.maxTemp; break;
					case "cloud": data = met.cloudCover; break;
					case "dewpoint": data = met.dewPointTemp; break;
					case "airpressure": data = met.airPressure; break;
					case "windspeed": data = met.windSpeed; break;
				};

				toolGraph.Titles.Clear();
				toolGraph.ChartAreas[0].AxisY.StripLines.Clear();

				tboxFilename.Text = met.filename.Substring(9);
				tboxLatitude.Text = met.latitude.ToString();
				tboxLongitude.Text = met.longitude.ToString();
				tboxAverage.Text = data.Average().ToString("0.00000");
				tboxStdDev.Text = Extensions.StdDev(data).ToString("0.00000");

				Series series = toolGraph.Series["data"];
				series.Points.Clear();
				series.XValueType = ChartValueType.Date;
				series.IsVisibleInLegend = false;

				int len = (int)data.LongCount();    // get number of points to plot

				// set marker size based on graph size
				series.MarkerSize = toolGraph.Size.Width * 15 / len;
				if (series.MarkerSize < 1) series.MarkerSize = 1;
				if (series.MarkerSize > 7) series.MarkerSize = 7;

				METFile.METGraphLabels labels;
				labels = Array.Find(METFile.labels, item => item.key == dataName);
				toolGraph.Titles.Add(labels.yaxis + " vs. " + labels.xaxis);
				toolGraph.Titles[0].Font = new Font(toolGraph.Titles[0].Font.Name, 12, FontStyle.Bold);
				toolGraph.Palette = ChartColorPalette.Berry;
				for (int ii = 0; ii < len; ii++)
					series.Points.AddXY(met.date[ii].ToString("yyyy"), data[ii]);
				toolGraph.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
				toolGraph.ChartAreas[0].AxisX.Interval = 12;

				toolGraph.ChartAreas[0].AxisY.Minimum = Extensions.GetMinimum(data);
				toolGraph.ChartAreas[0].AxisY.Maximum = Extensions.GetMaximum(data);
				toolGraph.ChartAreas[0].AxisY.LabelStyle.Angle = -90;
				toolGraph.ChartAreas[0].AxisX.Title = labels.xaxis;
				toolGraph.ChartAreas[0].AxisY.Title = labels.yaxis;
				toolGraph.ChartAreas[0].AxisX.TitleFont = new Font(toolGraph.ChartAreas[0].AxisX.TitleFont.Name, 12, FontStyle.Bold);
				toolGraph.ChartAreas[0].AxisY.TitleFont = new Font(toolGraph.ChartAreas[0].AxisY.TitleFont.Name, 12, FontStyle.Bold);

				// plot average
				if (chkboxAverage.Checked) {
					double dblRes;
					StripLine line = new StripLine();
					line.StripWidth = 0.01;
					line.BackColor = Color.Green;
					//line.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
					line.IntervalOffset = Double.TryParse(tboxAverage.Text, out dblRes) ? dblRes : 0;
					toolGraph.ChartAreas[0].AxisY.StripLines.Add(line);
				}

				// plot std dev
				if (chkboxStdDev.Checked) {
					double dblRes;
					double average = Double.TryParse(tboxAverage.Text, out dblRes) ? dblRes : 0;
					StripLine line = new StripLine();
					line.StripWidth = 0.01;
					line.BackColor = Color.Orange;
					//line.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
					line.IntervalOffset = average + (Double.TryParse(tboxStdDev.Text, out dblRes) ? dblRes : 0);
					toolGraph.ChartAreas[0].AxisY.StripLines.Add(line);

					StripLine line2 = new StripLine();
					line2.StripWidth = 0.01;
					line2.BackColor = Color.Orange;
					//line2.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
					line2.IntervalOffset = average - (Double.TryParse(tboxStdDev.Text, out dblRes) ? dblRes : 0);
					toolGraph.ChartAreas[0].AxisY.StripLines.Add(line2);
				}
			}
		}

		// fills MET file grid
		private void fillMETGrid() {
			if (cboxFilename.SelectedIndex != -1) {
				toolDataGrid.ColumnCount = 11;
				toolDataGrid.Columns[0].Name = "Line Num";
				toolDataGrid.Columns[1].Name = "Date";
				toolDataGrid.Columns[2].Name = "Time";
				toolDataGrid.Columns[3].Name = "Precipitation";
				toolDataGrid.Columns[4].Name = "Min Temperature";
				toolDataGrid.Columns[5].Name = "Max Temperature";
				toolDataGrid.Columns[6].Name = "Cloud Cover";
				toolDataGrid.Columns[7].Name = "Dew Point Temperature";
				toolDataGrid.Columns[8].Name = "Air Pressure";
				toolDataGrid.Columns[9].Name = "Wind Speed";
				toolDataGrid.Columns[10].Name = "Data Source";

				int[] colWidths = new int[] { 70, 70, 50, 120, 120, 120, 120, 120, 120, 120, 120 };
				for (int ii = 0; ii < 11; ii++) {
					toolDataGrid.Columns[ii].Width = colWidths[ii];
				}

				for (int ii = 0; ii < met.date.Count(); ii++) {
					string[] row = new string[]
						{   ii.ToString(),
							met.date[ii].ToString("yyyy"),
							met.clockTime[ii],
							met.precip[ii].ToString(),
							met.minTemp[ii].ToString(),
							met.maxTemp[ii].ToString(),
							met.cloudCover[ii].ToString(),
							met.dewPointTemp[ii].ToString(),
							met.airPressure[ii].ToString(),
							met.windSpeed[ii].ToString(),
							met.comment[ii].ToString()
						};
					toolDataGrid.Rows.Add(row);
				}
			}
		}
	}
}