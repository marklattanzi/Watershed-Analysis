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


            //Point Sources


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
        
    }
}
