//using Xunit;

//namespace QuickGenerate.Tests.DomainGeneratorTests
//{
//    public class StringUniquePropertyNameTests
//    {
//        [Fact]
//        public void GeneratorIsApplied()
//        {
//            var generator =
//                new DomainGenerator()
//                    .WithStringNameCounterPattern();

//            var something1 = generator.One<SomethingToGenerate>();
//            Assert.Equal("SomeString1", something1.SomeString);
//            Assert.Equal("MyString1", something1.MyString);

//            var something2 = generator.One<SomethingToGenerate>();
//            Assert.Equal("SomeString2", something2.SomeString);
//            Assert.Equal("MyString2", something2.MyString);

//            var something3 = generator.One<SomethingToGenerate>();
//            Assert.Equal("SomeString3", something3.SomeString);
//            Assert.Equal("MyString3", something3.MyString);
//        }

//        public class SomethingToGenerate
//        {
//            public string SomeString { get; set; }
//            public string MyString { get; set; }
//        }
//    }
//}
