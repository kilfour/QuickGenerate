using System;
using QuickDotNetCheck;
using Xunit;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class DefaultStringGeneratorTests
    {
        [Fact]
        public void VerifyAllSpecs()
        {
            var state = new DefaultPrimitiveGeneratorsTestsState();
            new Suite(10, 200)
                .Register(() => new GetGenerator(state))
                .Register(() => new GetRandomValue(state))
                .Run();
        }

        public class GetGenerator : Fixture
        {
            private readonly DefaultPrimitiveGeneratorsTestsState state;
            private Type type;

            public GetGenerator(DefaultPrimitiveGeneratorsTestsState state)
            {
                this.state = state;
            }

            public override bool CanAct()
            {
                return state.Generator == null;
            }

            public override void Arrange()
            {
                type = new[] { typeof(string), typeof(String) }.PickOne();
            }

            protected override void Act()
            {
                state.Generator = state.PrimitiveGenerators.Get(type);
            }

            [Spec]
            public void StringGeneratorExistsInThePrimitiveGenerators()
            {
                Ensure.NotNull(state.Generator);
            }
        }

        public class GetRandomValue : Fixture
        {
            private readonly DefaultPrimitiveGeneratorsTestsState state;

            public GetRandomValue(DefaultPrimitiveGeneratorsTestsState state)
            {
                this.state = state;
            }

            public override bool CanAct()
            {
                return state.Generator != null;
            }

            protected override void Act()
            {
                state.GeneratedValue = state.Generator.RandomAsObject();
            }

            [Spec]
            public void GeneratedValueIsNotNull()
            {
                Ensure.NotNull(state.GeneratedValue);
            }

            [Spec]
            public void GeneratedValueIsOfTypeString()
            {
                Ensure.Equal(typeof(string), state.GeneratedValue.GetType());
            }

            [Spec]
            [IfAfter(typeof(GeneratedValueLengthIsZero))]
            public void GeneratedValueLengthIsSomeTimesZero()
            {
                Ensure.True(true);
            }

            [Spec]
            public void GeneratedValueLengthIsNeverBiggerThanNine()
            {
                Ensure.False(((string)state.GeneratedValue).Length > 9);
            }

            [Spec]
            [IfAfter(typeof(GeneratedValueLengthIsNine))]
            public void GeneratedValueLengthIsSomeTimesNine()
            {
                Ensure.True(true);
            }

            public class GeneratedValueLengthIsZero : Condition<GetRandomValue>
            {
                public override bool Evaluate(GetRandomValue fixture)
                {
                    return ((string)fixture.state.GeneratedValue).Length == 0;
                }
            }

            public class GeneratedValueLengthIsNine : Condition<GetRandomValue>
            {
                public override bool Evaluate(GetRandomValue fixture)
                {
                    return ((string)fixture.state.GeneratedValue).Length == 9;
                }
            }

            public class GeneratedValueIsZero : Condition<GetRandomValue>
            {
                public override bool Evaluate(GetRandomValue fixture)
                {
                    return (int)fixture.state.GeneratedValue == 0;
                }
            }
        }
    }
}