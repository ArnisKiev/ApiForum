using ApiForum.DataBase.Models;
using System;
using System.Collections.Generic;

namespace ApiForum
{
    public partial class Articles
    {
        public Guid ArticlesId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? ThemeId { get; set; }
        public string UserId { get; set; }
        public int Likes { get; set; }

        public virtual Themes Theme { get; set; }
        public virtual User User { get; set; }
    }
}
