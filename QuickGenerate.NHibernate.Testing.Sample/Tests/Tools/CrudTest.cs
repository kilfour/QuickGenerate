using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Exceptions;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using Xunit;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.Tools
{
    public abstract class CrudTest<TEntity> : DatabaseTest
        where TEntity : IHaveAnId
    {
        protected virtual DomainGenerator GenerateIt()
        {
            return
                new DomainGenerator()
                    .With<IHaveAnId>(opt => opt.Ignore(e => e.Id));
        }

        protected void IsRequired<T>(Expression<Func<TEntity, T>> expression)
        {
            Assert.Throws<GenericADOException>(
                () =>
                    {
                        GenerateIt().Ignore(expression).One<TEntity>();
                        NHibernateSession.Flush();
                    });
        }

        protected void Has_A<T>(Expression<Func<TEntity, T>> expression)
            where T : IHaveAnId
        {
            var compiledExpression = expression.Compile();
            var entity = BuildEntity();
            NHibernateSession.Flush();
            var entityId = entity.Id;
            var childId = compiledExpression.Invoke(entity).Id;
            NHibernateSession.Clear();
            entity = NHibernateSession.Get<TEntity>(entityId);
            Assert.Equal(childId, compiledExpression.Invoke(entity).Id);
        }

        protected void HasMany<TMany>(Expression<Func<TEntity, IEnumerable<TMany>>> expression)
            where TMany : IHaveAnId
        {
            var compiledExpression = expression.Compile();
            var entity = BuildEntity();
            NHibernateSession.Flush();
            var manies = compiledExpression.Invoke(entity).ToList();
            Assert.NotEqual(0, manies.Count());
            var entityId = entity.Id;
            var ids = manies.Select(many => many.Id).ToList();
            NHibernateSession.Clear();
            entity = NHibernateSession.Get<TEntity>(entityId);
            manies = compiledExpression.Invoke(entity).ToList();
            Assert.Equal(ids.Count, manies.Count());
            foreach (var many in manies)
            {
                Assert.True(ids.Contains(many.Id));
            }
        }

        [Fact]
        public void SelectQueryWorks()
        {
            NHibernateSession.CreateCriteria(typeof (TEntity)).SetMaxResults(5).List();
        }

        [Fact]
        public void AddEntity_EntityWasAdded()
        {
            var entity = BuildEntity();
            NHibernateSession.Flush();
            NHibernateSession.Evict(entity);
            var reloadedEntity = NHibernateSession.Get<TEntity>(entity.Id);
            Assert.NotNull(reloadedEntity);
            AssertEqual(entity, reloadedEntity);
            Assert.NotEqual(Guid.Empty, entity.Id);
        }

        [Fact]
        public void UpdateEntity_EntityWasUpdated()
        {
            var entity = BuildEntity();
            NHibernateSession.Flush();
            ModifyEntity(entity);
            UpdateEntity(entity);
            NHibernateSession.Evict(entity);
            var reloadedEntity = NHibernateSession.Get<TEntity>(entity.Id);
            Assert.NotNull(reloadedEntity);
            AssertEqual(entity, reloadedEntity);
        }

        [Fact]
        public void DeleteEntity_EntityWasDeleted()
        {
            var entity = BuildEntity();
            NHibernateSession.Flush();
            if (DeleteEntity(entity))
            {
                NHibernateSession.Flush();
                Assert.Null(NHibernateSession.Get<TEntity>(entity.Id));
            }
        }

        private static void AssertEqual<T>(T expectedEntity, T actualEntity)
        {
            Inspect
                .This(expectedEntity, actualEntity)
                .Report(Assert.True)
                .AreMemberWiseEqual();
        }

        protected virtual TEntity BuildEntity()
        {
            var generator = GenerateIt();
            var entity = generator.One<TEntity>();
            return entity;
        }

        protected virtual TEntity ModifyEntity(TEntity entity)
        {
            GenerateIt().ModifyThis(entity).ChangeAll();
            return entity;
        }

        protected virtual void UpdateEntity(TEntity entity)
        {
            NHibernateSession.Update(entity);
            NHibernateSession.Flush();
        }

        protected virtual bool DeleteEntity(TEntity entity)
        {
            NHibernateSession.Delete(entity);
            return true;
        }
    }
}