using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Api.IntegrationTests.AutoData
{
    public class AutoDataNSubstituteAttribute : AutoDataAttribute
    {
        public AutoDataNSubstituteAttribute() : base(() =>
            new Fixture().Customize(
                new CompositeCustomization(
                    new AutoNSubstituteCustomization(),
                    new SqlConnectionCustomization())))
        {
        }
    }
}
