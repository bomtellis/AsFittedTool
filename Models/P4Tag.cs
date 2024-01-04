using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAlarmTool.Models
{
    public class P4Tag
    {
        public string CollectorBoxNumber { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string BlockRef { get; set; }
        public string RoomName { get; set; }
        public bool Selected { get; set; }
    }
}
