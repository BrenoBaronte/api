using Api.Domain.Entities;
using Api.IntegrationTests.AutoData;
using Api.Repositories.DbConnection;
using Api.Repositories.Repositories;
using AutoFixture.Idioms;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests.Repositories
{
    [Collection("DatabaseSetup")]
    public class GoalRepositoryTests
    {
        [Theory, AutoDataNSubstitute]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalRepository).GetConstructors());
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAllAsync_ShouldReturnCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalRepository(sqlConnectionFactory);

            // Act
            var result = await sut.GetAllAsync();

            // Asserts
            result.Should().Contain(g =>
                g.Title == "Study English" &&
                g.Count == 110);
            result.Should().Contain(g =>
                g.Title == "Workout" &&
                g.Count == 77);
            result.Should().Contain(g =>
                g.Title == "Create Portfolio" &&
                g.Count == 40);
        }

        [Theory, AutoDataNSubstitute]
        public async Task CreateAsync_ShouldPerformCorrectly(
            Goal goal,
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalRepository(sqlConnectionFactory);

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
            var sut = new GoalRepository(sqlConnectionFactory);
            var goal = new Goal
            {
                Id = new Guid("f4f25a21-6a88-4623-89cc-7d0ed349e0ea"),
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
            var sut = new GoalRepository(sqlConnectionFactory);
            var goalId = new Guid("74e2948a-37a4-457d-9254-2cbed39ae27f");

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
            var sut = new GoalRepository(sqlConnectionFactory);
            var goalId = new Guid("74e2948a-0000-0000-0000-2cbed39ae27f");

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeFalse();
        }
    }
}
