using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// List all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _mediator.Send(new ListEmployeeRequestDto());
                if (result.Success)
                {
                    if (result.Result.Length == 0)
                        return NotFound(result.Message);
                    return Ok(result.Result);
                }

                return BadRequest(result.Message);
            }
            catch (System.Exception ex)
            {
                if (ex is ValidationException)
                    return BadRequest(ex.Message.Split("\r\n"));

                return this.Problem(ex.Message, nameof(EmployeesController), 500);
            }
        }

        /// <summary>
        /// Get employee by <paramref name="id"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetEmployeeRequestDto(id));
                if (result.Success)
                {
                    if (result.Result is null)
                        return NotFound(result.Message);
                    return Ok(result.Result);
                }

                return BadRequest(result.Message);
            }
            catch (System.Exception ex)
            {
                if (ex is ValidationException)
                    return BadRequest(ex.Message.Split("\r\n"));

                return this.Problem(ex.Message, nameof(EmployeesController), 500);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeRequestDto input)
        {
            try
            {
                var result = await _mediator.Send(input);
                if (result.Success)
                {
                    return Ok(result.Result);
                }

                if (result.Message.Contains("not found")) return NotFound();

                return BadRequest(result.Message);
            }
            catch (System.Exception ex)
            {
                if (ex is ValidationException)
                    return BadRequest(ex.Message.Split("\r\n"));

                return this.Problem(ex.Message, nameof(EmployeesController), 500);
            }
        }

        /// <summary>
        /// Create a employee record based on <paramref name="input"/>
        /// </summary>
        /// <param name="input"><inheritdoc cref="CreateEmployeeRequestDto"/> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeRequestDto input)
        {
            try
            {
                var result = await _mediator.Send(input);
                if (result.Success)
                {
                    return Created($"/api/employees/{result.Result.Id}", result.Result.Id);
                }

                return BadRequest(result.Message);
            }
            catch (System.Exception ex)
            {
                if (ex is ValidationException)
                    return BadRequest(ex.Message.Split("\r\n"));

                return this. Problem(ex.Message, nameof(EmployeesController), 500);
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeRequestDto
                {
                    Id = id
                });

                if (result.Success)
                {
                    return Ok(id);
                }

                if (result.Message.Contains("not found")) return NotFound();

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
