using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ModifyTests
    {
        private readonly IDomainGenerator domainGenerator;

        public ModifyTests()
        {
            domainGenerator =
                new DomainGenerator()
                            .With(opt => opt.Ignore(mi => mi.Name == "Id"))
                            .With<SomethingToGenerate>(g => g.For(e => e.MyProperty, 42, 43));
        }

        [Fact]
        public void ChangeAll()
        {
            var something = domainGenerator.One<SomethingToGenerate>();

            var prop = something.MyProperty;
            if(prop != 42 && prop != 43)
                Assert.True(false, "Generation went wrong.");

            domainGenerator
                .ModifyThis(something)
                .ChangeAll();
            
            if(prop == 42)
            {
                Assert.Equal(43, something.MyProperty);
            }

            if (prop == 43)
            {
                Assert.Equal(42, something.MyProperty);
            }
        }

        [Fact]
        public void ChangeThis()
        {
            var something = domainGenerator.One<SomethingToGenerate>();

            var prop = something.MyProperty;
            if (prop != 42 && prop != 43)
                Assert.True(false, "Generation went wrong.");

            domainGenerator
                .ModifyThis(something)
                .Change(s => s.MyProperty);

            if (prop == 42)
            {
                Assert.Equal(43, something.MyProperty);
            }

            if (prop == 43)
            {
                Assert.Equal(42, something.MyProperty);
            }
        }

        [Fact]
        public void IgnoreConventionsRemain()
        {
            var something = domainGenerator.One<SomethingToGenerate>();
            10.Times(
                () =>
                    {
                        domainGenerator.ModifyThis(something).ChangeAll();
                        Assert.Equal(0, something.Id);
                    });
        }


        public class SomethingToGenerate
        {
            public int Id { get; set; }
            public int MyProperty { get; set; }
        }
    }
}
