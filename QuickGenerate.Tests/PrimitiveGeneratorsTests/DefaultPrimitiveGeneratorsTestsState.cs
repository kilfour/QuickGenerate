using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Interfaces;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class DefaultPrimitiveGeneratorsTestsState
    {
        public readonly PrimitiveGenerators PrimitiveGenerators = new PrimitiveGenerators();
        public IGenerator Generator;
        public object GeneratedValue;
    }
}