using System;
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
    public partial class DialogRiverCoeffs : Form
    {
        FormMain parent;

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
            for (int i = 0; i < 9; i++)
            {
                string Stage = river.segment[i].stage.ToString("F2");
                string Width = river.segment[i].width.ToString("F1");
                dgvStageWidth.Rows.Insert(i, Stage, Width);
            }

            chartStageWidth.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartStageWidth.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartStageWidth.Series[0].XValueMember = dgvStageWidth.Columns[1].DataPropertyName;
            chartStageWidth.Series[0].YValueMembers = dgvStageWidth.Columns[0].DataPropertyName;
            chartStageWidth.DataSource = dgvStageWidth.DataSource;

            chartStageWidth.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            
            //chartStageWidth.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
            //for (int i = 0; i < 9; i++)
            //{
            //    string Stage = river.segment[i].stage.ToString("F2");
            //    string Width = river.segment[i].width.ToString("F1");
            //    dgvStageWidth.Rows.Insert(i, Stage, Width);
            //    chartStageWidth.Series[0].Points.AddXY(Width, Stage);
            //}
            //chartStageWidth.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chartStageWidth.ChartAreas[0].AxisX.Title = "Stage (m)";
            //chartStageWidth.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Center;
            ////chartStageWidth.ChartAreas[0].AxisX.label
            //chartStageWidth.ChartAreas[0].AxisY.Title = "Width (m)";
            //chartStageWidth.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            //chartStageWidth.ChartAreas[0].AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;

            //Diversions


            //Point Sources


            //Reactions


            //Sediment


            //Initial Concentrations


            //Adsorption


            //Observed Data


            //CE-QUAL-W2
        }

        //private void label3_Click(object sender, EventArgs e)
        //{

        //}
    }
}
