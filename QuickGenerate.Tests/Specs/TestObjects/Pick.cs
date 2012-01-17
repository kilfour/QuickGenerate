using System;
using QuickGenerate.Reflect;

namespace QuickGenerate.Tests.Specs.TestObjects
{
    public static class Pick
    {
        private static readonly Type[] types = 
            FromAssembly
                .Containing<AllPrimitives>()
                .Implementing<ITestObject>();

        public static Type TestObjectType()
        {
            return types.PickOne();
        }
    }
}