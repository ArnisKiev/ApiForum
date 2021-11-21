using ApiForum.DataBase.Models;
using ApiForum.Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.Managers
{
    public class PublisherSubscriberManager : IRepository<PublicherSubscriber>
    {
        private ForumContext FC;
        public PublisherSubscriberManager()
        {
            FC = new ForumContext();
        }
        public PublicherSubscriber Add(PublicherSubscriber item)
        {
            FC.PublicherSubscribers.Add(item);
            return item;
           
        }

        public void Delete(PublicherSubscriber item)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id)
        {
            var deletedRaw = FC.PublicherSubscribers.FirstOrDefault(x => x.Id == id);
            if (deletedRaw != null)
                FC.PublicherSubscribers.Remove(deletedRaw);
        }

        public Task<PublicherSubscriber> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PublicherSubscriber> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            FC.SaveChanges();
        }

        public void Update(PublicherSubscriber item)
        {
            FC.PublicherSubscribers.Update(item);
        }
    }
}
