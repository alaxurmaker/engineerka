using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Eregister.Models
{
    public static class UserHelpers
    {

        public static string CustomSkin(this IPrincipal user)
        {
            var context = new ApplicationDbContext();
           // var userId = user.Identity.Name;
            var skin = context.Users.Where(x => x.Id==user.Identity.Name).FirstOrDefault().CustomSkin;         

            if(String.IsNullOrEmpty(skin))
            {
                skin = "~/Views/Shared/_AdminTemplate.cshtml";
            }
            return skin; 
        }
    }
}