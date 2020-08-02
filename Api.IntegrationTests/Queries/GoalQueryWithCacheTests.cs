using Api.Domain.Entities;
using Api.IntegrationTests.AutoData;
using Api.Repositories.Queries;
using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests.Queries
{
    public class GoalQueryWithCacheTests
    {
        [Theory, AutoDataNSubstitute]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalQueryWithCache).GetConstructors());
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAsync_WhenGoalCached_ShouldReturnCorrectly(
            Guid goalId,
            Goal goal,
            GoalQueryWithCache sut)
        {
            // Arrange
            var goalKey = $"{typeof(Goal).Name}-{goalId}";
            sut.Cache.GetAsync<Goal>(Arg.Is(goalKey))
                .Returns(goal);

            // Act
            var result = await sut.GetAsync(goalId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Goal>();
            result.Should().BeEquivalentTo(goal);
            await sut.Cache.Received(1).GetAsync<Goal>(Arg.Is(goalKey));
            await sut.InnerGoalQuery.DidNotReceive().GetAsync(Arg.Any<Guid>());
            await sut.Cache.DidNotReceive().SetAsync<Goal>(Arg.Any<string>(), Arg.Any<Goal>());
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAsync_WhenGoalNotCached_ShouldCallMethodsCorrectly(
            Guid goalId,
            Goal goal,
            GoalQueryWithCache sut)
        {
            // Arrange
            var goalKey = $"{typeof(Goal).Name}-{goalId}";
            sut.Cache.GetAsync<Goal>(Arg.Any<string>()).ReturnsNull();
            sut.InnerGoalQuery.GetAsync(Arg.Is(goalId)).Returns(goal);

            // Act
            var result = await sut.GetAsync(goalId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Goal>();
            result.Should().BeEquivalentTo(goal);
            await sut.Cache.Received(1).GetAsync<Goal>(Arg.Is(goalKey));
            await sut.InnerGoalQuery.Received(1).GetAsync(Arg.Is(goalId));
            await sut.Cache.Received(1).SetAsync<Goal>(Arg.Is(goalKey), Arg.Is(goal));
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAllAsync_WhenGoalsCached_ShouldReturnCorrectly(
            List<Goal> goals,
            GoalQueryWithCache sut)
        {
            // Arrange
            var allGoalsKey = "All-Goals";

            sut.Cache.GetAsync<List<Goal>>(Arg.Is(allGoalsKey))
                .Returns(goals);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Goal>>();
            result.Should().BeEquivalentTo(goals);
            await sut.Cache.Received(1).GetAsync<List<Goal>>(Arg.Is(allGoalsKey));
            await sut.InnerGoalQuery.DidNotReceive().GetAllAsync();
            await sut.Cache.DidNotReceive().SetAsync<List<Goal>>(Arg.Any<string>(), Arg.Any<List<Goal>>());
        }

        [Theory, AutoDataNSubstitute]
        public async Task GetAllAsync_WhenGoalsNotCached_ShouldCallMethodsCorrectly(
            List<Goal> goals,
            GoalQueryWithCache sut)
        {
            // Arrange
            var allGoalsKey = "All-Goals";
            sut.Cache.GetAsync<List<Goal>>(Arg.Any<string>()).ReturnsNull();
            sut.InnerGoalQuery.GetAllAsync().Returns(goals);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Goal>>();
            result.Should().BeEquivalentTo(goals);
            await sut.Cache.Received(1).GetAsync<List<Goal>>(Arg.Is(allGoalsKey));
            await sut.InnerGoalQuery.Received(1).GetAllAsync();
            await sut.Cache.Received(1).SetAsync<List<Goal>>(Arg.Is(allGoalsKey), Arg.Is(goals));
        }
    }
}
