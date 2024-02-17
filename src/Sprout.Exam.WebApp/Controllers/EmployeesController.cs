using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Domain.DTOs;
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
            var result = await _mediator.Send(new ListEmployeeRequestDto());
            if (result.Success)
            {
                if (result.Result.Length == 0)
                    return NotFound(result.Message);
                return Ok(result.Result);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Get employee by <paramref name="id"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeRequestDto
            {
                Id = id
            });
            if (result.Success)
            {
                if (result.Result is null)
                    return NotFound(result.Message);
                return Ok(result.Result);
            }

            return BadRequest(result.Message);
        }

        ///// <summary>
        ///// Refactor this method to go through proper layers and update changes to the DB.
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(EditEmployeeDto input)
        //{
        //    var item = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == input.Id));
        //    if (item == null) return NotFound();
        //    item.FullName = input.FullName;
        //    item.Tin = input.Tin;
        //    item.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
        //    item.TypeId = input.TypeId;
        //    return Ok(item);
        //}

        /// <summary>
        /// Create a employee record based on <paramref name="input"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeRequestDto input)
        {
            var result = await _mediator.Send(input);
            if (result.Success)
            {
                return Created($"/api/employees/{result.Result.Id}", result.Result.Id);
            }

            return BadRequest(result.Message);

        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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



        ///// <summary>
        ///// Refactor this method to go through proper layers and use Factory pattern
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="absentDays"></param>
        ///// <param name="workedDays"></param>
        ///// <returns></returns>
        //[HttpPost("{id}/calculate")]
        //public async Task<IActionResult> Calculate(int id,decimal absentDays,decimal workedDays)
        //{
        //    var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));

        //    if (result == null) return NotFound();
        //    var type = (EmployeeType) result.TypeId;
        //    return type switch
        //    {
        //        EmployeeType.Regular =>
        //            //create computation for regular.
        //            Ok(25000),
        //        EmployeeType.Contractual =>
        //            //create computation for contractual.
        //            Ok(20000),
        //        _ => NotFound("Employee Type not found")
        //    };

        //}

    }
}
