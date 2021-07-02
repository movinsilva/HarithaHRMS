using HarithaHRMS.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class Leave : Form
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

        public Leave()
        {
            InitializeComponent();
        }

        private void Leave_Load(object sender, EventArgs e)
        {
            PopulateItems();
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 25, 25));
        }


        private void PopulateItems()
        {
            try
            {


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                    "/api/windowsservice/GetAllLeaveRequests?userid=" + RuntimeConstants.userid);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = LeaveListDto.FromJson(content);

                if (result.Success)
                {

                    foreach (var leave in result.Leaves)
                    {
                        String user = " ";
                        if(leave.Approved != null)
                        {
                            if (leave.IsApproved == 1)
                            {
                                user = "Confirmed by " + leave.Approved.Name;
                            }
                            else if (leave.IsApproved == 2)
                            {
                                user = "Declined by " + leave.Approved.Name;
                            }
                        }
                        string r = leave.Reason;
                        string type = "";
                        if (r.Contains("#"))
                        {
                            type = leave.Reason.Split('#').Last();
                        }
                        


                        flowLayoutPanel1.Controls.Add(new LeaveListItem
                        {
                            LeaveDay = DateTime.Parse(leave.Date),
                            //LeaveDay = leave.Date.AddDays(),
                            Status = leave.IsApproved,
                            Name = user,
                            Type = type,


                        }); ;

                    }

                }
                else
                {
                    MessageBox.Show("Error");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var leaveForm = new LeaveForm();
            leaveForm.TopLevel = false;
            leaveForm.TopMost = true;
            this.Parent.Controls.Add(leaveForm);
            leaveForm.BringToFront();
            leaveForm.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
