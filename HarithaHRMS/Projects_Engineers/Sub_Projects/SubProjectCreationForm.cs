using HarithaHRMS.Models;
using HarithaHRMS.Snippets;
using Newtonsoft.Json.Linq;
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
    public partial class SubProjectCreationForm : Form
    {
        private string dID;
        private string dName;
        private int id;
        private int projectID;
        public SubProjectCreationForm(EngSubProjectListDTO subProject, int projectID)
        {
            InitializeComponent();
            textBox1.Text = subProject.Name;
            textBox2.Text = subProject.ManHours.ToString();
            textBox3.Text = subProject.PriorityLevel.ToString();
            textBox5.Text = subProject.Remark;
            dateTimePicker1.Value = subProject.Deadline.UtcDateTime;
            this.dID = subProject.User.Id;
            this.dName = subProject.User.Name;
            this.id = subProject.Id;
            this.projectID = projectID;
        }
        public SubProjectCreationForm(int projectID)
        {
            InitializeComponent();
            this.id = 0;
            this.projectID = projectID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SubProjectCreationForm_Load(object sender, EventArgs e)
        {
            string response = RequestBuilder.getRequest(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "/api/ProjectApi/getDraughtmenListForProjectAssignement");

            var result = DraughtmaListDto.FromJson(response);

            var dropdownModelDraughtsman = new List<DropDownModelDraughtsman>();

            foreach (var i in result)
            {
                dropdownModelDraughtsman.Add(new DropDownModelDraughtsman() { draughtmanID = i.Id, draughtmanName = i.Name });
            }

            var bindingSource = new BindingSource();
            bindingSource.DataSource = dropdownModelDraughtsman;

            comboBox1.DataSource = bindingSource.DataSource;
            comboBox1.DisplayMember = "draughtmanName";
            comboBox1.ValueMember = "draughtmanID";

            comboBox1.SelectedItem = new DropDownModelDraughtsman() { draughtmanID = dID, draughtmanName = dName };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string response = RequestBuilder.postRequest($"http://{RuntimeConstants.ip}:{RuntimeConstants.port}/api/ProjectApi/createSublevel",
                new
                {
                    ID = id,
                    ProjectID = projectID,
                    Name = textBox1.Text,
                    Deadline = dateTimePicker1.Value,
                    Remark = textBox5.Text,
                    UserID = comboBox1.SelectedValue,
                    ManHours = int.Parse(textBox2.Text),
                    PriorityLevel = int.Parse(textBox3.Text),
                });
                JObject obj = JObject.Parse(response);
                if ((bool)obj.GetValue("success"))
                {
                    MessageBox.Show("Sub project created successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(obj.GetValue("message").ToString());
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }
    }
}
