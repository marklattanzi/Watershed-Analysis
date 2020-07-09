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
        private List<PTSFile> pointSourceFiles;
        
        // array for land application data [landuse, parameter, month]
        public double[,,] LandAppArray = new double[Global.coe.numLanduses, Global.coe.numChemicalParams + Global.coe.numPhysicalParams, 12];
        public bool anyLandAppChanged = false;
        public bool thisLandAppChanged = false;
        public int cbLanduseIndex;
        public List<int> landAppLandUsesChanged = new List<int>();

        public DialogCatchCoeffs(FormMain par)
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
//                string Percent = catchment.landUsePercent[ii].ToString("F6");
                string Percent = catchment.landUsePercent[ii].ToString();
                string luName = Global.coe.landuse[ii].name;
                dgLanduse.Rows.Insert(ii, luName, Percent);
            }
            for (int i = 0; i < dgLanduse.Columns.Count; i++)
                dgLanduse.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            //Land Application tab

            //LandAppArray = [landuse, parameter, month] = [i,j,k]
            for (int i = 0; i < Global.coe.numLanduses; i++) //Landuse
            {
                for (int j = 0; j < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; j++) //parameter
                {
                    for (int k = 0; k < 12; k++) //month
                    {
                        LandAppArray[i, j, k] = Global.coe.landuse[i].fertPlanApplication[catchment.fertPlanNum[i]][k][j];
                    }
                }
            }

            cbLanduse.Items.Clear();
            cbLanduse.Items.AddRange(landuselist.ToArray());
            int iNumParams = Global.coe.numChemicalParams + Global.coe.numPhysicalParams;
            //set up dgLandApp
            dgLandApp.CellValueChanged -= dgLandApp_CellValueChanged;
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
            for (int i = 0; i < dgLandApp.Columns.Count; i++)
                dgLandApp.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            
            List<string> hideParameters = new List<string>() { "MSOX", "MNOX", "MH", "MTIC", "MOAAL", "MALG", "MCO2", "MDO", "MSSED", "MALG1", "MALG2", "MALG3", "MALG4", "MSDET" };
            for (int i = 0; i < hideParameters.Count; i++)
            {
                int parameterIndex = Global.coe.GetParameterNumberFromCode(hideParameters[i]);
                if (parameterIndex >= 0 && parameterIndex + 1 < dgLandApp.Rows.Count)
                    dgLandApp.Rows[parameterIndex + 1].Visible = false;
            }
