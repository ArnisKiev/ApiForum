using ApiForum.DataBase.Models;
using ApiForum.Managers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace ApiForum.ForumManagers
{
    public class SubscriberManager
    {
        private int HHH { get; set; }
        ArticlesManager _articleManager;
        PublisherSubscriberManager _publicherSubscriberManager;
        UserManager<User> _userManager;
        public SubscriberManager(UserManager<User> userManager)
        {
            _articleManager = new ArticlesManager();
            _publicherSubscriberManager = new PublisherSubscriberManager();
            _userManager = userManager;
         
        }
        public async Task<List<Articles>> GetNewArticlesForUser(string id, int page)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                var articles = _articleManager.GetItems().ToList<Articles>();
                var chanelsOfuser=_publicherSubscriberManager.GetItems().Where(x=>x.SubscriberId==id).ToList<PublicherSubscriber>();
                var usersArticles = from art in articles
                                    join chanel in chanelsOfuser
                                    on art.UserId equals chanel.PublischerId
                                    orderby art.ArticlesId descending
                                    select (art);


                return usersArticles.ToList<Articles>().Skip(10 * page).Take(10).ToList();



            }
            return null;
        }
        public async Task<List<Articles>> GetArticlesOfUser(string id, int page)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var articles = _articleManager.GetItems();
                var userArticles = articles.Where(x => x.UserId == user.Id).Skip(page*10).Take(10);
                return userArticles.ToList();
            }

            return null;
        }
        public async Task<int> GetCountOfSubscribersOfUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return  _publicherSubscriberManager.GetItems().ToList().Where(x => x.PublischerId == id).Count();
            }
            return 0;
        }



    }
}
