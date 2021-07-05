using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HarithaHRMS.DTOs;
using HarithaHRMS.Models;
using Newtonsoft.Json.Linq;
using QuickType;

namespace HarithaHRMS
{
    public partial class SecondaryProjects : Form
    {
        public static string currentProject;
        public SecondaryProjects()
        {
            InitializeComponent();
        }

        private void SecondaryProjects_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Select your project";
            if(currentProject != null)
            {
                label2.Text = currentProject;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "/SecondaryProject/GetSecondaryProjects");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = SecondaryProjectsDto.FromJson(content);

                var dropdownmodellist = new List<DropdownModel>();

                foreach (var i in result)
                {
                    dropdownmodellist.Add(new DropdownModel() { ID = i.Id, Name = i.Name });
                }


                var bindingSource1 = new BindingSource();
                bindingSource1.DataSource = dropdownmodellist;

                comboBox1.DataSource = bindingSource1.DataSource;

                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            } catch(Exception ex)
            {
                ErrorLog.errorLogger(ex.StackTrace, ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest webRequest;
                String requestParams = "{" + $"\"UserID\": \"{RuntimeConstants.userid}\", \"SecondaryProjectId\": {comboBox1.SelectedValue}, \"Remarks\": \"{textBox1.Text}\""
                     + "}";

                webRequest = (HttpWebRequest)WebRequest.Create($"http://{RuntimeConstants.ip}:{RuntimeConstants.port}/Api/SecondaryProject/SubmitProjectShift");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.Accept = "application/json";

                byte[] byteArray = Encoding.UTF8.GetBytes(requestParams);
                webRequest.ContentLength = byteArray.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }



                //HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                //string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //JObject json = JObject.Parse(content);

                //var x = json.GetValue("success");
                //var t = -8;
                // Get the response.
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {

                        StreamReader rdr = new StreamReader(responseStream, Encoding.UTF8);
                        string Json = rdr.ReadToEnd(); // response from server
                        JObject json = JObject.Parse(Json);
                        if ((bool)(json.GetValue("success")))
                        {
                            currentProject = ((DropdownModel)comboBox1.SelectedItem).Name;
                            label2.Text = currentProject;
                            MessageBox.Show("Project changed successfully", comboBox1.SelectedText);
                            textBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Couldn't change the project, Please try again", "Project");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }
    }
}
