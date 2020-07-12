using Api.Domain.Entities;
using Api.IntegrationTests.AutoData;
using Api.Repositories.DbConnection;
using Api.Repositories.Repositories;
using AutoFixture.Idioms;
using FluentAssertions;
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

        //// TODO: Validate with get by id
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
    }
}
