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
            //Physical Data
            tbLatitude.Text = Global.coe.latitude.ToString();
            tbLongitude.Text = Global.coe.longitude.ToString();
            tbElevation.Text = Global.coe.watershedElevation.ToString();
            tbArea.Text = Global.coe.watershedArea.ToString();
        
            //Land Uses

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

            }
            else if (ii == 1) //Rainfall Detachment Factor
            {

            }
            else if (ii == 2) //Flow Detachment Factor
            {

            }
            else if (ii == 3) //Fraction Impervious
            {

            }
            else if (ii == 4) //Interception Storage
            {

            }
            else if (ii == 5) //Long-term Growth
            {

            }
            else if (ii == 6) //Leaf Growth Factor
            {

            }
            else if (ii == 7) //Productivity
            {

            }
            else if (ii == 8) //Active Respiration
            {

            }
            else if (ii == 9) //Maintenance Respiration
            {

            }
            else if (ii == 10) //Dry Collection Efficiency
            {

            }
            else if (ii == 11) //Wet Collection Efficiency
            {

            }
            else if (ii == 12) //Leaf Weight/Area
            {

            }
            else if (ii == 13) //Canopy Height
            {

            }
            else if (ii == 14) //Stomatal Resistance
            {

            }
            else if (ii == 15) //Cropping Factor
            {

            }
            else if (ii == 16) //Leaf Area Index
            {

            }
            else if (ii == 17) //Annual Uptake Distribution
            {

            }
            else if (ii == 18) //Litter Fall Rate
            {

            }
            else if (ii == 19) //Exudation Rate
            {

            }
            else if (ii == 20) //Leaf Composion
            {

            }
            else //Trunk Composion
            {

            }
        }
    }
}
