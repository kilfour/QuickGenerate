using System;

namespace QuickGenerate.Primitives
{
    public class GuidGenerator : Generator<Guid>
    {
        public override Guid GetRandomValue()
        {
            return Guid.NewGuid();
        }
    }
}