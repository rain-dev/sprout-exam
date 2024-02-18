using FluentValidation;
using Sprout.Exam.Domain.DTOs.Employee.Commands;

namespace Sprout.Exam.Business.Features.Employee.Validation
{
    public class CreateEmployeeValidation : AbstractValidator<CreateEmployeeRequestDto>
    {
        public CreateEmployeeValidation() {
            Include(new EmployeeValidator());
        }
    }
}
