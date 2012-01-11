using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class CircularStuff
    {
        [Fact]
        public void JustTheRoot()
        {
            new DomainGenerator().One<Root>();
        }


        public class Root
        {
            public Root()
            {
                Ones = new List<One>();
            }
            public List<One> Ones { get; set; }
            public void Add(One one)
            {
                Ones.Add(one);
                one.TheRoot = this;
            }
        }

        public class One
        {
            public One()
            {
                Twos = new List<Two>();
            }

            public Root TheRoot { get; set; }
            public List<Two> Twos { get; set; }
            public void Add(Two two)
            {
                Twos.Add(two);
            }
        }

        public class Two
        {
            public Root TheRoot { get; set; }
        }
    }
}
