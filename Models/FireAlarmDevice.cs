using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAlarmTool.Models
{
    public class FireAlarmDevice
    {
        public string LoopAddress { get; set; }
        public string Loop { get; set; }
        public string Address { get; set; }
        public string PriZone { get; set; }
        public string SecZone { get; set; }
        public string MaskedOut { get; set; }
        public string DeviceType { get; set; }
        public string Latch { get; set; }
        public string OpGroup { get; set; }
        public string LocationText { get; set; }
        public string BlockReference { get; set; }
        public string RoomNumber { get; set; }
        public bool Selected { get; set; }
    }

    public class UpdateFireAlarmDevice
    {
        public string LoopAddress { get; set; }
        public string Loop { get; set; }
        public string Address { get; set; }
        public string PriZone { get; set; }
        public string SecZone { get; set; }
        public string MaskedOut { get; set; }
        public string DeviceType { get; set; }
        public string Latch { get; set; }
        public string OpGroup { get; set; }
        public string LocationText { get; set; }
        public string BlockReference { get; set; }
        public string RoomNumber { get; set; }
        public string NewZone { get; set; }
        public string NewLoop { get; set; }
        public string NewAddress { get; set; }
        public string NewLocationText { get; set; }
        public bool Selected { get; set; }
    }
}
