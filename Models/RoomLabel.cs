using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAlarmTool.Models
{
    public class RoomLabel
    {
        public string RNAME { get; set; }
        public string RNAME2 { get; set; }
        public string BLOCKREF { get; set; }
        public string RoomNumber { get; set; }
        public bool Selected { get; set; }
    }

    public class UpdateRoomLabel
    {
        public string RNAME { get; set; }
        public string RNAME2 { get; set; }
        public string BLOCKREF { get; set; }
        public string RoomNumber { get; set; }
        public string UPDATERNAME { get; set; }
        public string UPDATERNAME2 { get; set; }
        public string UPDATEBLOCKREF { get; set; }
        public string UPDATERoomNumber { get; set; }
        public bool Selected { get; set; }
    }
}
