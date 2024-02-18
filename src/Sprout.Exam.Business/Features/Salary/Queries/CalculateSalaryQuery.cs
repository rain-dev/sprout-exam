using MediatR;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Salary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Features.Salary.Queries
{
    public class CalculateSalaryQuery : IRequestHandler<CalculateSalaryRequestDto, ResponseDto<CalculatedSalaryDto>>
    {
        private readonly ILogger<CalculateSalaryQuery> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private const int WORK_DAYS = 22;
        public CalculateSalaryQuery(
            ILogger<CalculateSalaryQuery> logger,
            IEmployeeRepository employeeRepository) {
            this._logger = logger;
            this._employeeRepository = employeeRepository;
        }
        public async Task<ResponseDto<CalculatedSalaryDto>> Handle(CalculateSalaryRequestDto request, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepository.GetByAsync(f => f.Id == request.Id, cancellationToken, nameof(Domain.Models.EmployeeType));

            if (entity is null)
                return new ResponseDto<CalculatedSalaryDto>(null)
                {
                    Message = string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)),
                    Success = false
                };
            _logger.LogInformation("Calculating salary for {employeeId}", request.Id);

            var netIncome = CalculateNetIncome(
                (Exam.Common.Enums.EmployeeType)entity.EmployeeTypeId,
                entity.Salary,
                salary =>
                {
                    if (entity.EmployeeType.Tax.HasValue)
                        return (salary - (salary * entity.EmployeeType.Tax.Value / 100));

                    return salary;
                },
                salary =>
                {
                    if (request.WorkedDays > 0)
                        return salary * request.WorkedDays;

                    if (request.AbsentDays > 0)
                        return ((salary / WORK_DAYS) * request.AbsentDays);

                    return 0;
                });
            return new(new()
            {
                NetIncome = Math.Round(netIncome, 2),
            })
            {
                Message = MessageConstants.SALARY_CALCULATION_DONE,
                Success = true
            };
        }

        private decimal CalculateNetIncome(Exam.Common.Enums.EmployeeType employeeType, decimal salary, Func<decimal, decimal> netTaxes, Func<decimal, decimal> deductions)
        {
            return employeeType switch
            {
                Exam.Common.Enums.EmployeeType.Regular => (netTaxes(salary)) - (deductions(salary)),
                Exam.Common.Enums.EmployeeType.Contractual => deductions(salary)
            };
        }
    }
}
