using MediatR;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Mapping;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Features.Employee.Command
{
    public class DeleteEmployeeCommand : IRequestHandler<DeleteEmployeeRequestDto, ResponseDto<EmployeeDto>>
    {
        private readonly ILogger<DeleteEmployeeCommand> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommand(
            ILogger<DeleteEmployeeCommand> logger,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseDto<EmployeeDto>> Handle(DeleteEmployeeRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var mapper = new EmployeeMapping();
                _logger.LogInformation("Deleting Employee {employeeId}", request.Id);

                var result = await _employeeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result <= 0)
                    return new ResponseDto<EmployeeDto>(null)
                    {
                        Message = string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)),
                        Success = false
                    };

                return new ResponseDto<EmployeeDto>(null)
                {
                    Message = string.Format(MessageConstants.DB_DELETE_SUCCESS, nameof(Employee)),
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }

}