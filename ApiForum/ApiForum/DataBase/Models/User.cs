using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.DataBase.Models
{
    public class User:IdentityUser
    {
        public int? Subscribers { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
        public Publisher Publisher { get; set; }
        public User Subscriber { get; set; }

    }
}
