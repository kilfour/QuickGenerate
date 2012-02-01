using System.Linq;
 
namespace QuickGenerate.Complex
{
    public class CircleGenerator<T> : Generator<T>
    {
        private readonly T[] list;
        private int index;
        public CircleGenerator(T[] list)
        {
            this.list = list;
        }

        public override T GetRandomValue()
        {
            var val = list[index];
            index++;
            if (index >= list.Count())
                index = 0;
            return val;
        }
    }
}