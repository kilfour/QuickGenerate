using System;

namespace QuickGenerate.Implementation
{
    public class FuncGenerator<T> : Generator<T>
    {
        private T val;
        private readonly Func<T, T> func;

        public FuncGenerator(T val, Func<T, T> func)
        {
            this.val = val;
            this.func = func;
        }

        public override T GetRandomValue()
        {
            val = func(val);
            return val;
        }
    }

    public class FuncGenerator<TSeed, TResult> : Generator<TResult>
    {
        private TSeed val;
        private readonly Func<TSeed, TSeed> func;
        private readonly Func<TSeed, TResult> formatfunc;

        public FuncGenerator(TSeed val, Func<TSeed, TSeed> func, Func<TSeed, TResult> formatfunc)
        {
            this.val = val;
            this.func = func;
            this.formatfunc = formatfunc;
        }

        public override TResult GetRandomValue()
        {
            val = func(val);
            return formatfunc(val);
        }
    }
}