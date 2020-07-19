using Api.Controllers;
using Api.Controllers.Models;
using Api.Domain.Entities;
using Api.UnitTests.AutoData;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.Api.Controllers
{
    public class GoalControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardClausesTest(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(GoalController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Get_ShouldCallMethodsCorrectly(
            List<Goal> goals,
            List<GoalModel> goalModels,
            GoalController sut)
        {
            // Arranges
            sut.GoalService.GetAllAsync().Returns(goals);
            sut.GoalMapper.Map(Arg.Is(goals)).Returns(goalModels);

            // Act
            var result = await sut.Get();

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(goalModels);
            await sut.GoalService.Received(1).GetAllAsync();
            sut.GoalMapper.Received(1).Map(Arg.Is(goals));
        }

        [Theory, AutoNSubstituteData]
        public async Task GetById_ShouldCallMethodsCorrectly(
            Guid goalId,
            Goal goal,
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.GoalService.GetAsync(Arg.Is(goalId)).Returns(goal);
            sut.GoalMapper.Map(Arg.Is(goal)).Returns(goalModel);

            // Act
            var result = await sut.GetById(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(goalModel);
            await sut.GoalService.Received(1).GetAsync(Arg.Is(goalId));
            sut.GoalMapper.Received(1).Map(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task GetById_WhenGoalNull_ShouldReturnNotFound(
            Guid goalId,
            GoalController sut)
        {
            // Arranges
            sut.GoalService.GetAsync(Arg.Is(goalId)).ReturnsNull();

            // Act
            var result = await sut.GetById(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
            await sut.GoalService.Received(1).GetAsync(Arg.Is(goalId));
            sut.GoalMapper.DidNotReceive().Map(Arg.Any<Goal>());
        }

        [Theory, AutoNSubstituteData]
        public async Task Post_ShouldCallMethodsCorrectly(
            Goal goal,
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.GoalMapper.Map(Arg.Is(goalModel)).Returns(goal);
            sut.GoalService.CreateAsync(Arg.Is(goal)).Returns(true);

            // Act
            var result = await sut.Post(goalModel);

            // Asserts
            result.Should().BeOfType<CreatedResult>();
            ((CreatedResult)result).Value.Should().Be(goalModel);
            sut.GoalMapper.Received(1).Map(Arg.Is(goalModel));
            await sut.GoalService.Received(1).CreateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task Post_WhenCreateAsyncFails_ShouldReturnNotModified(
            Goal goal,
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.GoalMapper.Map(Arg.Is(goalModel)).Returns(goal);
            sut.GoalService.CreateAsync(Arg.Is(goal)).Returns(false);

            // Act
            var result = await sut.Post(goalModel);

            // Asserts
            result.Should().BeOfType<StatusCodeResult>();
            ((StatusCodeResult)result).StatusCode.Should().Be(304);
            sut.GoalMapper.Received(1).Map(Arg.Is(goalModel));
            await sut.GoalService.Received(1).CreateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task Post_WhenInvalidModelState_ShouldReturnBadRequest(
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.ModelState.AddModelError("Error key", "Error message");

            // Act
            var result = await sut.Post(goalModel);

            // Asserts
            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should()
                .BeOfType<SerializableError>();
            sut.GoalMapper.DidNotReceive().Map(Arg.Any<GoalModel>());
            await sut.GoalService.DidNotReceive().CreateAsync(Arg.Any<Goal>());
        }

        [Theory, AutoNSubstituteData]
        public async Task Put_ShouldReturnNoContent(
            Goal goal,
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.GoalMapper.Map(Arg.Is(goalModel)).Returns(goal);
            sut.GoalService.UpdateAsync(Arg.Is(goal)).Returns(true);

            // Act
            var result = await sut.Put(goalModel);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            sut.GoalMapper.Received(1).Map(Arg.Is(goalModel));
            await sut.GoalService.Received(1).UpdateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task Put_WhenUpdateAsyncFails_ShouldReturnNotModified(
            Goal goal,
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.GoalMapper.Map(Arg.Is(goalModel)).Returns(goal);
            sut.GoalService.UpdateAsync(Arg.Is(goal)).Returns(false);

            // Act
            var result = await sut.Put(goalModel);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>();
            ((StatusCodeResult)result).StatusCode.Should().Be(304);
            sut.GoalMapper.Received(1).Map(Arg.Is(goalModel));
            await sut.GoalService.Received(1).UpdateAsync(Arg.Is(goal));
        }

        [Theory, AutoNSubstituteData]
        public async Task Put_WhenInvalidModelState_ShouldReturnBadRequest(
            GoalModel goalModel,
            GoalController sut)
        {
            // Arranges
            sut.ModelState.AddModelError("Error key", "Error message");

            // Act
            var result = await sut.Put(goalModel);

            // Asserts
            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should()
                .BeOfType<SerializableError>();
            sut.GoalMapper.DidNotReceive().Map(Arg.Any<GoalModel>());
            await sut.GoalService.DidNotReceive().UpdateAsync(Arg.Any<Goal>());
        }

        [Theory, AutoNSubstituteData]
        public async Task Delete_ShouldReturnNoContent(
            Guid goalId,
            GoalController sut)
        {
            // Arrange
            sut.GoalService.DeleteAsync(Arg.Is(goalId)).Returns(true);

            // Act
            var result = await sut.Delete(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            await sut.GoalService.Received(1).DeleteAsync(Arg.Is(goalId));
        }

        [Theory, AutoNSubstituteData]
        public async Task Delete_WhenDeleteFails_ShouldReturnNotModified(
            Guid goalId,
            GoalController sut)
        {
            // Arrange
            sut.GoalService.DeleteAsync(Arg.Is(goalId)).Returns(false);

            // Act
            var result = await sut.Delete(goalId);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>();
            ((StatusCodeResult)result).StatusCode.Should().Be(304);
            await sut.GoalService.Received(1).DeleteAsync(Arg.Is(goalId));
        }
    }
}
