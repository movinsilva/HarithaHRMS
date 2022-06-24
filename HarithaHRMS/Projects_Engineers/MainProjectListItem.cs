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

        private void MainProjectListItem_Click(object sender, EventArgs e)
        {

        }

        #region properties
        private String _projectName;

        public String ProjectName
        {
            get { return _projectName; }
            set { _projectName = value;
                label1.Text = value;
            }
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
