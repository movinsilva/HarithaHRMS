namespace HarithaHRMS
{
    partial class Projects_engineer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Projects_engineer));
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanelP = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Ongoing = new System.Windows.Forms.TabPage();
            this.Completed = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.Ongoing.SuspendLayout();
            this.Completed.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanelP
            // 
            this.flowLayoutPanelP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.flowLayoutPanelP, "flowLayoutPanelP");
            this.flowLayoutPanelP.Name = "flowLayoutPanelP";
            this.flowLayoutPanelP.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Ongoing);
            this.tabControl1.Controls.Add(this.Completed);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // Ongoing
            // 
            this.Ongoing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(89)))), ((int)(((byte)(103)))));
            resources.ApplyResources(this.Ongoing, "Ongoing");
            this.Ongoing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ongoing.Controls.Add(this.flowLayoutPanelP);
            this.Ongoing.Name = "Ongoing";
            // 
            // Completed
            // 
            this.Completed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(89)))), ((int)(((byte)(103)))));
            resources.ApplyResources(this.Completed, "Completed");
            this.Completed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Completed.Controls.Add(this.flowLayoutPanel2);
            this.Completed.Name = "Completed";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // Projects_engineer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(89)))), ((int)(((byte)(103)))));
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Projects_engineer";
            this.Load += new System.EventHandler(this.Projects_engineer_Load);
            this.tabControl1.ResumeLayout(false);
            this.Ongoing.ResumeLayout(false);
            this.Completed.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelP;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Ongoing;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage Completed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}