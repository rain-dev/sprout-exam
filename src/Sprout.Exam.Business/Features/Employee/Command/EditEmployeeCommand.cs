using MediatR;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Mapping;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Features.Employee.Command
{
    public class EditEmployeeCommand : IRequestHandler<EditEmployeeRequestDto, ResponseDto<ReadEmployeeDto>>
    {
        private readonly ILogger<EditEmployeeCommand> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EditEmployeeCommand(
            ILogger<EditEmployeeCommand> logger,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseDto<ReadEmployeeDto>> Handle(EditEmployeeRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var mapper = new EmployeeMapping();
                _logger.LogInformation("Updating Employee {employeeId}", request.Id);

                var entity = await _employeeRepository.GetByAsync(f => f.Id == request.Id, cancellationToken);

                if (entity is null)
                    return new ResponseDto<ReadEmployeeDto>(null)
                    {
                        Message = string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)),
                        Success = false
                    };

                mapper.MapFromEdit(request, entity);

                var result = await _employeeRepository.UpdateAsync(entity, cancellationToken);

                if (result <= 0)
                    return new ResponseDto<ReadEmployeeDto>(null)
                    {
                        Message = string.Format(MessageConstants.DB_WRITE_FAILED, nameof(Employee)),
                        Success = false
                    };

                return new ResponseDto<ReadEmployeeDto>(mapper.MapFromEntity(entity))
                {
                    Message = string.Format(MessageConstants.DB_WRITE_SUCCESS, nameof(Employee)),
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
