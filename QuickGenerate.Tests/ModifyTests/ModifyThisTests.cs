using System;
using Xunit;

namespace QuickGenerate.Tests.ModifyTests
{
    public class ModifyThisTests
    {
        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
            public int MyOtherProperty { get; set; }
            public int AnotherProperty { get; set; }
            public int YetAnotherProperty { get; set; }
        }

        private readonly SomethingToGenerate something;

        public ModifyThisTests()
        {
            something =
                new SomethingToGenerate
                {
                    MyProperty = -42, // outside default intgenerator range
                    MyOtherProperty = -42,
                    AnotherProperty = -42,
                    YetAnotherProperty = -42
                };
        }

        [Fact]
        public void Change()
        {

            Modify
                .This(something)
                .Change(s => s.MyProperty);

            Assert.NotEqual(-42, something.MyProperty);
            Assert.Equal(-42, something.MyOtherProperty);
            Assert.Equal(-42, something.AnotherProperty);
            Assert.Equal(-42, something.YetAnotherProperty);
        }

        [Fact(Skip = "Lost in translation")]
        public void ChangeOne()
        {
            
            //Modify.This(something).ChangeOne();

            var changed = 0;
            if (something.MyProperty != -42)
                changed++;

            if (something.MyOtherProperty != -42)
                changed++;

            if (something.AnotherProperty != -42)
                changed++;

            if (something.YetAnotherProperty != -42)
                changed++;
            
            Assert.Equal(1, changed);
        }

        [Fact(Skip = "Lost in Translation")]
        public void ChangeHalf()
        {
            //Modify.This(something).ChangeHalf();

            var changed = 0;
            if (something.MyProperty != -42)
                changed++;

            if (something.MyOtherProperty != -42)
                changed++;

            if (something.AnotherProperty != -42)
                changed++;

            if (something.YetAnotherProperty != -42)
                changed++;

            Assert.Equal(2, changed);
        }

        [Fact]
        public void ChangeAll()
        {
        
            Modify
                .This(something)
                .ChangeAll();

            var changed = 0;
            if (something.MyProperty != -42)
                changed++;

            if (something.MyOtherProperty != -42)
                changed++;

            if (something.AnotherProperty != -42)
                changed++;

            if (something.YetAnotherProperty != -42)
                changed++;

            Assert.Equal(4, changed);
        }
    }
}
