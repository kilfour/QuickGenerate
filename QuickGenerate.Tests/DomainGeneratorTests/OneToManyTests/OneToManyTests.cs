using System;
using System.Collections.Generic;
using System.Linq;
using QuickDotNetCheck;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.OneToManyTests
{
    public class OneToManyTests : Fixture
    {
        private DomainGenerator domainGenerator;
        private ILevel result;
        private Func<ILevel> action;

        public override void Arrange()
        {
            domainGenerator =
                new DomainGenerator()
                // Zero throws nullreference somehow !
                .OneToMany<LevelOne, LevelTwo>(1, 10, (one, many) => { one.NextLevel.Add(many); many.Parent = one; })
                .OneToMany<LevelTwo, LevelThree>(1, 10, (one, many) => { one.NextLevel.Add(many); many.Parent = one; });
            
            action =
                new Func<ILevel>[]
                    {
                        () => domainGenerator.One<LevelOne>(),
                        () => domainGenerator.One<LevelTwo>(),
                        () => domainGenerator.One<LevelThree>()
                    }.PickOne();
        }

        protected override void Act()
        {
            result = action();
        }

        [Fact]
        public void VerifyAllSpecs()
        {
            new Suite(100, 10)
                .Register(() => new OneToManyTests())
                .Run();
        }

        [Spec]
        public void TotalNumberOfThingsInHierarchy_Equals_NumberOfGeneratedObjects()
        {
            Ensure.Equal(GetTotalNumberOfThingsInHierarchy(), domainGenerator.GeneratedObjects.Count());
        }
    
        private int GetTotalNumberOfThingsInHierarchy()
        {
            var root = GetRoot();
            const int levelOne = 1;
            var levelTwo = root.NextLevel.Count;
            var levelThree = root.NextLevel.SelectMany(l => l.NextLevel).Count();
            return levelOne + levelTwo + levelThree;
        }

        private LevelOne GetRoot()
        {
            if (result is LevelOne)
                return result as LevelOne;
            if (result is LevelTwo)
                return (result as LevelTwo).Parent;
            if (result is LevelThree)
                return (result as LevelThree).Parent.Parent;
            return null;
        }
    }
    
    public interface ILevel
    {
        IList<ILevel> NextLevel { get; }
    }
    
    public class LevelOne : ILevel
    {
        public IList<ILevel> NextLevel { get; set; }
        public LevelOne()
        {
            NextLevel = new List<ILevel>();
        }
    }

    public class LevelTwo : ILevel
    {
        public LevelOne Parent { get; set; }
        public IList<ILevel> NextLevel { get; set; }
        public LevelTwo()
        {
            NextLevel = new List<ILevel>();
        }
    }

    public class LevelThree : ILevel
    {
        public LevelTwo Parent { get; set; }
        public IList<ILevel> NextLevel { get; set; }
    }
}
