using Api.Core.Services;
using Api.Domain.Entities;
using Api.UnitTests.AutoData;
using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.Core.Services
{
    public class GoalServiceTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalService).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task GetAllAsync_ShouldCallMethodCorrectly(
            List<Goal> goals,
            GoalService sut)
        {
            // Arrange
            sut.GoalRepository.GetAllAsync().Returns(goals);

            // Act
            var result = await sut.GetAllAsync();

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(goals);
            result.Should().BeOfType<List<Goal>>();
            await sut.GoalRepository.Received(1).GetAllAsync();
        }

        [Theory, AutoNSubstituteData]
        public async Task GetAsync_ShouldCallMethodCorrectly(
            Guid goalId,
            Goal goal,
            GoalService sut)
        {
            // Arrange
            sut.GoalRepository.GetAsync(Arg.Is(goalId)).Returns(goal);

            // Act
            var result = await sut.GetAsync(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(goal);
            result.Should().BeOfType<Goal>();
            await sut.GoalRepository.Received(1).GetAsync(Arg.Is(goalId));
        }

        [Theory, AutoNSubstituteData]
        public async Task GetAsync_WhenGoalNull_ShouldReturnNull(
            Guid goalId,
            GoalService sut)
        {
            // Arrange
            sut.GoalRepository.GetAsync(Arg.Is(goalId)).ReturnsNull();

            // Act
            var result = await sut.GetAsync(goalId);

            // Asserts
            result.Should().BeNull();
            await sut.GoalRepository.Received(1).GetAsync(Arg.Is(goalId));
        }

        [Theory, AutoNSubstituteData]
        public async Task CreateAsync_WhenSuccess_ShouldCallMethodCorrectly(
            Goal goal,
            GoalService sut)
        {
            // Arrange
            goal.Id = Guid.Empty;
            sut.GoalRepository.CreateAsync(Arg.Is(goal)).Returns(true);

            // Act
            var result = await sut.CreateAsync(goal);

            // Asserts
            result.Should().BeTrue();
            await sut.GoalRepository.Received(1)
                .CreateAsync(Arg.Is<Goal>(x => x.Id != Guid.Empty));
        }

        [Theory, AutoNSubstituteData]
        public async Task CreateAsync_WhenFails_ShouldCallMethodCorrectly(
            Goal goal,
            GoalService sut)
        {
            // Arrange
            goal.Id = Guid.Empty;
            sut.GoalRepository.CreateAsync(Arg.Is(goal)).Returns(false);

            // Act
            var result = await sut.CreateAsync(goal);

            // Asserts
            result.Should().BeFalse();
            await sut.GoalRepository.Received(1)
                .CreateAsync(Arg.Is<Goal>(x => x.Id != Guid.Empty));
        }

        [Theory, AutoNSubstituteData]
        public async Task UpdateAsync_WhenSuccess_ShouldCallMethodCorrectly(
            Goal goal,
            GoalService sut)
        {
            // Arrange
            goal.Id = Guid.Empty;
            sut.GoalRepository.UpdateAsync(Arg.Is(goal)).Returns(true);

            // Act
            var result = await sut.UpdateAsync(goal);

            // Asserts
            result.Should().BeTrue();
            await sut.GoalRepository.Received(1).UpdateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task UpdateAsync_WhenFails_ShouldCallMethodCorrectly(
            Goal goal,
            GoalService sut)
        {
            // Arrange
            goal.Id = Guid.Empty;
            sut.GoalRepository.UpdateAsync(Arg.Is(goal)).Returns(false);

            // Act
            var result = await sut.UpdateAsync(goal);

            // Asserts
            result.Should().BeFalse();
            await sut.GoalRepository.Received(1).UpdateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task DeleteAsync_ShouldCallMethodCorrectly(
            Guid goalId,
            GoalService sut)
        {
            // Arrange
            sut.GoalRepository.DeleteAsync(Arg.Is(goalId)).Returns(true);

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeTrue();
            await sut.GoalRepository.Received(1).DeleteAsync(Arg.Is(goalId));
        }

        [Theory, AutoNSubstituteData]
        public async Task DeleteAsync_WhenDeleteFails_ShouldReturnCorrectly(
            Guid goalId,
            GoalService sut)
        {
            // Arrange
            sut.GoalRepository.DeleteAsync(Arg.Is(goalId)).Returns(false);

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeFalse();
            await sut.GoalRepository.Received(1).DeleteAsync(Arg.Is(goalId));
        }

        [Theory, AutoNSubstituteData]
        public async Task DeleteAsync_WhenGoalIdGuidEmpty_ShouldReturnFalse(
            GoalService sut)
        {
            // Arrange
            var goalId = Guid.Empty;

            // Act
            var result = await sut.DeleteAsync(goalId);

            // Asserts
            result.Should().BeFalse();
            await sut.GoalRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
        }
    }
}
