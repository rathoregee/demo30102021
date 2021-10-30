using AutoFixture;
using AutoFixture.AutoMoq;

namespace Demo.Tests
{
    public static class FixtureHelper
    {
        public static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization
            {
                GenerateDelegates = true,
                ConfigureMembers = true
            });

            //fixture.Register(() => new User { ["id"] = "new-okta-id", Profile = new UserProfile() { } });
            fixture.Customizations.Add(new StandardCalculationStrategyBuilder());
            return fixture;
        }
    }
}
