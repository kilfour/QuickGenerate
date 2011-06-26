//using Xunit;

//namespace QuickGenerate.Tests.DomainGeneratorTests
//{
//    public class ExplicitPolymorphicOneToOneTests
//    {
//        private readonly DomainGenerator domainGenerator;

//        public ExplicitPolymorphicOneToOneTests()
//        {
//            domainGenerator =
//                new DomainGenerator()
//                    .With<SomethingBaseToGenerate>(
//                        opt => opt.StartingValue(
//                            () => 
//                                new SomethingBaseToGenerate[]
//                                    {
//                                        new SomethingDerivedToGenerate(), 
//                                        new SomethingElseDerivedToGenerate()
//                                    }.PickOne()))
//                    .OneToOne<SomethingToGenerate, SomethingBaseToGenerate>((l, r) => l.MyMethod(r));
//        }

//        [Fact]
//        public void GeneratingLeftHand()
//        {
//            var something = domainGenerator.One<SomethingToGenerate>();
//            Assert.Equal(something, something.SomethingElse.Something);
//        }

//        [Fact]
//        public void GeneratingRightHand()
//        {
//            var somethingElse = domainGenerator.One<SomethingBaseToGenerate>();
//            Assert.Equal(somethingElse, somethingElse.Something.SomethingElse);
//        }

//        public class SomethingToGenerate
//        {
//            public SomethingBaseToGenerate SomethingElse { get; private set; }
//            public void MyMethod(SomethingBaseToGenerate somethingElse)
//            {
//                SomethingElse = somethingElse;
//                SomethingElse.Something = this;
//            }
//        }

//        public abstract class SomethingBaseToGenerate
//        {
//            public SomethingToGenerate Something { get; set; }
//        }

//        public class SomethingDerivedToGenerate : SomethingBaseToGenerate { }
//        public class SomethingElseDerivedToGenerate : SomethingBaseToGenerate { }
//    }
//}