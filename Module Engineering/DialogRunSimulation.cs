using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warmf
{
    public partial class DialogRunSimulation : Form
    {
        FormMain parent;
        List<NodeHydro> SubwatershedNodesList = new List<NodeHydro>();

        public DialogRunSimulation(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate()
        {
            string fileName;
            
            DateTime date = new DateTime(2050,1,1);
            DateTime minDate = new DateTime(1900, 1, 1);
            DateTime maxDate = new DateTime(2050, 1, 1);
            
            //dates: 4 file types considered: MET, AIR, FLO (DIV), PTS
            //Files must be sorted chronologically
            for (int i = 0; i < Global.coe.numMETFiles; i++)
            {
                fileName = Global.DIR.MET + Global.coe.METFilename[i];
                fileName = fileName.Trim();

                //read and parse the third line of the file. Then compare to minDate
                minDate = getMinDate(fileName, minDate);

                //read and parse the last line of the file. Then compare to maxDate
                maxDate = getMaxDate(fileName, maxDate);
            }

            for (int i = 0; i < Global.coe.numAIRFiles; i++)
            {
                fileName = Global.DIR.AIR + Global.coe.AIRFilename[i];
                fileName = fileName.Trim();

                //read and parse the third line of the file. Then compare to minDate
                minDate = getMinDate(fileName, minDate);

                //read and parse the last line of the file. Then compare to maxDate
                maxDate = getMaxDate(fileName, maxDate);
            }

            for (int i = 0; i < Global.coe.numDIVFiles; i++)
            {
                fileName = Global.DIR.FLO + Global.coe.DIVData[i].filename;
                fileName = fileName.Trim();

                //read and parse the third line of the file. Then compare to minDate
                minDate = getMinDate(fileName, minDate);

                //read and parse the last line of the file. Then compare to maxDate
                maxDate = getMaxDate(fileName, maxDate);
            }

            for (int i = 0; i < Global.coe.numPTSFiles; i++)
            {
                fileName = Global.DIR.PTS + Global.coe.PTSFilename[i];
                fileName = fileName.Trim();

                //read and parse the third line of the file. Then compare to minDate
                minDate = getMinDate(fileName, minDate);

                //read and parse the last line of the file. Then compare to maxDate
                maxDate = getMaxDate(fileName, maxDate);
            }

            // Set the initial dates - use previously used dates unless they are not within the data date range
            SetDate(ref dtpBeginDate, Global.coe.begDate, minDate, maxDate);
            SetDate(ref dtpEndDate, Global.coe.endDate, minDate, maxDate);

            //time steps per day
            nudTimeStepsPerDay.Value = Global.coe.numTimeStepsPerDay;

            //simulation switches
            if (Global.coe.swWaterQualSim)
            {
                chbxWaterQuality.Checked = true;
            }
            if (Global.coe.swSedimentSim)
            {
                chbxSediment.Checked = true;
            }
            if (Global.coe.swFertSim)
            {
                chbxLandApplication.Checked = true;
            }
            if (Global.coe.swPointSrcSim)
            {
                chbxPointSources.Checked = true;
            }
            if (Global.coe.swCalculateLoading)
            {
                chbxLoadingData.Checked = true;
            }
            if (Global.coe.swStartupFile)
            {
                chbxWarmStart.Checked = true;
                tbWarmStartFile.Text = Global.coe.warmstartOutFilename;
            }
            if (Global.coe.numAutoCalibrationLoops > 0)
            {
                chbxHydrologyAutocalibration.Checked = true;
                nudLoops.Value = Global.coe.numAutoCalibrationLoops;
            }
            
            //populate subwatersheds listbox
            //find the watershed pour point(s) and add to SubwatershedNodesList,
            //then for each successive watershed boundary,check its location relative to the others, and 
            //add it to the appropriate location in the listbox (ordered from upstream to downstream)

            //add pour points to list
            SubwatershedNodesList = Global.coe.GetWatershedPourPoints();
            int numPourPoints = SubwatershedNodesList.Count;
            for (int i = 0; i < Global.coe.numRivers; i++)
            {
                if (Global.coe.rivers[i].swIsSubwaterBoundary)
                {
                    // Go as far down the list as possible but stop when we are upstream of another subwatershed node
                    for (int j = 0; j < SubwatershedNodesList.Count; j++)
                    {
                        if (IsNodeUpstream(SubwatershedNodesList[j].idNum, Global.coe.rivers[i].idNum))
                        {
                            SubwatershedNodesList.Insert(j, Global.coe.rivers[i]);
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < Global.coe.numReservoirs; i++)
            {
                for (int k = 0; k < Global.coe.reservoirs[i].numSegments; k++)
                {
                    if (Global.coe.reservoirs[i].reservoirSegs[k].swIsSubwaterBoundary)
                    {
                        // Go as far down the list as possible but stop when we are upstream of another subwatershed node
                        for (int j = 0; j <  SubwatershedNodesList.Count; j++)
                        {
                            if (IsNodeUpstream(SubwatershedNodesList[j].idNum, Global.coe.reservoirs[i].reservoirSegs[k].idNum))
                            {
                                SubwatershedNodesList.Insert(j, Global.coe.reservoirs[i].reservoirSegs[k]);
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < SubwatershedNodesList.Count; i++)
            {
                lbSubwatersheds.Items.Add(SubwatershedNodesList[i].name);
                lbSubwatersheds.SetSelected(i, true);
            }
        }

        // Sets either the beginning or ending date and date range
        private void SetDate(ref System.Windows.Forms.DateTimePicker datePicker, DateTime theDate, DateTime minDate, DateTime maxDate)
        {
            if (minDate <= theDate)
                datePicker.Value = theDate;
            else
                datePicker.Value = minDate;
            if (maxDate >= theDate)
                datePicker.Value = theDate;
            else
                datePicker.Value = maxDate;
            datePicker.MinDate = minDate;
            datePicker.MaxDate = maxDate;
        }

        public static DateTime getMinDate(string fileName, DateTime minDate)
        {
            string line;
            string fileType;
            int compareResult;

            //read and parse the first data line of the file. Then compare to minDate
            fileType = fileName.Substring(fileName.Length - 3, 3);
            if (string.Equals(fileType, "PTS", StringComparison.OrdinalIgnoreCase))
                line = File.ReadLines(fileName).Skip(4).Take(1).First();
            else if (string.Equals(fileType, "MET", StringComparison.OrdinalIgnoreCase))
                line = File.ReadLines(fileName).Skip(2).Take(1).First();
            else
                line = File.ReadLines(fileName).Skip(3).Take(1).First();

            Int32.TryParse(line.Substring(4, 4), out int year);
            Int32.TryParse(line.Substring(2, 2), out int month);
            Int32.TryParse(line.Substring(0, 2), out int day);
            DateTime date = new DateTime(year, month, day);
            compareResult = System.DateTime.Compare(date, minDate);
            if (compareResult > 0)
            {
                minDate = date;
            }
            return minDate;
        }

        public static DateTime getMaxDate(string fileName, DateTime maxDate)
        {
            string line;
            int compareResult;

            if (string.Equals(fileName.Substring(fileName.Length - 3, 3), "AIR", StringComparison.OrdinalIgnoreCase))
            {
                var lineCount = File.ReadLines(fileName).Count();
                line = File.ReadLines(fileName).Skip(lineCount - 3).Take(1).First();
            }
            else
            {
                line = File.ReadLines(fileName).Last();
            }
            
            Int32.TryParse(line.Substring(4, 4), out int year);
            Int32.TryParse(line.Substring(2, 2), out int month);
            Int32.TryParse(line.Substring(0, 2), out int day);
            DateTime date = new DateTime(year, month, day);
            compareResult = System.DateTime.Compare(date, maxDate);
            if (compareResult < 0)
            {
                maxDate = date;
            }
            return maxDate;
        }

        private void chbxWarmStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxWarmStart.Checked)
            {
                btnSelectWst.Enabled = true;
                lblWarmStartFile.Enabled = true;
                tbWarmStartFile.Enabled = true;
            }
            else
            {
                btnSelectWst.Enabled = false;
                lblWarmStartFile.Enabled = false;
                tbWarmStartFile.Enabled = false;
            }
        }

        private void chbxHydrologyAutocalibration_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxHydrologyAutocalibration.Checked)
            {
                lblNumLoops.Enabled = true;
                nudLoops.Enabled = true;
            }
            else
            {
                lblNumLoops.Enabled = false;
                nudLoops.Enabled = false;
            }
        }

        public bool IsNodeUpstream(int downstreamNode, int upstreamNodeID)
        {
            List<int> upstreamRiverIDs = new List<int>();
            List<int> upstreamReservoirSegmentIDs = new List<int>();
            int coefficientIndex = Global.coe.GetRiverNumberFromID(downstreamNode);
            if (coefficientIndex >= 0) //downstreamNode is a river segment
            {
                upstreamRiverIDs = Global.coe.rivers[coefficientIndex].upstreamRiverIDs;
                upstreamReservoirSegmentIDs = Global.coe.rivers[coefficientIndex].upstreamReservoirIDs;
            }
            else //downstreamNode is a reservoir segment
            {
                List<int> reservoirAndSegment = Global.coe.GetReservoirAndSegmentNumberFromID(downstreamNode);
                if (reservoirAndSegment[0] >= 0 && reservoirAndSegment[1] >= 0)
                {
                    upstreamRiverIDs = Global.coe.reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].upstreamRiverIDs;
                    upstreamReservoirSegmentIDs = Global.coe.reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].upstreamReservoirIDs;
                }
            }

            int nodeID;
            for (int i = 0; i < 9; i++)
            {
                nodeID = upstreamRiverIDs[i];
                if (nodeID > 0 && nodeID == upstreamNodeID)
                    return true;

                if (nodeID > 0 && IsNodeUpstream(nodeID,upstreamNodeID))
                    return true;
            }
            for (int i = 0; i < 9; i++)
            {
                nodeID = upstreamReservoirSegmentIDs[i];
                if (nodeID > 0 && nodeID == upstreamNodeID)
                    return true;

                if (nodeID > 0 && IsNodeUpstream(nodeID, upstreamNodeID))
                    return true;
            }
            return false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string fileName;
            // update coe parameter values with entered information
            // dates
            Global.coe.begDate = dtpBeginDate.Value;
            Global.coe.endDate = dtpEndDate.Value;
            // time steps per day
            Global.coe.numTimeStepsPerDay = Convert.ToInt16(nudTimeStepsPerDay.Value);
            //simulation switches
            if (chbxWaterQuality.Checked)
            {
                Global.coe.swWaterQualSim = true;
            }
            if (chbxSediment.Checked)
            {
                Global.coe.swSedimentSim = true;
            }
            if (chbxLandApplication.Checked)
            {
                Global.coe.swFertSim = true;
            }
            if (chbxPointSources.Checked)
            {
                Global.coe.swPointSrcSim = true;
            }
            if (chbxLoadingData.Checked)
            {
                Global.coe.swCalculateLoading = true;
            }
            if (chbxWarmStart.Checked)
            {
                Global.coe.swStartupFile = true;
                Global.coe.warmstartOutFilename = tbWarmStartFile.Text;
            }
            if (chbxHydrologyAutocalibration.Checked)
            {
                Global.coe.numAutoCalibrationLoops = Convert.ToInt32(nudLoops.Value);
            }

            // Write MODEL.CTL file
            STechStreamWriter modelctl = new STechStreamWriter(Global.DIR.ROOT + "MODEL.CTL");
            // Number of batch loops
            modelctl.WriteLine("0");
            // Number of subwatersheds
            modelctl.WriteLine(lbSubwatersheds.SelectedIndices.Count.ToString());

            // Generate run file (00000000.Txx) for each subwatershed
            for (int i = 0; i < SubwatershedNodesList.Count; i++)
            {
                fileName = Global.coe.AppendFileNameWithSubwatershed(Global.DIR.INPUT + "00000000.T00", i + 1);

                // Write file name to MODEL.CTL if this subwatershed is to be simulated
                if (lbSubwatersheds.SelectedIndices.Contains(i))
                    modelctl.WriteLine(Global.coe.GetRelativePath(fileName));

                int nodeIndex = Global.coe.GetRiverNumberFromID(SubwatershedNodesList[i].idNum);
                if (nodeIndex >= 0) //rivers
                {
                    Global.coe.rivers[nodeIndex].subwatershed = i + 1;
                    Global.coe.DefineSubwatershed(Global.coe.rivers[nodeIndex],i + 1);
                }
                else //reservoirs & reservoir segments
                {
                    List<int> reservoirAndSegment = Global.coe.GetReservoirAndSegmentNumberFromID(SubwatershedNodesList[i].idNum);
                    if (reservoirAndSegment.Count == 2 && reservoirAndSegment[0] >= 0 && reservoirAndSegment[1] >= 0)
                    {
                        Global.coe.reservoirs[reservoirAndSegment[0]].subwatershed = i + 1;
                        Global.coe.reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]].subwatershed = i + 1;
                        Global.coe.DefineSubwatershed(Global.coe.reservoirs[reservoirAndSegment[0]].reservoirSegs[reservoirAndSegment[1]],i+1);
                    }
                    // Rogue catchment not connected to anything
                    else
                    {
                        nodeIndex = Global.coe.GetCatchmentNumberFromID(SubwatershedNodesList[i].idNum);
                        if (nodeIndex >= 0)
                        {
                            Global.coe.catchments[nodeIndex].subwatershed = i + 1;
                            Global.coe.DefineSubwatershed(Global.coe.catchments[nodeIndex], i + 1);
                        }
                    }
                }
                //Write the subwatershed information out to 00000000 file
                Global.coe.WriteCOE(fileName, i + 1);
                
            }

            // Write final output files to MODEL.CTL
            modelctl.WriteLine(Global.coe.GetRelativePath(Global.DIR.CAT) + Global.coe.catchmentOutFilename);
            modelctl.WriteLine(Global.coe.GetRelativePath(Global.DIR.RIV) + Global.coe.riverOutFilename);
            modelctl.WriteLine(Global.coe.GetRelativePath(Global.DIR.LAK) + Global.coe.reservoirOutFilename);
            modelctl.WriteLine(Global.coe.GetRelativePath(Global.DIR.PSM) + Global.coe.loadingOutFilename);
            modelctl.WriteLine(Global.coe.GetRelativePath(Global.DIR.WST) + Global.coe.warmstartOutFilename);
            modelctl.Close();

            // Run model.exe
            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = Global.DIR.ROOT + "\\MODEL.EXE";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = false; //display in a separate window
                pProcess.Start();
//                string output = pProcess.StandardOutput.ReadToEnd(); //The output result
//                pProcess.WaitForExit();
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbSubwatersheds.Items.Count; i++)
                lbSubwatersheds.SetSelected(i, true);
            btnRun.Enabled = true;
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lbSubwatersheds.ClearSelected();
            btnRun.Enabled = false;
        }

        private void lbSubwatersheds_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRun.Enabled = (lbSubwatersheds.SelectedIndices.Count > 0);
        }
    }
}
