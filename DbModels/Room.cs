using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class Room
    {
        public int RoomID { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

     //   public virtual ICollection<Room> Rooms { get; set; }
     //   public virtual ICollection<GroupTimetable> GroupTimetables { get; set; }
    }
}
