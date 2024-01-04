using FireAlarmTool.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FireAlarmTool.Services
{
    public class CSVParser
    {
        // SECTION:
        // FIRE ALARM
        // CSV PARSER

        public List<FireAlarmDevice> FireAlarmCSV(string filePath)
        {
            List<FireAlarmDevice> result = new List<FireAlarmDevice>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        FireAlarmDevice fireDevice = new FireAlarmDevice
                        {
                            LoopAddress = values[0],
                            Loop = values[1],
                            Address = values[2],
                            PriZone = values[3],
                            SecZone = values[4],
                            MaskedOut = values[5],
                            DeviceType = values[6],
                            Latch = values[7],
                            OpGroup = values[8],
                            LocationText = values[9],
                            BlockReference = "",
                            RoomNumber = "",
                            Selected = true
                        };

                        if(values.Length > 10)
                        {
                            fireDevice.BlockReference = values[10];
                            fireDevice.RoomNumber = values[11];
                        }

                        result.Add(fireDevice);
                    }
                }
            }
            catch
            {
                return new List<FireAlarmDevice>();
            }

            return result;

        }

        public string[] GetDistinctDeviceTypes(List<FireAlarmDevice> fd)
        {
            var distinctValues = fd.Select(x => x.DeviceType).Distinct().ToList();
            return distinctValues.ToArray();
        }

        // SECTION:
        // ROOM LABELS
        // CSV PARSER

        public List<RoomLabel> RoomLabelCSV(string filePath)
        {
            List<RoomLabel> result = new List<RoomLabel>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        RoomLabel roomLabel = new RoomLabel
                        {
                            RNAME = values[0],
                            RNAME2 = values[1],
                            BLOCKREF = values[2],
                            RoomNumber = values[3],
                            Selected = true
                        };

                        result.Add(roomLabel);
                    }
                }
            }
            catch
            {
                return new List<RoomLabel>();
            }

            return result;

        }

        public List<UpdateRoomLabel> UpdateRoomLabelCSV(string filePath)
        {
            List<UpdateRoomLabel> result = new List<UpdateRoomLabel>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        UpdateRoomLabel roomLabel = new UpdateRoomLabel
                        {
                            RNAME = values[0],
                            RNAME2 = values[1],
                            BLOCKREF = values[2],
                            RoomNumber = values[3],
                            UPDATERNAME = values[4],
                            UPDATERNAME2 = values[5],
                            UPDATEBLOCKREF = values[6],
                            UPDATERoomNumber = values[7],
                            Selected = true
                        };

                        result.Add(roomLabel);
                    }
                }
            }
            catch
            {
                return new List<UpdateRoomLabel>();
            }

            return result;

        }

        public List<P4Tag> P4CSV(string filePath)
        {
            List<P4Tag> result = new List<P4Tag>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        P4Tag tag = new P4Tag
                        {
                            CollectorBoxNumber = values[0],
                            Address = values[1],
                            Type = values[2],
                            BlockRef = values[3],
                            RoomName = values[4],
                            Selected = true
                        };

                        result.Add(tag);
                    }
                }
            }
            catch
            {
                return new List<P4Tag>();
            }

            return result;

        }
    }
}