using FluentValidation;
using Sprout.Exam.Domain.DTOs.Employee.Commands;

namespace Sprout.Exam.Business.Features.Employee.Validation
{
    public class DeleteEmployeeValidation : AbstractValidator<DeleteEmployeeRequestDto>
    {
        public DeleteEmployeeValidation()
        {
            RuleFor(f => f.Id).NotEqual(0).NotEmpty().NotNull()
                .WithMessage("Invalid employee.");
        }

    }
}
