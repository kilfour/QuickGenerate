using QuickGenerate.DomainGeneratorImplementation;

namespace QuickGenerate.Tests.PrimitiveGeneratorsTests
{
    public class DefaultPrimitiveGeneratorsTestsState
    {
        public readonly PrimitiveGenerators PrimitiveGenerators = new PrimitiveGenerators();
        public IGenerator Generator;
        public object GeneratedValue;
    }
}