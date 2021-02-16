using Api.Domain.Entities;
using Api.IntegrationTests.AutoData;
using Api.Repositories.Commands;
using Api.Repositories.DbConnection;
using AutoFixture.Idioms;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests.Commands
{
    [Collection("DatabaseSetup")]
    public class GoalCommand2Tests
    {
        [Theory, AutoDataNSubstitute]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalCommand2).GetConstructors());
        }

        [Theory, AutoDataNSubstitute]
        public async Task CreateAsync_ShouldPerformCorrectly(
            Goal goal,
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand2(sqlConnectionFactory);

            // Act
            var result = await sut.CreateAsync(goal);

            // Asserts
            result.Should().BeTrue();
        }

        [Theory, AutoDataNSubstitute]
        public async Task UpdateAsync_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand2(sqlConnectionFactory);
            var goal = new Goal
            {
                Id = new Guid("82f1c9f8-b009-471b-abbc-fde7d33ed1e4"),
                Title = "Study Investment",
                Count = 40
            };

            // Act
            var result = await sut.UpdateAsync(goal);

            // Asserts
            result.Should().BeTrue();
        }

        // TODO: test for update fails because id doesnt exist

        [Theory, AutoDataNSubstitute]
        public async Task DeleteAsync_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand2(sqlConnectionFactory);
            var goalId = new Guid("fee01c19-8cf4-431d-b233-d1238c6966c2");

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeTrue();
        }

        [Theory, AutoDataNSubstitute]
        public async Task DeleteAsync_WhenIdDoesntExist_ShouldPerformCorrectly(
           SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalCommand2(sqlConnectionFactory);
            var goalId = new Guid("74e2948a-0000-0000-0000-2cbed39ae27f");

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeFalse();
        }
    }
}
