using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Collections.Generic;



namespace warmf
{
    public partial class DialogOutput : Form
    {
        public static List <Output> scenarioOutput = new List <Output>();
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
                Text = river.name + " Output";

                //Observed data files
                if (river.hydrologyFilename != "")
                {
                    hydroData = new ObservedFile(Global.DIR.ORH + river.hydrologyFilename);
                    hydroData.ReadFile();
                }
                if (river.obsWQFilename != "")
                {
                    wqData = new ObservedFile(Global.DIR.ORC + river.obsWQFilename);
                    wqData.ReadFile();
                }

                //Read the .RIV files (for selected river) into memory
                for (int i = 0; i < FormMain.scenarios.Count; i++)
                {
                    Output newOutput = new Output();
                    // Compile river output file name from scenario name
                    string outputFilename = Path.GetFileNameWithoutExtension(FormMain.scenarios[i].Name) + ".RIV";
                    newOutput.ReadOutput(Global.DIR.RIV + outputFilename, river.idNum);
                    scenarioOutput.Add(newOutput);
                }

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
                Text = catchment.name + " Output";

                //Read the .CAT files (for selected catchment) into memory
                for (int i = 0; i < FormMain.scenarios.Count; i++)
                {
                    Output newOutput = new Output();
                    // Compile catchment output file name from scenario name
                    string outputFilename = Path.GetFileNameWithoutExtension(FormMain.scenarios[i].Name) + ".CAT";
                    newOutput.ReadOutput(Global.DIR.CAT + outputFilename, catchment.idNum);
                    scenarioOutput.Add(newOutput);
                }

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
            else if (featureType == "Lake")
            {
                // Unlike catchments and rivers, the cnum passed in is the reservoir segment ID, not the index
                List<int> reservoirAndSegment = Global.coe.GetReservoirAndSegmentNumberFromID(cnum);

                // Set the caption bar of the dialog
                Text = Global.coe.reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].name;

                // Read the .LAK file into memory
                
            }

            //Populate listbox containing output parameters
            // Needs to be modified to allow for different scenarios to have different parameters in output
            lbOutputParameters.DataSource = scenarioOutput[0].constituentNameUnits;

            //format the output graph (populated on ChartTheData)
            ChartArea chartArea1 = chartOutput.ChartAreas["ChartArea1"];
            Series series2 = chartOutput.Series["SeriesObserved"];
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisX.Title = "Date";
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;

            //select output type and chemical parameter
            cbOutputType.SelectedIndex = 1;
            if (lbOutputParameters.Items.Count > 0)
                lbOutputParameters.SelectedIndex = 0;

            //chart the data
            ChartTheData();

