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
        public List<int> masterConstituentNumbers;
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
            masterConstituentNumbers = new List<int>();
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

                    // Add constituents for each scenario to master list if not already present
                    for (int j = 0; j < newOutput.constituentNumbers.Count; j++)
                        if (!masterConstituentNumbers.Contains(newOutput.constituentNumbers[j]))
                            masterConstituentNumbers.Add(newOutput.constituentNumbers[j]);
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

            // Populate listbox containing output parameters
            masterConstituentNumbers.Sort();
            List<string> constituentNameUnits = new List<string>();
            for (int i = 0; i < masterConstituentNumbers.Count; i++)
                constituentNameUnits.Add(Global.coe.GetParameterNameAndUnitsFromNumber(masterConstituentNumbers[i]));
            lbOutputParameters.DataSource = constituentNameUnits;

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

            // Populate the combo box to select the scenario for text output
            rbThisConstituent.Checked = true;
            rbAllConstituents.Checked = false;
            for (int i = 0; i < FormMain.scenarios.Count; i++)
                cbTextFileScenario.Items.Add(Path.GetFileNameWithoutExtension(FormMain.scenarios[i].Name));
            if (cbTextFileScenario.Items.Count > 0)
                cbTextFileScenario.Text = cbTextFileScenario.Items[0].ToString();

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
                    // indexParameter is the position in the list of a scenario's output parameter list of the selected parameter in the list box
                    if (lbOutputParameters.SelectedIndex >= 0 && lbOutputParameters.SelectedIndex < masterConstituentNumbers.Count)
                        indexParameter = scenarioOutput[j].constituentNumbers.IndexOf(masterConstituentNumbers[lbOutputParameters.SelectedIndex]);
                    else
                        indexParameter = -1;

                    if (indexParameter >= 0)
                    {
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
            SetTextFileName();
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

        // Sets the default file name for exporting output to a CSV file
        private void SetTextFileName()
        {
            string textFileString;

            // Get the state of the radio buttons
            if (rbThisConstituent.Checked == true)
            {
                // Output for one constituent, multiple scenarios: get constituent name
                int selected = lbOutputParameters.SelectedIndex;
                if (lbOutputParameters.SelectedIndex >= 0)
                {
                    // Start with the constituent name as shown in the list box
                    textFileString = lbOutputParameters.SelectedItem.ToString();

                    // Truncate at the comma to remove the units
                    int commaLocation = textFileString.IndexOf(",");
                    if (commaLocation > 0)
                        textFileString = textFileString.Substring(0, commaLocation);
                }
                else
                    textFileString = "output";
            }
            else
                // Output for all constituents, one scenario
                textFileString = cbTextFileScenario.Text;

            // Add the extension for a comma delimited file
            textFileString = textFileString + ".CSV";

            // Set the text edit box
            tbTextFileName.Text = textFileString;
        }

        private void rbThisConstituent_CheckedChanged(object sender, EventArgs e)
        {
            SetTextFileName();
        }

        private void rbAllConstituents_CheckedChanged(object sender, EventArgs e)
        {
            SetTextFileName();
        }

        private void cbTextFileScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTextFileName();
        }

        private void btnCreateTextFile_Click(object sender, EventArgs e)
        {
            // Get the output file name and open the file
            STechStreamWriter dumpFile = new STechStreamWriter(tbTextFileName.Text);

            if (dumpFile != null)
            {
                // List of time series included in output
                List<int> timeSeriesInOutput = new List<int>();
                
                // Header
                dumpFile.Write("Date");
                if (rbThisConstituent.Checked == true)
                {
                    for (int i = 0; i < chartOutput.Series.Count; i++)
                    {
                        dumpFile.WriteDelimitedField(chartOutput.Series[i].LegendText);

                        // This constituent means all time series are included in text output
                        timeSeriesInOutput.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i < lbOutputParameters.Items.Count; i++)
                        dumpFile.WriteDelimitedField(lbOutputParameters.Items.ToString());

                    // The scenario to include in output is the one in the combo box
                    timeSeriesInOutput.Add(cbTextFileScenario.SelectedIndex);
                }
                dumpFile.WriteLine();

                // The first and last dates plotted for all the scenarios in output
                DateTime firstDateTime = new DateTime();
                DateTime lastDateTime = new DateTime();
                // The time steps per day in the text file is the maximum of all selected scenarios
                int outputTimeStepsPerDay = 1;
                // Counters for each time series in output
                List<int> pointCounters = new List<int>();
                for (int i = 0; i < timeSeriesInOutput.Count; i++)
                {
                    if (timeSeriesInOutput[i] >= 0 && timeSeriesInOutput[i] < scenarioOutput.Count)
                    {
                        // Greatest number of time steps per day
                        outputTimeStepsPerDay = Math.Max(outputTimeStepsPerDay, scenarioOutput[timeSeriesInOutput[i]].timeStepPerDay);

                        // Earliest start of model output
                        DateTime scenarioStartDate = new DateTime(scenarioOutput[timeSeriesInOutput[i]].startDateYear, scenarioOutput[timeSeriesInOutput[i]].startDateMonth, scenarioOutput[timeSeriesInOutput[i]].startDateDay);
                        if (i == 0 || firstDateTime > scenarioStartDate)
                            firstDateTime = scenarioStartDate;

                        // Last end of model output
                        DateTime scenarioEndDate = scenarioStartDate.AddDays(scenarioOutput[timeSeriesInOutput[i]].numSimDays);
                        if (i == 0 || lastDateTime < scenarioEndDate)
                            lastDateTime = scenarioEndDate;
                    }

                    pointCounters.Add(0);
                }

                // Output
                DateTime dateBase = new DateTime(1900, 1, 1);
                DateTime outputDateTime = firstDateTime;
                double outputDateIncrement = 1 / outputTimeStepsPerDay;
                while (outputDateTime <= lastDateTime)
                {
                    // Date and Time
                    dumpFile.Write(outputDateTime.Date.ToString("M/d/yyyy"));
                    dumpFile.WriteDelimitedField(outputDateTime.ToString("HH:mm"));

                    // Output
                    if (rbThisConstituent.Checked == true)
                    {
                        for (int i = 0; i < chartOutput.Series.Count; i++)
                        {
                            // Advance to the current date/time
                            while (pointCounters[i] < chartOutput.Series[i].Points.Count && dateBase.AddDays(chartOutput.Series[i].Points[pointCounters[i]].XValue - 2) < outputDateTime.AddDays(outputDateIncrement))
                            {
                                if (dateBase.AddDays(chartOutput.Series[i].Points[pointCounters[i]].XValue) >= outputDateTime)
                                    dumpFile.WriteDelimitedField(chartOutput.Series[i].Points[pointCounters[i]].YValues[0].ToString());
                                pointCounters[i]++;
                            }
                        }
                    }
                    // All constituents, this scenario
                    else
                    {

                    }
                    dumpFile.WriteLine();
                    outputDateTime = outputDateTime.AddDays(outputDateIncrement);
                }

                /*                if (rbThisConstituent.Checked == true)
                                {
                                    // Get the size of the data stored in the chart
                                    int nCols = ChartData->GetNSets();

                                    // Write header columns

                                    dumpFile << "     Day";
                                    headerText = new char[256];
                                    for (i = 0; i < nCols; i++)
                                    {
                                        // Don't output hidden/excluded series
                                        if (ChartData->GetDisplay(i) == XRT_DISPLAY_SHOW)
                                        {
                                            // Get the name of each plotted series
                                            strcpy(headerText, Chart->GetNthSetLabel(i));
                                            //trucate headerText if more than 8 characters
                                            headerText[8] = '\0';

                                            // Pad the beginning of the header text with spaces if it is
                                            // less than eight characters plus NULL.
                                            int extra = 8 - strlen(headerText);
                                            if (extra > 0)
                                            {
                                                for (j = 8; j >= extra; j--)
                                                    headerText[j] = headerText[j - extra];
                                                for (j = 0; j < extra; j++)
                                                    headerText[j] = ' ';
                                            }
                                            dumpFile << setw(9) << setiosflags(ios::right) << headerText;
                                        }
                                    }
                                    dumpFile << "\n";
                                    delete[] headerText;

                                    WriteConstituentData(dumpFile);
                                    dumpFile.close();
                                }
                                else
                                {
                                    // Write header columns
                                    dumpFile << "    Date";
                                    for (i = 0; i < ChartInfo->ConstituentColumn.Number; i++)
                                    {
                                        int length = ConstituentListBox->GetStringLen(i) + 1;
                                        headerText = new char[length];
                                        ConstituentListBox->GetString(headerText, i);
                                        AbbreviateConstituentName(headerText);
                                        // Pad the beginning of the header text with spaces if it is
                                        // less than eight characters plus NULL.
                                        int extra = 9 - strlen(headerText);
                                        for (j = 0; j < extra; j++)
                                            dumpFile << " ";
                                        dumpFile << headerText;
                                        delete[] headerText;
                                    }
                                    dumpFile << "\n";

                                    // Write scenario data
                                    int theScenario = ScenarioCombo->GetSelIndex();
                                    stDate outDate;
                                    char dateString[9];
                                    outDate = BegDate + ScenarioInfo[theScenario].DaysOffset;
                                    for (i = 0; i < ScenarioInfo[theScenario].NumberOfSimulationDays; i++)
                                    {
                                        for (int timestep = 0; timestep < ScenarioInfo[theScenario].TimeSteps; timestep++)
                                        {
                                            sprintf(dateString, "%2.2d%2.2d%4.4d", outDate.nDay, outDate.nMonth, outDate.nYear);
                                            dumpFile.SetString(dateString);
                                            for (j = 0; j < ChartInfo->ConstituentColumn.Number; j++)
                                            {
                                                dumpFile.put(' ');
                                                if (ScenarioData[theScenario])
                                                    dumpFile.SetDouble(ScenarioData[theScenario][j].Data.Values[i * ScenarioInfo[theScenario].TimeSteps + timestep]);
                                                else
                                                    dumpFile.SetDouble(-999.);
                                            }
                                            dumpFile << "\n";
                                        }
                                        outDate++;
                                    }
                                }*/
                dumpFile.Close();
            }
        }
    }
}
