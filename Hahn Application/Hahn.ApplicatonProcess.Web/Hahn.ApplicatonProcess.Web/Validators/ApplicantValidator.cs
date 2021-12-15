using FluentValidation;
using Hahn.ApplicatonProcess.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Web.Validators
{
    public class ApplicantValidator:AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.Age).NotEmpty().GreaterThanOrEqualTo(20).LessThanOrEqualTo(60);
            RuleFor(a => a.CountryOfOrigin).NotEmpty();
            RuleFor(a => a.IsHired).NotNull();

        }
    }
}
