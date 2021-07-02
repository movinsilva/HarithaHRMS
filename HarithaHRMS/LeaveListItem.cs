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
    public partial class LeaveListItem : UserControl
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

        public LeaveListItem()
        {
            InitializeComponent();
        }

        private void LeaveListItem_Load(object sender, EventArgs e)
        {
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 35, 35));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 25, 25));
        }


        #region Properties
        private int _status;
        private DateTime _leaveDay;
        private String _name;
        private String _type;
       
        

        public int Status
        {
            get { return _status; }
            set { _status = value; 
            
            if(value == 1)
                {
                    button1.Text = "Confirmed";
                    button1.BackColor = Color.LimeGreen;
                } else if(value == 2)
                {
                    button1.Text = "Declined";
                    button1.BackColor = Color.Red;
                } else if(value == 0)
                {
                    button1.Text = "Pending";
                    button1.BackColor = Color.DodgerBlue;
                }
            }
        }


        public DateTime LeaveDay
        {
            get { return _leaveDay; }
            set { _leaveDay = value; label1.Text = value.ToString("dd MMMM yyyy"); }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value;
                label2.Text = value;
            
            }
        }

        public String Type
        {
            get { return _type; }
            set { _type = value;
                label3.Text = value;
            }
        }

      
        
        #endregion

    }
}
