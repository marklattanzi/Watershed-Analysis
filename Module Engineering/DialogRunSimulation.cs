using System;
using System.IO;
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
            for (int i = 0; i < Global.coe.numRivers; i++)
            {
                if (Global.coe.rivers[i].swIsSubwaterBoundary)
                {
                    lbSubwatersheds.Items.Add(Global.coe.rivers[i].name);
                }
            }
            for (int i = 0; i < Global.coe.numReservoirs; i++)
            {
                for (int j = 0; j < Global.coe.reservoirs[i].numSegments; j++)
                {
                    if (Global.coe.reservoirs[i].reservoirSegs[j].swIsSubwaterBoundary)
                    {
                        lbSubwatersheds.Items.Add(Global.coe.reservoirs[i].reservoirSegs[j].name);
                    }
                }
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
    }
}
