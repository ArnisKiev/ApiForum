using ApiForum.DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForum
{
    public partial class PublicherSubscriber
    {
        [Key]
        public Guid? Id { get; set; }
        public string SubscriberId { get; set; }
        public string PublischerId { get; set; }
        public virtual Publisher Publischer{ get; set; }
        public virtual Subscriber Subscriber { get; set; }
    }
}
