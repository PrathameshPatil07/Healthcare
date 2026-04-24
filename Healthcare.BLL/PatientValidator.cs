using FluentValidation;
using Healthcare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.BLL
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[0-9]{10}$").WithMessage("Phone must be 10 digits");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Invalid DOB");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender");
        }
    }
}
