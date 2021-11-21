using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace ApiForum.DataBase.Models
{
    public class ForumContext:IdentityDbContext<User>
    {
        public virtual DbSet<Articles> Articles { get; set; }
       
        public virtual DbSet<PublicherSubscriber> PublicherSubscribers { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public ForumContext(DbContextOptions<ForumContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public ForumContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connString= ConfigurationManager.ConnectionStrings["DefaultString"].ConnectionString
            optionsBuilder.UseSqlServer("Initial Catalog= ForumDataBase; Integrated Security=True");
        }

    }
}
