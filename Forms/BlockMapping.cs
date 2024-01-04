using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireAlarmTool.Models;

namespace FireAlarmTool.Forms
{
    public partial class BlockMapping : Form
    {
        public List<BlockMap> blockMap = new List<BlockMap>();

        public event EventHandler BlockMapUpdated;

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public BlockMapping(List<BlockMap> liveBlockMap, string[] blockNames)
        {
            InitializeComponent();
            blockMap = liveBlockMap;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = blockMap;
            dataGridView1.Columns["DeviceType"].DataPropertyName = "DeviceType";
            dataGridView1.Columns["BlockName"].DataPropertyName = "BlockName";
            (dataGridView1.Columns["BlockName"] as DataGridViewComboBoxColumn).DataSource = blockNames;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(this.BlockMapUpdated != null)
            {
                BlockMapUpdated(this, EventArgs.Empty);
            }
        }
    }
}
