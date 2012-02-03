using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class NoRelationssssAllowedOnComponentsException : Exception
    {
        public NoRelationssssAllowedOnComponentsException(string message)
            : base(message) { }
    }
}