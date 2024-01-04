using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FireAlarmTool.Forms
{
    public partial class OperationSelect : Form
    {
        

        public OperationSelect()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilePathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog csvFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if(csvFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathTxtBox.Text = Path.GetFullPath(csvFileDialog.FileName);
            }
        }

        private bool Check_Inputs()
        {
            if (FilePathTxtBox.Text != "")
            {
                if (this.ValidateChildren())
                {
                    // continue
                    //MessageBox.Show("Done!");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if(Check_Inputs())
            {
                // run code
                this.Hide();

                // show next form
                DataView dv = new DataView(FilePathTxtBox.Text, ActionComboBox.Text, BlockComboBox.Text);
                dv.FormClosed += new FormClosedEventHandler(DataView_Closed);
                dv.CSVError += CSVError;

                dv.Show();
            }
            else
            {
                MessageBox.Show("Check all fields are correctly selected!");
            }
        }

        public void DataView_Closed(object sender, FormClosedEventArgs e)
        {
            this.Close(); // handles the other form getting closed prematurely. Kills the application for cad.
        }

        private void ActionComboBox_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = true;
            foreach(string item in ActionComboBox.Items)
            {
                if (ActionComboBox.Text == item)
                {
                    cancel = false;
                    continue;
                }
            }

            if(cancel)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

        }

        private void BlockComboBox_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = true;
            foreach (string item in BlockComboBox.Items)
            {
                if (BlockComboBox.Text == item)
                {
                    cancel = false;
                    continue;
                }
            }

            if (cancel)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        public void CSVError(object sender, EventArgs e)
        {
            DataView dv = (DataView)sender;
            dv.Close();

            MessageBox.Show("Error with CSV parsing. Please check the CSV file isn't open in Excel.");

            this.Close();
           
        }
    }
}
