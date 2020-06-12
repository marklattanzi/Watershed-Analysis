namespace warmf
{
    partial class DialogScenarioManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CopyActiveScenario = new System.Windows.Forms.Button();
            this.AddScenario = new System.Windows.Forms.Button();
            this.RemoveScenario = new System.Windows.Forms.Button();
            this.ScenarioMoveUp = new System.Windows.Forms.Button();
            this.ScenarioMoveDown = new System.Windows.Forms.Button();
            this.ScenarioOpen = new System.Windows.Forms.Button();
            this.ScenarioClose = new System.Windows.Forms.Button();
            this.ProjectScenariosList = new System.Windows.Forms.ListBox();
            this.OpenScenariosList = new System.Windows.Forms.ListBox();
            this.ProjectScenariosGroup = new System.Windows.Forms.GroupBox();
            this.OpenScenariosGroup = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ProjectScenariosGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CopyActiveScenario
            // 
            this.CopyActiveScenario.Location = new System.Drawing.Point(157, 16);
            this.CopyActiveScenario.Name = "CopyActiveScenario";
            this.CopyActiveScenario.Size = new System.Drawing.Size(334, 23);
            this.CopyActiveScenario.TabIndex = 0;
            this.CopyActiveScenario.Text = "&Copy Active Scenario: ";
            this.CopyActiveScenario.UseVisualStyleBackColor = true;
            this.CopyActiveScenario.Click += new System.EventHandler(this.CopyActiveScenario_Click);
            // 
            // AddScenario
            // 
            this.AddScenario.Location = new System.Drawing.Point(32, 83);
            this.AddScenario.Name = "AddScenario";
            this.AddScenario.Size = new System.Drawing.Size(85, 23);
            this.AddScenario.TabIndex = 1;
            this.AddScenario.Text = "&Add";
            this.AddScenario.UseVisualStyleBackColor = true;
            this.AddScenario.Click += new System.EventHandler(this.AddScenario_Click);
            // 
            // RemoveScenario
            // 
            this.RemoveScenario.Location = new System.Drawing.Point(32, 125);
            this.RemoveScenario.Name = "RemoveScenario";
            this.RemoveScenario.Size = new System.Drawing.Size(85, 23);
            this.RemoveScenario.TabIndex = 2;
            this.RemoveScenario.Text = "&Remove";
            this.RemoveScenario.UseVisualStyleBackColor = true;
            this.RemoveScenario.Click += new System.EventHandler(this.RemoveScenario_Click);
            // 
            // ScenarioMoveUp
            // 
            this.ScenarioMoveUp.Location = new System.Drawing.Point(32, 168);
            this.ScenarioMoveUp.Name = "ScenarioMoveUp";
            this.ScenarioMoveUp.Size = new System.Drawing.Size(85, 23);
            this.ScenarioMoveUp.TabIndex = 3;
            this.ScenarioMoveUp.Text = "Move &Up";
            this.ScenarioMoveUp.UseVisualStyleBackColor = true;
            this.ScenarioMoveUp.Click += new System.EventHandler(this.ScenarioMoveUp_Click);
            // 
            // ScenarioMoveDown
            // 
            this.ScenarioMoveDown.Location = new System.Drawing.Point(32, 212);
            this.ScenarioMoveDown.Name = "ScenarioMoveDown";
            this.ScenarioMoveDown.Size = new System.Drawing.Size(85, 23);
            this.ScenarioMoveDown.TabIndex = 4;
            this.ScenarioMoveDown.Text = "Move &Down";
            this.ScenarioMoveDown.UseVisualStyleBackColor = true;
            this.ScenarioMoveDown.Click += new System.EventHandler(this.ScenarioMoveDown_Click);
            // 
            // ScenarioOpen
            // 
            this.ScenarioOpen.Location = new System.Drawing.Point(466, 189);
            this.ScenarioOpen.Name = "ScenarioOpen";
            this.ScenarioOpen.Size = new System.Drawing.Size(85, 23);
            this.ScenarioOpen.TabIndex = 5;
            this.ScenarioOpen.Text = "&Open";
            this.ScenarioOpen.UseVisualStyleBackColor = true;
            this.ScenarioOpen.Click += new System.EventHandler(this.ScenarioOpen_Click);
            // 
            // ScenarioClose
            // 
            this.ScenarioClose.Location = new System.Drawing.Point(466, 222);
            this.ScenarioClose.Name = "ScenarioClose";
            this.ScenarioClose.Size = new System.Drawing.Size(85, 23);
            this.ScenarioClose.TabIndex = 6;
            this.ScenarioClose.Text = "&Close";
            this.ScenarioClose.UseVisualStyleBackColor = true;
            this.ScenarioClose.Click += new System.EventHandler(this.ScenarioClose_Click);
            // 
            // ProjectScenariosList
            // 
            this.ProjectScenariosList.FormattingEnabled = true;
            this.ProjectScenariosList.Location = new System.Drawing.Point(144, 83);
            this.ProjectScenariosList.Name = "ProjectScenariosList";
            this.ProjectScenariosList.Size = new System.Drawing.Size(187, 160);
            this.ProjectScenariosList.TabIndex = 7;
            this.ProjectScenariosList.SelectedIndexChanged += new System.EventHandler(this.ProjectScenariosList_SelectedIndexChanged);
            // 
            // OpenScenariosList
            // 
            this.OpenScenariosList.FormattingEnabled = true;
            this.OpenScenariosList.Location = new System.Drawing.Point(413, 87);
            this.OpenScenariosList.Name = "OpenScenariosList";
            this.OpenScenariosList.Size = new System.Drawing.Size(187, 82);
            this.OpenScenariosList.TabIndex = 8;
            this.OpenScenariosList.SelectedIndexChanged += new System.EventHandler(this.OpenScenariosList_SelectedIndexChanged);
            // 
            // ProjectScenariosGroup
            // 
            this.ProjectScenariosGroup.Controls.Add(this.OpenScenariosGroup);
            this.ProjectScenariosGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectScenariosGroup.Location = new System.Drawing.Point(12, 57);
            this.ProjectScenariosGroup.Name = "ProjectScenariosGroup";
            this.ProjectScenariosGroup.Size = new System.Drawing.Size(345, 201);
            this.ProjectScenariosGroup.TabIndex = 9;
            this.ProjectScenariosGroup.TabStop = false;
            this.ProjectScenariosGroup.Text = "Project Scenarios";
            // 
            // OpenScenariosGroup
            // 
            this.OpenScenariosGroup.Location = new System.Drawing.Point(360, 0);
            this.OpenScenariosGroup.Name = "OpenScenariosGroup";
            this.OpenScenariosGroup.Size = new System.Drawing.Size(292, 201);
            this.OpenScenariosGroup.TabIndex = 0;
            this.OpenScenariosGroup.TabStop = false;
            this.OpenScenariosGroup.Text = "Open Scenarios";
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(423, 270);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(137, 39);
            this.btnHelp.TabIndex = 19;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(251, 270);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 39);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(80, 270);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 39);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // DialogScenarioManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 321);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.OpenScenariosList);
            this.Controls.Add(this.ProjectScenariosList);
            this.Controls.Add(this.ScenarioClose);
            this.Controls.Add(this.ScenarioOpen);
            this.Controls.Add(this.ScenarioMoveDown);
            this.Controls.Add(this.ScenarioMoveUp);
            this.Controls.Add(this.RemoveScenario);
            this.Controls.Add(this.AddScenario);
            this.Controls.Add(this.CopyActiveScenario);
            this.Controls.Add(this.ProjectScenariosGroup);
            this.Name = "DialogScenarioManager";
            this.Text = "Scenario Manager";
            this.Load += new System.EventHandler(this.ScenarioManager_Load);
            this.ProjectScenariosGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button CopyActiveScenario;
        private System.Windows.Forms.Button AddScenario;
        private System.Windows.Forms.Button RemoveScenario;
        private System.Windows.Forms.Button ScenarioMoveUp;
        private System.Windows.Forms.Button ScenarioMoveDown;
        private System.Windows.Forms.Button ScenarioOpen;
        private System.Windows.Forms.Button ScenarioClose;
        public System.Windows.Forms.ListBox ProjectScenariosList;
        public System.Windows.Forms.ListBox OpenScenariosList;
        private System.Windows.Forms.GroupBox ProjectScenariosGroup;
        private System.Windows.Forms.GroupBox OpenScenariosGroup;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}