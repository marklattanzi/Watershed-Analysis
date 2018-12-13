using System;
using System.Windows.Forms;

namespace warmf {

    public class WMDialog : Form {
        Label msg;

        public WMDialog(string txtTitle, string message, bool showCancel = false) {
            this.Width = 300;
            this.Height = 300;

            msg = new Label();
            msg.Text = message;
            msg.AutoSize = true;
            msg.ForeColor = System.Drawing.Color.Black;
            msg.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Controls.Add(msg);
            msg.Left = (this.ClientSize.Width - msg.Width) / 2;
            msg.Top = 20;

            Button btnOK = new Button {
                Text = "OK",
                Top = this.ClientSize.Height - 30,
                Left = this.ClientSize.Width / 2 - 35,
                Width = 70,
                Height = 25
            };

            this.Controls.Add(btnOK);
            btnOK.Click += Btn_Click;

            if (showCancel) {
                Button btnCancel = new Button {
                    Text = "Cancel",
                    Top = this.ClientSize.Height - 30,
                    Left = this.ClientSize.Width / 2 + 35,
                    Width = 70,
                    Height = 25
                };

                btnOK.Left -= 70;
                btnCancel.Click += Btn_Click;
                this.Controls.Add(btnCancel);
            }

            this.FormClosed += Form_Close;
            this.Text = txtTitle;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void Btn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Form_Close(object sender, FormClosedEventArgs e) {
            this.Close();
        }

        public void SetTextColor(System.Drawing.Color c) {
            msg.ForeColor = c;
        }
    }
}
