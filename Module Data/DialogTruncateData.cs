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
    public partial class TruncateData : Form
    {
        public TruncateData()
        {
            InitializeComponent();
        }

        public bool TruncateBefore()
        {
            return TruncateDataBeforeCheckBox.Checked;
        }
        public bool TruncateAfter()
        {
            return TruncateDataAfterCheckBox.Checked;
        }
        public DateTime GetTruncateBeforeDate()
        {
            return TruncateBeforeDate.Value.Date;
        }
        public DateTime GetTruncateAfterDate()
        {
            return TruncateAfterDate.Value.Date;
        }
    }
}
