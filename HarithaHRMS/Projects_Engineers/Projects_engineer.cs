using HarithaHRMS.Projects_Engineers;
using HarithaHRMS.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using QuickType;

namespace HarithaHRMS
{
    public partial class Projects_engineer : Form
    {
        public Projects_engineer()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Projects_engineer_Load(object sender, EventArgs e)
        {
            populateProjects();
        }



        private void populateProjects()
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "/api/projectapi/getongoingprojects");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = EngUpcomingProjectListDto.FromJson(content);

                foreach (var each in result)
                {

                    flowLayoutPanelP.Controls.Add(new MainProjectListItem
                    {
                        ProjectName = each.Name,

                    });

                }

                

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }

            //completed projects loading
            try
            {
                flowLayoutPanel2.Controls.Add(new MainProjectListItem
                {
                    ProjectName = "template testing"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            var projectCreationForm = new ProjectCreationForm();
            projectCreationForm.TopLevel = false;
            projectCreationForm.TopMost = true;
            this.Parent.Controls.Add(projectCreationForm);
            projectCreationForm.BringToFront();
            projectCreationForm.Show();
        }
    }
}
