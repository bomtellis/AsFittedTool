// import autodesk dependancies

using Autodesk.AutoCAD.Geometry;
using FireAlarmTool.Models;
using FireAlarmTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using Autodesk.AutoCAD.Runtime;

namespace FireAlarmTool.Forms
{
    public partial class DataView : Form
    {
        public string csvFilePath, formAction, formBlock;
        private readonly CSVParser csvLib = new CSVParser();
        private readonly CAD cadLib = new CAD();
        List<FireAlarmDevice> fd = new List<FireAlarmDevice>();
        List<RoomLabel> roomLabels = new List<RoomLabel>();
        List<P4Tag> p4Tags = new List<P4Tag>();
        private DataGridViewSelectedRowCollection selected;
        private List<BlockMap> activeBlockMap = new List<BlockMap>();
        private bool firstMappingClick = true;

        // status bools
        readonly bool roomLabel = false;
        readonly bool fireAlarm = false;
        readonly bool emergencyLighting = false;

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnselectAllBtn_Click(object sender, EventArgs e)
        {
            if (fireAlarm)
            {
                foreach (FireAlarmDevice fa in fd)
                {
                    fa.Selected = false;
                }
                DataGridView.DataSource = fd;
            }

            if (roomLabel)
            {
                foreach (RoomLabel label in roomLabels)
                {
                    label.Selected = false;
                }
                DataGridView.DataSource = roomLabels;
            }

            if (emergencyLighting)
            {
                foreach (P4Tag tag in p4Tags)
                {
                    tag.Selected = false;
                }
                DataGridView.DataSource = p4Tags;
            }

            DataGridView.Refresh();
        }

        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            if (fireAlarm)
            {
                foreach (FireAlarmDevice fa in fd)
                {
                    fa.Selected = true;
                }
                DataGridView.DataSource = fd;
            }

            if (roomLabel)
            {
                foreach (RoomLabel label in roomLabels)
                {
                    label.Selected = true;
                }
                DataGridView.DataSource = roomLabels;
            }

            if (emergencyLighting)
            {
                foreach (P4Tag tag in p4Tags)
                {
                    tag.Selected = true;
                }
                DataGridView.DataSource = p4Tags;
            }

            DataGridView.Refresh();
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            selected = DataGridView.SelectedRows;
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridViewCell cell = dgv.CurrentCell;
            DataGridViewColumn owningColumn = dgv.CurrentCell.OwningColumn;

            //MessageBox.Show(owningColumn.Name);

            //var targetColumn = dgv.Columns.First(c => c.Header == "Selected");

