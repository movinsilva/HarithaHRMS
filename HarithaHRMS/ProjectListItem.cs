using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HarithaHRMS.DTOs;

namespace HarithaHRMS
{
    public partial class ProjectListItem : UserControl
    {

        //For rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        //Rounded corners
        private static extern IntPtr CreateRoundRectRgn
            (
        int nLeft,
        int nTop,
        int nRight,
        int nBottom,
        int nWidthEllipse,
        int nHeightEllipse
            );




        public ProjectListItem()
        {
            InitializeComponent();

            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            progressBar1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, progressBar1.Width, progressBar1.Height, 18, 18));




        }

        private void ProjectListItem_Load(object sender, EventArgs e)
        {


        }

        #region Properties
        private string _project;
        private Sublevel _tagEx;
        private String _subLevelName;
        private bool _isActive;
        private DateTime _deadline;
        private int _progress;
        private String _priorityLevel;

        //[Category("Custom Props")]
        public string Project
        {
            get { return _project; }
            set { _project = value; label1.Text = value; }
        }

        public String SubLevelName
        {
            get { return _subLevelName; }
            set { _subLevelName = value; label2.Text = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (value)
                {
                    label3.Text = "Active";
                    label3.ForeColor = Color.Green;
                }
                else
                {
                    label3.Text = "Inactive";
                    label3.ForeColor = Color.Gray;
                }
            }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set { _deadline = value; label5.Text = value.ToString("dd/MM/yyyy"); }
        }

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; label7.Text = value + "%"; progressBar1.Value = value; }
        }

        public String PriorityLevel
        {
            get { return _priorityLevel; }
            set { _priorityLevel = value; label6.Text = value;  }
        }

       

        public Sublevel TagEx
        {
            get { return _tagEx; }
            set { _tagEx = value; progressBar1.Tag = value; }
        }
        #endregion

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

            var progress = sender as ProgressBar;

            var tag = (Sublevel)progress.Tag;

            var fOther = new SpecificProject();
            fOther.tag = tag;
            fOther.TopLevel = false;
            fOther.TopMost = true;
            this.Parent.Hide();
            this.Parent.Parent.Controls.Add(fOther);
            fOther.BringToFront();
            fOther.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
