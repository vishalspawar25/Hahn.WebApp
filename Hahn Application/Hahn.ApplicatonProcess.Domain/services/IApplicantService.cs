using Hahn.ApplicatonProcess.Data.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.Domain.services
{
    public interface IApplicantService
    {
        IEnumerable<Applicant> GetAll();
        Applicant GetById(int id);
        void Insert(Applicant entity);
        void Update(Applicant entity);
        void Delete(int id);
    }
}
