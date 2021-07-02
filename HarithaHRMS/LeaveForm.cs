using MailKit.Net.Smtp;
using MimeKit;
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

  


public partial class LeaveForm : Form
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

        public LeaveForm()
        {
            InitializeComponent();
        }

        private void LeaveForm_Load(object sender, EventArgs e)
        {
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 35, 35));
            dateTimePicker1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, dateTimePicker1.Width, dateTimePicker1.Height, 15, 15));
            textBox1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 25, 25));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 25, 25));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Int32 unixTimestamp = (Int32)(dateTimePicker1.Value.Subtract(new
                                        DateTime(1970, 1, 1))).TotalSeconds;

                string reason = textBox1.Text + "#" + comboBox1.SelectedItem.ToString();

                HttpWebRequest webRequest;
                String requestParams = "{" + $"\"Date\": \"{unixTimestamp}\", \"Reason\": \"{reason}\", \"IsApproved\": 0, " +
                    $"\"UserId\": \"{RuntimeConstants.userid}\"" + "}";

                webRequest = (HttpWebRequest)WebRequest.Create($"http://{RuntimeConstants.ip}:{RuntimeConstants.port}/Api/windowsservice/CreateLeave");
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

                            //sendEmail();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Couldn't send your leave request, Please try again", "Leave Form");
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }

        }

        private void sendEmail()
        {
            List<InternetAddress> emails = new List<InternetAddress>();
            //Haritha
            //emails.Add(new MailboxAddress( "Mr. Yohan", "yj@haritha.lk"));
            //emails.Add(new MailboxAddress( "Mr. Jayalath", "jayalath@haritha.lk"));

            //Test
            emails.Add(new MailboxAddress("Mr. Kavindu", "kavindudenuwara19@gmail.com"));
            emails.Add(new MailboxAddress("Mr. Sandev", "sandevdewthilina2000@gmail.com"));

            List<InternetAddress> ccEmails = new List<InternetAddress>();
            //Haritha
            //ccEmails.Add(new MailboxAddress("info@haritha.lk"));
            //ccEmails.Add(new MailboxAddress("nikini@haritha.lk"));

            //Test
            ccEmails.Add(new MailboxAddress("movinpinsara@gmail.com"));

            var message = new MimeMessage();

            //Haritha
            //message.From.Add(new MailboxAddress("Haritha Consultations", "harithaconsultationinfo@gmail.com"));

            //Test
            message.From.Add(new MailboxAddress("Ecode Software Solutions", "ecodesoftwaresolutions@gmail.com"));

            message.To.AddRange(emails);
            message.Cc.AddRange(ccEmails);

            message.Subject = $" {RuntimeConstants.username} requesting a leave";
            message.Body = new TextPart("Plain")
            {
                Text = "Employee : " + RuntimeConstants.username + "\nDate : " + dateTimePicker1.Value.ToString("dd MMMM yyyy") + "\n\nReason : " + textBox1.Text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                //Haritha
                //client.Authenticate("harithaconsultationinfo@gmail.com", "harithainfo@123");

                //Test
                client.Authenticate("ecodesoftwaresolutions@gmail.com", "ESS@1234");


                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
