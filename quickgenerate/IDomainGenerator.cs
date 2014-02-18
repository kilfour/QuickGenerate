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
        //--

		// Replacing Primitive Generators
    	IDomainGenerator ForPrimitive<T>(IGenerator<T> generator);

        // Choosing a value
        IDomainGenerator With<T>(params T[] choices);

        // Suplying a starting value
        IDomainGenerator With<T>(Func<T> func);

        // Modyfying a generated value
        IDomainGenerator With<T>(Func<T, T> func);

        // Suplying a starting value by convention
        IDomainGenerator With<T>(Func<MemberInfo, bool> predicate, Func<T> func);

        // Modifying a generator option
        IDomainGenerator With<T>(Func<GeneratorOptions<T>, GeneratorOptions<T>> customization);

        // Ignoring stuff
        IDomainGenerator With<T>(Action<IgnoreGeneratorOptions<T>> customization);
        IDomainGenerator With(Action<IgnoreGeneratorOptions> customization);

        // Defining inheritance
        IDomainGenerator With<T>(Action<InheritanceGeneratorOptions<T>> customization);

        // Picking a constructor
        IDomainGenerator With<T>(Action<ConstructorGeneratorOptions<T>> customization);

        // Applying functions over generated values
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