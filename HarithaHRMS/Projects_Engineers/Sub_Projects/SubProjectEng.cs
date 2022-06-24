using QuickType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS.Projects_Engineers.Sub_Projects
{
    
    public partial class SubProjectEng : Form
    {
        private int projectID;
        public SubProjectEng(string projectName, int id)
        {
            InitializeComponent();
            // displaying project name
            label1.Text = projectName;
            this.projectID = id;
        }

        private void SubProjectEng_Load(object sender, EventArgs e)
        {
            populateSubProjects();
        }

        private void populateSubProjects()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                "/api/ProjectApi/getSubLevelsForProjectId?id=" + projectID);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var result = EngSubProjectListDTO.FromJson(content);

            foreach (var each in result)
            {

                flowLayoutPanelSub.Controls.Add(new SubProjectListItem(each)
                {
                    name = each.Name,
                    draughtman = each.User.Name,
                    allocatedHours = each.ManHours,
                    deadline = each.Deadline.Date.ToString("yyyy/MM/dd")


                }) ;

            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            var subProjectCreationForm = new SubProjectCreationForm(projectID);
            subProjectCreationForm.TopLevel = false;
            subProjectCreationForm.TopMost = true;
            this.Parent.Parent.Controls.Add(subProjectCreationForm);
            subProjectCreationForm.BringToFront();
            subProjectCreationForm.Show();
        }
    }
}
