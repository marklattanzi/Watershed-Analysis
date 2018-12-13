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
            //Physical Data tab
            tbName.Text = catchment.name.ToString();
            tbCatchID.Text = catchment.idNum.ToString();
            //tbArea.Text = catchment.
            tbWidth.Text = catchment.width.ToString();
            tbAspect.Text = catchment.aspect.ToString();
            tbSlope.Text = catchment.slope.ToString();

            //Meteorology tab
            tbMetFile.Text = Global.coe.METFilename.ElementAt(catchment.METFileNum);
            tbPrecipWeight.Text = catchment.precipMultiplier.ToString();
            tbTempLapse.Text = catchment.aveTempLapse.ToString();
            tbAltLapse.Text = catchment.altitudeTempLapse.ToString();
            //tbAirFile.Text = Global.coe.AIRFilename.ElementAt(catchment.airRainChemFileNum);
            //cant find the coarse particle air chem file variable tbPartAir.Text = Global.coe.(catchment.particleRainChemFileNum);

            //Land Uses tab
            for (int ii = 0; ii < Global.coe.numLanduses; ii++)
            {
                
                string Percent = catchment.landUsePercent.ElementAt(ii).ToString();
                //Struggling with the following assignment - It doesn't appear that values are being saved in the landuse list
                //but I can't figure out how to solve it...
                string luName = Global.coe.landuse.ElementAt(ii).name;
                MessageBox.Show(luName.ToString()); //, ": ", percent.ToString());
                MessageBox.Show(Percent.ToString());
                
            }
        }
    }
}
