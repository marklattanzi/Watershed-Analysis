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
            dgvOpenInWinter.Columns.Add("Value", "Value");
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
            dgvRainfallDetachment.Columns.Add("Value", "Value");
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvRainfallDetachment.Rows.Insert(ii);
                dgvRainfallDetachment.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                dgvRainfallDetachment.Rows[ii].Cells[0].Value =
                    Global.coe.landuse[ii].rainDetachFactor.ToString();
            }
            FormatDataGridView(dgvRainfallDetachment);
            //Flow Detachment Factor
            dgvFlowDetachment.Columns.Add("Value", "Value");
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
            dgvFractionImpervious.Columns.Add("Value", "Value");
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
            dgvInterceptionStorage.Columns.Add("Value", "Value");
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
            dgvAnnGrowthMult.Columns.Add("Value", "Value");
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
            dgvLeafGrowth.Columns.Add("Value", "Value");
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
            dgvProductivity.Columns.Add("Value", "Value");
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
            dgvActiveRespiration.Columns.Add("Value", "Value");
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
            dgvMaintenanceRespiration.Columns.Add("Value", "Value");
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
            dgvDryColEfficiency.Columns.Add("Value", "Value");
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
            dgvWetColEfficiency.Columns.Add("Value", "Value");
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
            dgvLeafWeightArea.Columns.Add("Value", "Value");
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
            dgvCanopyHeight.Columns.Add("Value", "Value");
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
            dgvStomatalResistance.Columns.Add("Value", "Value");
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
            AddMonthColumns(dgvCropFactor);
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
            AddMonthColumns(dgvLeafAreaIndex);
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLeafAreaIndex.Rows.Insert(ii);
                dgvLeafAreaIndex.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvLeafAreaIndex.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].leafAreaIdx[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvLeafAreaIndex);
            //Annual Uptake Distribution
            AddMonthColumns(dgvAnnUptakeDist);
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvAnnUptakeDist.Rows.Insert(ii);
                dgvAnnUptakeDist.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvAnnUptakeDist.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].annualUptake[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvAnnUptakeDist);
            //Litter Fall Rate
            AddMonthColumns(dgvLitterFallRate);
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLitterFallRate.Rows.Insert(ii);
                dgvLitterFallRate.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvLitterFallRate.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].litterFallRate[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvLitterFallRate);
            //Exudation Rate
            AddMonthColumns(dgvExudationRate);
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvExudationRate.Rows.Insert(ii);
                dgvExudationRate.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvExudationRate.Rows[ii].Cells[iMonth].Value =
                        Global.coe.landuse[ii].exudationRate[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvExudationRate);
            //Leaf Composition
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                string sName = ConstitShortNames[ii];
                string sUnits = ConstitUnits[ii];
                dgvLeafComp.Columns.Add(sName, string.Concat(sName, ", ", sUnits));
            }
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvLeafComp.Rows.Insert(ii);
                dgvLeafComp.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iConstit = 0; iConstit < Global.coe.numChemicalParams; iConstit++)
                {
                    dgvLeafComp.Rows[ii].Cells[iConstit].Value =
                        Global.coe.landuse[ii].leafComp[iConstit].ToString();
                }
            }
            FormatDataGridView(dgvLeafComp);
            //Trunk Composition
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                string sName = ConstitShortNames[ii];
                string sUnits = ConstitUnits[ii];
                dgvTrunkComp.Columns.Add(sName, string.Concat(sName, ", ", sUnits));
            }
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                dgvTrunkComp.Rows.Insert(ii);
                dgvTrunkComp.Rows[ii].HeaderCell.Value =
                    Global.coe.landuse[ii].name;
                for (int iConstit = 0; iConstit < Global.coe.numChemicalParams; iConstit++)
                {
                    dgvTrunkComp.Rows[ii].Cells[iConstit].Value =
                        Global.coe.landuse[ii].trunkComp[iConstit].ToString();
                }
            }
            FormatDataGridView(dgvTrunkComp);

            cbLandUseParameter.SelectedIndex = 0;
            dgvOpenInWinter.BringToFront();

            //Snow/Ice
            tbSnowFormTemp.Text = Global.coe.snow.formTemp.ToString();
            tbSnowMeltTemp.Text = Global.coe.snow.meltTemp.ToString();
            tbOpenAreaMelt.Text = Global.coe.snow.openAreaMeltRate.ToString();
            tbForestAreaMelt.Text = Global.coe.snow.forestAreaMeltRate.ToString();
            tbRainMelt.Text = Global.coe.snow.rainMeltRate.ToString();
            tbSnowFieldCapacity.Text = Global.coe.snow.fieldCapacity.ToString();
            tbSoluteFraction.Text = Global.coe.snow.soluteIceRetain.ToString();
            tbSnowLeach.Text = Global.coe.snow.meltLeaching.ToString();
            tbSnowNitrif.Text = Global.coe.snow.nitrificationRate.ToString();
            tbOpenAreaSublim.Text = Global.coe.snow.openAreaSubRate.ToString();
            tbForestAreaSublim.Text = Global.coe.snow.forestAreaSubRate.ToString();

            //Heat/Light
            tbAtmTurbidity.Text = Global.coe.atmosphericTurbidity.ToString();
            tbSoilTConduction.Text = Global.coe.soilThermalConduct.ToString();
            tbSnowTConduction.Text = Global.coe.snow.thermalConduct.ToString();
            tbIceTConduction.Text = Global.coe.snow.iceThermalConduct.ToString();
            tbEvapMag.Text = Global.coe.evaporationScaling.ToString();
            tbEvapSkew.Text = Global.coe.evaporationSeasonSkew.ToString();
            tbSedimentShading.Text = Global.coe.sedimentShading.ToString();
            tbAlgaeShading.Text = Global.coe.algaeShading.ToString();
            tbDetritusShading.Text = Global.coe.detritusShading.ToString();

            //Canopy
            tbStandingBiomass.Text = Global.coe.standingBiomass.ToString();
            AddMonthColumns(dgvDepUptakeVelocity);
            dgvDepUptakeVelocity.Rows.Insert(0, 2);
            dgvDepUptakeVelocity.Rows[0].HeaderCell.Value = "Particle Deposition";
            dgvDepUptakeVelocity.Rows[1].HeaderCell.Value = "Gas Uptake";
            for (int iMonth = 0; iMonth < 12; iMonth++)
            {
                dgvDepUptakeVelocity.Rows[0].Cells[iMonth].Value =
                    Global.coe.partDV[iMonth].ToString();
                dgvDepUptakeVelocity.Rows[1].Cells[iMonth].Value =
                    Global.coe.gasUptakeVelocity[iMonth].ToString();
            }

            //Litter
            tbLitterDecay.Text = Global.coe.litter.coarseLitterDecay.ToString();
            tbFineLitterDecay.Text = Global.coe.litter.fineLitterDecay.ToString();
            tbHumusDecay.Text = Global.coe.litter.humusDecay.ToString();
            tbLitterLeachFract.Text = Global.coe.litter.coarseLitterFrac.ToString();
            tbFineLitterLeachFract.Text = Global.coe.litter.fineLitterFrac.ToString();
            tbHumusLeachFract.Text = Global.coe.litter.humusFrac.ToString();
            tbNonstructLeachFract.Text = Global.coe.litter.nonStructLeach.ToString();

            //Septic Systems
            tbSepticFlow.Text = Global.coe.septic.failedFlow.ToString();
            dgvSepticDischQual.Columns.Add("Type1", "Type 1");
            dgvSepticDischQual.Columns.Add("Type2", "Type 2");
            dgvSepticDischQual.Columns.Add("Type3", "Type 3");
            for (int iConstit = 0; iConstit < Global.coe.numChemicalParams; iConstit++)
            {
                dgvSepticDischQual.Rows.Insert(iConstit);
                dgvSepticDischQual.Rows[iConstit].HeaderCell.Value = ConstitNames[iConstit];
                dgvSepticDischQual.Rows[iConstit].Cells[0].Value =
                    Global.coe.septic.type1[iConstit].ToString();
                dgvSepticDischQual.Rows[iConstit].Cells[1].Value =
                    Global.coe.septic.type2[iConstit].ToString();
                dgvSepticDischQual.Rows[iConstit].Cells[2].Value =
                    Global.coe.septic.type3[iConstit].ToString();
            }
            FormatDataGridView(dgvTrunkComp);

            //Minerals
            for (int ii = 0; ii < Global.coe.numMinerals; ii++)
            {
                cbMinerals.Items.Add(Global.coe.minerals[ii].name.ToString());
            }
            dgvRxnProducts.Columns.Add("Value", "Value");
            for (int iConstit = 0; iConstit < Global.coe.numChemicalParams; iConstit++)
            {
                dgvRxnProducts.Rows.Insert(iConstit);
                dgvRxnProducts.Rows[iConstit].HeaderCell.Value = ConstitNames[iConstit];
            }
            FormatDataGridView(dgvRxnProducts);
            cbMinerals.SelectedIndex = 0;
            tbMolWeight.Text = Global.coe.minerals[0].molecularWgt.ToString();
            tbWeatherRate.Text = Global.coe.minerals[0].weatheringRate.ToString();
            tbPHdepend.Text = Global.coe.minerals[0].phDepend.ToString();
            tbOconsumption.Text = Global.coe.minerals[0].oxyConsumption.ToString();

            //Sediment (This is not truly dynamic, by the way. There is no name for each
            //sediment class in the coe file, so row header cells are hard coded)
            dgvSediment.Columns.Add("Size", "Size (mm)");
            dgvSediment.Columns.Add("SGrav", "Specific Gravity");
            dgvSediment.Rows.Add(3);
            dgvSediment.Rows[0].HeaderCell.Value = "Clay";
            dgvSediment.Rows[1].HeaderCell.Value = "Silt";
            dgvSediment.Rows[2].HeaderCell.Value = "Sand";
            for (int ii = 0; ii < Global.coe.numSedParticleSizes; ii++)
            {
                dgvSediment.Rows[ii].Cells[0].Value =
                    Global.coe.sediments[ii].grainSize.ToString();
                dgvSediment.Rows[ii].Cells[1].Value =
                    Global.coe.sediments[ii].specGravity.ToString();
            }
            FormatDataGridView(dgvRxnProducts);

            //Phytoplankton (This is not truly dynamic. There is no name for each
            //algae class in the coe file, so column header cells are hard coded)
            //algae 0 = Blue-Green; algae 1 = Diatoms; algae 2 = Green
            dgvPhytoplankton.Columns.Add("BG", "Blue-Green Algae");
            dgvPhytoplankton.Columns.Add("Diatoms", "Diatoms");
            dgvPhytoplankton.Columns.Add("Green", "Green Algae");
            dgvPhytoplankton.Rows.Add(7);
            dgvPhytoplankton.Rows[0].HeaderCell.Value = "Nitrogen Half-Saturation (mg/L)";
            dgvPhytoplankton.Rows[1].HeaderCell.Value = "Phosphorus Half-Saturation (mg/L)";
            dgvPhytoplankton.Rows[2].HeaderCell.Value = "Silica Half-Saturation (mg/L)";
            dgvPhytoplankton.Rows[3].HeaderCell.Value = "Light Saturation (W/m2)";
            dgvPhytoplankton.Rows[4].HeaderCell.Value = "Lower Growth Temperature (C)";
            dgvPhytoplankton.Rows[5].HeaderCell.Value = "Upper Growth Temperature (C)";
            dgvPhytoplankton.Rows[6].HeaderCell.Value = "Optimum Growth Temperature (C)";
            for (int ii = 0; ii < dgvPhytoplankton.Columns.Count; ii++)
            {
                dgvPhytoplankton.Rows[0].Cells[ii].Value = Global.coe.algaes[ii].nitroHalfSat.ToString();
                dgvPhytoplankton.Rows[1].Cells[ii].Value = Global.coe.algaes[ii].phosHalfSat.ToString();
                dgvPhytoplankton.Rows[2].Cells[ii].Value = Global.coe.algaes[ii].silicaHalfSat.ToString();
                dgvPhytoplankton.Rows[3].Cells[ii].Value = Global.coe.algaes[ii].lightSat.ToString();
                dgvPhytoplankton.Rows[4].Cells[ii].Value = Global.coe.algaes[ii].lowTempLimit.ToString();
                dgvPhytoplankton.Rows[5].Cells[ii].Value = Global.coe.algaes[ii].highTempLimit.ToString();
                dgvPhytoplankton.Rows[6].Cells[ii].Value = Global.coe.algaes[ii].optGrowTemp.ToString();
            }
            FormatDataGridView(dgvPhytoplankton);

            //Periphyton
            tbEndRespCoeff.Text = Global.coe.peri.endoRespirationCoef.ToString();
            tbEndRespExpCoeff.Text = Global.coe.peri.endoRespirationExp.ToString();
            tbPhotoRespFract.Text = Global.coe.peri.photoRespirationFraction.ToString();
            tbRecycledFract.Text = Global.coe.peri.recycledFraction.ToString();
            tbSpatHalfSat.Text = Global.coe.peri.spatialLimitHalfSat.ToString();
            tbSpatLimitIntcpt.Text = Global.coe.peri.spatialLimitHalfSat.ToString();
            tbSpatLimitIntcpt.Text = Global.coe.peri.spatialLimitIntercept.ToString();
            tbScourRegCoeff.Text = Global.coe.peri.scourRegressionCoef.ToString();
            tbScourRegExp.Text = Global.coe.peri.scourRegressionExp.ToString();
            tbNhalfSat.Text = Global.coe.peri.nitroHalfSat.ToString();
            tbPhalfSat.Text = Global.coe.peri.phosHalfSat.ToString();
            tbVelHalfSat.Text = Global.coe.peri.velocityHalfSat.ToString();
            tbLightHalfSat.Text = Global.coe.peri.lightSat.ToString();
            //tbChlCarbonRatio.Text = Global.coe.peri.??
            tbAmmPrefCoeff.Text = Global.coe.peri.ammoniaPref.ToString();

            //Food Web
            //Needs to be added later, using an example that has food web dynamics activated

            //Parameters
            //Names
            int iCount = 0;
            dgvNames.Columns.Add("abbrev", "Abbreviation");
            dgvNames.Columns.Add("fortcode", "Fortran Code");
            dgvNames.Columns.Add("units", "units");
            for (int ii = 0; ii < Global.coe.numHydrologyParams; ii++)
            {
                dgvNames.Rows.Insert(iCount);
                dgvNames.Rows[iCount].HeaderCell.Value = Global.coe.hydroConstits[ii].fullName.ToString();
                dgvNames.Rows[iCount].Cells[0].Value = Global.coe.hydroConstits[ii].abbrevName.ToString();
                dgvNames.Rows[iCount].Cells[1].Value = Global.coe.hydroConstits[ii].fortranCode.ToString();
                dgvNames.Rows[iCount].Cells[2].Value = Global.coe.hydroConstits[ii].units.ToString();
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvNames.Rows.Insert(iCount);
                dgvNames.Rows[iCount].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                dgvNames.Rows[iCount].Cells[0].Value = Global.coe.chemConstits[ii].abbrevName.ToString();
                dgvNames.Rows[iCount].Cells[1].Value = Global.coe.chemConstits[ii].fortranCode.ToString();
                dgvNames.Rows[iCount].Cells[2].Value = Global.coe.chemConstits[ii].units.ToString();
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvNames.Rows.Insert(iCount);
                dgvNames.Rows[iCount].HeaderCell.Value = Global.coe.physicalConstits[ii].fullName.ToString();
                dgvNames.Rows[iCount].Cells[0].Value = Global.coe.physicalConstits[ii].abbrevName.ToString();
                dgvNames.Rows[iCount].Cells[1].Value = Global.coe.physicalConstits[ii].fortranCode.ToString();
                dgvNames.Rows[iCount].Cells[2].Value = Global.coe.physicalConstits[ii].units.ToString();
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numCompositeParams; ii++)
            {
                dgvNames.Rows.Insert(iCount);
                dgvNames.Rows[iCount].HeaderCell.Value = Global.coe.compositeConstits[ii].fullName.ToString();
                dgvNames.Rows[iCount].Cells[0].Value = Global.coe.compositeConstits[ii].abbrevName.ToString();
                dgvNames.Rows[iCount].Cells[1].Value = Global.coe.compositeConstits[ii].fortranCode.ToString();
                dgvNames.Rows[iCount].Cells[2].Value = Global.coe.compositeConstits[ii].units.ToString();
                iCount = iCount + 1;
            }
            FormatDataGridView(dgvNames);
            //Output Control
            iCount = 0;
            for (int ii = 0; ii < Global.coe.numHydrologyParams; ii++)
            {
                dgvOutputControl.Rows.Insert(iCount);
                dgvOutputControl.Rows[iCount].HeaderCell.Value = Global.coe.hydroConstits[ii].fullName.ToString();
                dgvOutputControl.Rows[iCount].Cells[1].Value = Global.coe.hydroConstits[ii].swCatchmentInclude;
                dgvOutputControl.Rows[iCount].Cells[2].Value = Global.coe.hydroConstits[ii].swRiverInclude;
                dgvOutputControl.Rows[iCount].Cells[3].Value = Global.coe.hydroConstits[ii].swReservoirInclude;
                dgvOutputControl.Rows[iCount].Cells[4].Value = Global.coe.hydroConstits[ii].swLoadingInclude;
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvOutputControl.Rows.Insert(iCount);
                dgvOutputControl.Rows[iCount].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                dgvOutputControl.Rows[iCount].Cells[1].Value = Global.coe.chemConstits[ii].swCatchmentInclude;
                dgvOutputControl.Rows[iCount].Cells[2].Value = Global.coe.chemConstits[ii].swRiverInclude;
                dgvOutputControl.Rows[iCount].Cells[3].Value = Global.coe.chemConstits[ii].swReservoirInclude;
                dgvOutputControl.Rows[iCount].Cells[4].Value = Global.coe.chemConstits[ii].swLoadingInclude;
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvOutputControl.Rows.Insert(iCount);
                dgvOutputControl.Rows[iCount].HeaderCell.Value = Global.coe.physicalConstits[ii].fullName.ToString();
                dgvOutputControl.Rows[iCount].Cells[1].Value = Global.coe.physicalConstits[ii].swCatchmentInclude;
                dgvOutputControl.Rows[iCount].Cells[2].Value = Global.coe.physicalConstits[ii].swRiverInclude;
                dgvOutputControl.Rows[iCount].Cells[3].Value = Global.coe.physicalConstits[ii].swReservoirInclude;
                dgvOutputControl.Rows[iCount].Cells[4].Value = Global.coe.physicalConstits[ii].swLoadingInclude;
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numCompositeParams; ii++)
            {
                dgvOutputControl.Rows.Insert(iCount);
                dgvOutputControl.Rows[iCount].HeaderCell.Value = Global.coe.compositeConstits[ii].fullName.ToString();
                dgvOutputControl.Rows[iCount].Cells[1].Value = Global.coe.compositeConstits[ii].swCatchmentInclude;
                dgvOutputControl.Rows[iCount].Cells[2].Value = Global.coe.compositeConstits[ii].swRiverInclude;
                dgvOutputControl.Rows[iCount].Cells[3].Value = Global.coe.compositeConstits[ii].swReservoirInclude;
                dgvOutputControl.Rows[iCount].Cells[4].Value = Global.coe.compositeConstits[ii].swLoadingInclude;
                iCount = iCount + 1;
            }
            FormatDataGridView(dgvOutputControl);
            //Physical Data (DGV Columns created in Dialog Designer)
            ((DataGridViewComboBoxColumn)dgvPhysicalData.Columns[2]).Items.Add("Particulate");
            ((DataGridViewComboBoxColumn)dgvPhysicalData.Columns[2]).Items.Add("Gaseous - Nutrient");
            ((DataGridViewComboBoxColumn)dgvPhysicalData.Columns[2]).Items.Add("Gaseous - Pollutant");
            iCount = 0;
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvPhysicalData.Rows.Insert(iCount);
                dgvPhysicalData.Rows[iCount].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                dgvPhysicalData.Rows[iCount].Cells[0].Value = Global.coe.chemConstits[ii].electricalCharge.ToString();
                dgvPhysicalData.Rows[iCount].Cells[1].Value = Global.coe.chemConstits[ii].massEquivalent.ToString();
                if (Global.coe.chemConstits[ii].fullName == "pH")
                {
                    DataGridViewTextBoxCell c = new DataGridViewTextBoxCell();
                    c.Value = "";
                    DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                    c2.Value = "";
                    dgvPhysicalData.Rows[iCount].Cells[2] = c;
                    dgvPhysicalData.Rows[iCount].Cells[2].ReadOnly = true;
                    dgvPhysicalData.Rows[iCount].Cells[3] = c2;
                    dgvPhysicalData.Rows[iCount].Cells[3].ReadOnly = true;
                }
                else
                {
                    if (Global.coe.chemConstits[ii].dryDepositionForm == 0)
                    {
                        dgvPhysicalData.Rows[iCount].Cells[2].Value = "Particulate";
                    }
                    else if (Global.coe.chemConstits[ii].dryDepositionForm == 1)
                    {
                        dgvPhysicalData.Rows[iCount].Cells[2].Value = "Gaseous - Nutrient";
                    }
                    else
                    {
                        dgvPhysicalData.Rows[iCount].Cells[2].Value = "Gaseous - Pollutant";
                    }
                    dgvPhysicalData.Rows[iCount].Cells[3].Value = Global.coe.chemConstits[ii].swChemAdvection;
                }
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvPhysicalData.Rows.Insert(iCount);
                dgvPhysicalData.Rows[iCount].HeaderCell.Value = Global.coe.physicalConstits[ii].fullName.ToString();
                dgvPhysicalData.Rows[iCount].Cells[0].Value = Global.coe.physicalConstits[ii].electricalCharge.ToString();
                dgvPhysicalData.Rows[iCount].Cells[1].Value = Global.coe.physicalConstits[ii].massEquivalent.ToString();
                if (Global.coe.chemConstits[ii].dryDepositionForm == 0)
                {
                    dgvPhysicalData.Rows[iCount].Cells[2].Value = "Particulate";
                }
                else if (Global.coe.chemConstits[ii].dryDepositionForm == 1)
                {
                    dgvPhysicalData.Rows[iCount].Cells[2].Value = "Gaseous - Nutrient";
                }
                else
                {
                    dgvPhysicalData.Rows[iCount].Cells[2].Value = "Gaseous - Pollutant";
                }
                dgvPhysicalData.Rows[iCount].Cells[3].Value = Global.coe.physicalConstits[ii].swChemAdvection;
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numCompositeParams; ii++)
            {
                dgvPhysicalData.Rows.Insert(iCount);
                dgvPhysicalData.Rows[iCount].HeaderCell.Value = Global.coe.compositeConstits[ii].fullName.ToString();
                dgvPhysicalData.Rows[iCount].Cells[0].Value = Global.coe.compositeConstits[ii].electricalCharge.ToString();
                dgvPhysicalData.Rows[iCount].Cells[1].Value = Global.coe.compositeConstits[ii].massEquivalent.ToString();
                DataGridViewTextBoxCell c = new DataGridViewTextBoxCell();
                c.Value = "";
                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                c2.Value = "";
                dgvPhysicalData.Rows[iCount].Cells[2] = c;
                dgvPhysicalData.Rows[iCount].Cells[2].ReadOnly = true;
                dgvPhysicalData.Rows[iCount].Cells[3] = c2;
                dgvPhysicalData.Rows[iCount].Cells[3].ReadOnly = true;
                iCount = iCount + 1;
            }
            FormatDataGridView(dgvPhysicalData);
            //Hydroxide Solubility
            dgvHydroxideSolubility.Columns.Add("SolProd", "Solubility Product");
            dgvHydroxideSolubility.Columns.Add("Cation", "Cation Valence");
            dgvHydroxideSolubility.Columns.Add("Anion", "Anion Valence");
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvHydroxideSolubility.Rows.Insert(ii);
                dgvHydroxideSolubility.Rows[ii].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                if (Global.coe.chemConstits[ii].solubWithHydrox != -999)
                {
                    dgvHydroxideSolubility.Rows[ii].Cells[0].Value = Global.coe.chemConstits[ii].solubWithHydrox.ToString();
                }
                dgvHydroxideSolubility.Rows[ii].Cells[1].Value = Global.coe.chemConstits[ii].stoichChemWithHydrox.ToString();
                dgvHydroxideSolubility.Rows[ii].Cells[2].Value = Global.coe.chemConstits[ii].stoichHydroxWithChem.ToString();
            }
            FormatDataGridView(dgvHydroxideSolubility);
            //Sulfate Solubility
            dgvSulfateSolubility.Columns.Add("SolProd", "Solubility Product");
            dgvSulfateSolubility.Columns.Add("Cation", "Cation Valence");
            dgvSulfateSolubility.Columns.Add("Anion", "Anion Valence");
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvSulfateSolubility.Rows.Insert(ii);
                dgvSulfateSolubility.Rows[ii].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                if (Global.coe.chemConstits[ii].solubWithSulfate != -999)
                {
                    dgvSulfateSolubility.Rows[ii].Cells[0].Value = Global.coe.chemConstits[ii].solubWithSulfate.ToString();
                }
                dgvSulfateSolubility.Rows[ii].Cells[1].Value = Global.coe.chemConstits[ii].stoichChemWithSulfate.ToString();
                dgvSulfateSolubility.Rows[ii].Cells[2].Value = Global.coe.chemConstits[ii].stoichSulfateWithChem.ToString();
            }
            FormatDataGridView(dgvSulfateSolubility);
            //Multipliers
            dgvMultipliers.Columns.Add("ptSrc", "Point Sources");
            dgvMultipliers.Columns.Add("nonPtSrc", "Nonpoint Sources");
            dgvMultipliers.Columns.Add("atmDep", "Atmospheric Deposition");
            iCount = 0;
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvMultipliers.Rows.Insert(ii);
                dgvMultipliers.Rows[ii].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                dgvMultipliers.Rows[ii].Cells[0].Value = Global.coe.chemConstits[ii].pointSourceMult.ToString();
                dgvMultipliers.Rows[ii].Cells[1].Value = Global.coe.chemConstits[ii].nonpointSourceMult.ToString();
                dgvMultipliers.Rows[ii].Cells[2].Value = Global.coe.chemConstits[ii].airRainMult.ToString();
                iCount = iCount + 1;
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvMultipliers.Rows.Insert(iCount);
                dgvMultipliers.Rows[iCount].HeaderCell.Value = Global.coe.physicalConstits[ii].fullName.ToString();
                dgvMultipliers.Rows[iCount].Cells[0].Value = Global.coe.physicalConstits[ii].pointSourceMult.ToString();
                dgvMultipliers.Rows[iCount].Cells[1].Value = Global.coe.physicalConstits[ii].nonpointSourceMult.ToString();
                dgvMultipliers.Rows[iCount].Cells[2].Value = Global.coe.physicalConstits[ii].airRainMult.ToString();
                iCount = iCount + 1;
            }
            FormatDataGridView(dgvMultipliers);
            //Composition
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvComposition.Columns.Add(Global.coe.chemConstits[ii].abbrevName.ToString(),
                    Global.coe.chemConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvComposition.Columns.Add(Global.coe.physicalConstits[ii].abbrevName.ToString(),
                    Global.coe.physicalConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numCompositeParams; ii++)
            {
                dgvComposition.Rows.Insert(ii);
                dgvComposition.Rows[ii].HeaderCell.Value = Global.coe.compositeConstits[ii].fullName.ToString();

                for (int constit = 0; constit < (Global.coe.numChemicalParams + Global.coe.numPhysicalParams); constit++)
                {
                    dgvComposition.Rows[ii].Cells[constit].Value = Global.coe.compositeConstits[ii].componentTotalMass[constit].ToString();
                }
            }
            FormatDataGridView(dgvComposition);
            //Reactions
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                ((DataGridViewComboBoxColumn)dgvReactions.Columns[0]).
                    Items.Add(Global.coe.chemConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                ((DataGridViewComboBoxColumn)dgvReactions.Columns[0]).
                    Items.Add(Global.coe.physicalConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numReactions; ii++)
            {
                ((DataGridViewComboBoxColumn)dgvReactions.Columns[5]).
                    Items.Add(Global.coe.reactions[ii].name);
                dgvReactions.Rows.Insert(ii);
                dgvReactions.Rows[ii].HeaderCell.Value = Global.coe.reactions[ii].name;
                if (Global.coe.reactions[ii].primReactantNumber > Global.coe.numChemicalParams)
                {
                    int constit = Global.coe.reactions[ii].primReactantNumber - Global.coe.numChemicalParams;
                    dgvReactions.Rows[ii].Cells[0].Value = Global.coe.physicalConstits[constit - 1].fullName;
                }
                else
                {
                    int constit = Global.coe.reactions[ii].primReactantNumber;
                    dgvReactions.Rows[ii].Cells[0].Value = Global.coe.chemConstits[constit - 1].fullName;
                }
                dgvReactions.Rows[ii].Cells[1].Value = Global.coe.reactions[ii].swIsAnoxic;
                dgvReactions.Rows[ii].Cells[2].Value = Global.coe.reactions[ii].dissolvedOxyLimit.ToString();
                dgvReactions.Rows[ii].Cells[3].Value = Global.coe.reactions[ii].swIsUVCatalysis;
                dgvReactions.Rows[ii].Cells[4].Value = Global.coe.reactions[ii].tempCorrectCoeff.ToString();
                int linkRxn = Global.coe.reactions[ii].numLinkedReactions;
                if (linkRxn != 0)
                {
                    dgvReactions.Rows[ii].Cells[5].Value = Global.coe.reactions[linkRxn].name;
                }
                
            }
            FormatDataGridView(dgvReactions);
            //Reaction Products
            for (int ii = 0; ii < Global.coe.numChemicalParams; ii++)
            {
                dgvReactionProds.Columns.Add(Global.coe.chemConstits[ii].abbrevName.ToString(),
                    Global.coe.chemConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numPhysicalParams; ii++)
            {
                dgvReactionProds.Columns.Add(Global.coe.physicalConstits[ii].abbrevName.ToString(),
                    Global.coe.physicalConstits[ii].fullName.ToString());
            }
            for (int ii = 0; ii < Global.coe.numReactions; ii++)
            {
                dgvReactionProds.Rows.Insert(ii);
                dgvReactionProds.Rows[ii].HeaderCell.Value = Global.coe.reactions[ii].name.ToString();

                for (int constit = 0; constit < (Global.coe.numChemicalParams + Global.coe.numPhysicalParams); constit++)
                {
                    dgvReactionProds.Rows[ii].Cells[constit].Value = Global.coe.reactions[ii].stoich[constit];
                }
            }
            FormatDataGridView(dgvReactionProds);
            //Gaseous Deposition Velocity
            AddMonthColumns(dgvGasDepVel);
            for (int ii = 0; ii < 2; ii++)
            {
                dgvGasDepVel.Rows.Insert(ii);
                dgvGasDepVel.Rows[ii].HeaderCell.Value = Global.coe.chemConstits[ii].fullName.ToString();
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgvGasDepVel.Rows[ii].Cells[iMonth].Value = Global.coe.chemConstits[ii].gasDepositVelocity[iMonth].ToString();
                }
            }
            FormatDataGridView(dgvGasDepVel);
            dgvNames.BringToFront();
            cbParameters.SelectedIndex = 0;
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

        public void AddMonthColumns(DataGridView dgv)
        {
            dgv.Columns.Add("Jan", "January");
            dgv.Columns.Add("Feb", "February");
            dgv.Columns.Add("Mar", "March");
            dgv.Columns.Add("Apr", "April");
            dgv.Columns.Add("May", "May");
            dgv.Columns.Add("Jun", "June");
            dgv.Columns.Add("Jul", "July");
            dgv.Columns.Add("Aug", "August");
            dgv.Columns.Add("Sep", "September");
            dgv.Columns.Add("Oct", "October");
            dgv.Columns.Add("Nov", "November");
            dgv.Columns.Add("Dec", "December");
        }

        private void cbMinerals_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ii = cbMinerals.SelectedIndex;
            tbMolWeight.Text = Global.coe.minerals[ii].molecularWgt.ToString();
            tbWeatherRate.Text = Global.coe.minerals[ii].weatheringRate.ToString();
            tbPHdepend.Text = Global.coe.minerals[ii].phDepend.ToString();
            tbOconsumption.Text = Global.coe.minerals[ii].oxyConsumption.ToString();
            for (int iConstit = 0; iConstit < Global.coe.numChemicalParams; iConstit++)
            {
                dgvRxnProducts.Rows[iConstit].Cells[0].Value =
                    Global.coe.minerals[ii].chemReactionProduct[iConstit].ToString();
            }
            FormatDataGridView(dgvRxnProducts);
        }

        private void cbParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ii = cbParameters.SelectedIndex;
            if (ii == 0) { dgvNames.BringToFront(); } //Names
            else if (ii == 1) { dgvOutputControl.BringToFront(); } //Output Control
            else if (ii == 2) { dgvPhysicalData.BringToFront(); } //Physical Data
            else if (ii == 3) { dgvHydroxideSolubility.BringToFront(); } //Hydroxide Solubility
            else if (ii == 4) { dgvSulfateSolubility.BringToFront(); } //Sulfate Solubility
            else if (ii == 5) { dgvMultipliers.BringToFront(); } //Multipliers
            else if (ii == 6) { dgvComposition.BringToFront(); } //Composition
            else if (ii == 7) { dgvReactions.BringToFront(); } //Reactions
            else if (ii == 8) { dgvReactionProds.BringToFront(); } //Reaction Products
            else { dgvGasDepVel.BringToFront(); } //Gaseous Deposition Velocity
        }
    }
}
