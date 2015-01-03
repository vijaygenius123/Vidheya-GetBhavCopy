using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            dateTimePicker2.MaxDate = DateTime.Now;
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
                MessageBox.Show("Selected Path " + sSelectedPath);


            }


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
