using MediatR;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Mapping;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Features.Employee.Queries
{
    public class ListEmployeeQuery : IRequestHandler<ListEmployeeRequestDto, ListResponseDto<EmployeeDto>>
    {
        private readonly ILogger<ListEmployeeQuery> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        public ListEmployeeQuery(
            ILogger<ListEmployeeQuery> logger,
            IEmployeeRepository employeeRepository)
        {

            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        public async Task<ListResponseDto<EmployeeDto>> Handle(ListEmployeeRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var mapper = new EmployeeMapping();

                _logger.LogInformation("Listing all Employees");
                var entities = await _employeeRepository.GetAllByAsync(f => f.IsDeleted == false, cancellationToken);

                if (entities.Count == 0)
                    return new ListResponseDto<EmployeeDto>(new EmployeeDto[] { } )
                    {
                        Message = string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)),
                        Success = true
                    };

                return new ListResponseDto<EmployeeDto>(mapper.MapFromEntities(entities).ToArray())
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

