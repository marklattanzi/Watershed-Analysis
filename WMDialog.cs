using System;
using System.Windows.Forms;

namespace warmf {

    public class WMDialog : Form {
		public int Result { get; set; } // -1 = cancel; 0 = no; 1 = yes
		public bool bResult { get; set; } // true = ok/yes, false = no/cancel
		Button btnYesOk;
		Button btnNo;
		Button btnCancel;
        Label msg;

		// dialog box with 1-3 buttons
		// if there is a third "cancel" third button, there must be a second "no" button
		public WMDialog(string txtTitle, string message, bool showNo = true, bool showCancel = false) {
			Result = -1;
			bResult = false;
			this.Width = 315;
            this.Height = 200;
			int btnWidth = 70;
			int gap = 20;

			msg = new Label();
            msg.Text = message;
            msg.AutoSize = true;
            msg.ForeColor = System.Drawing.Color.Black;
            msg.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Controls.Add(msg);
            msg.Left = (this.ClientSize.Width - msg.Width) / 2;
            msg.Top = 20;

            btnYesOk = new Button {
                Text = "OK",
                Top = this.ClientSize.Height - 40,
                Left = (this.ClientSize.Width - btnWidth) / 2,
                Width = btnWidth,
                Height = 25
            };
            this.Controls.Add(btnYesOk);
            btnYesOk.Click += BtnOk_Click;

			btnNo = new Button {
				Text = "No",
				Top = this.ClientSize.Height - 40,
				Left = btnWidth + gap * 2,
				Width = btnWidth,
				Height = 25
			};

			btnCancel = new Button {
				Text = "Cancel",
				Top = this.ClientSize.Height - 40,
				Left = btnWidth * 3,
				Width = 70,
				Height = 25
			};

			if (showNo) {
				btnYesOk.Left = this.ClientSize.Width / 2 - btnWidth - gap/2;
				btnNo.Left = this.ClientSize.Width / 2 + gap/2;
				this.Controls.Add(btnNo);
				btnNo.Click += BtnNo_Click;
			}

			if (showCancel) {
                this.Controls.Add(btnCancel);
				btnCancel.Click += BtnCancel_Click;
//				if (showNo) {
					gap = (this.ClientSize.Width - btnWidth*3) / 4;
					btnYesOk.Left = gap;
					btnNo.Left = 70 + gap*2;
					btnCancel.Left = btnWidth*2 + gap*3;
// this is for a "cancel" button without a "no" button - which seems silly
//				} else {
//					btnYesOk.Left = this.ClientSize.Width / 2 - btnWidth - gap / 2;
//					btnCancel.Left = this.ClientSize.Width / 2 + gap / 2;
//				}
			}

			this.Text = txtTitle;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

		public void setLabels(string lblYes, string lblNo, string lblCancel = "Cancel") {
			btnYesOk.Text = lblYes;
			btnNo.Text = lblNo;
			btnCancel.Text = lblCancel;
		}

        private void BtnOk_Click(object sender, EventArgs e) {
			Result = 1;
			this.Close();
        }

		private void BtnNo_Click(object sender, EventArgs e) {
			Result = 0;
			this.Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e) {
			Result = -1;
			this.Close();
		}

		public void SetTextColor(System.Drawing.Color c) {
            msg.ForeColor = c;
        }
    }
}
