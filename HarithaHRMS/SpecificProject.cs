using HarithaHRMS.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            label3.Text = tag.ProgressFraction.ToString() + "%";
            progressBar1.Value = (int)(tag.ProgressFraction);
            label4.Text = tag.Name;
            label8.Text = "Assigned by " + tag.Project.User.Name;
            label9.Text = tag.Deadline.ToString("dd/MM/yyyy");
            label10.Text = tag.ManHours.ToString();
            label11.Text = tag.PriorityLevel.ToString();





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
            textBox1.
        }

        //public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}
