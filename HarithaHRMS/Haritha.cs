using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarithaHRMS
{
    public partial class Haritha : Form
    {

        private String path = Application.StartupPath;
        private int mousePositionDifference;
        private int previousMousePosition = 0;
        private int inactiveTimeCount = 0;
        private DateTime lastTime;
        private double screenSleptMinutes;
        private string lastlyMouseChangedTime;
        private double workedHours;
        private int sec;

        public static double totalDutyHours;
        public static TimeSpan powerOffTime;
        public static int totalIdleTime;
        public static int autocadTimeCount;
        public static int excelTimeCount;
        public static int wordTimeCount;
        public static bool isDutyOn;


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

        public Haritha()
        {
            InitializeComponent();
            formattingHours(totalDutyHours);
            if (Form1.dutyStatus)
            {
                timer1.Start();
                timer2.Start();
                timer3.Start();
                button1.Text = "Duty Off";
                button1.BackColor = Color.Blue;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Haritha_Load(object sender, EventArgs e)
        {

            //Dots in the clock
            panel11.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel11.Width, panel11.Height, 10, 10));
            panel12.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel12.Width, panel12.Height, 10, 10));
            panel13.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel13.Width, panel13.Height, 10, 10));
            panel14.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel14.Width, panel14.Height, 10, 10));

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 35, 35));

            //Setting the username and email
            label13.Text = RuntimeConstants.username;
            label14.Text = RuntimeConstants.email;

            //setting the current date
            label1.Text = DateTime.Now.ToString("dd MMMM yyyy");


        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        public static Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel16.Controls.Add(childForm);
            panel16.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }



        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new Projects());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new Tasks());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new History());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            openChildForm(new Notices());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text.Equals("Duty On"))
            {

                powerOffTime = DateTime.Now.Subtract(DateTime.Now);
                totalIdleTime = 0;
                autocadTimeCount = 0;
                excelTimeCount = 0;
                wordTimeCount = 0;

                if (dutyUpdating(true))
                {
                    label12.Text = "Currently Working";
                    button1.Text = "Duty Off";
                    button1.BackColor = Color.Blue;

                    MessageBox.Show("Start Your Work", "Duty ON");
                    isDutyOn = true;
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();

                    //Restricting user to close the app while duty is on
                    this.ControlBox = false;

                    //To make the inactive time log 0 in the first log
                    lastTime = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Error, Please try again");
                }


                
            }
            else
            {

                if (dutyUpdating(false))
                {
                    if (workedHours > 0 && workedHours != 999 && workedHours != -1)
                    {
                        if (workedHours < 8)
                        {
                            MessageBox.Show($"You have {RoundUp((8 - workedHours), 2).ToString()} hours to complete today's work. Work ended. ", "Duty off");
                        }
                        else
                        {
                            MessageBox.Show("Work ended.", "Duty off");
                        }
                    }

                    label12.Text = "Uploading... Please Wait";
                    button1.Text = "Duty On";
                    button1.BackColor = Color.LimeGreen;
                    timer1.Stop();
                    timer2.Stop();
                    timer3.Stop();

                    isDutyOn = false;

                    powerOffTime = DateTime.Now.Subtract(DateTime.Now);

                    using (StreamWriter stream1 = File.CreateText(path + "tit.tlt"))
                    {
                        stream1.Write("0");
                    }
                    using (StreamWriter stream2 = File.CreateText(path + "actt.tlt"))
                    {
                        stream2.Write("0");
                    }
                    using (StreamWriter stream3 = File.CreateText(path + "wtt.tlt"))
                    {
                        stream3.Write("0");
                    }
                    using (StreamWriter stream4 = File.CreateText(path + "ett.tlt"))
                    {
                        stream4.Write("0");
                    }

                    SSUploading();
                    appLogUploading();

                    Form1.dutyStatus = false;

                }
                else
                {
                    MessageBox.Show("Error, Please try again");
                }

                
            }
        }

        private async void appLogUploading()
        {
            await Task.Delay(3000);
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    client.DefaultRequestHeaders.Clear();

                    var file = path + "system.encrypt";

                    if (File.Exists(file))
                    {

                        Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new
                                    DateTime(1970, 1, 1))).TotalSeconds;

                        var s = RuntimeConstants.email.Substring(0, RuntimeConstants.email.LastIndexOf("@")) + "_" + unixTimestamp + ".csv";
                        var bytearray = File.ReadAllBytes(file);
                        ByteArrayContent bytes = new ByteArrayContent(bytearray);
                        form.Add(bytes, "file", s);
                        //form.Add(new StringContent(DateTime.Now.ToString()), "uploaddate");




                        if (bytearray.Length > 8000)
                        {
                            HttpResponseMessage response = await client.PostAsync("http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + 
                                "/api/WindowsService/UploadAppLog", form);
                            var k = response.Content.ReadAsStringAsync().Result;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                File.Delete(file);
                            }

                        }
                    }
                    label12.Text = "Not working";
                    this.ControlBox = true;

                }
            }
            catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
                label12.Text = "Not working";
                this.ControlBox = true;
            }
        }

        private bool dutyUpdating(bool dutyOn)
        {

            int powerOffTimeInMinutes;
            int autocadT;
            int wordT;
            int excelT;

            if(powerOffTime.TotalMinutes > 13)
            {
                powerOffTimeInMinutes = (int)(RoundUp(powerOffTime.TotalMinutes, 2));
            }
            else
            {
                powerOffTimeInMinutes = 0;
            }

            if (autocadTimeCount > 0)
            {
                autocadT = autocadTimeCount * 5;
            } else
            {
                autocadT = 0;
            }

            if(wordTimeCount > 0)
            {
                wordT = wordTimeCount * 5;
            }
            else
            {
                wordT = 0;
            }

            if (excelTimeCount > 0)
            {
                excelT = excelTimeCount * 5;
            }
            else
            {
                excelT = 0;
            }

            try
            {
                Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new
                                DateTime(1970, 1, 1))).TotalSeconds;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + 
                    "/api/windowsservice/createdutyonoff?username="+ RuntimeConstants.email + "&isdutyon=" + dutyOn + "&punchdatetime=" + 
                    unixTimestamp + "&powerofftime=" + powerOffTimeInMinutes + "&idletime=" + totalIdleTime + "&autocadtime=" + autocadT + 
                    "&wordtime=" + wordT + "&exceltime=" + excelT);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JObject json = JObject.Parse(content);
                workedHours = Double.Parse(json.GetValue("workedhours").ToString());

                return (bool)(json.GetValue("success"));

            }
            catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
                return false;
            }
        }

        //Method to change hours to a format
        private void formattingHours(double hours)
        {

            String totalHoursString = hours.ToString();
            int onlyHours = (int)(hours);
            if (onlyHours < 10)
            {

                label2.Text = "0";
                label3.Text = onlyHours.ToString();

            }
            else
            {

                label2.Text = "1";
                label3.Text = (onlyHours - 10).ToString();

            }

            double minutes = (hours - onlyHours) * 60;
            int onlyMinutes = (int)(minutes);

            if (onlyMinutes < 10)
            {

                label4.Text = "0";
                label5.Text = onlyMinutes.ToString();

            }
            else
            {

                String m = onlyMinutes.ToString();
                label4.Text = m.Substring(0, 1);
                label5.Text = m.Substring(1);

            }

            int onlySeconds = (int)((minutes - onlyMinutes) * 60);

            if (onlySeconds < 10)
            {

                label6.Text = "0";
                label7.Text = onlySeconds.ToString();

            }
            else
            {

                String s = onlySeconds.ToString();
                label6.Text = s.Substring(0, 1);
                label7.Text = s.Substring(1);

            }
        }

        //Method to start the clock
        private void startTime()
        {
            int hour1 = int.Parse(label2.Text);
            int hour2 = int.Parse(label3.Text);
            int min1 = int.Parse(label4.Text);
            int min2 = int.Parse(label5.Text);
            int sec1 = int.Parse(label6.Text);
            int sec2 = int.Parse(label7.Text);
            int sec = (hour1 * 36000) + (hour2 * 3600) + (min1 * 600) + (min2 * 60) + (sec1 * 10) + sec2;
            sec++;

           // int  h, m, s;

           // h = (sec / 3600);

           // m = (sec - (3600 * h)) / 60;

           // s = (sec - (3600 * h) - (m * 60));

           //var time = $"{}:"

            //under 60 seconds
            if (sec < 60)
            {

                label6.Text = ((int)(sec / 10)).ToString();
                label7.Text = (sec % 10).ToString();

            }

            //between 1 minute and 60 minutes
            else if (sec > 59 && sec < 3600)
            {
                label4.Text = ((int)(sec / 600)).ToString();
                label5.Text = ((int)((sec % 600) / 60)).ToString();

                label6.Text = ((int)((sec % 60) / 10)).ToString();
                label7.Text = (sec % 10).ToString();
            }

            //more than 1 hour
            else if (sec > 3599)
            {

                label2.Text = ((int)(sec / 36000)).ToString();
                label3.Text = ((int)((sec % 36000) / 3600)).ToString();

                label4.Text = ((int)((sec % 3600) / 600)).ToString();
                label5.Text = ((int)((sec % 600) / 60)).ToString();

                label6.Text = ((int)((sec % 60) / 10)).ToString();
                label7.Text = (sec % 10).ToString();
            }

        }

        public static double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startTime();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ScreenshotCapturing();
        }

        //Method to capture screenshots
        private void ScreenshotCapturing()
        {
            if (isDutyOn)
            {
                try
                {
                    //Creating a new Bitmap object
                    Bitmap captureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                    //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                    //Creating a Rectangle object which will  
                    //capture our Current Screen
                    Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                    //Creating a New Graphics Object
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    //Copying Image from The Screen
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    //Saving the Image File (I am here Saving it in My E drive).
                    Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new
                                DateTime(1970, 1, 1))).TotalSeconds;
                    var s = path + RuntimeConstants.email.Substring(0, RuntimeConstants.email.LastIndexOf("@") < 0 ? 0 : RuntimeConstants.email.LastIndexOf("@")) + "_" + unixTimestamp + ".jpg";
                    captureBitmap.Save(s, ImageFormat.Jpeg);
                    //captureBitmap.Save(Path.Combine("D:\", Dat))'
                }
                catch (Exception ex)
                {
                    ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
                }


                //Recording last screenshot captured time in a text file in a case of power failure
                using (StreamWriter stream = File.CreateText(path + "lsut.tlt"))
                {
                    stream.Write(DateTime.Now.ToString());
                }

                //Recording total idle time in a text file as a backup in a case of power failure
                using (StreamWriter stream1 = File.CreateText(path + "tit.tlt"))
                {
                    stream1.Write(totalIdleTime);
                }


                //Recording total autocad time in a text file as a backup in a case of power failure
                using (StreamWriter stream2 = File.CreateText(path + "actt.tlt"))
                {
                    stream2.Write(autocadTimeCount);
                }

                //Recording total word time in a text file as a backup in a case of power failure
                using (StreamWriter stream3 = File.CreateText(path + "wtt.tlt"))
                {
                    stream3.Write(wordTimeCount);
                }

                //Recording total excel time in a text file as a backup in a case of power failure
                using (StreamWriter stream4 = File.CreateText(path + "ett.tlt"))
                {
                    stream4.Write(excelTimeCount);
                }

                SSUploading();
            }
        }


        //Method to upload screenshots
        private async void SSUploading()
        {
            //getting the jpg image files in the folder into a string array
            string[] fileArray = Directory.GetFiles(path, "*.jpg");

            foreach (var file in fileArray)
            {

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        client.DefaultRequestHeaders.Clear();



                        var s = file.Substring(file.LastIndexOf("\\") + 1);
                        ByteArrayContent bytes = new ByteArrayContent(File.ReadAllBytes(file));
                        form.Add(bytes, "file", s);


                        HttpResponseMessage response = await client.PostAsync("http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port + "/api/WindowsService/UploadImage", form);
                        var k = response.Content.ReadAsStringAsync().Result;

                        //deleting the image if uploaded
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            File.Delete(file);
                        }


                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
                }
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            appLog();
        }

        private void appLog()
        {

            

            try
            {

                //writing in the applog excel
                using (StreamWriter sw = File.AppendText(path + "system.encrypt"))
                {
                    String s = Cursor.Position.X.ToString() + Cursor.Position.Y.ToString();
                    int mousePosition = int.Parse(s);
                    mousePositionDifference = mousePosition - previousMousePosition;
                    previousMousePosition = mousePosition;

                    DateTime firstTime = DateTime.Now;
                    if (lastTime != null)
                    {
                        var timeDifference = firstTime.Subtract(lastTime);
                        screenSleptMinutes = timeDifference.TotalMinutes;
                    }

                    if (mousePositionDifference != 0)
                    {
                        inactiveTimeCount = 0;
                        lastlyMouseChangedTime = DateTime.Now.ToString();

                        if (screenSleptMinutes > 7)
                        {
                            sw.Write($"{lastTime.ToString()}, {RoundUp(screenSleptMinutes, 2)} minutes");
                            totalIdleTime += (int)screenSleptMinutes;

                        }
                        else
                        {
                            sw.Write($"{lastlyMouseChangedTime}, 0 minutes");
                        }

                    }
                    else
                    {
                        inactiveTimeCount++;

                        if (screenSleptMinutes > 7)
                        {
                            sw.Write($"{lastlyMouseChangedTime}, {RoundUp(screenSleptMinutes, 2)} minutes");
                            totalIdleTime += (int)screenSleptMinutes;
                        }
                        else
                        {
                            sw.Write($"{lastlyMouseChangedTime}, {(inactiveTimeCount * 5).ToString()} minutes");
                            totalIdleTime += 5;
                        }
                    }

                    sw.Write(",");
                    lastTime = firstTime;

                    String mainwindow = FocusedApp.GetActiveWindowTitle();

                    if (mainwindow != null)
                    {
                        sw.Write(mainwindow + ",");
                        if (mainwindow.ToLower().Contains("autocad"))
                        {
                            autocadTimeCount++;
                        }
                        else if (mainwindow.ToLower().Contains("excel"))
                        {
                            excelTimeCount++;
                        }
                        else if (mainwindow.ToLower().Contains("word"))
                        {
                            wordTimeCount++;
                        }
                    }
                    else
                    {
                        sw.Write(",");
                    }

                    //getting the current applications running 
                    Process[] processes = Process.GetProcesses();
                    String log = DateTime.Now.ToString();
                    foreach (Process p in processes)
                    {

                        if (!string.IsNullOrEmpty(p.MainWindowTitle))
                        {

                            log = log + $",{p.MainWindowTitle}";


                        }
                    }
                    sw.Write(log);


                    sw.Write("\n");


                }

            }catch(Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
            uploadErrorLog();
        }

        private async void uploadErrorLog()
        {
            await Task.Delay(2000);
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    client.DefaultRequestHeaders.Clear();

                    var file = path + "errorLogger.csv";

                    if (File.Exists(file))
                    {
                        Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new
                           DateTime(1970, 1, 1))).TotalSeconds;

                        var s = label1.Text.Substring(0, label1.Text.LastIndexOf("@")) + "_" + unixTimestamp + ".csv";
                        var bytearray = File.ReadAllBytes(file);
                        ByteArrayContent bytes = new ByteArrayContent(bytearray);
                        form.Add(bytes, "file", s);
                        //form.Add(new StringContent(DateTime.Now.ToString()), "uploaddate");
                        if (bytearray.Length > 8000)
                        {
                            HttpResponseMessage response = await client.PostAsync("http://" + RuntimeConstants.ip + ":" + RuntimeConstants.port +
                                "/api/WindowsService/UploadLog", form);
                            var k = response.Content.ReadAsStringAsync().Result;

                            if (response.StatusCode == HttpStatusCode.OK)
                            {

                                File.Delete(file);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }
    }
}
