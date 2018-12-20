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
	public partial class FormTMDL : Form {
		FormMain parent;
		public FormTMDL(FormMain par) {
			InitializeComponent();
			this.parent = par;
		}

		private void miExit_Click(object sender, EventArgs e) {
			parent.ShowForm("engineering");
		}

		private void miData_Click(object sender, EventArgs e) {
			parent.ShowForm("data");
		}

		private void miKnowledge_Click(object sender, EventArgs e) {
			parent.ShowForm("knowledge");
		}

		private void miManager_Click(object sender, EventArgs e) {
			parent.ShowForm("manager");
		}

		private void miTMDL_Click(object sender, EventArgs e) {
			parent.ShowForm("tmdl");
		}

		private void miConsensus_Click(object sender, EventArgs e) {
			parent.ShowForm("consensus");
		}

		private void miEngineering_Click(object sender, EventArgs e) {
			parent.ShowForm("engineering");
		}
	}
}
