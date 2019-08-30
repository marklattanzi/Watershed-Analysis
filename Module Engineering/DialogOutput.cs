using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;



namespace warmf
{
    public partial class DialogOutput : Form
    {
        public static Output scenarioOutput = new Output();
        public static River river = new River();
        public static Catchment catchment = new Catchment();
        public bool loading;
        FormMain parent;

        public DialogOutput(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(string featureType, int cnum)
        {
            loading = true;

            if (featureType == "River")
            {
                //Set reference to river and form header text
                river = Global.coe.rivers[cnum];
                Text = featureType + " " + river.idNum + " Output";

                //Read the .RIV file (for selected river) into memory
                scenarioOutput.ReadOutput(Global.DIR.RIV + "Catawba_SC_June2018.RIV", river.idNum);

                //Output Types
                cbOutputType.Items.Clear();
                cbOutputType.Items.Add("Water Column");
                cbOutputType.Items.Add("Bed Sediment");

                //Show Observations (show for rivers)
                chkShowObservations.Show();
            }
            else if (featureType == "Catchment")
            {
                //set reference to catchment and form header text
                catchment = Global.coe.catchments[cnum];
                Text = featureType + " " + catchment.idNum + " Output";

                //Read the .CAT file (for selected catchment) into memory
                scenarioOutput.ReadOutput(Global.DIR.CAT + "Catawba_SC_June2018.CAT", catchment.idNum);

                //Output Types
                cbOutputType.Items.Clear();
                cbOutputType.Items.Add("Surface Water");
                cbOutputType.Items.Add("Combined Output");
                for (int i = 0; i < catchment.numSoilLayers; i++)
                {
                    cbOutputType.Items.Add("Soil Layer " + (i + 1));
                }

                //Show Observations (Hide for catchments)
                chkShowObservations.Hide();
            }

            //Populate listbox containing output parameters
            lbOutputParameters.DataSource = scenarioOutput.constituentNameUnits;

            //format the output graph (populated on lbOutputParameters_SelectedIndexChanged event)
            ChartArea chartArea1 = chartOutput.ChartAreas["ChartArea1"];
            Series series1 = chartOutput.Series["SeriesOutput"];
            series1.ChartType = SeriesChartType.Line;
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisX.Title = "Date";
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;

            //select output type and chemical parameter
            cbOutputType.SelectedIndex = 1;
            lbOutputParameters.SelectedIndex = 0;

            loading = false;
        }

        private void lbOutputParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            double timeStep = new double();
            timeStep = 24 / scenarioOutput.timeStepPerDay;
            DateTime xValue = new DateTime(scenarioOutput.startDateYear, scenarioOutput.startDateMonth, scenarioOutput.startDateDay, 0, 0, 0);
            Single yValue = new float();
            int index1, index2;

            if (loading == true)
            {
                if (this.Text.Contains("River") == true)
                {
                    index1 = 0; //water column for river output
                }
                else
                {
                    index1 = 1; //combined output for catchments
                }
                index2 = 0; //first output parameter
            }
            else
            {
                index1 = cbOutputType.SelectedIndex; //output type
                index2 = lbOutputParameters.SelectedIndex; //parameter
            }

            for (int i = 0; i < (scenarioOutput.output[index1, index2].Count); i++)
            {
                //x values (date/time)
                xValue = xValue.AddHours(timeStep);
                //y values (time series output)
                yValue = scenarioOutput.output[index1, index2][i];

                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = scenarioOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
        }

        private void cbOutputType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            double timeStep = new double();
            timeStep = 24 / scenarioOutput.timeStepPerDay;
            DateTime xValue = new DateTime(scenarioOutput.startDateYear, scenarioOutput.startDateMonth, scenarioOutput.startDateDay, 0, 0, 0);
            Single yValue = new float();
            int index1, index2;

            if (loading == true)
            {
                if (this.Text.Contains("River") == true)
                {
                    index1 = 0; //water column for river output
                }
                else
                {
                    index1 = 1; //combined output for catchments
                }
                index2 = 0; //first output parameter
            }
            else
            {
                index1 = cbOutputType.SelectedIndex; //output type
                index2 = lbOutputParameters.SelectedIndex; //parameter
            }

            for (int i = 0; i < (scenarioOutput.output[index1, index2].Count); i++)
            {
                //x values (date/time)
                xValue = xValue.AddHours(timeStep);
                //y values (time series output)
                yValue = scenarioOutput.output[index1, index2][i];
                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = scenarioOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
        }

        private void chkShowObservations_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowObservations.Checked == true)
            {
                string HydroFileName = river.hydrologyFilename;
                string waterQualityFileName = river.waterQualFilename;
            }
            else
            {

            }
        }
    }
}
