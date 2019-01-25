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

        }

            private void btnHelp_Click(object sender, EventArgs e)
        {
            // Launch browser to http://warmf.com/...
            System.Diagnostics.Process.Start("http://warmf.com/home/index.php/engineering-module/system/");
        }
    }
}
