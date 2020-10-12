using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Data
{
    public class TestGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private T[] database = new T[5];
        public T Get(int id)
        {
            return database[0];
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public void Insert(T t)
        {
            
        }

        public void Update(T t)
        {
            Console.WriteLine("");
        }

        public void Delete(T t)
        {
            throw new NotImplementedException();
        }
    }
    
}