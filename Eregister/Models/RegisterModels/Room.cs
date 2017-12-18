using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eregister
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

     //   public virtual ICollection<Room> Rooms { get; set; }
     //   public virtual ICollection<GroupTimetable> GroupTimetables { get; set; }
    }
}
