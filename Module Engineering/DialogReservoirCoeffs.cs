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

            //Physical Data
            tbName.Text = reservoirSeg.name.ToString();
            tbResSegID.Text = reservoirSeg.idNum.ToString();
            tbInitElev.Text = reservoir.elevation.ToString();
            //tbMinElev.Text = 
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
            dgvStageArea.Columns.Add("Stage", "Stage");
            dgvStageArea.Columns.Add("Area", "Area");
            for (int ii = 0; ii < 9; ii++)
            {
                dgvStageArea.Rows.Insert(ii,
                    reservoir.bathymetry[ii].stage.ToString(),
                    reservoir.bathymetry[ii].area.ToString());
            }


            //Inflow/Outflow

                //Meteorology
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
    }
}
