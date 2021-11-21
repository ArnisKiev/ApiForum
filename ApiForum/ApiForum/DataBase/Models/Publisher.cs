using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.DataBase.Models
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ICollection<PublicherSubscriber> PublicherSubscribers { get; set; }

    }
}
