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
                dgvStageWidth.Rows.Insert(i, river.stageWidthCurve[i].stage, river.stageWidthCurve[i].width);
                chartStageWidth.Series["SeriesStageWidth"].Points.AddXY(river.stageWidthCurve[i].width, river.stageWidthCurve[i].stage);
            }

            //Diversions
            for (int i = 0; i < river.diversionFromFilenums.Count; i++)
                lbDiversionsFrom.Items.Add(Global.coe.DIVData[river.diversionFromFilenums[i] - 1].filename);

            for (int i = 0; i < river.diversionToFilenums.Count; i++)
                lbDiversionsTo.Items.Add(Global.coe.DIVData[river.diversionToFilenums[i] - 1].filename);

            tbMinRiverFlow.Text = river.minFlow.ToString();

            //Point Sources
            if (river.pointSources.Count > 0)
            {
                pointSourceFiles = new List<PTSFile>();
                for (int i = 0; i < river.pointSources.Count; i++)
                {
                    lbPointSources.Items.Add(Global.coe.PTSFilename[river.pointSources[i] - 1]);
                    PTSFile ptFile = new PTSFile(Global.coe.PTSFilename[river.pointSources[i] - 1]);
                    pointSourceFiles.Add(ptFile);
                }
                lbPointSources.SelectedIndex = 0;
                PointSourceInfo(lbPointSources.SelectedIndex);
            }

            //Reactions
            for (int i = 0; i < Global.coe.numReactions; i++)
            {
                dgvReactions.Rows.Add();
                dgvReactions.Rows[i].HeaderCell.Value = Global.coe.reactions[i].name + ", " + Global.coe.reactions[i].units;
                dgvReactions.Rows[i].Cells["water"].Value = river.waterReactionRate[i].ToString();
                dgvReactions.Rows[i].Cells["bed"].Value = river.bedReactionRate[i].ToString();
            }
            for (int i = 0; i < dgvReactions.Columns.Count; i++)
                dgvReactions.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            tbAerationFactor.Text = river.reaerationRateMult.ToString();
            tbConvHeatFactor.Text = river.convectiveHeatFactor.ToString();
            tbPrecipitateSettling.Text = river.precipSettleRate.ToString();

            //Sediment
            tbInitSedDepth.Text = river.sedBedDepth.ToString();
            tbBedDiffRate.Text = river.sedDiffusionRate.ToString();
            tbDetachVelMult.Text = river.sedDetachVelMult.ToString();
            tbDetachVelExp.Text = river.sedDetachVelExp.ToString();
            tbVegFactor.Text = river.sedVegFactor.ToString();
            tbBankStabilityFactor.Text = river.sedBankStabFactor.ToString();
            //number of sediment particle sizes is not flexible in current code. Consider revising?
            dgvBedParticleContent.Rows.Add();
            dgvBedParticleContent.Rows[0].HeaderCell.Value = "Clay";
            dgvBedParticleContent.Rows[0].Cells[0].Value = (river.sedFirstPartSizePct * 100).ToString();
            dgvBedParticleContent.Rows.Add();
            dgvBedParticleContent.Rows[1].HeaderCell.Value = "Silt";
            dgvBedParticleContent.Rows[1].Cells[0].Value = (river.sedSecondPartSizePct * 100).ToString();
            dgvBedParticleContent.Rows.Add();
            dgvBedParticleContent.Rows[2].HeaderCell.Value = "Sand";
            dgvBedParticleContent.Rows[2].Cells[0].Value = (river.sedThirdPartSizePct * 100).ToString();
            for (int i = 0; i < dgvBedParticleContent.Columns.Count; i++)
                dgvBedParticleContent.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            //Initial Concentrations
            string strWaterUnits, strShortUnits;
            dgvRiverInitConcs.Columns.Add("water", "Water");
            dgvRiverInitConcs.Columns.Add("waterunits", "Units");
            dgvRiverInitConcs.Columns["waterunits"].ReadOnly = true;
            dgvRiverInitConcs.Columns.Add("bed", "Bed");
            dgvRiverInitConcs.Columns.Add("bedunits", "Units");
            dgvRiverInitConcs.Columns["bedunits"].ReadOnly = true;
            for (int i = 0; i < dgvRiverInitConcs.Columns.Count; i++)
                dgvRiverInitConcs.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < Global.coe.numChemicalParams; i++)
            {
                dgvRiverInitConcs.Rows.Add();
                dgvRiverInitConcs.Rows[i].HeaderCell.Value = Global.coe.chemConstits[i].fullName;
                dgvRiverInitConcs.Rows[i].Cells["water"].Value = river.componentConcentration[i].ToString();
                strWaterUnits = Global.coe.chemConstits[i].units;
                dgvRiverInitConcs.Rows[i].Cells["waterunits"].Value = strWaterUnits;
                dgvRiverInitConcs.Rows[i].Cells["bed"].Value = river.bedAdsorpConcentration[i].ToString();
                strShortUnits = strWaterUnits.Substring(0, 4);
                if (String.Equals(strShortUnits, "mg/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvRiverInitConcs.Rows[i].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "mg/g");
                }
                else if (String.Equals(strShortUnits, "ug/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvRiverInitConcs.Rows[i].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "ug/g");
                }
                else
                {
                    dgvRiverInitConcs.Rows[i].Cells["bedunits"].Value = strWaterUnits;
                }
            }
            int iRow = 0;
            for (int i = 0; i < Global.coe.numPhysicalParams; i++)
            {
                iRow = i + Global.coe.numChemicalParams;
                dgvRiverInitConcs.Rows.Add();
                dgvRiverInitConcs.Rows[iRow].HeaderCell.Value = Global.coe.physicalConstits[i].fullName;
                dgvRiverInitConcs.Rows[iRow].Cells["water"].Value = river.componentConcentration[i + Global.coe.numChemicalParams].ToString();
                strWaterUnits = Global.coe.physicalConstits[i].units;
                dgvRiverInitConcs.Rows[iRow].Cells["waterunits"].Value = strWaterUnits;
                dgvRiverInitConcs.Rows[iRow].Cells["bed"].Value = river.bedAdsorpConcentration[i + Global.coe.numChemicalParams].ToString();
                strShortUnits = strWaterUnits.Substring(0, 4);
                if (String.Equals(strShortUnits, "mg/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvRiverInitConcs.Rows[iRow].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "mg/g");
                }
                else if (String.Equals(strShortUnits, "ug/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvRiverInitConcs.Rows[iRow].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "ug/g");
                }
                else
                {
                    dgvRiverInitConcs.Rows[iRow].Cells["bedunits"].Value = strWaterUnits;
                }
            }
            List<string> hideParameters = new List<string>() { "MSOX", "MNOX", "MH", "MALK", "MALG", "MCO2", "MSSED", "MSDET" };
            int temperatureParameter = Global.coe.GetParameterNumberFromCode("MTEMP");
            for (int i = 0; i < hideParameters.Count; i++)
            {
                int parameterIndex = Global.coe.GetParameterNumberFromCode(hideParameters[i]) - temperatureParameter - 1;
                if (parameterIndex >= 0 && parameterIndex < dgvRiverInitConcs.Rows.Count)
                    dgvRiverInitConcs.Rows[parameterIndex].Visible = false;
            }

            //Adsorption
            dgvAdsorption.Columns.Add("water", "Water");
            dgvAdsorption.Columns.Add("bed", "Bed");
            for (int i = 0; i < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; i++)
            {
                dgvAdsorption.Rows.Add();
                dgvAdsorption.Rows[i].HeaderCell.Value = Global.coe.AllConstits[i + temperatureParameter + 1].fullName;
                dgvAdsorption.Rows[i].Cells["water"].Value = river.waterAdsorpIsotherm[i].ToString();
                dgvAdsorption.Rows[i].Cells["bed"].Value = river.bedAdsorpIsotherm[i].ToString();
            }
            for (int i = 0; i < dgvAdsorption.Columns.Count; i++)
                dgvAdsorption.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            List <string> hideAdsorbed = new List<string> () { "MSOX", "MNOX", "MH", "MALK", "MTIC", "MOAAL", "MALG", "MCOLI", "MCO2", "MDO", "MBOD", "MBOD2", "MALG1", "MALG2", "MALG3", "MALG4", "MSED1", "MSED2", "MSED3", "MSSED", "MSDET" };
            for (int i = 0; i < hideAdsorbed.Count; i++)
            {
                int parameterIndex = Global.coe.GetParameterNumberFromCode(hideAdsorbed[i]) - temperatureParameter - 1;
                if (parameterIndex >= 0 && parameterIndex < dgvAdsorption.Rows.Count)
                    dgvAdsorption.Rows[parameterIndex].Visible = false;
            }
/*            List<int> HideCols = new List<int> { 0, 1, 2, 13 };
            for (int i = 0; i < Global.coe.numChemicalParams; i++)
            {
                if (HideCols.Contains(i))
                {
                    dgvAdsorption.Rows[i].Visible = false;
                }
            }*/

            //Observed Data
            tbObsHydroFile.Text = river.hydrologyFilename;
            tbObsWaterQualFile.Text = river.obsWQFilename;
            cbSimulationOverride.Checked = river.overrideSimulation.swUseObsData;
            tbHydroInterpPd.Text = river.overrideSimulation.hydroInterpPeriod.ToString();
            tbWQInterpPd.Text = river.overrideSimulation.waterQualityInterpPeriod.ToString();
            if (river.overrideSimulation.monthAverageMethod == 1)
            {
                rbAvgSimulation.Checked = true;
                rbAverageData.Checked = false;
            }
            else
            {
                rbAvgSimulation.Checked = false;
                rbAverageData.Checked = true;
            }
            tbPriorityTDS.Text = river.overrideSimulation.tdsAdjustmentPriority.ToString();
            tbPriorityAlkalinity.Text = river.overrideSimulation.alkAdjustmentPriority.ToString();
            tbPriorityPh.Text = river.overrideSimulation.phAdjustmentPriority.ToString();

            //CE-QUAL-W2
            if (river.numCEQW2Files == 3)
            {
                cbWriteCEQUALoutput.Checked = true;
                tbCEQUALflowFile.Text = river.flowInputFilename;
                tbCEQUALtempFile.Text = river.tempInputFilename;
                tbCEQUALconcFile.Text = river.waterQualInputFilename; 
            }
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

        private void PointSourceInfo(int FileIndex)
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

        private void btnSelectORH_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.ORH);
            RiverOpenFileDialog.Title = "Select Observed Hydrology File";
            RiverOpenFileDialog.Filter = "Observed Hydrology Files | *.ORH";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbObsHydroFile.Text = RiverOpenFileDialog.SafeFileName;
            }
        }

        private void btnClearORH_Click(object sender, EventArgs e)
        {
            tbObsHydroFile.Text = null;
        }

        private void btnSelectORC_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.ORC);
            RiverOpenFileDialog.Title = "Select Observed Water Quality File";
            RiverOpenFileDialog.Filter = "Observed River Chemistry Files | *.ORC";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbObsWaterQualFile.Text = RiverOpenFileDialog.SafeFileName;
            }
        }

        private void btnClearORC_Click(object sender, EventArgs e)
        {
            tbObsWaterQualFile.Text = null;
        }

        private void tbObsHydroFile_TextChanged(object sender, EventArgs e)
        {
            CheckSimulationOverride();
        }

        private void tbObsWaterQualFile_TextChanged(object sender, EventArgs e)
        {
            CheckSimulationOverride();
        }

        public void CheckSimulationOverride()
        {
            if (string.IsNullOrWhiteSpace(tbObsHydroFile.Text) || string.IsNullOrEmpty(tbObsHydroFile.Text))
                gbObsAsInput.Enabled = false;
            else
            {
                if (string.IsNullOrWhiteSpace(tbObsWaterQualFile.Text) || string.IsNullOrEmpty(tbObsWaterQualFile.Text))
                    gbObsAsInput.Enabled = false;
                else
                    gbObsAsInput.Enabled = true;
            }
        }

        private void btnSelectFlowFile_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            RiverOpenFileDialog.Title = "Select CE-QUAL-W2 Flow File";
            RiverOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALflowFile.Text = RiverOpenFileDialog.SafeFileName;
            }
        }

        private void btnSelectTempFile_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            RiverOpenFileDialog.Title = "Select CE-QUAL-W2 Temp File";
            RiverOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALtempFile.Text = RiverOpenFileDialog.SafeFileName;
            }
        }

        private void btnSelectChemFile_Click(object sender, EventArgs e)
        {
            RiverOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.NPT);
            RiverOpenFileDialog.Title = "Select CE-QUAL-W2 Concentration File";
            RiverOpenFileDialog.Filter = "CE-QUAL-W2 Control File | *.NPT";

            if (RiverOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbCEQUALconcFile.Text = RiverOpenFileDialog.SafeFileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            River river = Global.coe.rivers[Global.coe.GetRiverNumberFromID(Convert.ToInt32(tbStreamID.Text))];
            List<int> warmfRiverNumbers = new List<int>();

            if (chbxApplyToSelected.Checked) //if apply to selected
            {
                // Get the indices of selected catchments in the catchment layer of the main map
                int riverLayerNumber = parent.GetRiverLayerNumber();
                List<int> selectedRiverIDs = parent.GetSelectedWARMFIDs(riverLayerNumber);
                for (int i = 0; i < selectedRiverIDs.Count; i++)
                {
                    int riverNumber = Global.coe.GetRiverNumberFromID(selectedRiverIDs[i]);
                    if (riverNumber >= 0)
                        warmfRiverNumbers.Add(riverNumber);
                }
            }
            else if (chbxApplyToAll.Checked) //if apply to all
            {
                warmfRiverNumbers = Enumerable.Range(0, Global.coe.numRivers-1).ToList();
            }
            else //if neither apply to all or selected are checked
            {
                warmfRiverNumbers.Add(Global.coe.GetRiverNumberFromID(Convert.ToInt32(tbStreamID.Text)));
            }

            // Move the edited catchment to the end of the list so that changes will be made to other selected / all catchments
            if (warmfRiverNumbers.Count > 1)
            {
                // The list warmfCatchmentNumbers is of indexes in the master coefficient list of catchments
                int thisRiverNumber = Global.coe.GetRiverNumberFromID(river.idNum);
                int thisRiverIndex = parent.FindInList(warmfRiverNumbers, thisRiverNumber);
                if (thisRiverIndex >= 0)
                {
                    // Put this catchment at the end of the list and remove it from where it was found
                    warmfRiverNumbers.Add(thisRiverNumber);
                    warmfRiverNumbers.RemoveAt(thisRiverIndex);
                }
            }

            #region Update each of the coefficients that should not be applied to other rivers first
            //Physical Data Tab
            river.name = tbName.Text;
            river.upElevation = Convert.ToDouble(tbUpElevation.Text);
            river.downElevation = Convert.ToDouble(tbDownElevation.Text);
            river.length = Convert.ToDouble(tbLength.Text);
            river.depth = Convert.ToDouble(tbDepth.Text);
            river.impoundArea = Convert.ToDouble(tbImpArea.Text);
            river.impoundVol = Convert.ToDouble(tbImpVolume.Text);
            
            //Diversions
            river.diversionFromFilenums.Clear();
            for (int i = 0; i < lbDiversionsFrom.Items.Count; i++)
            {
                river.diversionFromFilenums.Add(Global.coe.GetFLONumberFromName(lbDiversionsFrom.Items[i].ToString()));
            }
            river.diversionToFilenums.Clear();
            for (int i = 0; i < lbDiversionsTo.Items.Count; i++)
            {
                river.diversionToFilenums.Add(Global.coe.GetFLONumberFromName(lbDiversionsTo.Items[i].ToString()));
            }
            river.minFlow = Convert.ToDouble(tbMinRiverFlow.Text);

            //Point Sources
            river.pointSources.Clear();
            for (int i = 0; i < lbPointSources.Items.Count; i++)
            {
                river.pointSources.Add(Global.coe.GetPTSNumberFromName(lbPointSources.Items[i].ToString()));
            }
            
            //Observed Data
            river.hydrologyFilename = tbObsHydroFile.Text;
            river.obsWQFilename = tbObsWaterQualFile.Text;
            river.overrideSimulation.swUseObsData = cbSimulationOverride.Checked;
            river.overrideSimulation.hydroInterpPeriod = Convert.ToInt16(tbHydroInterpPd.Text);
            river.overrideSimulation.waterQualityInterpPeriod = Convert.ToInt16(tbWQInterpPd.Text);
            if (rbAvgSimulation.Checked == true)
            {
                river.overrideSimulation.monthAverageMethod = 1;
            }
            else
            {
                river.overrideSimulation.monthAverageMethod = 2;
            }
            river.overrideSimulation.tdsAdjustmentPriority = Convert.ToInt16(tbPriorityTDS.Text);
            river.overrideSimulation.alkAdjustmentPriority = Convert.ToInt16(tbPriorityAlkalinity.Text);
            river.overrideSimulation.phAdjustmentPriority = Convert.ToInt16(tbPriorityPh.Text);

            //CE-QUAL-W2
            if (cbWriteCEQUALoutput.Checked == true)
            {
                river.numCEQW2Files = 3;
                river.flowInputFilename = tbCEQUALflowFile.Text;
                river.tempInputFilename = tbCEQUALtempFile.Text;
                river.waterQualInputFilename = tbCEQUALconcFile.Text;
            }
            else
            {
                river.numCEQW2Files = 0;
                river.flowInputFilename = null;
                river.tempInputFilename = null;
                river.waterQualInputFilename = null;
            }
            #endregion

            #region Update the coefficients that can be applied to selected or all
            for (int i = 0; i < warmfRiverNumbers.Count; i++)
            {
                //Physical Data Tab
                if (river.name != tbName.Text)
                    Global.coe.rivers[warmfRiverNumbers[i]].name = tbName.Text;
                if (river.ManningN != Convert.ToDouble(tbManningsN.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].ManningN = Convert.ToDouble(tbManningsN.Text);
                
                //Stage-Width
                for (int j = 0; j < 9; j++)
                {
                    if (river.stageWidthCurve[j].stage != Convert.ToDouble(dgvStageWidth.Rows[j].Cells[0].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].stageWidthCurve[j].stage = Convert.ToDouble(dgvStageWidth.Rows[j].Cells[0].Value);
                    if (river.stageWidthCurve[j].width != Convert.ToDouble(dgvStageWidth.Rows[j].Cells[1].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].stageWidthCurve[j].width = Convert.ToDouble(dgvStageWidth.Rows[j].Cells[1].Value);
                }

                //Reactions
                for (int j = 0; j < Global.coe.numReactions; j++)
                {
                    if (river.waterReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["water"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].waterReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["water"].Value);
                    if (river.bedReactionRate[j] != Convert.ToDouble(dgvReactions.Rows[j].Cells["bed"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].bedReactionRate[j] = Convert.ToDouble(dgvReactions.Rows[j].Cells["bed"].Value);
                }
                if (river.reaerationRateMult != Convert.ToDouble(tbAerationFactor.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].reaerationRateMult = Convert.ToDouble(tbAerationFactor.Text);
                if (river.convectiveHeatFactor != Convert.ToDouble(tbConvHeatFactor.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].convectiveHeatFactor = Convert.ToDouble(tbConvHeatFactor.Text);
                if (river.precipSettleRate != Convert.ToDouble(tbPrecipitateSettling.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].precipSettleRate = Convert.ToDouble(tbPrecipitateSettling.Text);

                //Sediment
                if (river.sedBedDepth != Convert.ToDouble(tbInitSedDepth.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedBedDepth = Convert.ToDouble(tbInitSedDepth.Text);
                if (river.sedDiffusionRate != Convert.ToDouble(tbBedDiffRate.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedDiffusionRate = Convert.ToDouble(tbBedDiffRate.Text);
                if (river.sedDetachVelMult != Convert.ToDouble(tbDetachVelMult.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedDetachVelMult = Convert.ToDouble(tbDetachVelMult.Text);
                if (river.sedDetachVelExp != Convert.ToDouble(tbDetachVelExp.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedDetachVelExp = Convert.ToDouble(tbDetachVelExp.Text);
                if (river.sedVegFactor != Convert.ToDouble(tbVegFactor.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedVegFactor = Convert.ToDouble(tbVegFactor.Text);
                if (river.sedBankStabFactor != Convert.ToDouble(tbBankStabilityFactor.Text))
                    Global.coe.rivers[warmfRiverNumbers[i]].sedBankStabFactor = Convert.ToDouble(tbBankStabilityFactor.Text);
                if (river.sedFirstPartSizePct != Convert.ToDouble(dgvBedParticleContent.Rows[0].Cells[0].Value) / 100)
                    Global.coe.rivers[warmfRiverNumbers[i]].sedFirstPartSizePct = Convert.ToDouble(dgvBedParticleContent.Rows[0].Cells[0].Value) / 100; //Clay
                if (river.sedSecondPartSizePct != Convert.ToDouble(dgvBedParticleContent.Rows[1].Cells[0].Value) / 100)
                    Global.coe.rivers[warmfRiverNumbers[i]].sedSecondPartSizePct = Convert.ToDouble(dgvBedParticleContent.Rows[1].Cells[0].Value) / 100; //Silt
                if (river.sedThirdPartSizePct != Convert.ToDouble(dgvBedParticleContent.Rows[2].Cells[0].Value) / 100)
                    Global.coe.rivers[warmfRiverNumbers[i]].sedThirdPartSizePct = Convert.ToDouble(dgvBedParticleContent.Rows[2].Cells[0].Value) / 100; //Sand

                //Initial Concentrations
                for (int j = 0; j < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; j++) 
                {
                    if (river.componentConcentration[j] != Convert.ToDouble(dgvRiverInitConcs.Rows[j].Cells["water"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].componentConcentration[j] = Convert.ToDouble(dgvRiverInitConcs.Rows[j].Cells["water"].Value);
                    if (river.bedAdsorpConcentration[j] != Convert.ToDouble(dgvRiverInitConcs.Rows[j].Cells["bed"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].bedAdsorpConcentration[j] = Convert.ToDouble(dgvRiverInitConcs.Rows[j].Cells["bed"].Value);
                }

                //Adsorption
                for (int j = 0; j < Global.coe.numChemicalParams + Global.coe.numPhysicalParams; j++)
                {
                    if (river.waterAdsorpIsotherm[j] != Convert.ToDouble(dgvAdsorption.Rows[j].Cells["water"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].waterAdsorpIsotherm[j] = Convert.ToDouble(dgvAdsorption.Rows[j].Cells["water"].Value);
                    if (river.bedAdsorpIsotherm[j] != Convert.ToDouble(dgvAdsorption.Rows[j].Cells["bed"].Value))
                        Global.coe.rivers[warmfRiverNumbers[i]].bedAdsorpIsotherm[j] = Convert.ToDouble(dgvAdsorption.Rows[j].Cells["bed"].Value);
                }
            }
            #endregion 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
