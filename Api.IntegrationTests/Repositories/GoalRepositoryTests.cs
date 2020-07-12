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
            result.Should().HaveCount(3);
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
