using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace warmf
{
    public partial class DialogRiverCoeffs : Form
    {
        FormMain parent;
        private List<PTSFile> pointSourceFiles;

        public DialogRiverCoeffs(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(int cnum)
        {
            //list of chemical and physical constituent names
            List<string> ConstitNames = new List<string>();
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                ConstitNames.Add(Global.coe.chemConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                ConstitNames.Add(Global.coe.physicalConstits[ii].fullName.ToString());
            }

            List<string> ConstitShortNames = new List<string>();
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                ConstitShortNames.Add(Global.coe.chemConstits[ii].abbrevName.ToString().Trim());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                ConstitShortNames.Add(Global.coe.physicalConstits[ii].abbrevName.ToString().Trim());
            }

            //units for each chemical and physical parameter
            List<string> ConstitUnits = new List<string>();
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                ConstitUnits.Add(Global.coe.chemConstits[ii].units.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                ConstitUnits.Add(Global.coe.physicalConstits[ii].units.ToString());
            }

            River river = Global.coe.rivers[cnum];
            Text = "River " + Global.coe.rivers[cnum].idNum + " Coefficients";

            //Physical Data Tab
            tbName.Text = river.name;
            tbStreamID.Text = river.idNum.ToString();
            tbUpElevation.Text = river.upElevation.ToString();
            tbDownElevation.Text = river.downElevation.ToString();
            tbLength.Text = river.length.ToString();
            tbDepth.Text = river.depth.ToString();
            tbImpArea.Text = river.impoundArea.ToString();
            tbImpVolume.Text = river.impoundVol.ToString();
            tbManningsN.Text = river.ManningN.ToString();

            //Stage-Width
            ChartArea chartArea1 = chartStageWidth.ChartAreas["ChartArea1"];
            Series series1 = chartStageWidth.Series["SeriesStageWidth"];
            series1.ChartType = SeriesChartType.Line;
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisX.Title = "Width (m)";
            chartArea1.AxisX.Minimum = 0;
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisY.Title = "Stage (m)";
            chartArea1.AxisY.Minimum = 0;
            for (int i = 0; i < 9; i++)
            {
                dgvStageWidth.Rows.Insert(i, river.segment[i].stage, river.segment[i].width);
                chartStageWidth.Series["SeriesStageWidth"].Points.AddXY(river.segment[i].width, river.segment[i].stage);
            }

            //Diversions
            if (river.numDiversionsFrom > 0)
            {
                for (int i = 0; i < river.numDiversionsFrom; i++)
                    lbDiversionsFrom.Items.Add(Global.coe.DIVData[river.divFilenumFrom[i]-1].filename);
            }
            
            if (river.numDiversionsTo > 0)
            {
                for (int i = 0; i < river.numDiversionsTo; i++)
                    lbDiversionsTo.Items.Add(Global.coe.DIVData[river.divFilenumTo[i]-1].filename);
            }
            
            tbMinRiverFlow.Text = river.minFlow.ToString();

            //Point Sources
            if (river.numPointSrcs > 0)
            {
                pointSourceFiles = new List<PTSFile>();
                for (int i = 0; i < river.numPointSrcs; i++)
                {
                    lbPointSources.Items.Add(Global.coe.PTSFilename[river.pointSrcFilenum[i] - 1]);
                    PTSFile ptFile = new PTSFile(Global.coe.PTSFilename[river.pointSrcFilenum[i] - 1]);
                    pointSourceFiles.Add(ptFile);
                }
                lbPointSources.SelectedIndex = 0;
                PointSourceInfo(lbPointSources.SelectedIndex);

            }

        //Reactions


        //Sediment


        //Initial Concentrations


        //Adsorption


        //Observed Data


        //CE-QUAL-W2
    }

        private void btnRedrawChart_Click(object sender, EventArgs e)
        {
            chartStageWidth.Series["SeriesStageWidth"].Points.Clear();
            for (int i = 0; i < dgvStageWidth.Rows.Count; i++)
            {
                double dblStage = Convert.ToDouble(dgvStageWidth.Rows[i].Cells[0].Value); //Stage
                double dblWidth = Convert.ToDouble(dgvStageWidth.Rows[i].Cells[1].Value); //Width
                chartStageWidth.Series["SeriesStageWidth"].Points.AddXY(dblWidth, dblStage);
            }
            chartStageWidth.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartStageWidth.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        }

        private void lbDiversionsFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDiversionsFrom.SelectedItem != null)
                btnFromRemove.Enabled = true;
        }

        private void lbDiversionsTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDiversionsTo.SelectedItem != null)
                btnToRemove.Enabled = true;
        }

        private void btnFromAdd_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.FLO);
            RiverOpenFileDialog.Title = "Select Managed Flow File";
            RiverOpenFileDialog.Filter = "Managed Flow Files | *.FLO";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbDiversionsFrom.Items.Add(RiverOpenFileDialog.SafeFileName);
            }
        }

        private void btnFromRemove_Click(object sender, EventArgs e)
        {
            lbDiversionsFrom.Items.Remove(lbDiversionsFrom.SelectedItem);
        }

        private void btnToAdd_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.FLO);
            RiverOpenFileDialog.Title = "Select Managed Flow File";
            RiverOpenFileDialog.Filter = "Managed Flow Files | *.FLO";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbDiversionsTo.Items.Add(RiverOpenFileDialog.SafeFileName);               
            }
        }

        private void btnToRemove_Click(object sender, EventArgs e)
        {
            lbDiversionsTo.Items.Remove(lbDiversionsTo.SelectedItem);
        }

        private void btnAddPTS_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.PTS);
            RiverOpenFileDialog.Title = "Select Point Source File";
            RiverOpenFileDialog.Filter = "Point Source Files | *.PTS";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPointSources.Items.Add(RiverOpenFileDialog.SafeFileName);
            }
        }

        private void btnRemovePTS_Click(object sender, EventArgs e)
        {
            lbPointSources.Items.Remove(lbPointSources.SelectedItem);
        }

        private void lbPointSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPointSources.SelectedItem != null)
            {
                PointSourceInfo(lbPointSources.SelectedIndex);
                btnRemovePTS.Enabled = true;
            }  
        }

        public void PointSourceInfo(int FileIndex)
        {
            PTSFile pFile = pointSourceFiles[FileIndex];
            pFile.ReadHeader();
            if (pFile.swInternal == true)
            {
                rbSourceInternal.Checked = true;
                rbSourceExternal.Checked = false;
                rbUnspecZero.Checked = false;
                rbUnspecZero.Enabled = false;
                rbUnspecAmbient.Checked = true;
            }
            else
            {
                rbSourceInternal.Checked = false;
                rbSourceExternal.Checked = true;
                rbUnspecZero.Checked = true;
                rbUnspecAmbient.Checked = false;
                rbUnspecAmbient.Enabled = false;
            }
            tbNPDESNumber.Text = pFile.npdesPermit;
        }



    }
}
