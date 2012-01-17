using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class CantFindConstructorException : Exception
    {
        public CantFindConstructorException(string message) 
            : base(message) { }
    }
}