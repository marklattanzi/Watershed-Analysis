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
		string dataPlotted;

		public FormData(FormMain par) {
			InitializeComponent();
			this.parent = par;
			dataPlotted = null;
			this.ResizeEnd += new EventHandler(DataForm_ResizeEnd);
		}

		private void miDataEngr_Click(object sender, EventArgs e) {
			parent.showForm("engr");
		}

		private void miDataKnow_Click(object sender, EventArgs e) {
			parent.showForm("know");
		}

		private void DataForm_ResizeEnd(Object sender, EventArgs r) {
			if (dataPlotted != null) plotMETData(met, dataPlotted);
		}

		// plots MET file data
		private void plotMETData(METFile met, string dataName) {
			List<double> data = null;

			switch (dataName) {
				case "precip": data = met.precip; break;
				case "mintemp": data = met.minTemp; break;
				case "maxtemp": data = met.maxTemp; break;
				case "cloud": data = met.cloudCover; break;
				case "dewpoint": data = met.dewPointTemp; break;
				case "airpressure": data = met.airPressure; break;
				case "windspeed": data = met.windSpeed; break;
			};

			Series series = toolChart.Series["data"];
			series.Points.Clear();
			series.XValueType = ChartValueType.Date;
			series.IsVisibleInLegend = false;

			int len = (int)data.LongCount();	// get number of points to plot

			// set marker size based on graph size
			series.MarkerSize = toolChart.Size.Width * 15 / len;
			if (series.MarkerSize < 1) series.MarkerSize = 1;
			if (series.MarkerSize > 7) series.MarkerSize = 7;


			toolChart.Titles.Clear();
			METFile.METGraphLabels labels;
			labels = Array.Find(METFile.labels, item => item.key == dataName);
			toolChart.Titles.Add(labels.yaxis+" vs. "+labels.xaxis);
			toolChart.Palette = ChartColorPalette.Berry;
			for (int ii = 0; ii < len; ii++)
				series.Points.AddXY(met.date[ii].ToString("yyyy"), data[ii]);
			toolChart.ChartAreas.First().AxisX.IntervalType = DateTimeIntervalType.Months;
			toolChart.ChartAreas.First().AxisX.Interval = 12;
			toolChart.ChartAreas.First().AxisY.LabelStyle.Angle = -90;
			toolChart.ChartAreas[0].AxisX.Title = labels.xaxis;
			toolChart.ChartAreas[0].AxisY.Title = labels.yaxis;
		}

		private void miDataGraph_Click(object sender, EventArgs e) {
			string filename = "data/met/X350Y081.MET";
			met = new METFile(filename);
			if (met.readMETFile()) {
				dataPlotted = "windspeed";
				plotMETData(met, dataPlotted);
			}
		}

	}
}
