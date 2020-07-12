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
        [Theory, AutoNSubstituteData]
        public void MapGoalListToGoalModelList_WhenListFilled_ShouldMapCorrectly(
            List<Goal> goals,
            GoalMapper sut)
        {
            // Act
            var result = sut.Map(goals);

            // Assert
            result.Should().NotBeNullOrEmpty();
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
    }
}
