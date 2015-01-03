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
            SaveLocationTextBox.Text = Properties.Settings.Default.SaveLocation;

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
    }
}
