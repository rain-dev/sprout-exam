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
    public class DeleteEmployeeCommandTests
    {
        [Theory]
        [InlineAutoData(1)]
        [InlineAutoData(2)]
        [InlineAutoData(5)]
        public async Task Handle_Should_Delete_Employee_And_Return_Success_Response(
            int employeeId)
        {
            // Arrange
            var request = new DeleteEmployeeRequestDto 
            {
                Id = employeeId
            };

            var logger = Substitute.For<ILogger<DeleteEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Returns(1);
            
            var command = new DeleteEmployeeCommand(logger, employeeRepository);

            // Act
            var response = await command.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().BeNull();
            response.Message.Should().Contain(string.Format(MessageConstants.DB_DELETE_SUCCESS, nameof(Employee)));
        }
        [Theory]
        [AutoData]
        public async Task Handle_Should_Not_Delete_Employee_And_Return_Failed_Response(
            int employeeId)
        {
            // Arrange
            var request = new DeleteEmployeeRequestDto
            {
                Id = employeeId
            };

            var logger = Substitute.For<ILogger<DeleteEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Returns(-1);

            var command = new DeleteEmployeeCommand(logger, employeeRepository);

            // Act
            var response = await command.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Result.Should().BeNull();
            response.Message.Should().Contain(string.Format(MessageConstants.DB_NOT_FOUND, nameof(Employee)));
        }

        [Theory]
        [AutoData]
        public async Task Handle_Should_Not_Save_Employee_And_Throw_Exception(
            int employeeId)
        {
            // Arrange
            var request = new DeleteEmployeeRequestDto
            {
                Id = employeeId
            };

            var logger = Substitute.For<ILogger<DeleteEmployeeCommand>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();


            employeeRepository
                .DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Throws(f => new Exception());

            var command = new DeleteEmployeeCommand(logger, employeeRepository);

            await Assert.ThrowsAsync<Exception>(async () => await command.Handle(request, CancellationToken.None));


            employeeRepository.ReceivedCalls();
            logger.ReceivedCalls();
        }
    }
}
