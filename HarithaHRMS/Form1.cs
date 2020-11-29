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

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + 
                    "/api/windowsservice/validateUserByUsernamePassword?username="
                                                                + textBox1.Text + "&password=" + textBox2.Text);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JObject json = JObject.Parse(content);

                if((bool)(json.GetValue("success")))
                {



                    RuntimeConstants.username = json.GetValue("username").ToString();
                    RuntimeConstants.userid = json.GetValue("userid").ToString();
                    RuntimeConstants.email = textBox1.Text;

                    Haritha.totalDutyHours = (double)(json.GetValue("workingHours"));
                    dutyStatus = (bool)(json.GetValue("islogedin"));

                    if (dutyStatus)
                    {
                        var lastUploadingTimeFile = path + "lsut.tlt";
                        if (File.Exists(lastUploadingTimeFile))
                        {
                            var time = File.ReadAllText(lastUploadingTimeFile);
                            DateTime lastSsCapturedTime = DateTime.Parse(time);
                            Haritha.powerOffTime = DateTime.Now.Subtract(lastSsCapturedTime);

                            if(Haritha.powerOffTime.TotalMinutes > 13)
                            {
                                Haritha.totalDutyHours -= Haritha.powerOffTime.TotalMinutes;
                            }
                        }

                        var inactiveTimeFile = path + "tit.tlt";
                        if (File.Exists(inactiveTimeFile))
                        {

                            String totalIdleTime = File.ReadAllText(inactiveTimeFile);
                            Haritha.totalIdleTime = int.Parse(totalIdleTime);

                        }

                        var autocadTimeFile = path + "actt.tlt";
                        if (File.Exists(autocadTimeFile))
                        {

                            string totalAutocadTime = File.ReadAllText(autocadTimeFile);
                            Haritha.autocadTimeCount = int.Parse(totalAutocadTime);

                        }

                        var wordTimeFile = path + "wtt.tlt";
                        if (File.Exists(wordTimeFile))
                        {

                            string totalWordTime = File.ReadAllText(wordTimeFile);
                            Haritha.wordTimeCount = int.Parse(totalWordTime);

                        }

                        var excelTimeFile = path + "ett.tlt";
                        if (File.Exists(excelTimeFile))
                        {

                            string totalExcelTime = File.ReadAllText(excelTimeFile);
                            Haritha.excelTimeCount = int.Parse(totalExcelTime);

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
