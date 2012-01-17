using System;

namespace QuickGenerate.Tests.Doc.Domain.Primitives.SingleProperty
{
    public class JustAChar : IHaveOnlyOnePrimitive
    {
        public Char CharProperty { get; set; }
    }
}