using System;
using System.Net.Http;
using AutoFixture.Kernel;
using AutoFixture;
using Demo.BLL.Classes;
using Demo.BLL.Validation;

namespace Demo.Tests
{
    public class StandardCalculationStrategyBuilder : ISpecimenBuilder
    {
        public StandardCalculationStrategyBuilder(IRequestSpecification requestSpecification)
        {
            RequestSpecification = requestSpecification ??
                throw new ArgumentNullException(nameof(requestSpecification));
        }

        public StandardCalculationStrategyBuilder() : this(new StandardStrategySpecification()) { }

        public IRequestSpecification RequestSpecification { get; }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!RequestSpecification.IsSatisfiedBy(request))
            {
                return new NoSpecimen();
            }

            var processor = new StandardJsonProcessor(new StandardRowValidator());

            return new StandardMigrationStrategy(processor);
        }

        private class StandardStrategySpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                return request is Type type && type == typeof(StandardMigrationStrategy);
            }
        }


    }
}
