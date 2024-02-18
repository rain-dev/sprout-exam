using FluentValidation;
using Sprout.Exam.Domain.DTOs.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using System;

namespace Sprout.Exam.Business.Features.Employee.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator() {

            RuleFor(f => f.FullName).NotEmpty().NotNull()
                .WithMessage($"{nameof(CreateEmployeeRequestDto.FullName)} is required.");

            RuleFor(f => f.Tin).NotEmpty().NotNull()
                .WithMessage($"TIN is required.");

            RuleFor(f => f.Salary).NotEmpty().NotNull();
            RuleFor(f => f.Salary).GreaterThan(0);

            RuleFor(f => f.FullName).MaximumLength(100).MinimumLength(10);
            RuleFor(f => f.Tin).MaximumLength(100).MinimumLength(10);
            RuleFor(f => f.Tin).Matches("([1-9]{3})-([1-9]{3})-([1-9]{3})-([0]{3})")
                .WithMessage("Invalid TIN number. Should be XXX-XXX-XXX-000.");
            RuleFor(f => f.Birthdate).GreaterThan(DateTime.MinValue);

        }
    }
}
