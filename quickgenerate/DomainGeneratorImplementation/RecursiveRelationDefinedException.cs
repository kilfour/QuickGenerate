using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class RecursiveRelationDefinedException : Exception 
    {
        public RecursiveRelationDefinedException(string message) 
            : base(message) { }
    }
}