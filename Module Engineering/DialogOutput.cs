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
    public partial class DialogOutput : Form
    {
        FormMain parent;

        public DialogOutput(FormMain par)
        {
            InitializeComponent();
            this.parent = par;
        }

        public void Populate(string featureType, int cnum)
        {
            if (featureType == "River")
            {
                Text = featureType + " " + Global.coe.rivers[cnum].idNum + " Output";
            }
            else if (featureType == "Catchment")
            {
                Text = featureType + " " + Global.coe.catchments[cnum].idNum + " Output";
            }
            
        }
    }
}
