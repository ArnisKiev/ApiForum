using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Subscribers { get; set; }
        public string Email { get; set; }
        
        
    }
}
