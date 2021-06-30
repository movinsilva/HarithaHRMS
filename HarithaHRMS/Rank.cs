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
    public partial class Rank : Form
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

        public Rank()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rank_Load(object sender, EventArgs e)
        {

            try
            {
                //Rounding corners
                panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 25, 25));
                panel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 25, 25));
                panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 25, 25));

                label2.Text = "Month " + DateTime.Now.ToString("MMMM");

                label3.Text = RuntimeConstants.firstWorker.ToString();
                label6.Text = RuntimeConstants.secondWorker.ToString();
                label5.Text = RuntimeConstants.thirdWorker.ToString();
            } catch (Exception ex)
            {
                ErrorLog.errorLogger(stackTrace: ex.StackTrace, message: ex.Message);
            }
        }
    }
}
