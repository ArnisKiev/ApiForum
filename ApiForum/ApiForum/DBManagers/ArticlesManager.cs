using ApiForum.DataBase.Models;
using ApiForum.Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.Managers
{
    public class ArticlesManager : IRepository<Articles>
    {
        private ForumContext FC;
        public ArticlesManager()
        {
            FC = new ForumContext();
        }

        public Articles Add(Articles item)
        {
            FC.Articles.Add(item);
            return FC.Articles.Last();

        }

        public void Delete(Articles item)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id)
        {
            var deletedArticle = FC.Articles.FirstOrDefault(x => x.ArticlesId == id);
            if (deletedArticle!=null)
                FC.Articles.Remove(deletedArticle);
        }

        public async Task<Articles> FindById(Guid id)
        {
            var article = await FC.Articles.FindAsync(id);
            if (article != null)
            {
                return article;
            }
            return null;
        }
        public async Task<Articles> LikeArticleAsync(Guid id)
        {
            var article =await FC.Articles.FindAsync(id);
            if (article != null)
            {
                article.Likes += 1;
                FC.Articles.Update(article);
                return article;
            }

            return null;

        }

        public IEnumerable<Articles> GetItems()
        {
            return FC.Articles.ToList<Articles>();
        }

        public async void Save()
        {
            await FC.SaveChangesAsync();
        }

        public void Update(Articles item)
        {
            FC.Articles.Update(item);
        }
    }
}
