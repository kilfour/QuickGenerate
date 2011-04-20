using QuickGenerate.Implementation;

namespace QuickGenerate.Primitives
{
    public class BoolGenerator : Generator<bool>
    {
        private readonly ChoiceGenerator<bool> boolGen = 
            new ChoiceGenerator<bool>( () => new[] { true, false } );

        public override bool GetRandomValue()
        {
            return boolGen.GetRandomValue();
        }
    }
}