using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData;
using Microsoft.EntityFrameworkCore;

namespace GameRepository
{
    public class GameRepository<T>
        where T : class
    {
        private GameDataBase dbContext;

        public GameRepository(GameDataBase dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateItem(T obj)
        {
            this.dbContext.Set<T>().Add(obj);
            this.dbContext.SaveChanges();
        }

        public void DeleteItem(T obj)
        {
            this.dbContext.Remove(obj);
            this.dbContext.SaveChanges();
        }

        public T GetOne(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return this.dbContext.Set<T>();
        }

        public void Update(T entity)
        {
            this.dbContext.Set<T>().Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }
    }
}
