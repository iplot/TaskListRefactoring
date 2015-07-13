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
        private DbContext _context;
        private DbSet<T> _dataStore;

        public AbstractRepository(TaskListContext context)
        {
            _context = context;
            _dataStore = _context.Set<T>();
        }

        public T Get(object id)
        {
            openAndCloseConnection(true);
            var entity = _dataStore.Find(id);
            openAndCloseConnection(false);

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            openAndCloseConnection(true);
            var data = _dataStore.ToList();
            openAndCloseConnection(false);

            return data;
        }

        public void Add(T newEntity)
        {
            openAndCloseConnection(true);
            _dataStore.Add(newEntity);
            _context.SaveChanges();
            openAndCloseConnection(false);
        }

        public void Update(T entity)
        {
            openAndCloseConnection(true);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            openAndCloseConnection(false);
        }

        public void Delete(int id)
        {
            openAndCloseConnection(true);
            var entity = _dataStore.Find(id);
            _dataStore.Remove(entity);

            _context.SaveChanges();
            openAndCloseConnection(false);
        }

        private void openAndCloseConnection(bool openConnection)
        {
            if (openConnection)
            {
                _context.Database.Connection.Open(); 
            }
            else
            {
                _context.Database.Connection.Close();
            }
        }
    }
}
