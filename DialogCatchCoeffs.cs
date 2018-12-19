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
               landuselist.Add (Global.coe.landuse.ElementAt(ii).name.ToString());
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
            tbArea.Text = catchment.soils.ElementAt(1).area.ToString();
            tbWidth.Text = catchment.width.ToString();
            tbAspect.Text = catchment.aspect.ToString();
            tbSlope.Text = catchment.slope.ToString();
            tbDetention.Text = catchment.detentionStorage.ToString();
            tbRoughness.Text = catchment.ManningN.ToString();

            //Meteorology tab
            tbMetFile.Text = Global.coe.METFilename.ElementAt(catchment.METFileNum);
            tbPrecipWeight.Text = catchment.precipMultiplier.ToString();
            tbTempLapse.Text = catchment.aveTempLapse.ToString();
            tbAltLapse.Text = catchment.altitudeTempLapse.ToString();
            tbAirFile.Text = Global.coe.AIRFilename.ElementAt(catchment.airRainChemFileNum - 1);
            //tbPartAir.Text = Global.coe.//(catchment.particleRainChemFileNum);

            //Land Uses tab
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                string Percent = catchment.landUsePercent.ElementAt(ii).ToString("F2");
                string luName = Global.coe.landuse.ElementAt(ii).name;
                dgLanduse.Rows.Insert(ii, luName, Percent);
            }

            //Land Application tab
            cbLanduse.Items.Clear();
            cbLanduse.Items.AddRange(landuselist.ToArray());
            cbLanduse.SelectedIndex = 7;

            int iFertPlanNum = catchment.fertPlanNum[cbLanduse.SelectedIndex];
            int iNumParams = Global.coe.numChemicalParams + Global.coe.numPhysicalParams;

            for (int iParam = 0; iParam < iNumParams; iParam++)
            {
                dgLandApp.Rows.Insert(iParam);
                string NameUnit = ConstitsList[iParam].ToString() + " (" + UnitsList[iParam].ToString().Trim() + ")";
                dgLandApp.Rows[iParam].HeaderCell.Value = NameUnit.ToString();
                for (int iMonth = 0; iMonth < 12; iMonth++)
                {
                    dgLandApp.Rows[iParam].Cells[iMonth].Value = Global.coe.landuse[cbLanduse.SelectedIndex].fertPlanApplication[iFertPlanNum][iMonth][iParam];
                }
            }
            
            //Irrigation tab

            //Sediment tab

            //BMP's tab

            //Point Sources tab

            //Pumping tab

            //Septic Systems tab

            //Reactions tab

            //Soil Layers tab

            //Mining tab

            //CE-QUAL-W2 tab

        }
    }
}
