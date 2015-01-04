using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;


namespace Vidheya_GetBhavCopy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyInitialize();
        }

        private void MyInitialize()
        {
            SaveLocationTextBox.Text = Properties.Settings.Default.SaveLocation;
            dateTimePicker1.MaxDate = DateTime.Now;
            //dateTimePicker2.MaxDate = DateTime.Now;
            //MessageBox.Show("Welcome");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //   DialogResult result = MessageBox.Show("Do you really wanna exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //   if (result == DialogResult.No)
            //   {
            //       e.Cancel = true;
            //   }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FileSave = new FolderBrowserDialog();
            FileSave.Description = "Select Folder To Save";

            if (FileSave.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = FileSave.SelectedPath;
                Properties.Settings.Default.SaveLocation = sSelectedPath;
                Properties.Settings.Default.Save();
                SaveLocationTextBox.Text = sSelectedPath.ToString();
                //MessageBox.Show("Selected Path " + sSelectedPath);


            }


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var thismonth = dateTimePicker1.Value.Month;
            var thisday = dateTimePicker1.Value.Day;
            var thisyear = dateTimePicker1.Value.Year;
            var thisdayname = dateTimePicker1.Value.DayOfWeek;
            string sthismonth = dateTimePicker1.Value.Month.ToString();
            string sthisday = dateTimePicker1.Value.Day.ToString();
            string sthisyear = dateTimePicker1.Value.Year.ToString();
            string sthisdayname = thisdayname.ToString();
            if (sthisdayname == "Sunday" || sthisdayname == "Saturday")
            {
                NotValidDay();
            }
            else
            {
                if (thisday <= 10)
                {
                    sthisday = '0' + sthisday;
                    // MessageBox.Show(sthisday);
                }
                string monthsel = "";
                switch (thismonth)
                {
                    case 1: monthsel = "JAN"; break;
                    case 2: monthsel = "FEB"; break;
                    case 3: monthsel = "MAR"; break;
                    case 4: monthsel = "APR"; break;
                    case 5: monthsel = "MAY"; break;
                    case 6: monthsel = "JUN"; break;
                    case 7: monthsel = "JUL"; break;
                    case 8: monthsel = "AUG"; break;
                    case 9: monthsel = "SEP"; break;
                    case 10: monthsel = "OCT"; break;
                    case 11: monthsel = "NOV"; break;
                    case 12: monthsel = "DEC"; break;

                }
                string mydate = sthisday + "/" + monthsel + "/" + sthisyear;
                string DLink = "http://www.nseindia.com/content/historical/EQUITIES/" + thisyear + "/" + monthsel + "/cm" + sthisday + monthsel + thisyear + "bhav.csv.zip";
                string SaveFileTarget = GetLocation();
                string SFile = SaveFileTarget+"/"+sthisday + "_" + monthsel + "_" + thisyear;
                // string SaveFileName= sthisday + "-" + monthsel + "-" + thisyear ;
                WebClient webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,/;q=0.8";
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31");
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(DLink), @SFile);
                while (webClient.IsBusy) ;
                FastZip zip = new FastZip();
                zip.ExtractZip(SFile, SaveFileTarget, "");
                //Console.WriteLine(SaveFileTarget);
                string path = @SaveFileTarget+"/cm"+sthisday+monthsel+thisyear+"bhav.csv";
                string converted = @SFile + "_Converted.csv";
                StreamWriter sw = new StreamWriter(@converted);
                StreamReader sr = new StreamReader(@path);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] results = line.Split(',');
                    string Symbol = results[0].ToString();
                    string Open = results[2].ToString();
                    string High = results[3].ToString();
                    string Low = results[4].ToString();
                    string Close = results[5].ToString();
                    string Last = results[6].ToString();
                    string Volume = results[8].ToString();
                    string Date = results[10].ToString();
                    //MessageBox.Show(Symbol + " " + Open + " " + High + " " + Low + " " + Close + " " +Last +" "+Volume+" "+Date);
                    string processedline = Symbol + "," + Date + "," + Open + "," + High + "," + Low + "," + Close + "," + Last + "," + Volume;
                    sw.WriteLine(processedline);
                }
                sr.Close();
                sw.Close();
                File.Delete(SFile);
                File.Delete(@path);
                MessageBox.Show("Done");



                //string[] lines = System.IO.File.ReadAllLines(SaveFileTarget);
                //Console.WriteLine(lines);

            }
        }
        private void NotValidDay()
        {
            MessageBox.Show("Not A Valid Day");
            Form1 Again = new Form1();

        }
        private string GetLocation()
        {
            string SaveFileTarget = Properties.Settings.Default.SaveLocation;
            return SaveFileTarget;
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }

        private void Download_Click(object sender, EventArgs e)
        {

        }

    }


}
