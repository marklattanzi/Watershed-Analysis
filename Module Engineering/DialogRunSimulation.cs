﻿using System;
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
        
        public DialogRunSimulation(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate()
        {
            string fileName;
            
            List<NodeHydro> SubwatershedNodesList = new List<NodeHydro>();
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

            dtpBeginDate.Value = minDate;
            dtpBeginDate.MinDate = minDate;
            dtpEndDate.Value = maxDate;
            dtpEndDate.MaxDate = maxDate;

            //time steps per day
            nudTimeStepsPerDay.Value = Global.coe.numTimeStepsPerDay;

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
                    for (int j = SubwatershedNodesList.Count - 1; j >= 0; j--)
                    {
                        if (IsNodeUpstream(SubwatershedNodesList[j].idNum, Global.coe.rivers[i].idNum) == false
                            || j==0)
                        {
                            if (SubwatershedNodesList.Count==numPourPoints)
                            {
                                SubwatershedNodesList.Insert(j, Global.coe.rivers[i]);
                            }
                            else
                            {
                                SubwatershedNodesList.Insert(j + 1, Global.coe.rivers[i]);//added +1 - if conditions met, add below comparison node, not above
                            }
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
                        for (int j = SubwatershedNodesList.Count - 1; j >= 0; j--)
                        {
                            if (IsNodeUpstream(SubwatershedNodesList[j].idNum, Global.coe.reservoirs[i].reservoirSegs[k].idNum) == false
                                || j==0)
                            {
                                if (SubwatershedNodesList.Count==numPourPoints)
                                {
                                    SubwatershedNodesList.Insert(j, Global.coe.rivers[i]);
                                }
                                else
                                {
                                    SubwatershedNodesList.Insert(j + 1, Global.coe.reservoirs[i].reservoirSegs[k]);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < SubwatershedNodesList.Count; i++)
            {
                lbSubwatersheds.Items.Add(SubwatershedNodesList[i].name);
            }
        }

        public static DateTime getMinDate(string fileName, DateTime minDate)
        {
            string line;
            string fileType;
            int year, month, day;
            int compareResult;

            //read and parse the first data line of the file. Then compare to minDate
            fileType = fileName.Substring(fileName.Length - 3, 3);
            if (string.Equals(fileType, "PTS", StringComparison.OrdinalIgnoreCase))
                line = File.ReadLines(fileName).Skip(4).Take(1).First();
            else if (string.Equals(fileType, "MET", StringComparison.OrdinalIgnoreCase))
                line = File.ReadLines(fileName).Skip(2).Take(1).First();
            else
                line = File.ReadLines(fileName).Skip(3).Take(1).First();

            Int32.TryParse(line.Substring(4, 4), out year);
            Int32.TryParse(line.Substring(2, 2), out month);
            Int32.TryParse(line.Substring(0, 2), out day);
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
            int year, month, day;
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
            
            Int32.TryParse(line.Substring(4, 4), out year);
            Int32.TryParse(line.Substring(2, 2), out month);
            Int32.TryParse(line.Substring(0, 2), out day);
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
                List<int> reservoirAndSegment = Global.coe.GetReservoirSegmentNumberFromID(downstreamNode);
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
            //figure out what to run

        }
    }
}
