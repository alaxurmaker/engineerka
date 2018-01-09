using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eregister.Models
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public byte[] ImageByte { get; set; }
    }
}