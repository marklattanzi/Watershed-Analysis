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
    }
}
