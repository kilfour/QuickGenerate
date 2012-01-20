using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class NoRelationAllowedOnComponentsException : Exception
    {
        public NoRelationAllowedOnComponentsException(string message)
            : base(message) { }
    }
}