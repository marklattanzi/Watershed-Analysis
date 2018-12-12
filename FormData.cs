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

		public static readonly string[] PlotFileTypes = new string[] {
			"Meterology", "Air Quality", "Observed Hydrology", "Observed Water Quality", "Managed Flow", "Point Sources", "Pictures"
		};

		public FormData(FormMain par) {
			InitializeComponent();
			this.parent = par;

			// populate "Type of Data" combo box
			for (int ii = 0; ii < 7; ii++)
				cboxTypeOfFile.Items.Add(PlotFileTypes[ii]);

			this.Load += new System.EventHandler(this.FormData_Load);
			this.ResizeEnd += new EventHandler(DataForm_ResizeEnd);
		}

		private void FormData_Load(object sender, EventArgs e) {
		}

		private void populateFilenameDataCboxs(int dataIdx) {
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

		private void miDataEngr_Click(object sender, EventArgs e) {
			parent.showForm("engr");
		}

		private void miDataKnow_Click(object sender, EventArgs e) {
			parent.showForm("know");
		}

		private void DataForm_ResizeEnd(Object sender, EventArgs r) { 
			plotData();
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

				toolChart.Titles.Clear();
				toolChart.ChartAreas[0].AxisY.StripLines.Clear();

				tboxFilename.Text = met.filename.Substring(9);
				tboxLatitude.Text = met.latitude.ToString();
				tboxLongitude.Text = met.longitude.ToString();
				tboxAverage.Text = data.Average().ToString("0.00000");
				tboxStdDev.Text = Extensions.StdDev(data).ToString("0.00000");
				
				Series series = toolChart.Series["data"];
				series.Points.Clear();
				series.XValueType = ChartValueType.Date;
				series.IsVisibleInLegend = false;

				int len = (int)data.LongCount();    // get number of points to plot

				// set marker size based on graph size
				series.MarkerSize = toolChart.Size.Width * 15 / len;
				if (series.MarkerSize < 1) series.MarkerSize = 1;
				if (series.MarkerSize > 7) series.MarkerSize = 7;

				METFile.METGraphLabels labels;
				labels = Array.Find(METFile.labels, item => item.key == dataName);
				toolChart.Titles.Add(labels.yaxis + " vs. " + labels.xaxis);
				toolChart.Palette = ChartColorPalette.Berry;
				for (int ii = 0; ii < len; ii++)
					series.Points.AddXY(met.date[ii].ToString("yyyy"), data[ii]);
				toolChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
				toolChart.ChartAreas[0].AxisX.Interval = 12;

				toolChart.ChartAreas[0].AxisY.Minimum = Extensions.GetMinimum(data);
				toolChart.ChartAreas[0].AxisY.Maximum = Extensions.GetMaximum(data);
				toolChart.ChartAreas[0].AxisY.LabelStyle.Angle = -90;
				toolChart.ChartAreas[0].AxisX.Title = labels.xaxis;
				toolChart.ChartAreas[0].AxisY.Title = labels.yaxis;

				// plot average
				if (chkboxAverage.Checked) {
					double dblRes;
					StripLine line = new StripLine();
					line.StripWidth = 0.01;
					line.BackColor = Color.Green;
					//line.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
					line.IntervalOffset = Double.TryParse(tboxAverage.Text, out dblRes) ? dblRes : 0;
					toolChart.ChartAreas[0].AxisY.StripLines.Add(line);
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
					toolChart.ChartAreas[0].AxisY.StripLines.Add(line);

					StripLine line2 = new StripLine();
					line2.StripWidth = 0.01;
					line2.BackColor = Color.Orange;
					//line2.BorderDashStyle = ChartDashStyle.Dash;	// use with BorderColor but need to refigure line width --MRL
					line2.IntervalOffset = average - (Double.TryParse(tboxStdDev.Text, out dblRes) ? dblRes : 0);
					toolChart.ChartAreas[0].AxisY.StripLines.Add(line2);
				}
			}
		}

		private void cboxTypeOfFile_SelectedIndexChanged(object sender, EventArgs e) {
				populateFilenameDataCboxs(cboxTypeOfFile.SelectedIndex);
		}

		private void cboxFilename_SelectedIndexChanged(object sender, EventArgs e) {
			switch (cboxTypeOfFile.SelectedIndex) {
				case 0: // MET
					if (cboxFilename.SelectedIndex != -1) {
						string filename = "data/met/" + Global.coe.METFilename[cboxFilename.SelectedIndex];
						met = new METFile(filename);
						if (met.readMETFile()) plotMETData();
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

		private void plotData() {
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
	
		private void cboxData_SelectedIndexChanged(object sender, EventArgs e) {
			plotData();
		}

		private void chkboxAverage_CheckedChanged(object sender, EventArgs e) {
			plotData();
		}

		private void chkboxStdDev_CheckedChanged(object sender, EventArgs e) {
			plotData();
		}
	}
}
