using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Geometry;

namespace FireAlarmTool.Models
{
    public class MTextScan
    {
        public string MTextString { get; set; }
        public Point3d InsertionPoint { get; set; }
    }
}
