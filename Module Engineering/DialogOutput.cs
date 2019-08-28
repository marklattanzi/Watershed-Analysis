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
        public static Output catchmentOutput = new Output();
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
                Text = featureType + " " + Global.coe.rivers[cnum].idNum + " Output";
            }
            else if (featureType == "Catchment")
            {
                Catchment catchment = Global.coe.catchments[cnum];
                Text = featureType + " " + catchment.idNum + " Output";

                //Read the .CAT file (for selected catchment) into memory
                catchmentOutput.ReadCAT(Global.DIR.CAT + "Catawba_SC_June2018.CAT", catchment.idNum);

                //Output Types
                cbOutputType.Items.Add("Surface Water");
                cbOutputType.Items.Add("Combined Output");
                for (int i = 0; i < catchment.numSoilLayers; i++)
                {
                    cbOutputType.Items.Add("Soil Layer " + (i + 1));
                }

                //Populate listbox containing output parameters
                lbOutputParameters.DataSource = catchmentOutput.constituentNameUnits;
            
                //Show Observations (Hide for catchments)
                chkShowObservations.Hide();

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
            }
            loading = false;
        }
        
        private void lbOutputParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            //DateTime startdate = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay);
            double timeStep = new double();
            timeStep = 24 / catchmentOutput.timeStepPerDay;
            DateTime xValue = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay, 0, 0, 0);
            Single yValue = new float();
            int index1, index2;

            if (loading == true)
            {
                index1 = 1; //combined catchment output
                index2 = 0; //first output parameter
            }
            else
            {
                index1 = cbOutputType.SelectedIndex; //output type
                index2 = lbOutputParameters.SelectedIndex; //parameter
            }

            for (int i = 0; i < (catchmentOutput.output[index1,index2].Count); i++)
            {
                //x values (date/time)
                xValue = xValue.AddHours(timeStep);
                //y values (time series output)
                yValue = catchmentOutput.output[index1, index2][i];

                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = catchmentOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
        }

        private void cbOutputType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            //DateTime startdate = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay);
            double timeStep = new double();
            timeStep = 24 / catchmentOutput.timeStepPerDay;
            DateTime xValue = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay, 0, 0, 0);
            Single yValue = new float();
            int index1, index2;

            if (loading == true)
            {
                index1 = 1; //combined catchment output
                index2 = 0; //first output parameter
            }
            else
            {
                index1 = cbOutputType.SelectedIndex; //output type
                index2 = lbOutputParameters.SelectedIndex; //parameter
            }

            for (int i = 0; i < (catchmentOutput.output[index1, index2].Count); i++)
            {
                //x values (date/time)
                xValue = xValue.AddHours(timeStep);
                //y values (time series output)
                yValue = catchmentOutput.output[index1, index2][i];
                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = catchmentOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
        }
    }
}