/*            List<int> HideList = new List<int> { 0, 1, 2, 15, 16, 20, 22, 23, 24, 29, 30, 31, 32, 37 };
            for (int i = 0; i < iNumParams; i++) //hide chemical and physical parameters that aren't applicable
            {
                if (HideList.Contains(i))
                {
                    dgLandApp.Rows[i].Visible = false;
                }
            }*/
            dgLandApp.CellValueChanged += dgLandApp_CellValueChanged;
            cbLanduse.SelectedIndex = 0;
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
//                string Percent = catchment.landApplicationLoad[ii].ToString("F6");
                string Percent = catchment.landApplicationLoad[ii].ToString();
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
            if (catchment.pointSources.Count > 0)
            {
                pointSourceFiles = new List<PTSFile>();
                for (int i = 0; i < catchment.pointSources.Count; i++)
                {
                    lbPointSources.Items.Add(Global.coe.PTSFilename[catchment.pointSources[i] - 1]);
                    PTSFile ptFile = new PTSFile(Global.coe.PTSFilename[catchment.pointSources[i] - 1]);
                    pointSourceFiles.Add(ptFile);
                }
                lbPointSources.SelectedIndex = 0;
                PointSourceInfo(lbPointSources.SelectedIndex);
            }

            //Pumping tab
            //Warning: this code block was constructed without having a case to test - probably contains errors!!
            for (int i = 0; i < catchment.pumpFromDivFile.Count; i++)
            {
                lbPumpingFrom.Items.Add(Global.coe.DIVData[catchment.pumpFromDivFile[i] - 1].filename);
            }
            for (int i = 0; i < catchment.pumpToDivFile.Count; i++)
            {
                lbPumpingTo.Items.Add(Global.coe.DIVData[catchment.pumpToDivFile[i] - 1].filename);
            }

            //Septic Systems tab
            tbDischargeSoilLayer.Text = catchment.septic.soilLayer.ToString();
            tbPopSeptic.Text = catchment.septic.population.ToString();
            tbTreatment1.Text = catchment.septic.failingPct.ToString();
            tbTreatment2.Text = catchment.septic.standardPct.ToString();
            tbTreatment3.Text = catchment.septic.advancedPct.ToString();
            tbInitBiomass.Text = catchment.septic.initialBiomass.ToString();
            tbBioThick.Text = catchment.septic.biomassThickness.ToString();
            tbBiozoneArea.Text = catchment.septic.biomassArea.ToString();
            tbBioRespCoeff.Text = catchment.septic.biomassRespRate.ToString();
            tbBioMortCoeff.Text = catchment.septic.biomassMortRate.ToString();

            //Reactions tab
            for (int i = 0; i < Global.coe.numReactions; i++)
            {
                dgvReactions.Rows.Add();
                dgvReactions.Rows[i].HeaderCell.Value = Global.coe.reactions[i].name + ", " + Global.coe.reactions[i].units;
                dgvReactions.Rows[i].Cells["soil"].Value = catchment.reactions.soilReactionRate[i].ToString();
                dgvReactions.Rows[i].Cells["surface"].Value = catchment.reactions.surfaceReactionRate[i].ToString();
                dgvReactions.Rows[i].Cells["canopy"].Value = catchment.reactions.canopyReactionRate[i].ToString();
                dgvReactions.Rows[i].Cells["biozone"].Value = catchment.reactions.biozoneReactionRate[i].ToString();
            }
            for (int i = 0; i < dgvReactions.Columns.Count; i++)
                dgvReactions.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            //Soil Layers tab
            tbNumSoilLayers.Text = catchment.numSoilLayers.ToString();
            cbSoilCoeffGroup.SelectedItem = "Hydrology";
            popSoilsData(catchment, catchment.numSoilLayers);

            //Mining tab
            //Dialog is set up in the designer, but no code for functionality yet
            //Deleted from collection at runtime.
            tcCatchTabs.TabPages.RemoveByKey("tpMining");

            //CE-QUAL-W2 tab
            if (catchment.mining.numCEQW2Files == 3)
            {
                cbWriteCEQUALoutput.Checked = true;
                tbCEQUALflowFile.Text = catchment.mining.flowInputFilename;
                tbCEQUALtempFile.Text = catchment.mining.tempInputFilename;
                tbCEQUALconcFile.Text = catchment.mining.waterQualInputFilename;
            }

            this.tbNumSoilLayers.TextChanged += new System.EventHandler(this.tbNumSoilLayers_TextChanged);
        }
        
        private void dgLandApp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            anyLandAppChanged = true;
            thisLandAppChanged = true;
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

        public void FormatDataGridView(DataGridView dgv)
        {
            dgLandApp.CellValueChanged -= dgLandApp_CellValueChanged;
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
            dgLandApp.CellValueChanged += dgLandApp_CellValueChanged;
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
            int catchNum = Global.coe.GetCatchmentNumberFromID(Convert.ToInt32(tbCatchID.Text));
            int iFertPlanNum = Global.coe.catchments[catchNum].fertPlanNum[cbLanduse.SelectedIndex];

            if (thisLandAppChanged)
            {
                if (landAppLandUsesChanged.Contains(cbLanduseIndex) == false)
                {
                    landAppLandUsesChanged.Add(cbLanduseIndex);
                }

                for (int iParam = 0; iParam < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; iParam++)
                {
                    for (int iMonth = 0; iMonth < 12; iMonth++)
                    {
                        LandAppArray[cbLanduseIndex, iParam, iMonth] = Convert.ToDouble(dgLandApp.Rows[iParam].Cells[iMonth].Value);
                    }
                }
            }

            //make sure we have a catchment number
            if (catchNum < 0)
            {
                return;
            }

            dgLandApp.CellValueChanged -= dgLandApp_CellValueChanged;
            for (int iParam = 0; iParam < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; iParam++)
            {
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgLandApp.Rows[iParam].Cells[iMonth].Value = LandAppArray[cbLanduse.SelectedIndex, iParam, iMonth];
                }
            }
            dgLandApp.Refresh();
            dgLandApp.CellValueChanged += dgLandApp_CellValueChanged;
            cbLanduseIndex = cbLanduse.SelectedIndex;
            thisLandAppChanged = false;
        }

        private void btnAddPTS_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.PTS);
            CatchmentOpenFileDialog.Title = "Select Point Source File";
            CatchmentOpenFileDialog.Filter = "Point Source | *.PTS";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPointSources.Items.Add(CatchmentOpenFileDialog.SafeFileName);
            }
        }

        private void btnRemovePTS_Click(object sender, EventArgs e)
        {

        }

        private void lbPointSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPointSources.SelectedItem != null)
            {
                PointSourceInfo(lbPointSources.SelectedIndex);
                btnRemovePTS.Enabled = true;
            }
        }

        private void btnFromRemove_Click(object sender, EventArgs e)
        {
            lbPumpingFrom.Items.Remove(lbPumpingFrom.SelectedItem);
        }

        private void btnToRemove_Click(object sender, EventArgs e)
        {
            lbPumpingTo.Items.Remove(lbPumpingTo.SelectedItem);
        }

        private void lbPumpingTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnToRemove.Enabled = true;
        }

        private void lbPumpingFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFromRemove.Enabled = true;
        }

        private void btnFromAdd_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.FLO);
            CatchmentOpenFileDialog.Title = "Select Managed Flow File";
            CatchmentOpenFileDialog.Filter = "Managed Flow | *.FLO";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPumpingFrom.Items.Add(CatchmentOpenFileDialog.SafeFileName);
            }
        }

        private void btnToAdd_Click(object sender, EventArgs e)
        {
            CatchmentOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.FLO);
            CatchmentOpenFileDialog.Title = "Select Managed Flow File";
            CatchmentOpenFileDialog.Filter = "Managed Flow | *.FLO";

            if (CatchmentOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPumpingTo.Items.Add(CatchmentOpenFileDialog.SafeFileName);
            }
        }

        private void chbxApplyToSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxApplyToSelected.Checked == true)
            {
                chbxApplyToAll.Checked = false;
            }
        }

        private void chbxApplyToAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxApplyToAll.Checked == true)
            {
                chbxApplyToSelected.Checked = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //clicking the ok button will save the coefficients contained within the dialog
            //into Global.coe.catchments[i] variables in memory. It does NOT rewrite the .coe file!!

            Catchment catchment = Global.coe.catchments[Global.coe.GetCatchmentNumberFromID(Convert.ToInt16(tbCatchID.Text))];
            List<int> warmfCatchmentNumbers = new List<int>();

            if (chbxApplyToSelected.Checked) //if apply to selected
            {
                warmfCatchmentNumbers.Add(Global.coe.GetRiverNumberFromID(Convert.ToInt32(tbCatchID.Text)));
                int warmfIDfield = parent.GetWarmfIDFieldIndex(1);
                int warmfCatchNumber;

                for (int j = 0; j < parent.frmMap[1].SelectedRecordIndices.Count; j++)
                {
                    string[] recordAttributes = parent.frmMap[1].GetAttributeFieldValues(parent.frmMap[1].SelectedRecordIndices[j]);
                    warmfCatchNumber = Global.coe.GetRiverNumberFromID(Convert.ToInt16(recordAttributes[warmfIDfield]));
                    if (!warmfCatchmentNumbers.Contains(warmfCatchNumber))
                    {
                        warmfCatchmentNumbers.Add(warmfCatchNumber);
                    }
                }
            }
            else if (chbxApplyToAll.Checked) //if apply to all
            {
                warmfCatchmentNumbers = Enumerable.Range(0, Global.coe.numRivers - 1).ToList();
            }
            else //if neither apply to all or selected are checked
            {
                warmfCatchmentNumbers.Add(Global.coe.GetRiverNumberFromID(Convert.ToInt32(tbCatchID.Text)));
            }

            #region Update each of the coefficients that should not be applied to other rivers first
            //Physical Data tab
            catchment.name = tbName.Text;
            catchment.width = Convert.ToDouble(tbWidth.Text);
            catchment.aspect = Convert.ToDouble(tbAspect.Text);
            catchment.slope = Convert.ToDouble(tbSlope.Text);
            catchment.detentionStorage = Convert.ToDouble(tbDetention.Text);

            //Meteorology tab
            
            //if (tbAirFile.Text = "" || tbAirFile.Text = null)
            //{
            //    catchment.airRainChemFileNum = Global.coe.GetAIRNumberFromName(tbAirFile.Text) + 1;
            //}
            //catchment.altitudeTempLapse = Convert.ToDouble(tbAltLapse.Text);
            //if (true)
            //{
            //}
            //tbAirFile.Text = Global.coe.AIRFilename[catchment.airRainChemFileNum - 1];
            ////tbPartAir.Text = Global.coe.//(catchment.particleRainChemFileNum);

            //Land Uses tab
            for (int i = 0; i < Global.coe.numLanduses; i++)
            {
                catchment.landUsePercent[i] = Convert.ToDouble(dgLanduse.Rows[i].Cells[1].Value);
            }

            //Land Application tab
            //detect change in landAppArray? see: dgLandApp_CellValueChanged

            //if change, set up new plan for applicable land uses
            //add plan to system coeffs for each land use.
            //set catchment to using new plan
            if (anyLandAppChanged)
            {
                for (int i = 0; i < Global.coe.numLanduses; i++)
                {
                    if (landAppLandUsesChanged.Contains(i))
                    {
                        List<List<double>> fertPlans = new List<List<double>>();
                        for (int j = 0; j < 12; j++)
                        {
                            List<double> plan = new List<double>();
                            for (int k = 0; k < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; k++)
                            {
                                plan.Add(LandAppArray[i, k, j]);
                            }
                            fertPlans.Add(plan);
                        }
                        Global.coe.landuse[i].fertPlanApplication.Add(fertPlans);
                        catchment.fertPlanNum[i] = Global.coe.landuse[i].fertPlanApplication.Count;
                    }

                }
            }

            //Irrigation tab - Tabled for now - there is no irrigation in the Catawba watershed...
            //cbIrrLandUse.Items.Clear();
            //cbIrrLandUse.Items.AddRange(landuselist.ToArray());
            //cbIrrLandUse.SelectedIndex = 7;

            //catchment.numIrrigationSources[cbIrrLandUse.SelectedIndex].ToString();
            //catchment.irrigationSource.ToString();
            //catchment.irrigationSourcePercent.ToString();

            //Point Sources tab
            catchment.pointSources.Clear();
            for (int i = 0; i < lbPointSources.Items.Count; i++)
            {
                catchment.pointSources[i] = Global.coe.GetPTSNumberFromName(lbPointSources.Items[i].ToString());
            }

            //Pumping tab
            catchment.pumpFromDivFile.Clear();
            catchment.pumpToDivFile.Clear();
            for (int i = 0; i < lbPumpingFrom.Items.Count; i++)
            {
                catchment.pumpFromDivFile.Add(Global.coe.GetFLONumberFromName(lbPumpingFrom.Items[i].ToString()));
            }
            for (int i = 0; i < lbPumpingTo.Items.Count; i++)
            {
                catchment.pumpToDivFile.Add(Global.coe.GetFLONumberFromName(lbPumpingTo.Items[i].ToString()));
            }

            //CE-QUAL-W2 tab
            if (cbWriteCEQUALoutput.Checked)
            {
                catchment.mining.numCEQW2Files = 3;
                catchment.mining.flowInputFilename = tbCEQUALflowFile.Text;
                catchment.mining.tempInputFilename = tbCEQUALtempFile.Text;
                catchment.mining.waterQualInputFilename = tbCEQUALconcFile.Text;
            }
            #endregion

            #region Update the coefficients that can be applied to selected or all
            for (int i = 0; i < warmfCatchmentNumbers.Count; i++)
            {
                //Physical Data tab
                if (catchment.ManningN != Convert.ToDouble(tbRoughness.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].ManningN = Convert.ToDouble(tbRoughness.Text);
                
                //Meteorology tab
                if (catchment.METFileNum != Global.coe.GetMETNumberFromName(tbMetFile.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].METFileNum = Global.coe.GetMETNumberFromName(tbMetFile.Text);
                if (catchment.precipMultiplier != Convert.ToDouble(tbPrecipWeight.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].precipMultiplier = Convert.ToDouble(tbPrecipWeight.Text);
                if (catchment.aveTempLapse != Convert.ToDouble(tbTempLapse.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].aveTempLapse = Convert.ToDouble(tbTempLapse.Text);
                if(catchment.altitudeTempLapse != Convert.ToDouble(tbAltLapse.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].altitudeTempLapse = Convert.ToDouble(tbAltLapse.Text);
                
                //Land Application
                if (catchment.bmp.maxFertAccumTime != Convert.ToDouble(tbMaxAccTime.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bmp.maxFertAccumTime = Convert.ToDouble(tbMaxAccTime.Text);

                //Sediment tab
                if (catchment.sediment.erosivity != Convert.ToDouble(tbSoilErosivity.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].sediment.erosivity = Convert.ToDouble(tbSoilErosivity.Text);
                if (catchment.sediment.firstPartSizePct != Convert.ToDouble(tbClay.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].sediment.firstPartSizePct = Convert.ToDouble(tbClay.Text);
                if (catchment.sediment.secondPartSizePct != Convert.ToDouble(tbSilt.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].sediment.secondPartSizePct = Convert.ToDouble(tbSilt.Text);
                if (catchment.sediment.thirdPartSizePct != Convert.ToDouble(tbSand.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].sediment.thirdPartSizePct = Convert.ToDouble(tbSand.Text);

                //BMP's tab
                for (int j = 0; j < Global.coe.numLanduses; j++)
                {
                    if(catchment.landApplicationLoad[j] != Convert.ToDouble(dgLivestockEx.Rows[j].Cells[1].Value))
                        Global.coe.catchments[warmfCatchmentNumbers[i]].landApplicationLoad[j] = Convert.ToDouble(dgLivestockEx.Rows[j].Cells[1].Value);
                }
                if(catchment.bufferingPct != Convert.ToDouble(tbPctBuffered.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bufferZoneWidth = Convert.ToDouble(tbBufferWidth.Text);
                if (catchment.bufferZoneSlope != Convert.ToDouble(tbBufferSlope.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bufferZoneSlope = Convert.ToDouble(tbBufferSlope.Text);
                if (catchment.bufferManningN != Convert.ToDouble(tbBufferRoughness.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bufferManningN = Convert.ToDouble(tbBufferRoughness.Text);
                if (catchment.bmp.streetSweepFreq != Convert.ToDouble(tbFrequency.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bmp.streetSweepEff = Convert.ToDouble(tbEfficiency.Text);
                if (catchment.bmp.divertedImpervFlow != Convert.ToDouble(tbImpRouting.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bmp.divertedImpervFlow = Convert.ToDouble(tbImpRouting.Text);
                if (catchment.bmp.detentionPondVol != Convert.ToDouble(tbDetVolume.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].bmp.detentionPondVol = Convert.ToDouble(tbDetVolume.Text);

                //Septic Systems tab
                if (catchment.septic.soilLayer != Convert.ToDouble(tbDischargeSoilLayer.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.soilLayer = Convert.ToDouble(tbDischargeSoilLayer.Text);
                if (catchment.septic.population != Convert.ToDouble(tbPopSeptic.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.population = Convert.ToDouble(tbPopSeptic.Text);
                if (catchment.septic.failingPct != Convert.ToDouble(tbTreatment1.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.failingPct = Convert.ToDouble(tbTreatment1.Text);
                if (catchment.septic.standardPct != Convert.ToDouble(tbTreatment2.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.standardPct = Convert.ToDouble(tbTreatment2.Text);
                if (catchment.septic.advancedPct != Convert.ToDouble(tbTreatment3.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.advancedPct = Convert.ToDouble(tbTreatment3.Text);
                if (catchment.septic.initialBiomass != Convert.ToDouble(tbInitBiomass.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.initialBiomass = Convert.ToDouble(tbInitBiomass.Text);
                if (catchment.septic.biomassThickness != Convert.ToDouble(tbBioThick.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.biomassThickness = Convert.ToDouble(tbBioThick.Text);
                if (catchment.septic.biomassArea != Convert.ToDouble(tbBiozoneArea.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.biomassArea = Convert.ToDouble(tbBiozoneArea.Text);
                if (catchment.septic.biomassRespRate != Convert.ToDouble(tbBioRespCoeff.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.biomassRespRate = Convert.ToDouble(tbBioRespCoeff.Text);
                if (catchment.septic.biomassMortRate != Convert.ToDouble(tbBioMortCoeff.Text))
                    Global.coe.catchments[warmfCatchmentNumbers[i]].septic.biomassMortRate = Convert.ToDouble(tbBioMortCoeff.Text);

                //Reactions tab
                for (int j = 0; j < Global.coe.numReactions; j++)
                {
                    if (catchment.reactions.soilReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["soil"].Value))
                        Global.coe.catchments[i].reactions.soilReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["soil"].Value);
                    if (catchment.reactions.surfaceReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["surface"].Value))
                        Global.coe.catchments[i].reactions.surfaceReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["surface"].Value);
                    if (catchment.reactions.canopyReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["canopy"].Value))
                        Global.coe.catchments[i].reactions.canopyReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["canopy"].Value);
                    if (catchment.reactions.biozoneReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["biozone"].Value))
                        Global.coe.catchments[i].reactions.biozoneReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["biozone"].Value);
                }

                //Soil Layers tab
                if (catchment.numSoilLayers != Convert.ToInt16(tbNumSoilLayers.Text))
                {
                    int oldNumSoilLayers = Global.coe.catchments[i].numSoilLayers;
                    int newNumSoilLayers = Convert.ToInt16(tbNumSoilLayers.Text);

                    if (newNumSoilLayers > oldNumSoilLayers)
                    {
                        int layersToAdd = newNumSoilLayers - oldNumSoilLayers;
                        Global.coe.catchments[i].numSoilLayers = newNumSoilLayers;
                        for (int j = 0; j < layersToAdd; j++)
                        {
                            Global.coe.catchments[i].soils.Add(Global.coe.catchments[i].soils[oldNumSoilLayers -1 + j]);
                        }
                    }
                    if (newNumSoilLayers < oldNumSoilLayers)
                    {
                        Global.coe.catchments[i].numSoilLayers = newNumSoilLayers;
                        int layersToRemove = oldNumSoilLayers - newNumSoilLayers;
                        for (int j = 0; j < layersToRemove; j++)
                        {
                            Global.coe.catchments[i].soils.RemoveAt(Global.coe.catchments[i].soils.Count - 1);
                        }
                    }
                }

                //Soil Layers > Hydrology coefficients
                for (int j = 0; j < catchment.numSoilLayers; j++)
                {
                    if (catchment.soils[j].thickness != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[0].Value))
                        Global.coe.catchments[i].soils[j].thickness = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[0].Value);
                    if (catchment.soils[j].moisture != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[1].Value))
                        Global.coe.catchments[i].soils[j].moisture = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[1].Value);
                    if (catchment.soils[j].fieldCapacity != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[2].Value))
                        Global.coe.catchments[i].soils[j].fieldCapacity = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[2].Value);
                    if (catchment.soils[j].saturationMoisture != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[3].Value))
                        Global.coe.catchments[i].soils[j].saturationMoisture = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[3].Value);
                    if (catchment.soils[j].horizHydraulicConduct != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[4].Value))
                        Global.coe.catchments[i].soils[j].horizHydraulicConduct = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[4].Value);
                    if (catchment.soils[j].vertHydraulicConduct != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[5].Value))
                        Global.coe.catchments[i].soils[j].vertHydraulicConduct = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[5].Value);
                    if (catchment.soils[j].evapTranspireFract != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[6].Value))
                        Global.coe.catchments[i].soils[j].evapTranspireFract = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[6].Value);
                    if (catchment.soils[j].density != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[7].Value))
                        Global.coe.catchments[i].soils[j].density = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[7].Value);
                    if (catchment.soils[j].tortuosity != Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[8].Value))
                        Global.coe.catchments[i].soils[j].tortuosity = Convert.ToDouble(dgSoilHydroCoeffs.Rows[j].Cells[8].Value);
                }

                //Soil Layers > Initial Concentrations
                for (int j = 0; j < catchment.numSoilLayers; j++)
                {
                    if (catchment.soils[j].waterTemp != Convert.ToDouble(dgInitialConc.Rows[j].Cells[0].Value))
                        Global.coe.catchments[i].soils[j].waterTemp = Convert.ToDouble(dgInitialConc.Rows[j].Cells[0].Value);
                    
                    for (int k = 0; k < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; k++)
                    {
                        if (catchment.soils[j].solutionConcen[k] != Convert.ToDouble(dgInitialConc.Rows[j].Cells[k + 1].Value))
                            Global.coe.catchments[i].soils[j].solutionConcen[k] = Convert.ToDouble(dgInitialConc.Rows[j].Cells[k + 1].Value);
                    }
                }

                //Soil Layers > Adsorption
                for (int j = 0; j < catchment.numSoilLayers; j++)
                {
                    if (catchment.soils[j].exchangeCapacity != Convert.ToDouble(dgAdsorption.Rows[j].Cells[0].Value))
                        Global.coe.catchments[i].soils[j].exchangeCapacity = Convert.ToDouble(dgAdsorption.Rows[j].Cells[0].Value);
                    if (catchment.soils[j].maxPhosAdsorption != Convert.ToDouble(dgAdsorption.Rows[j].Cells[1].Value))
                        Global.coe.catchments[i].soils[j].maxPhosAdsorption = Convert.ToDouble(dgAdsorption.Rows[j].Cells[1].Value);
                    for (int k = 0; k < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; k++)
                    {
                        if (catchment.soils[j].adsorptionIsotherm[k] != Convert.ToDouble(dgAdsorption.Rows[j].Cells[k + 2].Value))
                            Global.coe.catchments[i].soils[j].adsorptionIsotherm[k] = Convert.ToDouble(dgAdsorption.Rows[j].Cells[k + 2].Value);
                    }
                }

                //Soil Layers > Mineral Composition
                for (int j = 0; j < catchment.numSoilLayers; j++)
                {
                    for (int k = 0; k < Global.coe.numMinerals; k++)
                    {
                        if (catchment.soils[j].weightFract[k] != Convert.ToDouble(dgMineralComp.Rows[j].Cells[k].Value))
                            Global.coe.catchments[i].soils[j].weightFract[k] = Convert.ToDouble(dgMineralComp.Rows[j].Cells[k].Value);
                    }
                }

                //Soil Layers > Inorganic Carbon
                for (int j = 0; j < catchment.numSoilLayers; j++)
                {
                    string co2Selection = (dgInorganicC.Rows[i].Cells[0] as DataGridViewComboBoxCell).Value.ToString();
                    if (catchment.soils[j].CO2CalcMethod != (dgInorganicC.Rows[j].Cells[0] as DataGridViewComboBoxCell).Items.IndexOf(co2Selection) + 1)
                        Global.coe.catchments[i].soils[j].CO2CalcMethod = (dgInorganicC.Rows[j].Cells[0] as DataGridViewComboBoxCell).Items.IndexOf(co2Selection) + 1;
                    if (catchment.soils[j].CO2ConcenFactor != Convert.ToDouble(dgInorganicC.Rows[j].Cells[1].Value))
                        Global.coe.catchments[i].soils[j].CO2ConcenFactor = Convert.ToDouble(dgInorganicC.Rows[j].Cells[1].Value);
                }

                //Mining tab
                //skipping the mining tab for now. Skipped it in Populate() - Joel will have to do this piece.
            }





            #endregion

        }

        private void tbNumSoilLayers_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbNumSoilLayers.Text,out int newNumSoilLayers))
            {
                if (0 < newNumSoilLayers && newNumSoilLayers < 6)
                {
                    if (tbNumSoilLayers.BackColor == Color.Red)
                    {
                        tbNumSoilLayers.BackColor = SystemColors.Window;
                    }
                    //Catchment catchment = Global.coe.catchments[Global.coe.GetCatchmentNumberFromID(Convert.ToInt16(tbCatchID.Text))];
                    int oldNumSoilLayers = dgSoilHydroCoeffs.Rows.Count;

                    if (newNumSoilLayers > oldNumSoilLayers)
                    {
                        int layersToAdd = newNumSoilLayers - oldNumSoilLayers;
                        for (int j = 0; j < layersToAdd; j++)
                        {
                            DataGridViewRow lastRow = new DataGridViewRow();
                            DataGridViewRow clonedRow = new DataGridViewRow();

                            //hydrology coeffs
                            lastRow = dgSoilHydroCoeffs.Rows[dgSoilHydroCoeffs.Rows.Count - 1];
                            clonedRow = CloneWithValues(lastRow);
                            dgSoilHydroCoeffs.Rows.Add(clonedRow);
                            //dgSoilHydroCoeffs.Rows[dgSoilHydroCoeffs.Rows.Count - 1].Cells[0].Value = 
                                //"Soil Layer " + dgSoilHydroCoeffs.Rows.Count.ToString();
                            dgSoilHydroCoeffs.Rows[dgSoilHydroCoeffs.Rows.Count - 1].HeaderCell.Value =
                               "Soil Layer " + dgSoilHydroCoeffs.Rows.Count.ToString();

                            //initial concentrations
                            lastRow = dgInitialConc.Rows[dgInitialConc.Rows.Count - 1];
                            clonedRow = CloneWithValues(lastRow);
                            dgInitialConc.Rows.Add();
                            dgInitialConc.Rows[dgInitialConc.Rows.Count - 1].HeaderCell.Value =
                                "Soil Layer " + dgInitialConc.Rows.Count.ToString();

                            //adsorption
                            lastRow = dgAdsorption.Rows[dgAdsorption.Rows.Count - 1];
                            clonedRow = CloneWithValues(lastRow);
                            dgAdsorption.Rows.Add();
                            dgAdsorption.Rows[dgAdsorption.Rows.Count - 1].HeaderCell.Value =
                                "Soil Layer " + dgAdsorption.Rows.Count.ToString();

                            //mineral composition
                            lastRow = dgMineralComp.Rows[dgMineralComp.Rows.Count - 1];
                            clonedRow = CloneWithValues(lastRow);
                            dgMineralComp.Rows.Add();
                            dgMineralComp.Rows[dgMineralComp.Rows.Count - 1].HeaderCell.Value =
                                "Soil Layer " + dgMineralComp.Rows.Count.ToString();
                        
                            //inorganic carbon
                            lastRow = dgInorganicC.Rows[dgInorganicC.Rows.Count - 1];
                            clonedRow = CloneWithValues(lastRow);
                            dgInorganicC.Rows.Add();
                            dgInorganicC.Rows[dgInorganicC.Rows.Count - 1].HeaderCell.Value =
                                "Soil Layer " + dgInorganicC.Rows.Count.ToString();
                        }
                    }
                    if (newNumSoilLayers < oldNumSoilLayers)
                    {
                        int layersToRemove = oldNumSoilLayers - newNumSoilLayers;
                        for (int j = 0; j < layersToRemove; j++)
                        {
                            DataGridViewRow lastRow = new DataGridViewRow();
                            
                            //hydrology coeffs
                            lastRow = dgSoilHydroCoeffs.Rows[dgSoilHydroCoeffs.Rows.Count - 1];
                            dgSoilHydroCoeffs.Rows.Remove(lastRow);

                            //initial concentrations
                            lastRow = dgInitialConc.Rows[dgInitialConc.Rows.Count - 1];
                            dgInitialConc.Rows.Remove(lastRow);

                            //adsorption
                            lastRow = dgAdsorption.Rows[dgAdsorption.Rows.Count - 1];
                            dgAdsorption.Rows.Remove(lastRow);

                            //mineral composition
                            lastRow = dgMineralComp.Rows[dgMineralComp.Rows.Count - 1];
                            dgMineralComp.Rows.Remove(lastRow);

                            //inorganic carbon
                            lastRow = dgInorganicC.Rows[dgInorganicC.Rows.Count - 1];
                            dgInorganicC.Rows.Remove(lastRow);
                        }
                    }
                }
                else
                {
                    tbNumSoilLayers.BackColor = Color.Red;
                    ToolTip tt = new ToolTip();
                    tt.Show("1 - 5 Soil Layers", tbNumSoilLayers, 48, 26, 3000);
                }
            }
        }

        // clone a row in a datagridview
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }

        private void popSoilsData(Catchment catchment, int numSoilLayers)
        {
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

            //Soil Layers > Hydrology coefficients (displayed on load)
            dgSoilHydroCoeffs.Rows.Clear();
            for (int ii = 0; ii < numSoilLayers; ii++)
            {
                string area = catchment.soils[ii].area.ToString();
                string thickness = catchment.soils[ii].thickness.ToString();
                string moisture = catchment.soils[ii].moisture.ToString();
                string fieldCap = catchment.soils[ii].fieldCapacity.ToString();
                string satMoist = catchment.soils[ii].saturationMoisture.ToString();
                string horizCond = catchment.soils[ii].horizHydraulicConduct.ToString();
                string vertCond = catchment.soils[ii].vertHydraulicConduct.ToString();
                string rootDist = catchment.soils[ii].evapTranspireFract.ToString();
                string density = catchment.soils[ii].density.ToString();
                string tortuosity = catchment.soils[ii].tortuosity.ToString();
                dgSoilHydroCoeffs.Rows.Insert(ii, area, thickness, moisture, fieldCap, satMoist, horizCond, vertCond, rootDist, density, tortuosity);
                dgSoilHydroCoeffs.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
            }
            FormatDataGridView(dgSoilHydroCoeffs);
            dgSoilHydroCoeffs.Visible = true;

            //Soil Layers > Initial Concentrations
            dgInitialConc.Columns.Clear();
            dgInitialConc.Columns.Add("Temp", "Temperature (degrees C)");
            for (int ii = 0; ii < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; ii++)
            {
                dgInitialConc.Columns.Add(ConstitShortNames[ii], ConstitShortNames[ii].ToString() + " (" + ConstitUnits[ii].ToString().TrimEnd() + ")");
                dgInitialConc.Columns[ii].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Hide initial concentration columns which are not applicable
            List<string> hideInitConcParameters = new List<string>() { "MSOX", "MNOX", "MH", "MALK", "MALG", "MCO2", "MSSED", "MSDET" };
            for (int i = 0; i < hideInitConcParameters.Count; i++)
            {
                int parameterIndex = Global.coe.GetParameterNumberFromCode(hideInitConcParameters[i]);
                if (parameterIndex >= 0 && parameterIndex + 1 < dgInitialConc.Columns.Count)
                    dgInitialConc.Columns[parameterIndex + 1].Visible = false;
            }
            dgInitialConc.Rows.Clear();
            for (int ii = 0; ii < numSoilLayers; ii++)
            {
                dgInitialConc.Rows.Insert(ii);
                dgInitialConc.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                dgInitialConc.Rows[ii].Cells[0].Value = catchment.soils[ii].waterTemp.ToString();
                for (int iParam = 0; iParam < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; iParam++)
                {
                    dgInitialConc.Rows[ii].Cells[iParam + 1].Value = catchment.soils[ii].solutionConcen[iParam].ToString();
                }
            }
            FormatDataGridView(dgInitialConc);
            dgInitialConc.Visible = false;

            //Soil Layers > Adsorption
            //***************Adsorption is currently working differently than in WARMF v7****************
            //***************We need to make some decisions here, and decide how to proceed**************
            dgAdsorption.Columns.Clear();
            dgAdsorption.Columns.Add("CEC", "CEC (meq/100 g)");
            dgAdsorption.Columns.Add("MaxP", "Max PO4 (mg/kg)");
            for (int ii = 0; ii < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; ii++)
            {
                dgAdsorption.Columns.Add(ConstitShortNames[ii].ToString(), ConstitShortNames[ii].ToString().TrimEnd() + " (L/kg)");
            }
            dgAdsorption.Rows.Clear();
            for (int ii = 0; ii < numSoilLayers; ii++)
            {
                dgAdsorption.Rows.Insert(ii);
                dgAdsorption.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                //                dgAdsorption.Rows[ii].Cells[0].Value = catchment.soils[ii].exchangeCapacity.ToString("F0");
                //                dgAdsorption.Rows[ii].Cells[1].Value = catchment.soils[ii].maxPhosAdsorption.ToString("F0");
                dgAdsorption.Rows[ii].Cells[0].Value = catchment.soils[ii].exchangeCapacity.ToString();
                dgAdsorption.Rows[ii].Cells[1].Value = catchment.soils[ii].maxPhosAdsorption.ToString();
                for (int iParam = 0; iParam < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; iParam++)
                {
                    //                    dgAdsorption.Rows[ii].Cells[iParam + 2].Value = catchment.soils[ii].adsorptionIsotherm[iParam].ToString("F0");
                    dgAdsorption.Rows[ii].Cells[iParam + 2].Value = catchment.soils[ii].adsorptionIsotherm[iParam].ToString();
                }
            }
            List<int> HideCols = new List<int> { 0, 1, 13, 15, 16, 19, 20, 21, 22, 23, 24, 29, 30, 31, 32, 33, 34, 35, 36, 37 };
            for (int ii = 0; ii < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; ii++)
            {
                if (HideCols.Contains(ii))
                {
                    dgAdsorption.Columns[ii + 2].Visible = false;
                }
            }
            FormatDataGridView(dgAdsorption);
            dgAdsorption.Visible = false;

            //Soil Layers > Mineral Composition
            dgMineralComp.Columns.Clear();
            for (int ii = 0; ii < Global.coe.numMinerals; ii++)
            {
                dgMineralComp.Columns.Add(Global.coe.minerals[ii].name.ToString(), Global.coe.minerals[ii].name.ToString());
            }
            dgMineralComp.Rows.Clear();
            for (int ii = 0; ii < numSoilLayers; ii++)
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
            dgInorganicC.Rows.Clear();
            for (int ii = 0; ii < numSoilLayers; ii++)
            {
                dgInorganicC.Rows.Insert(ii);
                dgInorganicC.Rows[ii].HeaderCell.Value = "Soil Layer " + (ii + 1).ToString();
                dgInorganicC.Rows[ii].Cells[0].Value = (dgInorganicC.Rows[ii].Cells[0] as DataGridViewComboBoxCell).Items[catchment.soils[ii].CO2CalcMethod - 1];
                //                dgInorganicC.Rows[ii].Cells[1].Value = catchment.soils[ii].CO2ConcenFactor.ToString("F0");
                dgInorganicC.Rows[ii].Cells[1].Value = catchment.soils[ii].CO2ConcenFactor.ToString();
            }
            FormatDataGridView(dgInorganicC);
            dgInorganicC.Visible = false;
        }
    }
}
