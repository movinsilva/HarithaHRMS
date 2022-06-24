using HarithaHRMS.Projects_Engineers.Sub_Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS.Projects_Engineers
{
    public partial class MainProjectListItem : UserControl
    {
        public MainProjectListItem()
        {
            InitializeComponent();
        }

        private void MainProjectListItem_Load(object sender, EventArgs e)
        {
            
        }

      

        #region properties
        private string _projectName;
        private string _projectCode;
        private DateTime _datetime;
        private string _createBy;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value;
                label1.Text = value;
            }
        }
        public string projectCode
        {
            get { return _projectCode; }
            set
            {
                _projectCode = value;
                label2.Text = value;
            }
        }
        public DateTime datetime
        {
            get { return _datetime; }
            set
            {
                _datetime = value;
                label4.Text = value.ToString();
            }
        }
        public string createby
        {
            get { return _createBy; }
            set
            {
                _createBy = value;
                label3.Text = value;
            }
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            //opening a project to see sub projects
            var subProjectEng = new SubProjectEng();
            subProjectEng.TopLevel = false;
            subProjectEng.TopMost = true;
            this.Parent.Parent.Parent.Parent.Controls.Add(subProjectEng);
            subProjectEng.BringToFront();
            subProjectEng.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
