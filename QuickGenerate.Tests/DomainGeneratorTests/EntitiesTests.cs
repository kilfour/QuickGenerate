using System;
using System.Linq;
using QuickGenerate.Reflect;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class EntityPicking
    {
        [Fact]
        public void TwoEntities()
        {
            var types =
                FromAssembly
                    .Containing<EntityPicking>()
                    .Implementing<ISomeInterface>();
            Assert.Equal(2, types.Count());
            Assert.Contains(typeof(Something), types);
            Assert.Contains(typeof(SomethingElse), types);
        }

        [Fact]
        public void OneEntity()
        {
            var types =
                FromAssembly
                    .Containing<EntityPicking>()
                    .Implementing<ISomeOtherInterface>();
            Assert.Equal(1, types.Count());
            Assert.Contains(typeof(Something), types);
        }

        public interface ISomeInterface { }
        
        public interface ISomeOtherInterface { }

        public class Something : ISomeInterface, ISomeOtherInterface { }

        public class SomethingElse : ISomeInterface { }
    }
}
