using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Domain.DTOs.Salary;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaryController( IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint that calculates the salary of employee based on Employee Type.
        /// </summary>
        /// <param name="id">The ID of the employee</param>
        /// <param name="calculateSalary">The request parameters for calculation <see cref="CalculatedSalaryDto"/></param>
        /// <returns></returns>
        [HttpPost("calculate/{id}")]
        public async Task<IActionResult> Calculate([FromRoute] int id, CalculateSalaryRequestDto calculateSalary)
        {
            try
            {
                calculateSalary.Id = id;
                var result = await _mediator.Send(calculateSalary);

                if (result.Success)
                    return Ok(result.Result);

                return BadRequest(result.Message);

            }
            catch (System.Exception ex)
            {
                if (ex is ValidationException)
                    return BadRequest(ex.Message.Split("\r\n"));

                return this.Problem(ex.Message, nameof(EmployeesController), 500);
            }

        }
    }
}
