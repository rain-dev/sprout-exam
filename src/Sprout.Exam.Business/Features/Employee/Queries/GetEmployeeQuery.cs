using MediatR;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Mapping;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Features.Employee.Queries
{
    public class GetEmployeeQuery : IRequestHandler<GetEmployeeRequestDto, ResponseDto<EmployeeDto>>
    {
        private readonly ILogger<GetEmployeeQuery> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeQuery(
            ILogger<GetEmployeeQuery> logger,
            IEmployeeRepository employeeRepository)
        {

            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        public async Task<ResponseDto<EmployeeDto>> Handle(GetEmployeeRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var mapper = new EmployeeMapping();

                _logger.LogInformation("Searching {employeeId}.", request.Id);
                var entity = await _employeeRepository.GetByAsync(f=> f.Id == request.Id);

                if (entity is null)
                    return new ResponseDto<EmployeeDto>(null)
                    {
                        Message = string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)),
                        Success = false
                    };

                return new ResponseDto<EmployeeDto>(mapper.MapFromEntity(entity))
                {
                    Message = string.Format(MessageConstants.DB_FOUND, nameof(Employee)),
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