using System.Collections.Generic;
using QuickGenerate.Modifying;

namespace QuickGenerate.Uniqueness
{
    public class UniqueGenerator<T> : Generator<T>
    {
        private readonly IGenerator<T> generator;
        private readonly List<T> previouslyOn = new List<T>();
        public UniqueGenerator(IGenerator<T> generator)
        {
            this.generator = generator;
        }

        public override T GetRandomValue()
        {
            
            for (int i = 0; i < 50; i++)
            {
                var result = generator.GetRandomValue();
                if(!previouslyOn.Contains(result))
                {
                    previouslyOn.Add(result); 
                    return result;
                }
                previouslyOn.Add(result);
            }
            throw new HeyITriedFiftyTimesButCouldNotGetADifferentValue();
        }
    }

    public static class UniqueGeneratorFactory
    {
        public static UniqueGenerator<T> Unique<T>(this IGenerator<T> generator)
        {
            return new UniqueGenerator<T>(generator);
        }
    }
}
