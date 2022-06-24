using HarithaHRMS.DTOs;
using Newtonsoft.Json.Linq;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class Form1 : Form
    {

        public static bool dutyStatus;

        private string path = "C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\profile\\configurations\\";

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

        public Form1()
        {
            InitializeComponent();

            //Giving a transparency level for the login box
            panel1.BackColor = Color.FromArgb(170, Color.GhostWhite);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Icon = new Icon(Directory.GetCurrentDirectory() + "/app_logo.ico");

            //Rounding corners
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 40, 40));
            textBox1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 20, 20));
            textBox2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox2.Width, textBox2.Height, 20, 20));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));

            //label2 font features
            label2.Font = new Font("Arial", 28, FontStyle.Bold);

            //removing the border of button
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\profile\\configurations\\");
                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Database\\");
                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\current specifications\\");
                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\readme files\\");
                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Previous updates\\");
                System.IO.Directory.CreateDirectory("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\profile\\data\\");

                //testing
                //textBox1.Text = "isuru@haritha.lk";
                //textBox2.Text = "isuru123";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + 
                    "/api/windowsservice/validateUserByUsernamePassword?username="
                                                                + textBox1.Text + "&password=" + textBox2.Text);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var result = LoginValuesDto.FromJson(content);


                if((bool)(result.Success))
                {

                    RuntimeConstants.username = result.Username.ToString();
                    RuntimeConstants.userid = result.Userid.ToString();
                    RuntimeConstants.email = textBox1.Text;
                    RuntimeConstants.role = result.Role;

                    Haritha.totalDutyHours = (double)(result.WorkingHours);
                    dutyStatus = (bool)(result.Islogedin);

                    try
                    {
                        RuntimeConstants.firstWorker = result.Userreport.LeaveViewModels[0].Username.ToString();
                        RuntimeConstants.secondWorker = result.Userreport.LeaveViewModels[1].Username.ToString();
                        RuntimeConstants.thirdWorker = result.Userreport.LeaveViewModels[2].Username.ToString();

                       
                    } catch (Exception ex)
                    {
                        ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
                    }



                    if (dutyStatus)
                    {
                        var lastUploadingTimeFile = path + "lsut.tlt";
                        if (File.Exists(lastUploadingTimeFile))
                        {
                            var time = File.ReadAllText(lastUploadingTimeFile);

                            if (time != "0"  && time != "")
                            {
                                DateTime lastSsCapturedTime = DateTime.Parse(time);
                                Haritha.powerOffTime = DateTime.Now.Subtract(lastSsCapturedTime);

                                if (Haritha.powerOffTime.TotalMinutes > 13 && Haritha.powerOffTime.TotalMinutes < 1440)
                                {
                                    Haritha.totalDutyHours -= Haritha.powerOffTime.TotalHours;
                                }
                            }
                            
                        }

                        var inactiveTimeFile = path + "tit.tlt";
                        if (File.Exists(inactiveTimeFile))
                        {

                            String totalIdleTime = File.ReadAllText(inactiveTimeFile);
                            if(totalIdleTime != "")
                            {
                                Haritha.totalIdleTime = int.Parse(totalIdleTime);
                            } else
                            {
                                Haritha.totalIdleTime = 0;
                            }
                            

                        }

                        var autocadTimeFile = path + "actt.tlt";
                        if (File.Exists(autocadTimeFile))
                        {

                            string totalAutocadTime = File.ReadAllText(autocadTimeFile);
                            if(totalAutocadTime != "")
                            {
                                Haritha.autocadTimeCount = int.Parse(totalAutocadTime);
                            } else
                            {
                                Haritha.autocadTimeCount = 0;
                            }
                            

                        }

                        var wordTimeFile = path + "wtt.tlt";
                        if (File.Exists(wordTimeFile))
                        {

                            string totalWordTime = File.ReadAllText(wordTimeFile);
                            if(totalWordTime != "")
                            {
                                Haritha.wordTimeCount = int.Parse(totalWordTime);
                            } else
                            {
                                Haritha.wordTimeCount = 0;
                            }
                            

                        }

                        var excelTimeFile = path + "ett.tlt";
                        if (File.Exists(excelTimeFile))
                        {

                            string totalExcelTime = File.ReadAllText(excelTimeFile);
                            if(totalExcelTime != "")
                            {
                                Haritha.excelTimeCount = int.Parse(totalExcelTime);
                            } else
                            {
                                Haritha.excelTimeCount = 0;
                            }
                            

                        }

                        Haritha.isDutyOn = true;


                    }


                    

                    MessageBox.Show("You are Successfully Logged In.", "Log in");

                    var frm = new Haritha();
                    frm.Location = this.Location;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.FormClosing += delegate {

                        //Haritha.ss = false;
                        Application.Exit();

                    };
                    frm.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid Credentials", "Log in");

                    textBox1.Text = "";
                    textBox2.Text = "";
                }

            } catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);

            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
