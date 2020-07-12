using Api.Controllers;
using Api.Controllers.Models;
using Api.Domain.Entities;
using Api.UnitTests.AutoData;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
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
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(goalModels);
            await sut.GoalService.Received(1).GetAllAsync();
            sut.GoalMapper.Received(1).Map(Arg.Is(goals));
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
    }
}
