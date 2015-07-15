using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class AbstractRepository<T> where T : class
    {
        public T Get(object id)
        {
            using (TaskListContext session = new TaskListContext())
            {
                var entity = session.Set<T>().Find(id);
                return entity;
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (TaskListContext session = new TaskListContext())
            {
                return session.Set<T>().ToList();
            }
            
        }

        public void Add(T newEntity)
        {
            using (TaskListContext session = new TaskListContext())
            {
                session.Set<T>().Add(newEntity);
                session.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (TaskListContext session = new TaskListContext())
            {
                session.Entry(entity).State = EntityState.Modified;
                session.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (TaskListContext session = new TaskListContext())
            {
                var entity = session.Set<T>().Find(id);
                session.Set<T>().Remove(entity);

                session.SaveChanges();
            }
        }
    }
}
