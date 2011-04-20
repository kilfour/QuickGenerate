using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class ForEachOnOneToManyOrderTests
    {
        private readonly DomainGenerator domainGenerator;
        private readonly List<object> objects = new List<object>();

        public ForEachOnOneToManyOrderTests()
        {
            domainGenerator =
                new DomainGenerator()
                .OneToMany<One, Two>(1, (one, many) => one.Children.Add(many))
                .OneToMany<Two, Three>(1, (one, many) => one.Children.Add(many))
                .ForEach<Base>(o => objects.Add(o));
        }

        [Fact]
        public void GeneratingOne()
        {
            domainGenerator.One<One>();

            Assert.Equal(3, domainGenerator.GeneratedObjects.Count());
            Assert.Equal(3, objects.Count());
            Assert.IsType<One>(objects[0]);
            Assert.IsType<Two>(objects[1]);
            Assert.IsType<Three>(objects[2]);
        }

        [Fact]
        public void GeneratingTwo()
        {
            domainGenerator.One<Two>();

            Assert.Equal(3, domainGenerator.GeneratedObjects.Count());
            Assert.Equal(3, objects.Count());
            
            // order is not guaranteed 

            //Assert.IsType<One>(objects[0]);
            //Assert.IsType<Two>(objects[1]);
            //Assert.IsType<Three>(objects[2]);
        }

        [Fact]
        public void GeneratingThree()
        {
            domainGenerator.One<Three>();

            Assert.Equal(3, domainGenerator.GeneratedObjects.Count());
            Assert.Equal(3, objects.Count());

            // order is not guaranteed 

            //Assert.IsType<One>(objects[0]);
            //Assert.IsType<Two>(objects[1]);
            //Assert.IsType<Three>(objects[2]);
        }

        public class Base {}

        public class One : Base
        {
            public List<Two> Children { get; set; }
            public One()
            {
                Children = new List<Two>();
            }
        }

        public class Two : Base
        {
            public List<Three> Children { get; set; }
            public Two()
            {
                Children = new List<Three>();
            }
        }

        public class Three : Base { }
    }
}