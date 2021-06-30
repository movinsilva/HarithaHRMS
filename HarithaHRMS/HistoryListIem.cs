using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HarithaHRMS
{
    public partial class HistoryListIem : UserControl
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


        public HistoryListIem()
        {
            InitializeComponent();
        }

        private void HistoryListIem_Load(object sender, EventArgs e)
        {

            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
        }

        #region Properties
        private string _project;

        public string Project
        {
            get { return _project; }
            set { _project = value; label1.Text = value; }
        }
        #endregion

    }
}
