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
    public class CreateEmployeeCommand : IRequestHandler<CreateEmployeeRequestDto, ResponseDto<ReadEmployeeDto>>
    {
        private readonly ILogger<CreateEmployeeCommand> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommand(
            ILogger<CreateEmployeeCommand> logger,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseDto<ReadEmployeeDto>> Handle(CreateEmployeeRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var mapper = new EmployeeMapping();
                var entity = mapper.MapFromCreate(request);

                _logger.LogInformation("Saving {employeeName} as {employeeTypeId}", request.FullName, request.TypeId);
                var result = await _employeeRepository.SaveAsync(entity, cancellationToken);

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