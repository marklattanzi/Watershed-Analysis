using System;
using System.Windows.Forms;

namespace warmf {
	// a custom dialog box with a single msg and 1,2, or 3 buttons (Yes,No,Cancel)
	// if there is a third "cancel" third button, there *should* be a second "no" button
	// for OK/Cancel. use 2 buttons and change the labels
	public class WMDialog : Form {
		public int Result { get; private set; } // -1 = cancel; 0 = no; 1 = yes
		public bool BResult { get; private set; } // true = ok/yes, false = no/cancel
		
		private Button btnYesOk;
		private Button btnNo;
		private Button btnCancel;
        private Label msg;
		private int btnWidth;
		private int boxWidth, boxHeight;	//unused, for adding size change functionality
		private int gap;

		public WMDialog(string txtTitle, string message, bool showNo = true, bool showCancel = false) {
			Result = -1;
			BResult = false;
			this.Width = 315;
            this.Height = 200;
			btnWidth = 70;
			gap = 20;

			msg = new Label();
            msg.Text = message;
            msg.AutoSize = true;
            msg.ForeColor = System.Drawing.Color.Black;
            msg.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Controls.Add(msg);
            msg.Left = (this.ClientSize.Width - msg.Width) / 2;
            msg.Top = 20;

            btnYesOk = new Button {
                Name = "yes",
				Text = "OK",
                Top = this.ClientSize.Height - 40,
                Left = (this.ClientSize.Width - btnWidth) / 2,
                Width = btnWidth,
                Height = 25
            };
            this.Controls.Add(btnYesOk);
            btnYesOk.Click += Btn_Click;

			btnNo = new Button {
				Name="no",
				Text = "No",
				Top = this.ClientSize.Height - 40,
				Left = btnWidth + gap * 2,
				Width = btnWidth,
				Height = 25
			};

			btnCancel = new Button {
				Name = "cancel",
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
				btnNo.Click += Btn_Click;
			}

			if (showCancel) {
                this.Controls.Add(btnCancel);
				btnCancel.Click += Btn_Click;
				if (showNo) {
					gap = (this.ClientSize.Width - btnWidth*3) / 4;
					btnYesOk.Left = gap;
					btnNo.Left = 70 + gap*2;
					btnCancel.Left = btnWidth*2 + gap*3;
				//  for a "cancel" button without a "no" button - which seems silly
				} else {
					btnYesOk.Left = this.ClientSize.Width / 2 - btnWidth - gap / 2;
					btnCancel.Left = this.ClientSize.Width / 2 + gap / 2;
				}
			}

			this.Text = txtTitle;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

		
        private void Btn_Click(object sender, EventArgs e) {
			Button btn = (Button)sender;
			switch (btn.Name) {
				case "yes": Result = 1; BResult = true; break;
				case "no": Result = 0; BResult = false; break;
				case "cancel": Result = -1; BResult = false; break;
			}
			this.Close();
        }

		public void SetTextColor(System.Drawing.Color c) { msg.ForeColor = c; }
		public void SetBackColor(System.Drawing.Color c) { this.BackColor = c; }

		public void setLabels(string lblYes, string lblNo = "No", string lblCancel = "Cancel") {
			btnYesOk.Text = lblYes;
			btnNo.Text = lblNo;
			btnCancel.Text = lblCancel;
		}
	}
}
