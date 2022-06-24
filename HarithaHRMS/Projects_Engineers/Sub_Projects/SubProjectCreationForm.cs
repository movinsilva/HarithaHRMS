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
        public SubProjectCreationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SubProjectCreationForm_Load(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "api/ProjectApi/getDraughtmenListForProjectAssignement");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
