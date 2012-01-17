namespace QuickGenerate.Complex
{
    public class NullableGenerator<T> : Generator<object>
    {
        private readonly IGenerator<T> generator;

        public NullableGenerator(IGenerator<T> generator)
        {
            this.generator = generator;
        }

        public override object GetRandomValue()
        {
            if(new[]{1,10}.FromRange()==1)
                return null;
            return generator.GetRandomValue();
        }
    }
}