using System;
using QuickDotNetCheck;
using Xunit;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class DefaultIntGeneratorTests
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
                type = new[] { typeof(int), typeof(Int32) }.PickOne();
            }

            protected override void Act()
            {
                state.Generator = state.PrimitiveGenerators.Get(type);
            }

            [Spec]
            public void IntGeneratorExistsInThePrimitiveGenerators()
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
            public void GeneratedValueIsOfTypeInt()
            {
                Ensure.Equal(typeof(int), state.GeneratedValue.GetType());
            }

            [Spec]
            public void GeneratedValueIsNeverSmallerThanMinusOne()
            {
                Ensure.False((int)state.GeneratedValue < -1);
            }

            [Spec]
            [IfAfter(typeof(GeneratedValueIsMinusOne))]
            public void GeneratedValueIsSomeTimesMinusOne()
            {
                Ensure.True(true);
            }

            [Spec]
            public void GeneratedValueIsNeverBiggerThanNinetyNine()
            {
                Ensure.False((int)state.GeneratedValue > 99);
            }

            [Spec]
            [IfAfter(typeof(GeneratedValueIsNinetyNine))]
            public void GeneratedValueIsSomeTimesNinetyNine()
            {
                Ensure.True(true);
            }

            [Spec]
            [IfAfter(typeof(GeneratedValueIsZero))]
            public void GeneratedValueIsSomeTimesZero()
            {
                Ensure.True(true);
            }

            public class GeneratedValueIsMinusOne : Condition<GetRandomValue>
            {
                public override bool Evaluate(GetRandomValue fixture)
                {
                    return (int)fixture.state.GeneratedValue == -1;
                }
            }

            public class GeneratedValueIsNinetyNine : Condition<GetRandomValue>
            {
                public override bool Evaluate(GetRandomValue fixture)
                {
                    return (int)fixture.state.GeneratedValue == 99;
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
