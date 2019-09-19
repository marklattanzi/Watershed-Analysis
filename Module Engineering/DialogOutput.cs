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
        ObservedFile hydroData;
        ObservedFile wqData;
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
                //Set reference to river and form header text
                river = Global.coe.rivers[cnum];
                Text = featureType + " " + river.idNum + " Output";

                //Observed data files
                if (river.hydrologyFilename != "")
                {
                    hydroData = new ObservedFile(Global.DIR.ORH + river.hydrologyFilename);
                    hydroData.ReadFile();
                }
                if (river.waterQualFilename != "")
                {
                    wqData = new ObservedFile(Global.DIR.ORC + river.waterQualFilename);
                    wqData.ReadFile();
                }
                
                //Read the .RIV file (for selected river) into memory
                scenarioOutput.ReadOutput(Global.DIR.RIV + "Catawba_SC_June2018.RIV", river.idNum);

                //Output Types (add function places item at bottom of list)
                //****Don't change the order here, as it corresponds with the output order in the RIV file****
                cbOutputType.Items.Clear();
                cbOutputType.Items.Add("Bed Sediment");
                cbOutputType.Items.Add("Water Column");
                
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

                //Hide the "Show Observations" checkbox
                chkShowObservations.Hide();
            }

            //Populate listbox containing output parameters
            lbOutputParameters.DataSource = scenarioOutput.constituentNameUnits;

            //format the output graph (populated on ChartTheData)
            ChartArea chartArea1 = chartOutput.ChartAreas["ChartArea1"];
            Series series1 = chartOutput.Series["SeriesOutput"];
            Series series2 = chartOutput.Series["SeriesObserved"];
            series1.ChartType = SeriesChartType.Line;
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisX.Title = "Date";
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;

            //select output type and chemical parameter
            cbOutputType.SelectedIndex = 1;
            lbOutputParameters.SelectedIndex = 0;

            //chart the data
            ChartTheData();

            //add SelectedIndexChanged event handler
            this.lbOutputParameters.SelectedIndexChanged += new System.EventHandler(this.lbOutputParameters_SelectedIndexChanged);
        }

        public void ChartTheData()
        {
            chartOutput.Series["SeriesOutput"].Points.Clear();
            chartOutput.Series["SeriesObserved"].Points.Clear();
            double timeStep = new double();
            timeStep = 24 / scenarioOutput.timeStepPerDay;
            DateTime xValue = new DateTime(scenarioOutput.startDateYear, scenarioOutput.startDateMonth, scenarioOutput.startDateDay, 0, 0, 0);
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            float yValue = new float();
            int indexOutputType, indexParameter;

            startDate = xValue;

            indexOutputType = cbOutputType.SelectedIndex; //output type
            indexParameter = lbOutputParameters.SelectedIndex; //parameter

            for (int i = 0; i < (scenarioOutput.output[indexOutputType, indexParameter].Count); i++)
            {
                //y values (time series output)
                yValue = scenarioOutput.output[indexOutputType, indexParameter][i];

                if (yValue != -999)
                {
                    chartOutput.Series["SeriesOutput"].Points.AddXY(xValue, yValue);
                }

                //increment x value
                xValue = xValue.AddHours(timeStep);
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = scenarioOutput.constituentNameUnits[lbOutputParameters.SelectedIndex];
            chartOutput.Series["SeriesOutput"].LegendText = "Scenario Output";
           
            endDate = xValue;

            if (chkShowObservations.Checked) //show observed data
            {
                if (hydroData != null) //does a ORH file exist
                {
                    for (int i = 0; i < hydroData.NumParameters; i++)
                    {
                        //does Fortran code for selected parameter exist in ORH file
                        if (hydroData.ParameterCodes[i].Trim() == scenarioOutput.constituentFortranCode[lbOutputParameters.SelectedIndex].Trim())
                        {
                            for (int j = 0; j < hydroData.TheData.Count; j++) //add data to chart
                            {
                                if (hydroData.TheData[j].Date >= startDate && hydroData.TheData[j].Date <= endDate)
                                {
                                    xValue = hydroData.TheData[j].Date;
                                    yValue = Convert.ToSingle(hydroData.TheData[j].Values[i]);
                                    chartOutput.Series["SeriesObserved"].Points.AddXY(xValue, yValue);
                                }
                            }
                            break;
                        }
                    }
                }
                if (wqData != null && chartOutput.Series["SeriesObserved"].Points.Count == 0) //ORC exists, and parameter not found in ORH
                {
                    for (int i = 0; i < wqData.NumParameters; i++)
                    {
                        //does Fortran code for selected parameter exist in ORC file
                        if (wqData.ParameterCodes[i].Trim() == scenarioOutput.constituentFortranCode[lbOutputParameters.SelectedIndex].Trim())
                        {
                            for (int j = 0; j < wqData.TheData.Count; j++) //add data to chart
                            {
                                if (wqData.TheData[j].Date >= startDate && wqData.TheData[j].Date <= endDate)
                                {
                                    xValue = wqData.TheData[j].Date;
                                    yValue = Convert.ToSingle(wqData.TheData[j].Values[i]);
                                    chartOutput.Series["SeriesObserved"].Points.AddXY(xValue, yValue);
                                }
                            }
                            break;
                        }
                    }
                }
                chartOutput.Series["SeriesObserved"].LegendText = "Observations";
            }
        }

        private void lbOutputParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartTheData();
        }

        private void cbOutputType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.Text.Contains("River")) //Output types for rivers
            {
                if (cbOutputType.SelectedItem.ToString() == "Bed Sediment")
                {
                    chkShowObservations.Checked = false;
                    chkShowObservations.Enabled = false;
                    chartOutput.Series["SeriesObserved"].IsVisibleInLegend = false;
                }
                else
                {
                    chkShowObservations.Enabled = true;
                    if (river.waterQualFilename != "" || river.hydrologyFilename != "")
                    {
                        chkShowObservations.Checked = true;
                        chartOutput.Series["SeriesObserved"].IsVisibleInLegend = true;
                    }
                }
            }
            ChartTheData();
        }

        private void chkShowObservations_CheckedChanged(object sender, EventArgs e)
        {
            ChartTheData();
        }   
    }
}