            //add SelectedIndexChanged event handler
            this.lbOutputParameters.SelectedIndexChanged += new System.EventHandler(this.lbOutputParameters_SelectedIndexChanged);
        }

        // Returns the name of the string referenced by chartOutput.Series
        private string GetScenarioSeriesName(int scenarioNumber)
        {
            return "SeriesOutput" + (scenarioNumber + 1).ToString();
        }

        // Returns a nice round number larger than the maximum value passed in to be the maximum Y-axis value
        private double GetMaximumYAxisValue(float maxValue)
        {
            // Don't know what to do with zero or negative numbers
            if (maxValue <= 0)
                return 1;
            // Get the order of magnitude
            int orderOfMagnitude = Convert.ToInt32(Math.Log10(maxValue));
            // Make maxValue a number between 10 and 100
            double axisMaximum = maxValue / Math.Pow(10, orderOfMagnitude - 2);

            double increment;
            // 10-20: Get next increment of 4 higher
            if (axisMaximum < 20)
                increment = 4;

            else if (axisMaximum < 40)
                increment = 5;
            else
                increment = 10;

            return Convert.ToInt32(axisMaximum / increment + 1) * increment * Math.Pow(10, orderOfMagnitude - 2);
        }

        public void ChartTheData()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            DateTime xValue = new DateTime();
            float yValue;
            float maxValue = 0;
            chartOutput.Series.Clear();
            // Add the scenario output
            for (int j = 0; j < FormMain.scenarios.Count; j++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series scenarioSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
                scenarioSeries.ChartArea = "ChartArea1";
                scenarioSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                scenarioSeries.Legend = "Legend1";
                scenarioSeries.Name = GetScenarioSeriesName(j);

                double timeStep = new double();
                if (scenarioOutput[j].timeStepPerDay > 0)
                {
                    timeStep = 24 / scenarioOutput[j].timeStepPerDay;
                    xValue = new DateTime(scenarioOutput[j].startDateYear, scenarioOutput[j].startDateMonth, scenarioOutput[j].startDateDay, 0, 0, 0);
                    int indexOutputType, indexParameter;

                    if (j == 0 || xValue < startDate)
                        startDate = xValue;

                    indexOutputType = cbOutputType.SelectedIndex; //output type
                    indexParameter = lbOutputParameters.SelectedIndex; //parameter

                    for (int i = 0; i < (scenarioOutput[j].output[indexOutputType, indexParameter].Count); i++)
                    {
                        //y values (time series output)
                        yValue = scenarioOutput[j].output[indexOutputType, indexParameter][i];

                        if (yValue != -999)
                        {
                            scenarioSeries.Points.AddXY(xValue, yValue);
                            maxValue = Math.Max(maxValue, yValue);
                        }

                        //increment x value
                        xValue = xValue.AddHours(timeStep);
                    }
                    scenarioSeries.LegendText = Path.GetFileNameWithoutExtension(FormMain.scenarios[j].Name);
                    chartOutput.Series.Add(scenarioSeries);
                    if (j == 0 || xValue > endDate)
                        endDate = xValue;
                }
            }
            chartOutput.ChartAreas["ChartArea1"].AxisY.Title = lbOutputParameters.SelectedItem.ToString();
           
            if (chkShowObservations.Checked) //show observed data
            {
                System.Windows.Forms.DataVisualization.Charting.Series observedSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
                observedSeries.ChartArea = "ChartArea1";
                observedSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                observedSeries.Legend = "Legend1";
                observedSeries.MarkerBorderColor = System.Drawing.Color.Black;
                observedSeries.MarkerColor = System.Drawing.Color.Transparent;
                observedSeries.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                observedSeries.Name = "Observed";

                if (hydroData != null) //does a ORH file exist
                {
                    for (int i = 0; i < hydroData.NumParameters; i++)
                    {
                        //does Fortran code for selected parameter exist in ORH file
                        if (hydroData.ParameterCodes[i].Trim() == scenarioOutput[0].constituentFortranCode[lbOutputParameters.SelectedIndex].Trim())
                        {
                            for (int j = 0; j < hydroData.TheData.Count; j++) //add data to chart
                            {
                                if (hydroData.TheData[j].Date >= startDate && hydroData.TheData[j].Date <= endDate)
                                {
                                    xValue = hydroData.TheData[j].Date;
                                    yValue = Convert.ToSingle(hydroData.TheData[j].Values[i]);
                                    if (yValue != -999)
                                    {
                                        observedSeries.Points.AddXY(xValue, yValue);
                                        maxValue = Math.Max(maxValue, yValue);
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                if (wqData != null) //ORC exists, and parameter not found in ORH
                {
                    for (int i = 0; i < wqData.NumParameters; i++)
                    {
                        //does Fortran code for selected parameter exist in ORC file
                        if (wqData.ParameterCodes[i].Trim() == scenarioOutput[0].constituentFortranCode[lbOutputParameters.SelectedIndex].Trim())
                        {
                            for (int j = 0; j < wqData.TheData.Count; j++) //add data to chart
                            {
                                if (wqData.TheData[j].Date >= startDate && wqData.TheData[j].Date <= endDate)
                                {
                                    xValue = wqData.TheData[j].Date;
                                    yValue = Convert.ToSingle(wqData.TheData[j].Values[i]);
                                    if (yValue != -999)
                                    {
                                        observedSeries.Points.AddXY(xValue, yValue);
                                        maxValue = Math.Max(maxValue, yValue);
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                if (observedSeries.Points.Count > 0)
                    chartOutput.Series.Add(observedSeries);
            }

            // Reset the scaling on the Y axis
            if (maxValue > 0)
            {
                // First set the maximum value to get the tick mark interval
                chartOutput.ChartAreas["ChartArea1"].AxisY.Maximum = GetMaximumYAxisValue(maxValue);
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
                    if (river.obsWQFilename != "" || river.hydrologyFilename != "")
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
