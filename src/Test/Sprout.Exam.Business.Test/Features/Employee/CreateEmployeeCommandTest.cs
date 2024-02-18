using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Sprout.Exam.Business.Features.Employee.Command;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Test.Features.Employee
{
    public class CreateEmployeeCommandTests
    {
        [Theory]
        [InlineAutoData("John Doe", -18, "23151", 4444.0, CommonEnums.EmployeeType.Regular)]
        [InlineAutoData("John Cruz", -23, "123123123", 9999.2, CommonEnums.EmployeeType.Regular)]
        [InlineAutoData("John Jose", -20, "15123", 12315.2, CommonEnums.EmployeeType.Contractual)]
        public async Task Handle_Should_Save_Employee_And_Return_Success_Response(
            string name,
            int yearsMinus,
            string tin,
            decimal salary,
            CommonEnums.EmployeeType employeeType)
        {
            // Arrange
            var request = new CreateEmployeeRequestDto
            {
                FullName = name,
                Birthdate = DateTime.Now.AddYears(yearsMinus),
                Tin = tin,
                Salary = salary,
                TypeId = employeeType // Example type ID
            };

            var logger = Substitute.For<ILogger<CreateEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.SaveAsync(Arg.Any<Domain.Models.Employee>(), Arg.Any<CancellationToken>())
                .Returns(1);
            
            var command = new CreateEmployeeCommand(logger, employeeRepository);

            // Act
            var response = await command.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Message.Should().Contain(string.Format(MessageConstants.DB_WRITE_SUCCESS, nameof(Employee)));
        }
        [Theory]
        [AutoData]
        public async Task Handle_Should_Not_Save_Employee_And_Return_Faled_Response(
            string name,
            int yearsMinus,
            string tin,
            decimal salary,
            CommonEnums.EmployeeType employeeType)
        {
            // Arrange
            var request = new CreateEmployeeRequestDto
            {
                FullName = name,
                Birthdate = DateTime.Now.AddYears(yearsMinus),
                Tin = tin,
                Salary = salary,
                TypeId = employeeType // Example type ID
            };

            var logger = Substitute.For<ILogger<CreateEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository
                .SaveAsync(Arg.Any<Domain.Models.Employee>(), Arg.Any<CancellationToken>())
                .Returns(-1);

            var command = new CreateEmployeeCommand(logger, employeeRepository);

            // Act
            var response = await command.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Result.Should().BeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Contain(string.Format(MessageConstants.DB_WRITE_FAILED, nameof(Employee)));
        }

        [Theory]
        [AutoData]
        public async Task Handle_Should_Not_Save_Employee_And_Throw_Exception(
            string name,
            int yearsMinus,
            string tin,
            decimal salary,
            CommonEnums.EmployeeType employeeType)
        {
            // Arrange
            var request = new CreateEmployeeRequestDto
            {
                FullName = name,
                Birthdate = DateTime.Now.AddYears(yearsMinus),
                Tin = tin,
                Salary = salary,
                TypeId = employeeType // Example type ID
            };

            var logger = Substitute.For<ILogger<CreateEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();


            employeeRepository
                .SaveAsync(Arg.Any<Domain.Models.Employee>(), Arg.Any<CancellationToken>())
                .Throws(f => new Exception());

            var command = new CreateEmployeeCommand(logger, employeeRepository);

            await Assert.ThrowsAsync<Exception>(async () => await command.Handle(request, CancellationToken.None));


            employeeRepository.ReceivedCalls();
            logger.ReceivedCalls();
;
        }
    }
}
