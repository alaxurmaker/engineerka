using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
   public class User
    {
        public User()
        {
            Role = "student";
           // ImagePath = "default_avatar.jpg";
        }
        public int UserID { get; set; }

        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }

        public int ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImagePath { get; set; }
      //  public HttpPostedFileWrapper ImageFile { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
