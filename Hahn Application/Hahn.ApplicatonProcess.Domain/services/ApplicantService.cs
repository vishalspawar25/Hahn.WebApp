using Hahn.ApplicatonProcess.Data;
using Hahn.ApplicatonProcess.Data.models;
using Hahn.ApplicatonProcess.Data.Repository;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.Domain.services
{
    public class ApplicantService : IApplicantService
    {
       private readonly IRepository<Applicant> _repo;
        public  ApplicantService(IRepository<Applicant> repo)
        {
            _repo = repo;
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public IEnumerable<Applicant> GetAll()
        {
            return _repo.GetAll();
        }

        public Applicant GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Insert(Applicant entity)
        {
            _repo.Insert(entity);           
        }
        public void Update(Applicant entity)
        {
            _repo.Update(entity);
        }
    }
}
