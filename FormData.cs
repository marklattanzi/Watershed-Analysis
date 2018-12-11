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
	public partial class FormData : Form {

		FormMain parent;

		public FormData(FormMain par) {
			InitializeComponent();
			this.parent = par;
		}

		private void toolChart_Click(object sender, EventArgs e) {

		}

		private void miDataEngr_Click(object sender, EventArgs e) {
			parent.showForm("engr");
		}

		private void miDataKnow_Click(object sender, EventArgs e) {
			parent.showForm("know");
		}
	}
}
