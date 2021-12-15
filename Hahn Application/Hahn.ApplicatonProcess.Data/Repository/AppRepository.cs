using Hahn.ApplicatonProcess.Data.models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicatonProcess.Data.Repository
{
    public class AppRepository<T> : IRepository<T>  where T:BaseEntity
    {
        private readonly HahnAppContext _context;
        private DbSet<T> entities;
        public AppRepository(HahnAppContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return entities.AsEnumerable();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public T GetById(int id)
        {
            try
            {
                return entities.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public void Insert(T entity)
        {
            try
            {
                entities.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
           
            
        }
        public void Delete(int id)
        {
            try
            {
                T entity = entities.FirstOrDefault(s => s.Id == id);
                entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
