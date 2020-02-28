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
        

        public DialogRunSimulation(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate()
        {
            string fileName;
            
            List<Node> SubwatershedNodesList = new List<Node>();
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
            //find the first river segment listed as a subwatershed boundary,
            //then for each successive watershed boundary,check its location relative to the others, and 
            //add it to the appropriate location in the listbox (ordered from upstream to downstream
            for (int i = 0; i < Global.coe.numRivers; i++)
            {
                if (Global.coe.rivers[i].swIsSubwaterBoundary)
                {
                    Node n = new Node();
                    n.ID = Global.coe.rivers[i].idNum;
                    n.Type = "RIVER";
                    n.Name = Global.coe.rivers[i].name;

                    //first subwatershed boundary found
                    if (lbSubwatersheds.Items.Count == 0)
                    {
                        lbSubwatersheds.Items.Add(n.Name);
                        SubwatershedNodesList.Add(n);
                    }
                    else
                    {
                        //is current node upstream of last entry in SubwatershedNodes list
                        if (isDownstreamOf(n, SubwatershedNodesList[SubwatershedNodesList.Count - 1]))
                        {
                            lbSubwatersheds.Items.Add(n.Name);
                            SubwatershedNodesList.Add(n);
                        }
                        else //current node is either at same level as or upstream of last item in SubwatershedNodes list
                        {
                            bool sameLevel = true;
                            for (int j = SubwatershedNodesList.Count - 1; j >= 0; j--)
                            {
                                if (isDownstreamOf(n, SubwatershedNodesList[j]))
                                {
                                    lbSubwatersheds.Items.Insert(j, n.Name);
                                    SubwatershedNodesList.Insert(j,n);
                                    sameLevel = false;
                                }
                            }
                            if (sameLevel == true)
                            {
                                lbSubwatersheds.Items.Add(n.Name);
                                SubwatershedNodesList.Add(n);
                            }
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
                        Node n = new Node();
                        n.ID = Global.coe.reservoirs[i].reservoirSegs[k].idNum;
                        n.Type = "RESERVOIR SEGMENT";
                        n.Name = Global.coe.reservoirs[i].reservoirSegs[k].name;

                        //first subwatershed boundary found
                        if (lbSubwatersheds.Items.Count == 0)
                        {
                            lbSubwatersheds.Items.Add(n.Name);
                            SubwatershedNodesList.Add(n);
                        }
                        else
                        {
                            //is current node upstream of last entry in SubwatershedNodes list
                            if (isDownstreamOf(n, SubwatershedNodesList[SubwatershedNodesList.Count - 1]))
                            {
                                lbSubwatersheds.Items.Add(n.Name);
                                SubwatershedNodesList.Add(n);
                            }
                            else //current node is either at same level as or upstream of last item in SubwatershedNodes list
                            {
                                bool sameLevel = true;
                                for (int j = SubwatershedNodesList.Count - 1; j >= 0; j--)
                                {
                                    if (isDownstreamOf(n, SubwatershedNodesList[j]))
                                    {
                                        lbSubwatersheds.Items.Insert(j, n.Name);
                                        SubwatershedNodesList.Insert(j, n);
                                        sameLevel = false;
                                    }
                                }
                                if (sameLevel == true)
                                {
                                    lbSubwatersheds.Items.Add(n.Name);
                                    SubwatershedNodesList.Add(n);
                                }
                            }
                        }
                    }
                }
            }

        }
        public class Node
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
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

        private Boolean isDownstreamOf(Node node, Node USnode)
        {
            List<int> UpstreamReservoirList = new List<int>();
            List<int> UpstreamRiverList = new List<int>();
            List<int> TotalRiverNodes = new List<int>();
            List<int> TotalReservoirNodes = new List<int>();
            int riverCounter, reservoirCounter;

            if (node.Type == "RIVER")
            {
                //find the node to start from
                for (int i = 0; i < Global.coe.numRivers; i++)
                {
                    if (Global.coe.rivers[i].idNum == node.ID)
                    {
                        for (int ii = 0; ii < 9; ii++)
                        {
                            if (Global.coe.rivers[i].upstreamRiver[ii] == 0)
                            {
                                break;
                            }
                            else
                            {
                                TotalRiverNodes.Add(Global.coe.rivers[i].upstreamRiver[ii]);
                            }
                        }
                        for (int ii = 0; ii < 9; ii++)
                        {
                            if (Global.coe.rivers[i].upstreamReservoir[ii] == 0)
                            {
                                break;
                            }
                            else
                            {
                                TotalReservoirNodes.Add(Global.coe.rivers[i].upstreamReservoir[ii]);
                            }
                        }
                        break;
                    }  
                }  
            }
            else if (node.Type == "RESERVOIR SEGMENT")
            {
                for (int i = 0; i < Global.coe.numReservoirs; i++)
                {
                    for (int ii = 0; ii < Global.coe.reservoirs[i].numSegments; ii++)
                    {
                        if (Global.coe.reservoirs[i].reservoirSegs[ii].idNum == node.ID)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (Global.coe.reservoirs[i].reservoirSegs[ii].upstreamRiverIDs[j] == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    TotalRiverNodes.Add(Global.coe.reservoirs[i].reservoirSegs[ii].upstreamRiverIDs[j]);
                                }
                            }
                            for (int j = 0; j < 9; j++)
                            {
                                if (Global.coe.reservoirs[i].reservoirSegs[ii].upstreamLakeIDs[j] == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    TotalReservoirNodes.Add(Global.coe.reservoirs[i].reservoirSegs[ii].upstreamLakeIDs[j]);
                                }
                            }
                            break;
                        }
                    }
                    
                }
            }
            else //this would be for catchments listed as subwatershed boundaries.
            {

            }

            riverCounter = 0;
            reservoirCounter = 0;
            while (riverCounter < TotalRiverNodes.Count | reservoirCounter < TotalReservoirNodes.Count)
            {
                for (int i = riverCounter; i < TotalRiverNodes.Count; i++)
                {
                    for (int ii = 0; ii < Global.coe.numRivers; ii++)
                    {
                        if (Global.coe.rivers[ii].idNum == TotalRiverNodes[riverCounter])
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (Global.coe.rivers[ii].upstreamRiver[j] == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    TotalRiverNodes.Add(Global.coe.rivers[ii].upstreamRiver[j]);
                                }
                            }
                            for (int j = 0; j < 9; j++)
                            {
                                if (Global.coe.rivers[ii].upstreamReservoir[j] == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    TotalReservoirNodes.Add(Global.coe.rivers[ii].upstreamReservoir[j]);
                                }
                            }
                            riverCounter++;
                            break;
                        }
                    }
                }
                for (int i = reservoirCounter; i < TotalReservoirNodes.Count; i++)
                {
                    for (int ii = 0; ii < Global.coe.numReservoirs; ii++)
                    {
                        for (int iii = 0; iii < Global.coe.reservoirs[ii].numSegments; iii++)
                        {
                            if (Global.coe.reservoirs[ii].reservoirSegs[iii].idNum == TotalReservoirNodes[reservoirCounter])
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    if (Global.coe.reservoirs[ii].reservoirSegs[iii].upstreamRiverIDs[j] == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        TotalRiverNodes.Add(Global.coe.reservoirs[ii].reservoirSegs[iii].upstreamRiverIDs[j]);
                                    }
                                }
                                for (int j = 0; j < 9; j++)
                                {
                                    if (Global.coe.reservoirs[ii].reservoirSegs[iii].upstreamLakeIDs[j] == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        TotalReservoirNodes.Add(Global.coe.reservoirs[ii].reservoirSegs[iii].upstreamLakeIDs[j]);
                                    }
                                }
                                reservoirCounter++;
                                break;
                            }
                        }
                    }
                }
            }

            //assign boolean value
            if (node.Type == "RIVER")
            {
                if (TotalRiverNodes.Contains(USnode.ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (node.Type == "RESERVOIR SEGMENT")
            {
                if (TotalReservoirNodes.Contains(USnode.ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //figure out what to run

        }
    }
}
