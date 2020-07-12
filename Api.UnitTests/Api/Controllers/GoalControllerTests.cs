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
    }
}
