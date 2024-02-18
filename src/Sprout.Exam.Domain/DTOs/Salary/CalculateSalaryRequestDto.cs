using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sprout.Exam.Domain.DTOs.Salary
{
    /// <summary>
    /// Request for calculating employee salary based on <inheritdoc cref="AbsentDays" /> or <inheritdoc cref="WorkedDays"/> 
    /// </summary>
    public class CalculateSalaryRequestDto : IRequest<ResponseDto<CalculatedSalaryDto>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// Number of days absent
        /// </summary>
        [FromBody]
        public decimal AbsentDays { get; set; }
        /// <summary>
        /// Number of worked days
        /// </summary>
        [FromBody]
        public decimal WorkedDays { get; set; }

    }
}
