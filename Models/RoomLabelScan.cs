using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace FireAlarmTool.Models
{
    public class RoomLabelScan
    {
        public ObjectId BlockId { get; set; }
        public RoomLabel RoomLabel { get; set; }

        public Point3d InsertPoint { get; set; }
    }
}
