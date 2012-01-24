using QuickGenerate.Modifying;
using Xunit;

namespace QuickGenerate.Tests.ModifyTests
{
    public class WithBoolProperty
    {
        public bool MyPropery { get; set; }    
    }

    public class AConendrum
    {
        [Fact]
        public void ChangeReallyMeansChange()
        {
            var something = Generate.One<WithBoolProperty>();
            10.Times(
                () =>
                    {
                        var before = something.MyPropery;
                        Modify.This(something).Change(e => e.MyPropery);
                        Assert.NotEqual(before, something.MyPropery);
                    }
                );
        }

        [Fact]
        public void NoChangePossible_Throws()
        {
            var repository = new DomainGenerator().With(true);

            var something = repository.One<WithBoolProperty>();

            Assert.Throws<HeyITriedFiftyTimesButCouldNotGetADifferentValue>(
                () => repository.ModifyThis(something).Change(e => e.MyPropery));
        }
    }

}