            if (cell.RowIndex >= 0 && owningColumn.Name == "Selected")
            {
                bool checkValue = false;
                if (dgv.Rows[cell.RowIndex].Cells[cell.ColumnIndex].EditedFormattedValue != null && dgv.Rows[cell.RowIndex].Cells[cell.ColumnIndex].EditedFormattedValue.Equals(true))
                {
                    checkValue = true;
                }

                for (int i = 0; i < selected.Count; i++)
                {
                    dgv.Rows[selected[i].Index].Cells[cell.ColumnIndex].Value = checkValue;
                }

                DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DeselectPresentBtn_Click(object sender, EventArgs e)
        {
            // CAD stuff
            // has block mapping been done
            if (roomLabel)
            {
                var updatedRoomLabels = cadLib.DeselectInserted(roomLabels);
                if (updatedRoomLabels != null)
                {
                    DataGridView.DataSource = updatedRoomLabels;
                    DataGridView.Refresh();
                    roomLabels = updatedRoomLabels;
                }
            }

            if (fireAlarm)
            {
                var updatedFireSymbols = cadLib.DeselectInserted(fd, activeBlockMap);

                if (updatedFireSymbols.Count > 0)
                {
                    DataGridView.DataSource = updatedFireSymbols;
                    DataGridView.Refresh();
                    fd = updatedFireSymbols;
                }

                MessageBox.Show("If results are unexpected check the block mapping is correct!");
            }

            if (emergencyLighting)
            {
                var updatedP4Tags = cadLib.DeselectInserted(p4Tags);
                if (updatedP4Tags != null)
                {
                    DataGridView.DataSource = updatedP4Tags;
                    DataGridView.Refresh();

                    p4Tags = updatedP4Tags;
                }
            }
        }

        private void MapBtn_Click(object sender, EventArgs e)
        {
            if (firstMappingClick)
            {
                string[] sensorDevices = csvLib.GetDistinctDeviceTypes(fd);
                activeBlockMap.Clear();
                foreach (string device in sensorDevices)
                {
                    activeBlockMap.Add(new BlockMap
                    {
                        DeviceType = device
                    });
                }
                firstMappingClick = false;
            }

            string[] blocks = cadLib.FindAllBlocks();

            BlockMapping blockmap = new BlockMapping(activeBlockMap, blocks);
            blockmap.BlockMapUpdated += UpdateBlockMapping;
            blockmap.Show();
        }

        public void UpdateBlockMapping(object sender, EventArgs e)
        {
            BlockMapping blockMapping = (BlockMapping)sender;
            this.activeBlockMap = blockMapping.blockMap;
            blockMapping.Close();
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            this.Hide();


            if (fireAlarm)
            {
                // check if the automatically place blocks checkbox is checked
                if (AutoPlacementCheckBox.Checked)
                {
                    // find all inserted blocks within the drawing and compare to the csv details
                    // get insertion points of room labels and use them as the insert point for the new block.
                    // remove the inserted blocks from the list to get points for 
                    // move on after this has completed to pick points
                    var fds = cadLib.ScanDrawingAndInsert(fd, activeBlockMap);

                    // do fire alarm things
                    foreach (FireAlarmDevice device in fds)
                    {

                        // loop through all devices in csv import
                        // check they are selected
                        if (!device.Selected)
                        {
                            continue;
                        }

                        try
                        {
                            // get the insertion point
                            Point3d insertPoint = cadLib.GetInsertPoint("\nLoop/Address: " + device.LoopAddress + "\nLocation Text: " + device.LocationText + "\nDevice Zone: " + device.PriZone);

                            if (insertPoint != null)
                            {
                                // got the insertion point
                                // figure out what block is needed
                                foreach (var bm in activeBlockMap)
                                {
                                    if (bm.DeviceType == device.DeviceType)
                                    {
                                        cadLib.InsertBlock(bm.BlockName, insertPoint, device);
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }

                    }
                }
                else
                {
                    // do fire alarm things
                    foreach (FireAlarmDevice device in fd)
                    {

                        // loop through all devices in csv import
                        // check they are selected
                        if (!device.Selected)
                        {
                            continue;
                        }

                        // get the insertion point
                        try
                        {
                            Point3d insertPoint = cadLib.GetInsertPoint("\nLoop/Address: " + device.LoopAddress + "\nLocation Text: " + device.LocationText + "\nDevice Zone: " + device.PriZone);

                            if (insertPoint != null)
                            {
                                // got the insertion point
                                // figure out what block is needed
                                foreach (var bm in activeBlockMap)
                                {
                                    if (bm.DeviceType == device.DeviceType)
                                    {
                                        cadLib.InsertBlock(bm.BlockName, insertPoint, device);
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                }

            }

            if (roomLabel)
            {
                if(AutoPlacementCheckBox.Checked)
                {
                    // find all the mtext objects and get insertion point
                    var rms = cadLib.ScanDrawingAndInsert(roomLabels);

                    // do room label things
                    foreach (RoomLabel label in rms)
                    {

                        // loop through all devices in csv import
                        // check they are selected
                        if (!label.Selected)
                        {
                            continue;
                        }

                        // get the insertion point
                        try
                        {
                            Point3d insertPoint = cadLib.GetInsertPoint("\nRNAME: " + label.RNAME + "\nRNAME2: " + label.RNAME2 + "\nBlock Reference: " + label.BLOCKREF + "." + label.RoomNumber);

                            if (insertPoint != null)
                            {
                                // got the insertion point
                                // figure out what block is needed
                                cadLib.InsertBlock("RoomLabel", insertPoint, label);
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                }
                else
                {
                    // do room label things
                    foreach (RoomLabel label in roomLabels)
                    {

                        // loop through all devices in csv import
                        // check they are selected
                        if (!label.Selected)
                        {
                            continue;
                        }

                        // get the insertion point
                        try
                        {
                            Point3d insertPoint = cadLib.GetInsertPoint("\nRNAME: " + label.RNAME + "\nRNAME2: " + label.RNAME2 + "\nBlock Reference: " + label.BLOCKREF + "." + label.RoomNumber);

                            if (insertPoint != null)
                            {
                                // got the insertion point
                                // figure out what block is needed
                                cadLib.InsertBlock("RoomLabel", insertPoint, label);
                                continue;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                    }
                }
            }

            if (emergencyLighting)
            {
                if (AutoPlacementCheckBox.Checked)
                {
                    var updatedP4Tags = cadLib.ScanDrawingAndInsert(p4Tags);

                    bool escapePressed = false;
                    // do emergency lighting label things
                    foreach (P4Tag tag in updatedP4Tags)
                    {

                        // loop through all devices in csv import
                        // check they are selected
                        if (!tag.Selected)
                        {
                            continue;
                        }

                        // get the insertion point
                        if (!escapePressed)
                        {
                            try
                            {
                                Point3d insertPoint = cadLib.GetInsertPoint("CB Number: " + tag.CollectorBoxNumber + "\nAddress: " + tag.Address + "\nLocation Text: " + tag.RoomName + "\nBlock Ref: " + tag.BlockRef);

                                if (insertPoint != null)
                                {
                                    // got the insertion point
                                    // figure out what block is needed
                                    cadLib.InsertBlock("P4 Reference Block", insertPoint, tag);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    bool escapePressed = false;
                    // do emergency lighting label things
                    foreach (P4Tag tag in p4Tags)
                    {
                        // loop through all devices in csv import
                        // check they are selected
                        if (!tag.Selected)
                        {
                            continue;
                        }

                        // get the insertion point
                        if (!escapePressed)
                        {
                            try
                            {
                                Point3d insertPoint = cadLib.GetInsertPoint("CB Number: " + tag.CollectorBoxNumber + "\nAddress: " + tag.Address + "\nLocation Text: " + tag.RoomName + "\nBlock Ref: " + tag.BlockRef);

                                if (insertPoint != null)
                                {
                                    // got the insertion point
                                    // figure out what block is needed
                                    cadLib.InsertBlock("P4 Reference Block", insertPoint, tag);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                break;
                            }
                        }
                    }
                }
            }



            this.Show();
            MessageBox.Show("Operation has completed.");
            //this.Close();
        }

        public DataView(string filePath, string action, string block)
        {
            InitializeComponent();

            csvFilePath = filePath;
            formAction = action;
            formBlock = block;

            switch (formAction)
            {
                //Sequential
                //Bulk
                //Update
                case "Sequential":
                    break;
                case "Bulk":
                    break;
                case "Update":
                    break;
            }

            // status bools
            //bool roomLabel = false;
            //bool fireAlarm = false;
            //bool emergencyLighting = false;

            switch (formBlock)
            {
                //Room Label
                //Fire Alarm Symbols
                //P4 Symbols
                case "Room Label":
                    MapBtn.Enabled = false;
                    this.roomLabel = true;
                    this.fireAlarm = false;
                    this.emergencyLighting = false;
                    break;
                case "Fire Alarm Symbols":
                    this.roomLabel = false;
                    this.fireAlarm = true;
                    this.emergencyLighting = false;
                    break;
                case "P4 Symbols":
                    MapBtn.Enabled = false;
                    this.emergencyLighting = true;
                    this.roomLabel = false;
                    this.fireAlarm = false;
                    break;
            }

            LoadCSVFileData();
        }

        public event EventHandler CSVError;

        public void LoadCSVFileData()
        {
            switch (formBlock)
            {

                //Room Label
                //Fire Alarm Symbols
                //P4 Symbols

                case "Room Label":
                    // do something
                    try
                    {
                        roomLabels = csvLib.RoomLabelCSV(csvFilePath);
                        roomLabels.Remove(roomLabels.First());

                        DataGridView.DataSource = roomLabels;
                    }
                    catch (Exception ex)
                    {
                        CSVError(this, EventArgs.Empty);
                        MessageBox.Show(ex.Message + "\nCheck the CSV File isn't open in Excel!");
                    }
                    break;
                case "Fire Alarm Symbols":
                    try
                    {
                        fd = csvLib.FireAlarmCSV(csvFilePath);
                        //PropertyInfo[] properties = fd[0].GetType().GetProperties();
                        fd.Remove(fd.First());

                        DataGridView.DataSource = fd;
                    }
                    catch (Exception ex)
                    {
                        CSVError(this, EventArgs.Empty);
                        MessageBox.Show(ex.Message + "\nCheck the CSV File isn't open in Excel!");
                    }
                    break;
                case "P4 Symbols":
                    // do something
                    try
                    {
                        p4Tags = csvLib.P4CSV(csvFilePath);
                        p4Tags.Remove(p4Tags.First());

                        DataGridView.DataSource = p4Tags;
                    }
                    catch (Exception ex)
                    {
                        CSVError(this, EventArgs.Empty);
                        MessageBox.Show(ex.Message + "\nCheck the CSV File isn't open in Excel!");
                    }
                    break;
            }


        }

        public class MyMessageFilter : IMessageFilter

        {

            public const int WM_KEYDOWN = 0x0100;

            public bool bCanceled = false;

            public bool PreFilterMessage(ref Message m)

            {

                if (m.Msg == WM_KEYDOWN)

                {

                    // Check for the Escape keypress

                    Keys kc = (Keys)(int)m.WParam & Keys.KeyCode;

                    if (m.Msg == WM_KEYDOWN && kc == Keys.Escape)

                    {

                        bCanceled = true;

                    }

                    // Return true to filter all keypresses

                    return true;

                }

                // Return false to let other messages through

                return false;

            }

        }
    }
}
