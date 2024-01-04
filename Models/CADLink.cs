using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Geometry;

namespace FireAlarmTool.Models
{
    public class CADLink
    {
        public FireAlarmDevice FireAlarmDevice { get; set;}
        public Point2d InsertionPoint { get; set; }
        public BlockMap MappedBlock { get; set; }
    }
}
