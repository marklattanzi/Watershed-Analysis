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
        FormMain parent;

        public DialogOutput(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(string featureType, int cnum)
        {
            
            if (featureType == "River")
            {
                Text = featureType + " " + Global.coe.rivers[cnum].idNum + " Output";
            }
            else if (featureType == "Catchment")
            {
                Catchment catchment = Global.coe.catchments[cnum];
                Text = featureType + " " + catchment.idNum + " Output";
                
                //Output Types
                cbOutputType.Items.Add("Surface Water");
                cbOutputType.Items.Add("Combined Output");
                for (int i = 0; i < catchment.numSoilLayers; i++)
                {  
                    cbOutputType.Items.Add("Soil Layer " + (i+1));
                }
                cbOutputType.SelectedIndex = 1;

                //Read the .CAT file (for selected catchment) into memory
                catchmentOutput.ReadCAT(Global.DIR.CAT + "Catawba_SC_June2018.CAT", catchment.idNum);
                
                //Populate listbox containing output parameters
                lbOutputParameters.DataSource = catchmentOutput.constituentNameUnits;
                lbOutputParameters.SelectedIndex = 0;

                //Show Observations (Hide for catchments)
                chkShowObservations.Hide();

                //format the output graph (populated on lbOutputParameters_SelectedIndexChanged event)
                ChartArea chartArea1 = chartOutput.ChartAreas["ChartArea1"];
                Series series1 = chartOutput.Series["SeriesOutput"];
                series1.ChartType = SeriesChartType.Line;
                chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
                chartArea1.AxisX.Title = "Date";
                chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;
                
            }
        }

        private void lbOutputParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            //DateTime startdate = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay);
            double timeStep = new double();
            timeStep = 24 / catchmentOutput.timeStepPerDay;
            DateTime xValue = new DateTime(catchmentOutput.startDateYear, catchmentOutput.startDateMonth, catchmentOutput.startDateDay, 0, 0, 0);
            Single yValue = new float();

            for (int i = 0; i < (catchmentOutput.output[lbOutputParameters.SelectedIndex].Count); i++)
            {
                //x values (date/time)
                xValue = xValue.AddHours(timeStep);
                //y values (time series output)
                yValue = catchmentOutput.output[lbOutputParameters.SelectedIndex][i];
                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = catchmentOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
        }
    }
}
