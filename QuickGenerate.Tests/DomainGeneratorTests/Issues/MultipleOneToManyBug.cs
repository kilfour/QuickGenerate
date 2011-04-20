using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class MultipleOneToManyBug
    {
        private DomainGenerator generator;

        public MultipleOneToManyBug()
        {
            generator = new DomainGenerator()
                .OneToMany<Entity, Relation>(
                    1,
                    (entity, relation) =>
                        {
                            entity.ChildRelations.Add(relation);
                            relation.Parent = entity;
                        })
                .OneToMany<Entity, Relation>(
                    1,
                    (entity, relation) =>
                        {
                            //var rel = new Relation();
                            entity.ParentRelations.Add(relation);
                            relation.Child = entity;
                        });
        }

        [Fact]
        public void Generating()
        {
            var something = generator.One<Entity>();
            foreach (var generatedObject in generator.GeneratedObjects)
            {
                Console.WriteLine(generatedObject.GetType());    
            }
            
            Assert.Equal(5, generator.GeneratedObjects.Count());
        }

        public class Entity
        {
            public IList<Relation> ChildRelations { get; set; }
            public IList<Relation> ParentRelations { get; set; }
            public Entity()
            {
                ChildRelations = new List<Relation>();
                ParentRelations = new List<Relation>();
            }
        }

        public class Relation
        {
            public Entity Parent { get; set; }
            public Entity Child { get; set; }
        }
    }

    
}
