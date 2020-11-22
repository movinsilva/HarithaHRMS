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

namespace HarithaHRMS
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            getHistoryList();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void getHistoryList ()
        {

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "/api/DraughtmanApi/GetSublevelhistory?userid=" + RuntimeConstants.userid);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = SubLevelListDto.FromJson(content);

                if (result.Success)
                {

                    foreach (var sublevel in result.Sublevels)
                    {

                        flowLayoutPanel1.Controls.Add(new HistoryListIem
                        {

                            Project = sublevel.Name,

                        });

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
