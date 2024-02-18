using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Sprout.Exam.Business.Features.Employee.Queries;
using Sprout.Exam.DataAccess.Repository.Employee;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Test.Features.Employee
{
    public class ListEmployeeQueryTest
    {
        [Theory]
        [InlineAutoData(100)]
        [InlineAutoData(500)]
        [InlineAutoData(200)]
        public async Task Handle_Should_List_Employee_And_Return_Success_Response(
            int recordCount)
        {
            // Arrange
            var request = new ListEmployeeRequestDto();
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var logger = Substitute.For<ILogger<ListEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAllByAsync(Arg.Any<Expression<Func<Domain.Models.Employee ,bool>>>(), Arg.Any<CancellationToken>())
                .Returns(fixture.CreateMany<Domain.Models.Employee>(recordCount).ToList());

            var query = new ListEmployeeQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.Length.Should().Be(recordCount);
        }

        [Fact]
        public async Task Handle_Should_Not_List_Employee_And_Return_Failed_Response()
        {
            // Arrange
            var request = new ListEmployeeRequestDto();

            var logger = Substitute.For<ILogger<ListEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAllByAsync(Arg.Any<Expression<Func<Domain.Models.Employee, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(new List<Domain.Models.Employee>());

            var query = new ListEmployeeQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
            response.Result.Length.Should().Be(0);
        }

        [Fact]
        public async Task Handle_Should_Not_List_Employee_And_Throw_Exception()
        {
            // Arrange
            var request = new ListEmployeeRequestDto();

            var logger = Substitute.For<ILogger<ListEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();


            employeeRepository
                .GetAllByAsync(Arg.Any<Expression<Func<Domain.Models.Employee, bool>>>(), Arg.Any<CancellationToken>())
                .Throws(f => new Exception());

            var command = new ListEmployeeQuery(logger, employeeRepository);

            await Assert.ThrowsAsync<Exception>(async () => await command.Handle(request, CancellationToken.None));


            employeeRepository.ReceivedCalls();
            logger.ReceivedCalls();
        }
    }
}
