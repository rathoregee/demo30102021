using System;
using AutoFixture.Xunit2;

namespace Demo.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class StandardAutoMoqDataAttribute : AutoDataAttribute
    {
        public StandardAutoMoqDataAttribute() : base(FixtureHelper.CreateFixture) { }
    }
}
