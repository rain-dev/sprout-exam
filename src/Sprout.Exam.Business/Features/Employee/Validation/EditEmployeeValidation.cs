using FluentValidation;
using Sprout.Exam.Domain.DTOs.Employee.Commands;

namespace Sprout.Exam.Business.Features.Employee.Validation
{
    public class EditEmployeeValidation : AbstractValidator<EditEmployeeRequestDto>
    {
        public EditEmployeeValidation()
        {
            Include(new EmployeeValidator());

            RuleFor(f => f.Id).NotEqual(0).NotEmpty().NotNull()
                .WithMessage("Invalid employee.");
        }

    }
}
