using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeActionTwoParameterMethod_MultipleTimes_Tests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeActionTwoParameterMethod_MultipleTimes_Tests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Method<int, int>(3, (e, i1, i2) => e.MyMethod(i1, i2)))
                    .With(42);
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            Assert.Equal(3, domainGenerator.One<SomethingToGenerate>().MyInts.Count);
        }

        public class SomethingToHoldTheInts
        {
            public int AnInt { get; set; }
            public int AnotherInt { get; set; }
        }

        public class SomethingToGenerate
        {
            private List<SomethingToHoldTheInts> myInts = new List<SomethingToHoldTheInts>();
            public List<SomethingToHoldTheInts> MyInts
            {
                get { return myInts; }
                private set { myInts = value; }
            }

            public string MyStringProperty { get; private set; }
            public void MyMethod(int anInt, int anotherInt)
            {
                MyInts.Add(new SomethingToHoldTheInts {AnInt = anInt , AnotherInt = anotherInt});
            }
        }
    }
}