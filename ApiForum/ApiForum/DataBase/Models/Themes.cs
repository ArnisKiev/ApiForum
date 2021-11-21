using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiForum
{
    public partial class Themes
    {
        
    
        public Guid ThemesId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
    }
}
