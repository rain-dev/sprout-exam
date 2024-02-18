using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Sprout.Exam.Business.Features.Salary.Queries;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs.Salary;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Test.Features.Salary
{
    public class CalculateSalaryQueryTest
    {
        [Theory]
        [AutoData]
        [InlineAutoData(1, 12.0, 100_000)]
        [InlineAutoData(2, 5, 50_000)]
        [InlineAutoData(3, 10, 30_000)]
        [InlineAutoData(0, 1, 30_000)]
        public async Task Handle_Should_Calculate_Regular_Employee_Salary_And_Return_Success_Response(
            int absentDays,
            decimal tax,
            decimal salary,
            int employeeId)
        {
            // Arrange
            var request = new CalculateSalaryRequestDto
            {
                AbsentDays = absentDays,
                WorkedDays = 0,
                Id = employeeId
            };
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var logger = Substitute.For<ILogger<CalculateSalaryQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetByAsync(Arg.Any<Expression<Func<Domain.Models.Employee ,bool>>>(), Arg.Any<CancellationToken>(), Arg.Any<string>())
                .Returns(fixture.Build<Domain.Models.Employee>()
                    .With(f=> f.Salary, salary)
                    .With(f=> f.EmployeeTypeId, 1)
                    .With(f=> f.EmployeeType, new Domain.Models.EmployeeType()
                    {
                        Tax = tax
                    })
                    .Create());

            var query = new CalculateSalaryQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            var assumptionSalary = salary -  ((salary / 22) * absentDays) - (salary * tax / 100);
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.NetIncome.Should().Be(Math.Round(assumptionSalary, 2));

        }
        [Theory]
        [AutoData]
        [InlineAutoData(10)]
        [InlineAutoData(22)]
        [InlineAutoData(15)]
        public async Task Handle_Should_Calculate_Contractual_Employee_Salary_And_Return_Success_Response(
            int workedDays,
            decimal salary,
            int employeeId)
        {
            // Arrange
            var request = new CalculateSalaryRequestDto
            {
                AbsentDays = 0,
                WorkedDays = workedDays,
                Id = employeeId
            };
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var logger = Substitute.For<ILogger<CalculateSalaryQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetByAsync(Arg.Any<Expression<Func<Domain.Models.Employee, bool>>>(), Arg.Any<CancellationToken>(), Arg.Any<string>())
                .Returns(fixture.Build<Domain.Models.Employee>()
                    .With(f => f.Salary, salary)
                    .With(f => f.EmployeeTypeId, 2)
                    .With(f => f.EmployeeType, new Domain.Models.EmployeeType()
                    {
                        Tax = null
                    })
                    .Create());

            var query = new CalculateSalaryQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            var assumptionSalary = salary * workedDays;
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.NetIncome.Should().Be(Math.Round(assumptionSalary, 2));

        }

        [Fact]
        public async Task Handle_Should_Not_Exists_Employee_And_Return_Failed_Response()
        {
            // Arrange
            var request = new CalculateSalaryRequestDto();


            var logger = Substitute.For<ILogger<CalculateSalaryQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();


            var query = new CalculateSalaryQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Result.Should().BeNull();
        }

        //[Fact]
        //public async Task Handle_Should_Not_List_Employee_And_Throw_Exception()
        //{
        //    // Arrange
        //    var request = new GetEmployeeRequestDto(0);

        //    var logger = Substitute.For<ILogger<GetEmployeeQuery>>();
        //    var employeeRepository = Substitute.For<IEmployeeRepository>();

        //    employeeRepository
        //        .GetByAsync(Arg.Any<Expression<Func<Domain.Models.Employee, bool>>>(), Arg.Any<CancellationToken>())
        //        .Throws(f => new Exception());

        //    var query = new GetEmployeeQuery(logger, employeeRepository);

        //    // Act
        //    await Assert.ThrowsAsync<Exception>(async () => await query.Handle(request, CancellationToken.None));

        //    // Assert
        //    employeeRepository.ReceivedCalls();
        //    logger.ReceivedCalls();
        //}
    }
}
