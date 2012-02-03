using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.Modifying;

namespace QuickGenerate
{
    public interface IDomainGenerator
    {
        // -----------------------------------
        // Defining relations
        IDomainGenerator Component<T>();
        IDomainGenerator OneToOne<TOne, TMany>(Action<TOne, TMany> action);
        IDomainGenerator OneToMany<TOne, TMany>(int numberOfMany, Action<TOne, TMany> action);
        IDomainGenerator OneToMany<TOne, TMany>(int numberOfMany, Func<TOne, TMany> manyFunc, Action<TOne, TMany> action);
        IDomainGenerator OneToMany<TOne, TMany>(int minmumNumberOfMany, int maximumNumberOfMany, Func<TOne, TMany> manyFunc, Action<TOne, TMany> action);
        IDomainGenerator OneToMany<TOne, TMany>(int minmumNumberOfMany, int maximumNumberOfMany, Action<TOne, TMany> action);
        IDomainGenerator ManyToOne<TMany, TOne>(int numberOfMany, Action<TMany, TOne> action);
        IDomainGenerator ManyToOne<TMany, TOne>(int minmumNumberOfMany, int maximumNumberOfMany, Action<TMany, TOne> action);
        // -----------------------------------

        // -----------------------------------
        // Customization methods
        IDomainGenerator With<T>(params T[] choices);
        IDomainGenerator With<T>(Func<T> func);
        IDomainGenerator With<T>(Func<T, T> func);
        IDomainGenerator With<T>(Func<MemberInfo, bool> predicate, Func<T> func);
        IDomainGenerator With<T>(Func<GeneratorOptions<T>, GeneratorOptions<T>> customization);
        IDomainGenerator With<T>(Action<IgnoreGeneratorOptions<T>> customization);
        IDomainGenerator With(Action<IgnoreGeneratorOptions> customization);
        IDomainGenerator With<T>(Action<InheritanceGeneratorOptions<T>> customization);
        IDomainGenerator With<T>(Action<ConstructorGeneratorOptions<T>> customization);
        IDomainGenerator ForEach<T>(Action<T> action);
        IDomainGenerator ForEach<T>(Action<int, T> action);
        // -----------------------------------

        // -----------------------------------
        // Generating stuff
        T One<T>();
        object One(Type resultType);
        IEnumerable<T> Many<T>(int numberOfMany);
        IEnumerable<T> Many<T>(int minNumberOfMany, int maxNumberOfMany);
        // -----------------------------------

        // -----------------------------------
        // shorthand for obtaining an 'experimental', but already usable (see the NHibernate testing sample) DomainModifier
        DomainModifier<T> ModifyThis<T>(T somethingToModify);
        // -----------------------------------
    }
}