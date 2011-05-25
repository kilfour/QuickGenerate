using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypePolymorphism
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypePolymorphism()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<BaseClass>(
                        opt => opt.StartingValue(
                            () =>
                            new[]
                                {
                                    new BaseClass(), new DervivedOne(), new DervivedTwo()
                                }.PickOne()))
                    .With<AbstractBaseClass>(
                        opt => opt.StartingValue(
                            () =>
                            new AbstractBaseClass[]
                                {
                                    new AbstractDervivedOne(), new AbstractDervivedTwo()
                                }.PickOne()))
                    .With<DervivedTwo>(opt => opt.For(e => e.TheAnswer, 42));
        }

        [Fact]
        public void Hierarchy()
        {
            var isBaseClass = false;
            var isDervivedOne = false;
            var isDervivedTwo = false;

            50.Times(
                () =>
                    {
                        var something = domainGenerator.One<BaseClass>();
                        isBaseClass = isBaseClass || something.GetType() == typeof(BaseClass);
                        isDervivedOne = isDervivedOne || something.GetType() == typeof (DervivedOne);
                        isDervivedTwo = isDervivedTwo || something.GetType() == typeof(DervivedTwo);
                    });

            Assert.True(isBaseClass, "No BaseClass Generated");
            Assert.True(isDervivedOne, "No DervivedOne Generated");
            Assert.True(isDervivedTwo, "No DervivedTwo Generated");
        }

        [Fact]
        public void WithAbstractBase()
        {
            var isBaseClass = false;
            var isDervivedOne = false;
            var isDervivedTwo = false;

            50.Times(
                () =>
                {
                    var something = domainGenerator.One<AbstractBaseClass>();
                    isBaseClass = isBaseClass || something.GetType() == typeof(AbstractBaseClass);
                    isDervivedOne = isDervivedOne || something.GetType() == typeof(AbstractDervivedOne);
                    isDervivedTwo = isDervivedTwo || something.GetType() == typeof(AbstractDervivedTwo);
                });

            Assert.False(isBaseClass);
            Assert.True(isDervivedOne, "No AbstractDervivedOne Generated");
            Assert.True(isDervivedTwo, "No AbstractDervivedTwo Generated");
        }

        [Fact]
        public void CanApplyOptions()
        {
            50.Times(
                () =>
                    {
                        var something = domainGenerator.One<BaseClass>();
                        if (something.GetType() == typeof (DervivedTwo))
                            Assert.Equal(42, ((DervivedTwo) something).TheAnswer);
                    });
        }

        public class BaseClass { }
        public class DervivedOne : BaseClass { }
        public class DervivedTwo : BaseClass { public int TheAnswer { get; set; } }

        public abstract class AbstractBaseClass { }
        public class AbstractDervivedOne : AbstractBaseClass { }
        public class AbstractDervivedTwo : AbstractBaseClass { }
    }
}