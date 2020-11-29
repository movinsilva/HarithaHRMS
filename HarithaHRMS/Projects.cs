using HarithaHRMS.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void populateItems()
        {
            //ProjectListItem[] listItems = new ProjectListItem[7];

            //for(int i = 0; i < listItems.Length; i++)
            //{

            //    listItems[i] = new ProjectListItem();
            //    listItems[i].Title = "My Data List";

            //    flowLayoutPanel1.Controls.Add(listItems[i]);

            //}

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + 
                    "/api/DraughtmanApi/GetSublevelsForUserId?userid=" + RuntimeConstants.userid);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = SubLevelListDto.FromJson(content);

                if (result.Success)
                {

                    foreach(var sublevel in result.Sublevels)
                    {

                        flowLayoutPanel1.Controls.Add(new ProjectListItem
                        {
                            Project = sublevel.Project.Name,
                            TagEx = sublevel,
                            SubLevelName = sublevel.Name,
                            IsActive = sublevel.IsActive,
                            Deadline = sublevel.Deadline,
                            Progress = (int)(sublevel.ProgressFraction*100),
                            PriorityLevel = sublevel.PriorityLevel.ToString(),
                        }) ;

                    }

                }
                else
                {
                    MessageBox.Show("Error!");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);

            }

        }

        private void Projects_Load(object sender, EventArgs e)
        {

            populateItems();

        }
    }
}
