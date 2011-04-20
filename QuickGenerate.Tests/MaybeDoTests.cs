using System;
using Xunit;

namespace QuickGenerate.Tests
{
    public class MaybeDoTests
    {
        private bool flag;

        [Fact]
        public void Foo()
        {
            bool yep = false;
            Action action = () => flag = true;

            10.Times(
                () =>
                    {
                        flag = false;
                        Maybe.Do(action);
                        if(flag)
                            yep = true;
                    });

            Assert.True(yep);
        }
    }
}
