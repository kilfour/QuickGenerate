using System;
using QuickGenerate.Implementation;
using Xunit;

namespace QuickGenerate.Example
{
    public class GatheringExample
    {
        private Gatherer<SomeEntity> gatherer;
        private SomeEntity someEntity;
        private AuditSomeEntity auditEntity;

        [Fact]
        public void Collect_Recall()
        {
            someEntity = Generate.One<SomeEntity>();
            gatherer =
                Gather.From(someEntity)
                    .Collect(e => e.Iet)
                    .Collect(e => e.Something)
                    .Collect(e => e.Etwas)
                    .Collect(e => e.QuelqueChose)
                    .Collect(e => e.Algo);
            
            Act();

            Assert.Equal(gatherer.Recall(e => e.Iet), auditEntity.AuditIet);
        }

        private AuditSomeEntity GetAuditEntity()
        {
            throw new NotImplementedException();
        }

        private void Act()
        {
            throw new NotImplementedException();
        }

        public class SomeEntity
        {
            public int Iet { get; set; }
            public int Something { get; set; }
            public int Etwas { get; set; }
            public int QuelqueChose { get; set; }
            public int Algo { get; set; }
        }

        public class AuditSomeEntity
        {
            public int AuditIet { get; set; }
            public int AuditSomething { get; set; }
            public int AuditEtwas { get; set; }
            public int AuditQuelqueChose { get; set; }
            public int AuditAlgo { get; set; }
        }
    }
}
