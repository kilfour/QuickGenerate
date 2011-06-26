//using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
//using Xunit;

//namespace QuickGenerate.Tests.DomainGeneratorTests
//{
//    public class IgnoreConventionTests
//    {
//        private readonly DomainGenerator domainGenerator;

//        public IgnoreConventionTests()
//        {
//            domainGenerator = new DomainGenerator().Ignore(mi => mi.Name == "Id");
//        }

//        [Fact]
//        public void StaysDefaultvalue()
//        {
//            10.Times(
//                () =>
//                    {
//                        var something = domainGenerator.One<SomethingTogenerate>();
//                        Assert.Equal(0, something.Id);
//                    });

//            10.Times(
//                () =>
//                {
//                    var something = domainGenerator.One<SomethingElsegenerate>();
//                    Assert.Equal(null, something.Id);
//                });
//        }

//        public class SomethingTogenerate
//        {
//            public int Id { get; set; }
//        }

//        public class SomethingElsegenerate
//        {
//            public string Id { get; set; }
//        }
//    }
//}
