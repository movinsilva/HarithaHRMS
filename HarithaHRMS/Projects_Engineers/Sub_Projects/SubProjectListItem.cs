using QuickType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS.Projects_Engineers.Sub_Projects
{
    public partial class SubProjectListItem : UserControl
    {
        private EngSubProjectListDTO dto;
        public SubProjectListItem(EngSubProjectListDTO dto)
        {
            InitializeComponent();
            this.dto = dto;
        }

        private void SubProjectListItem_Load(object sender, EventArgs e)
        {

        }

        #region properties
        private string _name;
        private string _draughtman;
        private int _allocatedHours;
        private string _date;
        private string _status;
        private string _deadline;

        public string name { 
            get { return _name; } 
            set { _name = value; label1.Text = value; } }

        public string draughtman
        {
            get { return _draughtman; }
            set { _draughtman = value; label2.Text = "Assigned to " + value; }
        }

        public int allocatedHours
        {
            get { return _allocatedHours; }
            set { _allocatedHours = value; label3.Text = value.ToString() + " hours allocated"; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; button1.Text = value; }
        }

        public string deadline
        {
            get { return _deadline; }
            set { _deadline = value; label6.Text = "(Deadline: " + value + ")"; }
        }

        #endregion

        private void iconButton1_Click(object sender, EventArgs e)
        {
            var subProjectCreationForm = new SubProjectCreationForm(dto, dto.ProjectId);
            subProjectCreationForm.TopLevel = false;
            subProjectCreationForm.TopMost = true;
            this.Parent.Parent.Controls.Add(subProjectCreationForm);
            subProjectCreationForm.BringToFront();
            subProjectCreationForm.Show();
        }
    }
}
