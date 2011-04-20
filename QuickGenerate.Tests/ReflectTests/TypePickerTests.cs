using System.Linq;
using QuickGenerate.Reflect;
using Xunit;

namespace QuickGenerate.Tests.ReflectTests
{
    public class TypePickerTests
    {
        [Fact]
        public void Implementing()
        {
            var result = FromAssembly.Containing<JustAClass>().Implementing<IAmJustAnInterface>();
            Assert.Equal(1, result.Length);
            Assert.Equal(typeof(JustAClass), result[0]);
        }

        [Fact]
        public void Excluding()
        {
            var result = 
                FromAssembly
                    .Containing<JustAClass>()
                    .Implementing<IAmJustAnInterface>()
                    .Excluding<JustAClass>();

            Assert.Equal(0, result.Length);
        }

        public interface IAmJustAnInterface { }
        public class JustAClass : IAmJustAnInterface { }


        [Fact]
        public void Generics()
        {
            var result = FromAssembly.Containing<JustAClass>().Implementing(typeof(IAmAGenericInterface<>));
            Assert.Equal(2, result.Length);
            Assert.True(result.Any(t => t == typeof(GenericImpel)));
            Assert.True(result.Any(t => t == typeof(GenericOtherImpel)));
        }

        public interface IAmAGenericInterface<T> { }
        public class Impel {}
        public class OtherImpel { }
        public class GenericImpel : IAmAGenericInterface<Impel> { }
        public class GenericOtherImpel : IAmAGenericInterface<OtherImpel> { }
    }
}
