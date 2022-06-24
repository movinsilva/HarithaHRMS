using HarithaHRMS.Snippets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace HarithaHRMS.Projects_Engineers
{
    public partial class ProjectCreationForm : Form
    {
        public ProjectCreationForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           string results = RequestBuilder.postRequest($"http://{RuntimeConstants.ip}:{RuntimeConstants.port}/api/ProjectApi/createUpcomingProject",
                new
                {
                    ID = 0,
                    AssigedUserId = RuntimeConstants.userid,
                    Deadline = dateTimePicker1.Value,
                    Code = textBox2.Text,
                    Remark = textBox5.Text,
                    Name = textBox1.Text,
                    Customer = textBox3.Text,
                    IsNotified = true
                });

            JObject obj = JObject.Parse(results);

            if((bool)obj.GetValue("success"))
            {
                string response = RequestBuilder.getRequest($"http://{RuntimeConstants.ip}:{RuntimeConstants.port}/api/ProjectApi/mergeToOngoingProject?upcommingid={obj.GetValue("newid")}");
                JObject responseObj = JObject.Parse(response);
                if((bool)responseObj.GetValue("success"))
                {
                    MessageBox.Show("success!!!");
                    this.Close();
                } else
                {
                    MessageBox.Show(responseObj.GetValue("message").ToString());
                }
            } 
            else
            {
                MessageBox.Show(obj.GetValue("message").ToString());
            }
        }

    }
}
