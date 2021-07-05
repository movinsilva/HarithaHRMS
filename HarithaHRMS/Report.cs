using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    try
        //    {
        //        Process.Start(new ProcessStartInfo("cmd", $"/c start {"http://"+ RuntimeConstants.ip +":"+ RuntimeConstants.port + "/Home/MonthDraughtmenSummary"}"));
        //    } catch(Exception ex)
        //    {
        //        ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
        //    }

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + "/Home/MonthDraughtmenSummary"}"));
            }
            catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sMonth = DateTime.Now.ToString("MM");
            string lastMonth = (int.Parse(sMonth) - 1).ToString();

            var url = "http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + "/Reporter/ReturnReport?args=" + RuntimeConstants.userid + "*entity*third,"+ lastMonth + "*entity*forth&hasparams=true&sql=&filename=MonthEndUserReport&reportname=Month%20End%20Draughtmen%20Report&IsDynamic=false";

            try
            {
                //Process.Start(new ProcessStartInfo("cmd", "/c start http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + "/Reporter/ReturnReport?args=" + RuntimeConstants.userid + url));
                System.Windows.Forms.Clipboard.SetText(url);
                MessageBox.Show("Link copied. Paste this link in Browser");
            }
            catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }
    }
}
