using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warmf {
	public partial class FormKnowledge : Form {
		private FormMain parent;

		public FormKnowledge(FormMain par) {
			InitializeComponent();
			this.parent = par;
		}

		private void miKnowEngr_Click(object sender, EventArgs e) {
			parent.showForm("engr");
		}

		private void miKnowData_Click(object sender, EventArgs e) {
			parent.showForm("data");
		}
	}
}
