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
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Test.Features.Employee
{
    public class GetEmployeeQueryTest
    {
        [Theory]
        [InlineAutoData(1)]
        [InlineAutoData(2)]
        [InlineAutoData(3)]
        public async Task Handle_Should_Get_Employee_And_Return_Success_Response(
            int employeeId)
        {
            // Arrange
            var request = new GetEmployeeRequestDto(employeeId);
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var logger = Substitute.For<ILogger<GetEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetByAsync(Arg.Any<Expression<Func<Domain.Models.Employee ,bool>>>(), Arg.Any<CancellationToken>())
                .Returns(fixture.Create<Domain.Models.Employee>());

            var query = new GetEmployeeQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_Should_Not_Exists_Employee_And_Return_Failed_Response()
        {
            // Arrange
            var request = new GetEmployeeRequestDto(0);

            var logger = Substitute.For<ILogger<GetEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var query = new GetEmployeeQuery(logger, employeeRepository);

            // Act
            var response = await query.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_Not_List_Employee_And_Throw_Exception()
        {
            // Arrange
            var request = new GetEmployeeRequestDto(0);

            var logger = Substitute.For<ILogger<GetEmployeeQuery>>();
            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository
                .GetByAsync(Arg.Any<Expression<Func<Domain.Models.Employee, bool>>>(), Arg.Any<CancellationToken>())
                .Throws(f => new Exception());

            var query = new GetEmployeeQuery(logger, employeeRepository);

            // Act
            await Assert.ThrowsAsync<Exception>(async () => await query.Handle(request, CancellationToken.None));

            // Assert
            employeeRepository.ReceivedCalls();
            logger.ReceivedCalls();
        }
    }
}
