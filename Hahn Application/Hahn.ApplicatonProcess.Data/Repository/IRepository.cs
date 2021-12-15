using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.Data.models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }

    
}
