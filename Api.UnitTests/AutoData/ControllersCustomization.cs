using Api.Controllers;
using Api.Controllers.Mappers.Interfaces;
using Api.Domain.Services;
using AutoFixture;
using NSubstitute;

namespace Api.UnitTests.AutoData
{
    internal class ControllersCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new GoalController(
                Substitute.For<IGoalService>(),
                Substitute.For<IGoalMapper>()));
        }
    }
}