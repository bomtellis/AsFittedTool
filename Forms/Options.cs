using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireAlarmTool.Services;
using Autodesk.AutoCAD.DatabaseServices;

namespace FireAlarmTool.Forms
{
    public partial class Options : Form
    {
        private readonly CAD cadLib = new CAD();
        public Options()
        {
            InitializeComponent();

            string[] blocks = cadLib.FindAllBlocks();
            comboBox1.DataSource = blocks;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(comboBox1.Text);

            ObjectIdCollection objIDs = cadLib.GetDynamicBlocksByName(comboBox1.Text); 

            System.Diagnostics.Debug.WriteLine(objIDs.Count);

            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
