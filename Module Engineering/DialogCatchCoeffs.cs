﻿using System;
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
    public partial class FormCatch : Form
    {
        FormMain parent;
        public FormCatch(FormMain par )
        {
            InitializeComponent();
            this.parent = par;
        }
      
        private void FormCatch_Load(object sender, EventArgs e)
        {

        }

        public void Populate(int cnum)
        {
            Catchment catchment = Global.coe.catchments[cnum];
            Text = "Catchment " + Global.coe.catchments[cnum].idNum + " Coefficients";

            //Stuff used repeatedly
            //List of land uses
            List<string> landuselist = new List<string>();
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
               landuselist.Add (Global.coe.landuse[ii].name.ToString());
            }

            //list of chemical and physical constituents
            List<string> ConstitsList = new List<string>();
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                ConstitsList.Add(Global.coe.chemConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                ConstitsList.Add(Global.coe.physicalConstits[ii].fullName.ToString());
            }

            //units for each chemical and physical parameter
            List<string> UnitsList = new List<string>();
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                UnitsList.Add(Global.coe.chemConstits[ii].units.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                UnitsList.Add(Global.coe.physicalConstits[ii].units.ToString());
            }

            //Physical Data tab
            tbName.Text = catchment.name.ToString();
            tbCatchID.Text = catchment.idNum.ToString();
            tbArea.Text = catchment.soils[1].area.ToString();
            tbWidth.Text = catchment.width.ToString();
            tbAspect.Text = catchment.aspect.ToString();
            tbSlope.Text = catchment.slope.ToString();
            tbDetention.Text = catchment.detentionStorage.ToString();
            tbRoughness.Text = catchment.ManningN.ToString();

            //Meteorology tab
            tbMetFile.Text = Global.coe.METFilename[catchment.METFileNum];
            tbPrecipWeight.Text = catchment.precipMultiplier.ToString();
            tbTempLapse.Text = catchment.aveTempLapse.ToString();
            tbAltLapse.Text = catchment.altitudeTempLapse.ToString();
            tbAirFile.Text = Global.coe.AIRFilename[catchment.airRainChemFileNum - 1];
            //tbPartAir.Text = Global.coe.//(catchment.particleRainChemFileNum);

            //Land Uses tab
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                string Percent = catchment.landUsePercent[ii].ToString("F2");
                string luName = Global.coe.landuse[ii].name;
                dgLanduse.Rows.Insert(ii, luName, Percent);
            }

            //Land Application tab
            cbLanduse.Items.Clear();
            cbLanduse.Items.AddRange(landuselist.ToArray());
            cbLanduse.SelectedIndex = 7;

            int iFertPlanNum = catchment.fertPlanNum[cbLanduse.SelectedIndex];
            int iNumParams = Global.coe.numChemicalParams + Global.coe.numPhysicalParams;
            if (dgLandApp.Rows.Count == 0)
            {
                //populate datagridview
                for (int iParam = 0; iParam < iNumParams; iParam++)
                {
                    string NameUnit = ConstitsList[iParam].ToString() + " (" + UnitsList[iParam].ToString().Trim() + ")";
                    dgLandApp.Rows.Insert(iParam);
                    dgLandApp.Rows[iParam].HeaderCell.Value = NameUnit.ToString();
                    for (int iMonth = 0; iMonth < 12; iMonth++)
                    {
                        dgLandApp.Rows[iParam].Cells[iMonth].Value = Global.coe.landuse[cbLanduse.SelectedIndex].fertPlanApplication[iFertPlanNum][iMonth][iParam];
                    }
                }
                //hide chemical and physical parameters that aren't applicable
                List<int> HideList = new List<int> { 0, 1, 2, 15, 16, 20, 22, 23, 24, 29, 30, 31, 32, 37 };
                for (int ii = 0; ii < iNumParams; ii++)
                {
                    if (HideList.Contains(ii))
                    {
                        dgLandApp.Rows[ii].Visible = false;
                    }
                }
                //Format datagridview
                dgLandApp.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dgLandApp.AllowUserToAddRows = false;
                dgLandApp.AllowUserToDeleteRows = false;
                dgLandApp.AllowUserToOrderColumns = false;
                dgLandApp.ReadOnly = false;

                tbMaxAccTime.Text = catchment.bmp.maxFertAccumTime.ToString();
            }

            //Irrigation tab - Tabled for now - there is no irrigation in the Catawba watershed...
            //cbIrrLandUse.Items.Clear();
            //cbIrrLandUse.Items.AddRange(landuselist.ToArray());
            //cbIrrLandUse.SelectedIndex = 7;

            //catchment.numIrrigationSources[cbIrrLandUse.SelectedIndex].ToString();
            //catchment.irrigationSource.ToString();
            //catchment.irrigationSourcePercent.ToString();

            //Sediment tab
            tbSoilErosivity.Text = catchment.sediment.erosivity.ToString();
            tbClay.Text = catchment.sediment.firstPartSizePct.ToString();
            tbSilt.Text = catchment.sediment.secondPartSizePct.ToString();
            tbSand.Text = catchment.sediment.thirdPartSizePct.ToString();

            //BMP's tab
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                string luName = Global.coe.landuse[ii].name;
                string Percent = catchment.landApplicationLoad[ii].ToString("F1");
                dgLivestockEx.Rows.Insert(ii, luName, Percent);
            }
            dgLivestockEx.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgLivestockEx.AllowUserToAddRows = false;
            dgLivestockEx.AllowUserToDeleteRows = false;
            dgLivestockEx.AllowUserToOrderColumns = false;
            dgLivestockEx.ReadOnly = false;

            tbPctBuffered.Text = catchment.bufferingPct.ToString();
            tbBufferWidth.Text = catchment.bufferZoneWidth.ToString();
            tbBufferSlope.Text = catchment.bufferZoneSlope.ToString();
            tbBufferRoughness.Text = catchment.bufferManningN.ToString();

            tbFrequency.Text = catchment.bmp.streetSweepFreq.ToString();
            tbEfficiency.Text = catchment.bmp.streetSweepEff.ToString();

            tbImpRouting.Text = catchment.bmp.divertedImpervFlow.ToString();
            tbDetVolume.Text = catchment.bmp.detentionPondVol.ToString();

            //Point Sources tab

            //Pumping tab

            //Septic Systems tab
            tbDischargeSoilLayer.Text = catchment.septic.soilLayer.ToString();
            tbPopSeptic.Text = catchment.septic.population.ToString();
            tbTreatment1.Text = catchment.septic.failingPct.ToString();
            tbTreatment2.Text = catchment.septic.standardPct.ToString();
            tbTreatment3.Text = catchment.septic.advancedPct.ToString();
            tbInitBiomass.Text = catchment.septic.initialBiomass.ToString();
            tbBioThick.Text = catchment.septic.thickness.ToString();
            tbBiozoneArea.Text = catchment.septic.area.ToString();
            tbBioRespCoeff.Text = catchment.septic.biomassRespRate.ToString();
            tbBioMortCoeff.Text = catchment.septic.biomassMortRate.ToString();

            //Reactions tab

            //Soil Layers tab

            //Mining tab

            //CE-QUAL-W2 tab

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
