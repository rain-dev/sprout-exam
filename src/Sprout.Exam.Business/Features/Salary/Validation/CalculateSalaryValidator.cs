using FluentValidation;
using Sprout.Exam.Domain.DTOs.Salary;

namespace Sprout.Exam.Business.Features.Salary.Validation
{
    public class CalculateSalaryValidator : AbstractValidator<CalculateSalaryRequestDto>
    {
        public CalculateSalaryValidator() {

            RuleFor(f => f.Id).NotEqual(0)
                .WithMessage("Employee is required.");
        }
    }
}
