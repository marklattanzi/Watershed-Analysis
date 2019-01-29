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
    public partial class DialogSystemCoeffs : Form
    {
        FormMain parent;
        public DialogSystemCoeffs(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate()
        {
            //Stuff used repeatedly
            //List of land uses
            List<string> landuselist = new List<string>();
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                landuselist.Add(Global.coe.landuse[ii].name.ToString());
            }

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

            //Physical Data
            tbLatitude.Text = Global.coe.latitude.ToString();
            tbLongitude.Text = Global.coe.longitude.ToString();
            tbElevation.Text = Global.coe.watershedElevation.ToString();
            tbArea.Text = Global.coe.watershedArea.ToString();

            //Land Uses
            DataGridViewColumn colValue = new DataGridViewTextBoxColumn();
            //Fraction Open in Winter
            dgvOpenInWinter.Columns.Insert(0,colValue);
            dgvOpenInWinter.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvOpenInWinter.Rows.Insert(ii);
                dgvOpenInWinter.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvOpenInWinter.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].openWinterFrac.ToString();
            }
            FormatDataGridView(dgvOpenInWinter);
            //Rainfall Detachment Factor
            dgvRainfallDetachment.Columns.Insert(0, colValue);
            dgvRainfallDetachment.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvOpenInWinter.Rows.Insert(ii);
                dgvOpenInWinter.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvOpenInWinter.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].openWinterFrac.ToString();
            }
            FormatDataGridView(dgvRainfallDetachment);
            //Flow Detachment Factor
            dgvFlowDetachment.Columns.Insert(0, colValue);
            dgvFlowDetachment.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvFlowDetachment.Rows.Insert(ii);
                dgvFlowDetachment.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvFlowDetachment.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].flowDetachFactor.ToString();
            }
            FormatDataGridView(dgvFlowDetachment);
            //Fraction Impervious
            dgvFractionImpervious.Columns.Insert(0, colValue);
            dgvFractionImpervious.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvFractionImpervious.Rows.Insert(ii);
                dgvFractionImpervious.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvFractionImpervious.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].imperviousFrac.ToString();
            }
            FormatDataGridView(dgvFractionImpervious);
            //Interception Storage
            dgvInterceptionStorage.Columns.Insert(0, colValue);
            dgvInterceptionStorage.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvInterceptionStorage.Rows.Insert(ii);
                dgvInterceptionStorage.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvInterceptionStorage.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].maxPotentInceptStorage.ToString();
            }
            FormatDataGridView(dgvInterceptionStorage);
            //Long-Term Growth
            dgvAnnGrowthMult.Columns.Insert(0, colValue);
            dgvAnnGrowthMult.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvAnnGrowthMult.Rows.Insert(ii);
                dgvAnnGrowthMult.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvAnnGrowthMult.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].annualGrowMult.ToString();
            }
            FormatDataGridView(dgvAnnGrowthMult);
            //Leaf Growth Factor
            dgvLeafGrowth.Columns.Insert(0, colValue);
            dgvLeafGrowth.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLeafGrowth.Rows.Insert(ii);
                dgvLeafGrowth.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvLeafGrowth.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].leafGrowFactor.ToString();
            }
            FormatDataGridView(dgvLeafGrowth);
            //Productivity
            dgvProductivity.Columns.Insert(0, colValue);
            dgvProductivity.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvProductivity.Rows.Insert(ii);
                dgvProductivity.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvProductivity.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].productivity.ToString();
            }
            FormatDataGridView(dgvProductivity);
            //Active Respiration
            dgvActiveRespiration.Columns.Insert(0, colValue);
            dgvActiveRespiration.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvActiveRespiration.Rows.Insert(ii);
                dgvActiveRespiration.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvActiveRespiration.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].activeRespRate.ToString();
            }
            FormatDataGridView(dgvActiveRespiration);
            //Maintenance Respiration
            dgvMaintenanceRespiration.Columns.Insert(0, colValue);
            dgvMaintenanceRespiration.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvMaintenanceRespiration.Rows.Insert(ii);
                dgvMaintenanceRespiration.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvMaintenanceRespiration.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].maintRespRate.ToString();
            }
            FormatDataGridView(dgvMaintenanceRespiration);
            //Dry Collection Efficiency
            dgvDryColEfficiency.Columns.Insert(0, colValue);
            dgvDryColEfficiency.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvDryColEfficiency.Rows.Insert(ii);
                dgvDryColEfficiency.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvDryColEfficiency.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].dryCollEff.ToString();
            }
            FormatDataGridView(dgvDryColEfficiency);
            //Wet Collection Efficiency
            dgvWetColEfficiency.Columns.Insert(0, colValue);
            dgvWetColEfficiency.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvWetColEfficiency.Rows.Insert(ii);
                dgvWetColEfficiency.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvWetColEfficiency.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].wetCollEff.ToString();
            }
            FormatDataGridView(dgvWetColEfficiency);
            //Leaf Weight/Area
            dgvLeafWeightArea.Columns.Insert(0, colValue);
            dgvLeafWeightArea.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLeafWeightArea.Rows.Insert(ii);
                dgvLeafWeightArea.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvLeafWeightArea.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].leafWgtArea.ToString();
            }
            FormatDataGridView(dgvLeafWeightArea);
            //Canopy Height
            dgvCanopyHeight.Columns.Insert(0, colValue);
            dgvCanopyHeight.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvCanopyHeight.Rows.Insert(ii);
                dgvCanopyHeight.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvCanopyHeight.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].canopyHeight.ToString();
            }
            FormatDataGridView(dgvCanopyHeight);
            //Stomatal Resistance
            dgvStomatalResistance.Columns.Insert(0, colValue);
            dgvStomatalResistance.Columns[0].HeaderText = "Value";
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvStomatalResistance.Rows.Insert(ii);
                dgvStomatalResistance.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvStomatalResistance.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].stomatalResist.ToString();
            }
            FormatDataGridView(dgvStomatalResistance);
            //Cropping Factor
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvCropFactor.Rows.Insert(ii);
                dgvCropFactor.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvCropFactor.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].cropping[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvCropFactor);
            //Leaf Area Index
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLeafAreaIndex.Rows.Insert(ii);
                dgvLeafAreaIndex.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvLeafAreaIndex.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].cropping[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvCropFactor);
            //Snow/Ice

            //Heat/Light

            //Canopy

            //Litter

            //Septic Systems

            //Minerals

            //Sediment

            //Phytoplankton

            //Periphyton

            //Food Web

            //Parameters
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Launch browser to http://warmf.com/...
            System.Diagnostics.Process.Start("http://warmf.com/home/index.php/engineering-module/system/");
        }

        private void cbLandUseParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ii = cbLandUseParameter.SelectedIndex;
            if (ii == 0) //Open in Winter
            {
                dgvOpenInWinter.BringToFront();
            }
            else if (ii == 1) //Rainfall Detachment Factor
            {
                dgvRainfallDetachment.BringToFront();
            }
            else if (ii == 2) //Flow Detachment Factor
            {
                dgvFlowDetachment.BringToFront();
            }
            else if (ii == 3) //Fraction Impervious
            {
                dgvFractionImpervious.BringToFront();
            }
            else if (ii == 4) //Interception Storage
            {
                dgvInterceptionStorage.BringToFront();
            }
            else if (ii == 5) //Annual Growth Multiplier
            {
                dgvAnnGrowthMult.BringToFront();
            }
            else if (ii == 6) //Leaf Growth Factor
            {
                dgvLeafGrowth.BringToFront();
            }
            else if (ii == 7) //Productivity
            {
                dgvProductivity.BringToFront();
            }
            else if (ii == 8) //Active Respiration
            {
                dgvActiveRespiration.BringToFront();
            }
            else if (ii == 9) //Maintenance Respiration
            {
                dgvMaintenanceRespiration.BringToFront();
            }
            else if (ii == 10) //Dry Collection Efficiency
            {
                dgvDryColEfficiency.BringToFront();
            }
            else if (ii == 11) //Wet Collection Efficiency
            {
                dgvWetColEfficiency.BringToFront();
            }
            else if (ii == 12) //Leaf Weight/Area
            {
                dgvLeafWeightArea.BringToFront();
            }
            else if (ii == 13) //Canopy Height
            {
                dgvCanopyHeight.BringToFront();
            }
            else if (ii == 14) //Stomatal Resistance
            {
                dgvStomatalResistance.BringToFront();
            }
            else if (ii == 15) //Cropping Factor
            {
                dgvCropFactor.BringToFront();
            }
            else if (ii == 16) //Leaf Area Index
            {
                dgvLeafAreaIndex.BringToFront();
            }
            else if (ii == 17) //Annual Uptake Distribution
            {
                dgvAnnUptakeDist.BringToFront();
            }
            else if (ii == 18) //Litter Fall Rate
            {
                dgvLitterFallRate.BringToFront();
            }
            else if (ii == 19) //Exudation Rate
            {
                dgvExudationRate.BringToFront();
            }
            else if (ii == 20) //Leaf Composion
            {
                dgvLeafComp.BringToFront();
            }
            else //Trunk Composion
            {
                dgvTrunkComp.BringToFront();
            }
        }

        public void FormatDataGridView(DataGridView dgv)
        {
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            for (int ii = 0; ii < dgv.Columns.Count; ii++)
            {
                dgv.Columns[ii].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgv.ReadOnly = false;
            dgv.Visible = true;
        }
    }
}
