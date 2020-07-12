using Api.Controllers.Models;
using Api.Domain.Entities;
using Api.Mappers;
using Api.UnitTests.AutoData;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Api.UnitTests.Api.Mappers
{
    public class GoalMapperTests
    {
        //// TODO: Use InlineData in empty/null tests 

        [Theory, AutoNSubstituteData]
        public void MapGoalListToGoalModelList_WhenListFilled_ShouldMapCorrectly(
            List<Goal> goals,
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(goals);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<GoalModel>>();
            result.Should().BeEquivalentTo(goals,
                opt => opt.Excluding(src => src.Id));
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalListToGoalModelList_WhenListEmpty_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Arrange
            var goals = new List<Goal>();

            // Act
            var result = sut.Map(goals);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalListToGoalModelList_WhenListNull_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Arrange
            List<Goal> goals = null;

            // Act
            var result = sut.Map(goals);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalModelToGoal_ShouldMapCorrectly(
            GoalModel goalModel,
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(goalModel);

            // Asserts
            result.Should().BeOfType<Goal>();
            result.Should().BeEquivalentTo(goalModel,
                opt => opt.Excluding(src => src.Id));
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalModelToGoal_WhenGoalEmpty_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(new GoalModel());

            // Asserts
            result.Should().BeOfType<Goal>();
            result.Title.Should().BeNull();
            result.Count.Should().Be(default);
            result.Count.Should().Be(default);
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalModelToGoal_WhenGoalNull_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Act
            var result = sut.Map((GoalModel)null);

            // Asserts
            result.Should().BeOfType<Goal>();
            result.Title.Should().BeNull();
            result.Count.Should().Be(default);
            result.Count.Should().Be(default);
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalToGoalModel_ShouldMapCorrectly(
            Goal goal,
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(goal);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<GoalModel>();
            result.Should().BeEquivalentTo(goal);
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalToGoalModel_WhenGoalModelEmpty_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(new Goal());

            // Asserts
            result.Should().BeOfType<GoalModel>();
            result.Title.Should().BeNull();
            result.Count.Should().Be(default);
            result.Count.Should().Be(default);
        }

        [Theory, AutoNSubstituteData]
        public void MapGoalToGoalModel_WhenGoalModelNull_ShouldReturnEmpty(
            GoalMapper sut)
        {
            // Act
            var result = sut.Map((Goal)null);

            // Asserts
            result.Should().BeOfType<GoalModel>();
            result.Title.Should().BeNull();
            result.Count.Should().Be(default);
            result.Count.Should().Be(default);
        }
    }
}
