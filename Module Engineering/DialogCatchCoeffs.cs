using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace warmf
{
    public partial class DialogCatchCoeffs : Form
    {
        FormMain parent;
        public DialogCatchCoeffs(FormMain par )
        {
            InitializeComponent();
            this.parent = par;
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
            int iNumParams = Global.coe.numChemicalParams + Global.coe.numPhysicalParams;
            
            for (int iParam = 0; iParam < iNumParams; iParam++) //add blank rows to datagridview (row headers labeled)
            {
                string Units = ConstitUnits[iParam].Trim();
                if (Units.Contains("mg/l"))
                {
                    Units = " (" + Units.Replace("mg/l", "kg/ha") + ")";
                }
                else if (Units.Contains("#/100 ml"))
                {
                    Units = " (" + Units.Replace("#/100 ml", "10^6 #/ha") + ")";
                }
                string NameUnit = ConstitNames[iParam] + Units;
                dgLandApp.Rows.Insert(iParam);
                dgLandApp.Rows[iParam].HeaderCell.Value = NameUnit.ToString();
                
            }
            
            List<int> HideList = new List<int> { 0, 1, 2, 15, 16, 20, 22, 23, 24, 29, 30, 31, 32, 37 };
            for (int ii = 0; ii < iNumParams; ii++) //hide chemical and physical parameters that aren't applicable
            {
                if (HideList.Contains(ii))
                {
                    dgLandApp.Rows[ii].Visible = false;
                }
            }
            cbLanduse.SelectedIndex = 7;
            FormatDataGridView(dgLandApp); //Format datagridview

            tbMaxAccTime.Text = catchment.bmp.maxFertAccumTime.ToString();
            
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

            FormatDataGridView(dgLivestockEx);
            
            tbPctBuffered.Text = catchment.bufferingPct.ToString();
            tbBufferWidth.Text = catchment.bufferZoneWidth.ToString();
            tbBufferSlope.Text = catchment.bufferZoneSlope.ToString();
            tbBufferRoughness.Text = catchment.bufferManningN.ToString();

            tbFrequency.Text = catchment.bmp.streetSweepFreq.ToString();
            tbEfficiency.Text = catchment.bmp.streetSweepEff.ToString();

            tbImpRouting.Text = catchment.bmp.divertedImpervFlow.ToString();
            tbDetVolume.Text = catchment.bmp.detentionPondVol.ToString();

            //Point Sources tab
            //Warning: this code block was constructed without having a case to test - probably contains errors!!
            if (catchment.numPointSources > 0)
            {
                for (int ii = 0; ii < catchment.numPointSources; ii++)
                    lbPointSources.Items.Add(Global.coe.PTSFilename[catchment.pointSources[ii]]);
                lbPointSources.SelectedIndex = 0;
                PointSourceInfo(lbPointSources.SelectedIndex);
            }
            


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
            tbNumSoilLayers.Text = catchment.numSoilLayers.ToString();
            cbSoilCoeffGroup.SelectedItem = "Hydrology";
            //Soil Layers > Hydrology coefficients (displayed on load)
            for (int ii = 0; ii < catchment.numSoilLayers; ii++)
            {
                string area = catchment.soils[ii].area.ToString();
                string thickness = catchment.soils[ii].thickness.ToString("F0");
                string moisture = catchment.soils[ii].moisture.ToString("F2");
                string fieldCap = catchment.soils[ii].fieldCapacity.ToString("F2");
                string satMoist = catchment.soils[ii].saturationMoisture.ToString("F2");
                string horizCond = catchment.soils[ii].horizHydraulicConduct.ToString("F0");
                string vertCond = catchment.soils[ii].vertHydraulicConduct.ToString("F0");
                string rootDist = catchment.soils[ii].evapTranspireFract.ToString("F2");
                string density = catchment.soils[ii].density.ToString("F1");
                string tortuosity = catchment.soils[ii].tortuosity.ToString("F0");
                dgSoilHydroCoeffs.Rows.Insert(ii, area, thickness, moisture, fieldCap, satMoist, horizCond, vertCond, rootDist, density, tortuosity);
                dgSoilHydroCoeffs.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
            }
            FormatDataGridView(dgSoilHydroCoeffs);
            dgSoilHydroCoeffs.Visible = true;
            //Soil Layers > Initial Concentrations
            dgInitialConc.Columns.Add("Temp", "Temperature (degrees C)");
            for (int ii = 0; ii < iNumParams; ii++)
            {
                dgInitialConc.Columns.Add(ConstitShortNames[ii], ConstitShortNames[ii].ToString() + " (" + ConstitUnits[ii].ToString().TrimEnd() + ")");
            }
            for (int ii = 0; ii < catchment.numSoilLayers; ii++)
            {
                dgInitialConc.Rows.Insert(ii);
                dgInitialConc.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                dgInitialConc.Rows[ii].Cells[0].Value = catchment.soils[ii].waterTemp.ToString();
                for (int iParam = 0; iParam < iNumParams; iParam++)
                {
                    dgInitialConc.Rows[ii].Cells[iParam + 1].Value = catchment.soils[ii].solutionConcen[iParam].ToString();
                }
            }
            FormatDataGridView(dgInitialConc);
            dgInitialConc.Visible = false;

            //Soil Layers > Adsorption
            //***************Adsorption is currently working differently than in WARMF v7****************
            //***************We need to make some decisions here, and decide how to proceed**************
            dgAdsorption.Columns.Add("CEC", "CEC (meq/100 g)");
            dgAdsorption.Columns.Add("MaxP", "Max PO4 (mg/kg)");
            for (int ii = 0; ii < iNumParams; ii++)
            {
                dgAdsorption.Columns.Add(ConstitShortNames[ii].ToString(), ConstitShortNames[ii].ToString().TrimEnd() + " (L/kg)");
            }
            for (int ii = 0; ii < catchment.numSoilLayers; ii++)
            {
                dgAdsorption.Rows.Insert(ii);
                dgAdsorption.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                dgAdsorption.Rows[ii].Cells[0].Value = catchment.soils[ii].exchangeCapacity.ToString("F0");
                dgAdsorption.Rows[ii].Cells[1].Value = catchment.soils[ii].maxPhosAdsorption.ToString("F0");
                for (int iParam = 0; iParam < iNumParams; iParam++)
                {
                    dgAdsorption.Rows[ii].Cells[iParam + 2].Value = catchment.soils[ii].adsorptionIsotherm[iParam].ToString("F0");
                }
            }
            List<int> HideCols = new List<int> { 0, 1, 13, 15, 16, 19, 20, 21, 22, 23, 24, 29, 30, 31, 32, 33, 34, 35, 36, 37 };
            for (int ii = 0; ii < iNumParams; ii++)
            {
                if (HideCols.Contains(ii))
                {
                    dgAdsorption.Columns[ii + 2].Visible = false;
                }
            }
            FormatDataGridView(dgAdsorption);
            dgAdsorption.Visible = false;

            //Soil Layers > Mineral Composition
            for (int ii = 0; ii < Global.coe.numMinerals; ii++)
            {
                dgMineralComp.Columns.Add(Global.coe.minerals[ii].name.ToString(), Global.coe.minerals[ii].name.ToString());
            }
            for (int ii = 0; ii < catchment.numSoilLayers; ii++)
            {
                dgMineralComp.Rows.Insert(ii);
                dgMineralComp.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                for (int iMineral = 0; iMineral < Global.coe.numMinerals; iMineral++)
                {
                    dgMineralComp.Rows[ii].Cells[iMineral].Value = catchment.soils[ii].weightFract[iMineral].ToString();
                }
            }
            FormatDataGridView(dgMineralComp);
            dgMineralComp.Visible = false;

            //Soil Layers > Inorganic Carbon
            for (int ii = 0; ii < catchment.numSoilLayers; ii++)
            {
                dgInorganicC.Rows.Insert(ii);
                dgInorganicC.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                dgInorganicC.Rows[ii].Cells[0].Value = (dgInorganicC.Rows[ii].Cells[0] as DataGridViewComboBoxCell).Items[catchment.soils[ii].CO2CalcMethod - 1];
                dgInorganicC.Rows[ii].Cells[1].Value = catchment.soils[ii].CO2ConcenFactor.ToString("F0");
            }
            FormatDataGridView(dgInorganicC);
            dgInorganicC.Visible = false;

            //Mining tab
            //Dialog is set up in the designer, but no code for functionality yet
            tpMining.Hide();

            //CE-QUAL-W2 tab
            if (catchment.mining.numCEQW2Files == 3)
            {
                cbWriteCEQUALoutput.Checked = true;
                tbCEQUALflowFile.Text = catchment.mining.flowInputFilename;
                tbCEQUALtempFile.Text = catchment.mining.tempInputFilename;
                tbCEQUALconcFile.Text = catchment.mining.waterQualInputFilename;
            }
        }

        public void PointSourceInfo(int FileIndex)
        {
            PTSFile pFile = Global.coe.PTSFilenamepointSourceFiles[FileIndex];
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
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Launch browser to http://warmf.com/...
            System.Diagnostics.Process.Start("http://warmf.com/home/index.php/engineering-module/catchments/");
        }

        private void cbSoilCoeffGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = cbSoilCoeffGroup.SelectedIndex;
            if (Index == 0) //Hydrology
            {
                dgSoilHydroCoeffs.Visible = true;
                dgSoilHydroCoeffs.BringToFront();
            }
            else if (Index == 1) //Initial Concentrations
            {
                dgInitialConc.Visible = true;
                dgInitialConc.BringToFront();
            }
            else if (Index == 2) //Adsorption
            {
                dgAdsorption.Visible = true;
                dgAdsorption.BringToFront();
            }
            else if (Index == 3) //Mineral Composition
            {
                dgMineralComp.Visible = true;
                dgMineralComp.BringToFront();
            }
            else //Inorganic Carbon
            {
                dgInorganicC.Visible = true;
                dgInorganicC.BringToFront();
            }
        }

        private void btnSelectMet_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.MET);
            CatchmentOpenFileDialog.Title = "Select Meteorology File";
            CatchmentOpenFileDialog.Filter = "Meteorology Files | *.MET";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbMetFile.Text = CatchmentOpenFileDialog.FileName;
            }
        }

        private void btnSelectAir_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.AIR);
            CatchmentOpenFileDialog.Title = "Select Air and Rain Chemistry File";
            CatchmentOpenFileDialog.Filter = "Air and Rain Chemistry Files | *.AIR";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAirFile.Text = CatchmentOpenFileDialog.FileName;
            }
        }

        private void btnSelectPartAir_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.CPA);
            CatchmentOpenFileDialog.Title = "Select Coarse Particle Air Chemistry File";
            CatchmentOpenFileDialog.Filter = "Coarse Particle Air Chemistry Files | *.CPA";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPartAir.Text = CatchmentOpenFileDialog.FileName;
            }
        }

        private void cbWriteCEQUALoutput_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWriteCEQUALoutput.Checked == true)
            {
                gbCEQUALW2chem.Enabled = true;
                gbCEQUALW2flow.Enabled = true;
                gbCEQUALW2temp.Enabled = true;
            }
            else
            {
                gbCEQUALW2chem.Enabled = false;
                gbCEQUALW2flow.Enabled = false;
                gbCEQUALW2temp.Enabled = false;
            }
        }

        private void btnSelectFlowFile_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            CatchmentOpenFileDialog.Title = "Select CE-QUAL-W2 Flow File";
            CatchmentOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALflowFile.Text = CatchmentOpenFileDialog.SafeFileName;
            }
        }

        private void btnSelectTempFile_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            CatchmentOpenFileDialog.Title = "Select CE-QUAL-W2 Temp File";
            CatchmentOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALtempFile.Text = CatchmentOpenFileDialog.SafeFileName;
            }
        }

        private void btnSelectChemFile_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            CatchmentOpenFileDialog.Title = "Select CE-QUAL-W2 Concentration File";
            CatchmentOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALconcFile.Text = CatchmentOpenFileDialog.SafeFileName;
            }
        }

        private void cbLanduse_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopLandAppGrid();
        }

        private void PopLandAppGrid()
        {
            int Index = cbLanduse.SelectedIndex;
            int catchID = Convert.ToInt32(tbCatchID.Text);
            int catchNum = Global.coe.GetCatchmentNumberFromID(catchID);
            int iFertPlanNum = Global.coe.catchments[catchNum].fertPlanNum[cbLanduse.SelectedIndex];
            int iNumParams = Global.coe.numChemicalParams + Global.coe.numPhysicalParams;

            //make sure we have a catchment number
            if (catchNum < 0)
            {
                return;
            }

            for (int iParam = 0; iParam < iNumParams; iParam++)
            {
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgLandApp.Rows[iParam].Cells[iMonth].Value = Global.coe.landuse[cbLanduse.SelectedIndex].fertPlanApplication[iFertPlanNum][iMonth][iParam];
                }
            }
        }

        private void btnAddPTS_Click(object sender, EventArgs e)
        {

        }

        private void btnRemovePTS_Click(object sender, EventArgs e)
        {

        }
    }
}
