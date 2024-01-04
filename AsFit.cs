using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireAlarmTool.Forms;

// Import Autodesk Dependancies
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace FireAlarmTool
{
    public class AsFit
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new OperationSelect());
        }

        [CommandMethod("FAT", CommandFlags.UsePickSet)]
        public void FAT()
        {
            OperationSelect op = new OperationSelect();
            op.Show();
        }

        [CommandMethod("FAT_version", CommandFlags.UsePickSet)]
        public void FAT_version()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nFire Alarm Tool Version: 0.1");
        }
    }    
}
