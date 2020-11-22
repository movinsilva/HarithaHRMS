using HarithaHRMS.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class SpecificProject : Form
    {

        //For rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        //Rounded corners
        private static extern IntPtr CreateRoundRectRgn
            (
        int nLeft,
        int nTop,
        int nRight,
        int nBottom,
        int nWidthEllipse,
        int nHeightEllipse
            );

        public Sublevel tag { get; set; }

        public SpecificProject()
        {
            InitializeComponent();
           
        }

        private void SpecificProject_Load(object sender, EventArgs e)
        {
            checkBox1.Height = 25;
            checkBox1.Width = 25;


            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30));
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20));
            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 10, 10));
            progressBar1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, progressBar1.Width, progressBar1.Height, 20, 20));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));

            label1.Text = tag.Project.Name;
            label3.Text = (int)(tag.ProgressFraction * 100) + "%";
            progressBar1.Value = (int)(tag.ProgressFraction*100);
            label4.Text = tag.Name;
            label8.Text = "Assigned by " + tag.Project.User.Name;
            label9.Text = tag.Deadline.ToString("dd/MM/yyyy");
            label10.Text = tag.ManHours.ToString();
            label11.Text = tag.PriorityLevel.ToString();

            panel2.Hide();


            textBox1.Text = ((int)(tag.ProgressFraction * 100)).ToString();
            checkBox1.Checked = tag.IsActive;


            //progressBar = Orientation.Vertical;

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            //ProgressBar progbar = new ProgressBar();
            //progbar.IsIndeterminate = false;
            //progbar.Orientation = Orientation.Horizontal;
            //progbar.Width = 150;
            //progbar.Height = 15;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {

                int progressValue = int.Parse(textBox1.Text);
                if (progressValue < 101 && progressValue > -1)
                {
                    if (progressValue > tag.ProgressFraction*100)
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" +
                            RuntimeConstants.port + "/api/DraughtmanApi/SublevelProgressUpdate?sublevelid=" +
                            tag.Id + "&isActive=" + checkBox1.Checked + "&progressFraction=" + (progressValue / 100.0));
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        JObject json = JObject.Parse(content);

                        if ((bool)(json.GetValue("success")))
                        {
                            MessageBox.Show("Updated Successfully");
                            this.Close();
                        } else
                        {
                            MessageBox.Show("Error, Please try again");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid value to progress");
                }

            } catch(Exception ex)
            {
                MessageBox.Show("Error, Please try Again");
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
            
        }

        //public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}
