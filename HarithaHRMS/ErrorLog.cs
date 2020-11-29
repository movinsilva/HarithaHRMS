using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HarithaHRMS
{
    class ErrorLog
    {

        public static void errorLogger(String stackTrace, String message)
        {
            using (StreamWriter writer = File.AppendText("C:\\Programme Data\\Programme Files\\System info\\Windows\\os Data\\Updates\\new\\profile\\configurations\\exceptions.csv"))
            {
                writer.Write($"{DateTime.Now.ToString()}, { stackTrace}, {message} + \n\n");
            }
        }

    }
}
