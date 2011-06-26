//using QuickGenerate.Primitives;
//using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
//using Xunit;

//namespace QuickGenerate.Tests.DomainGeneratorTests
//{
//    public class GeneratorByConventionTests
//    {
//        private readonly DomainGenerator domainGenerator;

//        public GeneratorByConventionTests()
//        {
//            domainGenerator =
//                new DomainGenerator()
//                    .With(mi => mi.Name == "Value", new IntGenerator(42, 42));
//        }


//        [Fact]
//        public void GeneratorIsApplied()
//        {
//            Assert.Equal(42, domainGenerator.One<ProductPrice>().Value);
//        }
//    }
//}
