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
        private List<PTSFile> pointSourceFiles;

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
            dgvReactions.Columns.Add("water", "Water");
            dgvReactions.Columns.Add("bed", "Bed");
            for (int i = 0; i < Global.coe.numReactions; i++)
            {
                dgvReactions.Rows.Add();
                dgvReactions.Rows[i].HeaderCell.Value = Global.coe.reactions[i].name + ", " + Global.coe.reactions[i].units;
                dgvReactions.Rows[i].Cells["water"].Value = reservoir.waterReactionRate[i].ToString();
                dgvReactions.Rows[i].Cells["bed"].Value = reservoir.bedReactionRate[i].ToString();
            }
            FormatDataGridView(dgvReactions);
            //Phytoplankton
            //Hard-coding some of the datagridview setup, because I think the phytoplankton species are hard coded in the model - check with Joel
            //Columns: Blue-green, Diatoms, Green algae
            //Rows: N half-sat, p half-sat, Si half-sat, Light sat, Lower growth temp, Upper growth temp, Optimum growth temp
            dgvPhytoplankton.Columns.Add("bluegreen", "Blue-Green Algae");
            dgvPhytoplankton.Columns.Add("diatoms", "Diatoms");
            dgvPhytoplankton.Columns.Add("green", "Green Algae");
            dgvPhytoplankton.Rows.Add(7);
            dgvPhytoplankton.Rows[0].HeaderCell.Value = "Nitrogen Half-Saturation, mg/L";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[0].Cells[i].Value = reservoir.algae[i].nitroHalfSat.ToString();
            dgvPhytoplankton.Rows[1].HeaderCell.Value = "Phosphorus Half-Saturation, mg/L";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[1].Cells[i].Value = reservoir.algae[i].phosHalfSat.ToString();
            dgvPhytoplankton.Rows[2].HeaderCell.Value = "Silica Half-Saturation, mg/L";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[2].Cells[i].Value = reservoir.algae[i].silicaHalfSat.ToString();
            dgvPhytoplankton.Rows[3].HeaderCell.Value = "Light Half-Saturation, W/m2";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[3].Cells[i].Value = reservoir.algae[i].lightSat.ToString();
            dgvPhytoplankton.Rows[4].HeaderCell.Value = "Lower Growth Temperature, C";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[4].Cells[i].Value = reservoir.algae[i].lowTempLimit.ToString();
            dgvPhytoplankton.Rows[5].HeaderCell.Value = "Upper Growth Temperature, C";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[5].Cells[i].Value = reservoir.algae[i].highTempLimit.ToString();
            dgvPhytoplankton.Rows[6].HeaderCell.Value = "Optimum Growth Temperature, C";
            for (int i = 0; i < 3; i++) dgvPhytoplankton.Rows[6].Cells[i].Value = reservoir.algae[i].optGrowTemp.ToString();
            FormatDataGridView(dgvPhytoplankton);
            //Heat/Light
            tbRadiationAbsorbed.Text = reservoirSeg.radiationFraction.ToString();
            tbRadiationFractionDepth.Text = reservoirSeg.radiationFractionDepth.ToString();
            tbSecchiDepth.Text = reservoirSeg.SecchiDiskDepth.ToString();
            //Diffusion
            tbInflowEntrainment.Text = reservoirSeg.inflowEntrain.ToString();
            tbMinNegDensGrad.Text = reservoirSeg.minNegDensity.ToString();
            tbWindMinDiff.Text = reservoirSeg.minDiffCoeff.ToString();
            tbWindA1.Text = reservoirSeg.windMixA1Coef.ToString();
            tbWindA2.Text = reservoirSeg.windMixA2Coef.ToString();
            tbWindMaxDiff.Text = reservoirSeg.windMixMaxDiffCoef.ToString();
            tbDensityCriticalGradient.Text = reservoirSeg.criticalDensityGradient.ToString();
            tbDensityMaxDiff.Text = reservoirSeg.densityGradMaxDiffCoef.ToString();
            tbDensityAttenuationExponent.Text = reservoirSeg.densityGradExp.ToString();
            //Sediment
            tbSedThickness.Text = reservoirSeg.sedBottomThickness.ToString();
            tbSedDiffRate.Text = reservoirSeg.sedDiffusion.ToString();
            //Initial Concentrations
            string strWaterUnits, strShortUnits;
            dgvInitialConc.Columns.Add("water", "Water");
            dgvInitialConc.Columns.Add("waterunits", "Units");
            dgvInitialConc.Columns.Add("bed", "Bed");
            dgvInitialConc.Columns.Add("bedunits", "Units");
            for (int i = 0; i < Global.coe.numChemicalParams; i++)
            {
                dgvInitialConc.Rows.Add();
                dgvInitialConc.Rows[i].HeaderCell.Value = Global.coe.chemConstits[i].fullName;
                dgvInitialConc.Rows[i].Cells["water"].Value = reservoirSeg.chemConcentrations[i].ToString();
                strWaterUnits = Global.coe.chemConstits[i].units;
                dgvInitialConc.Rows[i].Cells["waterunits"].Value = strWaterUnits;
                dgvInitialConc.Rows[i].Cells["bed"].Value = reservoirSeg.chemConBedSediment[i].ToString();
                strShortUnits = strWaterUnits.Substring(0, 4);
                if (String.Equals(strShortUnits, "mg/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvInitialConc.Rows[i].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "mg/g");
                }
                else if (String.Equals(strShortUnits, "ug/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvInitialConc.Rows[i].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "ug/g");
                }
                else
                {
                    dgvInitialConc.Rows[i].Cells["bedunits"].Value = strWaterUnits;
                }
            }
            int iRow = 0;
            for (int i = 0; i < Global.coe.numPhysicalParams; i++)
            {
                iRow = i + Global.coe.numChemicalParams;
                dgvInitialConc.Rows.Add();
                dgvInitialConc.Rows[iRow].HeaderCell.Value = Global.coe.physicalConstits[i].fullName;
                dgvInitialConc.Rows[iRow].Cells["water"].Value = reservoirSeg.chemConcentrations[i + Global.coe.numChemicalParams].ToString();
                strWaterUnits = Global.coe.physicalConstits[i].units;
                dgvInitialConc.Rows[iRow].Cells["waterunits"].Value = strWaterUnits;
                dgvInitialConc.Rows[iRow].Cells["bed"].Value = reservoirSeg.chemConBedSediment[i + Global.coe.numChemicalParams].ToString();
                strShortUnits = strWaterUnits.Substring(0, 4);
                if (String.Equals(strShortUnits, "mg/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvInitialConc.Rows[iRow].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "mg/g");
                }    
                else if (String.Equals(strShortUnits, "ug/L", StringComparison.OrdinalIgnoreCase))
                {
                    dgvInitialConc.Rows[iRow].Cells["bedunits"].Value = strWaterUnits.Replace(strWaterUnits.Substring(0, 4), "ug/g");
                }
                else
                {
                    dgvInitialConc.Rows[iRow].Cells["bedunits"].Value = strWaterUnits;
                }
            }
            List<int> hideRowsList = new List<int>() { 1, 2, 3, 14, 17, 21, 23, 24, 33, 38 };
            foreach (int i in hideRowsList)
                dgvInitialConc.Rows[i-1].Visible = false;
            FormatDataGridView(dgvInitialConc);
            //Point Sources
            if (reservoirSeg.numPointSrcs > 0)
            {
                pointSourceFiles = new List<PTSFile>();
                for (int i = 0; i < reservoirSeg.numPointSrcs; i++)
                {
                    lbPointSources.Items.Add(Global.coe.PTSFilename[reservoirSeg.pointSrcFilenums[i] - 1]);
                    PTSFile ptFile = new PTSFile(Global.coe.PTSFilename[reservoirSeg.pointSrcFilenums[0] - 1]);
                    pointSourceFiles.Add(ptFile);
                }
                lbPointSources.SelectedIndex = 0;
                PTSFile pFile = pointSourceFiles[lbPointSources.SelectedIndex];
                pFile.ReadHeader();
                if (pFile.swInternal == true)
                {
                    rbInternal.Checked = true;
                    rbExternal.Checked = false;
                    rbZero.Checked = false;
                    rbZero.Enabled = false;
                    rbAmbient.Checked = true;
                    tbOutletElev.Text = pFile.outElevation.ToString();
                    tbOutletWidth.Text = pFile.outWidth.ToString();
                }
                else
                {
                    rbInternal.Checked = false;
                    rbExternal.Checked = true;
                    rbZero.Checked = true;
                    rbAmbient.Checked = false;
                    rbAmbient.Enabled = false;
                    tbOutletElev.Text = "";
                    tbOutletElev.Enabled = false;
                    tbOutletWidth.Text = "";
                    tbOutletWidth.Enabled = false;
                }
                tbNPDESpermit.Text = pFile.npdesPermit;  
            }
            //Adsorption
            dgvAdsorption.Columns.Add("water", "Water");
            dgvAdsorption.Columns.Add("bed", "Bed");
            for (int i = 0; i < Global.coe.numChemicalParams; i++)
            {
                dgvAdsorption.Rows.Add();
                dgvAdsorption.Rows[i].HeaderCell.Value = Global.coe.chemConstits[i].fullName;
                dgvAdsorption.Rows[i].Cells["water"].Value = reservoir.waterAdsorpIsotherm[i].ToString();
                dgvAdsorption.Rows[i].Cells["bed"].Value = reservoir.bedAdsorpIsotherm[i].ToString();
            }
            FormatDataGridView(dgvAdsorption);
            //Observed Data
            tbObsWQFile.Text = reservoirSeg.obsWQFilename;
            tbObsHydroFile.Text = reservoir.hydrologyFilename;
            //Stage-Area
            dgvStageArea.Columns.Add("Stage", "Stage (m)");
            //dgvStageArea.Columns["stage"].ValueType = ;
            dgvStageArea.Columns["stage"].SortMode = DataGridViewColumnSortMode.Automatic;
            dgvStageArea.Columns.Add("Area", "Area (m2)");
            for (int ii = 0; ii < 9; ii++)
            {
                dgvStageArea.Rows.Insert(ii,
                    reservoir.bathymetry[ii].stage.ToString(),
                    reservoir.bathymetry[ii].area.ToString());
            }
            dgvStageArea.RowHeadersVisible = false;
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
            FormatDataGridView(dgvStageArea);
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
            tbMetFile.Text = Global.coe.METFilename[reservoir.METFilenum - 1];
            tbPrecipWeight.Text = reservoirSeg.precipWgtMult.ToString();
            tbTempLapse.Text = reservoirSeg.tempLapse.ToString();
            tbWindSpeedFactor.Text = reservoirSeg.windSpeedMult.ToString();
            tbAirRainChemFile.Text = Global.coe.AIRFilename[reservoir.airRainChemFilenum - 1];
            //tbCoarseParticleChemFile --> I can't figure this one out - need to ask Joel about CPA files, since we've never used
            //this functionality in any of the projects I've worked on.
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
        private void BtnUpdateChart_Click(object sender, EventArgs e)
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
        private void BtnSelectHydroFile_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory = Global.DIR.OLH;
            ReservoirOpenFileDialog.Title = "Select Observed Lake Hydrology File";
            ReservoirOpenFileDialog.Filter = "Observed Lake Hydrology Files | *.OLH";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbObsHydroFile.Text = ReservoirOpenFileDialog.FileName;
            }
        }

        private void BtnSelectWQfile_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory =
                System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.OLC);
            ReservoirOpenFileDialog.Title = "Select Observed Lake Chemistry File";
            ReservoirOpenFileDialog.Filter = "Observed Lake Chemistry Files | *.OLC";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbObsWQFile.Text = ReservoirOpenFileDialog.FileName;
            }
        }

        private void BtnClearHydroFile_Click(object sender, EventArgs e)
        {
            tbObsHydroFile.Text = "";
        }

        private void BtnClearWQfile_Click(object sender, EventArgs e)
        {
            tbObsWQFile.Text = "";
        }

        private void BtnSelectMetFile_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.MET);
            ReservoirOpenFileDialog.Title = "Select Meteorology File";
            ReservoirOpenFileDialog.Filter = "Meteorology Files | *.MET";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbMetFile.Text = ReservoirOpenFileDialog.FileName;
            }
        }

        private void BtnSelectRainChemFile_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.AIR);
            ReservoirOpenFileDialog.Title = "Select Air and Rain Chemistry File";
            ReservoirOpenFileDialog.Filter = "Air and Rain Chemistry Files | *.AIR";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAirRainChemFile.Text = ReservoirOpenFileDialog.FileName;
            }
        }

        private void btnSelectCoarseParticleFile_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.CPA);
            ReservoirOpenFileDialog.Title = "Select Coarse Particle Air Chemistry File";
            ReservoirOpenFileDialog.Filter = "Coarse Particle Air Chemistry Files | *.CPA";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbAirRainChemFile.Text = ReservoirOpenFileDialog.FileName;
            }
        }

        private void BtnAddPtSource_Click(object sender, EventArgs e)
        {
            ReservoirOpenFileDialog.InitialDirectory =
               System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), Global.DIR.PTS);
            ReservoirOpenFileDialog.Title = "Select Point Source File";
            ReservoirOpenFileDialog.Filter = "Point Source Files | *.PTS";

            if (ReservoirOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPointSources.Items.Add(ReservoirOpenFileDialog.FileName);
            }
        }

        private void BtnRemovePointSource_Click(object sender, EventArgs e)
        {

        }
    }
}
