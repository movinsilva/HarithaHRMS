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
        public SubProjectEng(string projectName)
        {
            InitializeComponent();
            // displaying project name
            label1.Text = projectName;
        }

        private void SubProjectEng_Load(object sender, EventArgs e)
        {
            populateSubProjects();
        }

        private void populateSubProjects()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                "/api/ProjectApi/getSubLevelsForProjectId?id=" + "1");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var result = EngSubProjectListDTO.FromJson(content);

            foreach (var each in result)
            {

                flowLayoutPanelSub.Controls.Add(new SubProjectListItem
                {
                    name = each.Name,
                    draughtman = each.User.Name,
                    allocatedHours = each.ManHours,
                    

                }) ;

            }
        }
    }
}
