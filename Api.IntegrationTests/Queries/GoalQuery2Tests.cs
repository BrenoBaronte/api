using Api.IntegrationTests.AutoData;
using Api.Repositories.DbConnection;
using Api.Repositories.Queries;
using AutoFixture.Idioms;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests.Queries
{
    [Collection("DatabaseSetup")]
    public class GoalQuery2Tests
    {
        [Theory, AutoDataNSubstitute]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalQuery2).GetConstructors());
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAsync_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var goalId = new Guid("ca41679d-ffb0-4899-a357-9f4de75d278a");
            var sut = new GoalQuery2(sqlConnectionFactory);

            // Act
            var result = await sut.GetAsync(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Id.Should().Be(new Guid("ca41679d-ffb0-4899-a357-9f4de75d278a"));
            result.Title.Should().Be("Check E-mails");
            result.Count.Should().Be(50);
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAsync_WhenGoalDoesntExists_ShouldPerformCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var goalId = Guid.NewGuid();
            var sut = new GoalQuery2(sqlConnectionFactory);

            // Act
            var result = await sut.GetAsync(goalId);

            // Asserts
            result.Should().BeNull();
        }


        [Theory, AutoDataNSubstitute]
        public async Task GetAllAsync_ShouldReturnCorrectly(
            SqlConnectionFactory sqlConnectionFactory)
        {
            // Arrange
            var sut = new GoalQuery2(sqlConnectionFactory);

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
    }
}
