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
    public partial class DialogReservoirCoeffs : Form
    {
        FormMain parent;
        public DialogReservoirCoeffs(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(int iRes, int iSeg)
        {
            ReservoirSeg reservoirSeg = Global.coe.reservoirs[iRes].reservoirSegs[iSeg];
            Reservoir reservoir = Global.coe.reservoirs[iRes];

            Text = "Reservoir Segment " + Global.coe.reservoirs[iRes].reservoirSegs[iSeg].idNum + " Coefficients";

            //Stage-Flow

            //Reactions

            //Phytoplankton

            //Heat/Light

            //Diffusion

            //Sediment

            //Initial Concentrations

            //Point Sources

            //Adsorption

            //Observed Data

            //Stage-Area
            dgvStageArea.Columns.Add("Stage", "Stage (m)");
            dgvStageArea.Columns["stage"].ValueType = ;
            dgvStageArea.Columns["stage"].SortMode = DataGridViewColumnSortMode.Automatic;
            dgvStageArea.Columns.Add("Area", "Area (m2)");
            for (int ii = 0; ii < 9; ii++)
            {
                dgvStageArea.Rows.Insert(ii,
                    reservoir.bathymetry[ii].stage.ToString(),
                    reservoir.bathymetry[ii].area.ToString());
            }
            dgvStageArea.RowHeadersVisible = false;
            FormatDataGridView(dgvStageArea);

            double dblX, dblY;
            chartStageArea.Series.Clear();
            chartStageArea.Series.Add("Area");
            chartStageArea.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chartStageArea.Series[0].LegendText = "Area (m2)";
            chartStageArea.ChartAreas[0].AxisX.Title = "Stage (m)";
            chartStageArea.ChartAreas[0].AxisY.Title = "Area (m2)";
            chartStageArea.ChartAreas[0].AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            for (int i = 0; i < dgvStageArea.Rows.Count; i++)
            {
                dblX = Convert.ToDouble(dgvStageArea.Rows[i].Cells["Stage"].Value);
                dblY = Convert.ToDouble(dgvStageArea.Rows[i].Cells["Area"].Value);
                chartStageArea.Series["Area"].Points.AddXY(dblX, dblY);           
            }
            
            //Physical Data
            tbName.Text = reservoirSeg.name.ToString();
            tbResSegID.Text = reservoirSeg.idNum.ToString();
            tbInitElev.Text = reservoir.elevation.ToString();
            double Min = Convert.ToDouble(dgvStageArea.Rows[0].Cells["Stage"].Value);
            double Max = Convert.ToDouble(dgvStageArea.Rows[0].Cells["Stage"].Value);
            for (int i = 1; i < dgvStageArea.Rows.Count; i++)
            {
                double Val = Convert.ToDouble(dgvStageArea.Rows[i].Cells["Stage"].Value);
                Min = Math.Min(Val, Min);
                Max = Math.Max(Val, Max);
            }
            tbMinElev.Text = Min.ToString();
            tbMaxElev.Text = Max.ToString();
            //Inflow/Outflow

            //Meteorology
        }

        public void FormatDataGridView(DataGridView dgv)
        {
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            //for (int ii = 0; ii < dgv.Columns.Count; ii++)
            //{
            //    dgv.Columns[ii].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            dgv.ReadOnly = false;
            dgv.Visible = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdateChart_Click(object sender, EventArgs e)
        {
            double dblX, dblY;
            chartStageArea.Series[0].Points.Clear();
            for (int i = 0; i < dgvStageArea.Rows.Count; i++)
            {
                dblX = Convert.ToDouble(dgvStageArea.Rows[i].Cells["Stage"].Value);
                dblY = Convert.ToDouble(dgvStageArea.Rows[i].Cells["Area"].Value);
                chartStageArea.Series["Area"].Points.AddXY(dblX, dblY);
            }

        }
    }
}
