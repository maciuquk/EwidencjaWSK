using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.ViewModel
{
    public class UserRoleViewModel
    {
        public IdentityUser ApplicationUser { get; set; }
        public IList<string> Role { get; set; }

        //public IdentityUser ApplicationUser { get; set; }
        //public IList<string> Role { get; set; }
    }

}
