using System;
using System.Collections.Generic;
using System.Text;
using QuickGenerate.Implementation;
using QuickGenerate.Interfaces;

namespace QuickGenerate.Complex
{
    public class StringBuilderGenerator : Generator<string>
    {
        private readonly List<Func<string>> builders = new List<Func<string>>();
        public override string GetRandomValue()
        {
            var stringBuilder = new StringBuilder();
            builders.ForEach(b => stringBuilder.Append(b()));
            return stringBuilder.ToString();
        }

        public StringBuilderGenerator Append(IGenerator<string> generator)
        {
            builders.Add(generator.GetRandomValue);
            return this;
        }

        public StringBuilderGenerator Append(params string[] values)
        {
            builders.Add(() => new ChoiceGenerator<string>(values).GetRandomValue());
            return this;
        }

        public StringBuilderGenerator Append(string value)
        {
            builders.Add(() => value);
            return this;
        }

        public StringBuilderGenerator AppendCounter()
        {
            var generator = new FuncGenerator<long, string>(0, val => ++val, val => val.ToString());
            builders.Add(generator.GetRandomValue);
            return this;
        }

        public StringBuilderGenerator Space()
        {
            return Append(" ");
        }

        public StringBuilderGenerator Comma()
        {
            return Append(",");
        }

        public StringBuilderGenerator Dot()
        {
            return Append(".");
        }

        public StringBuilderGenerator TripleDot()
        {
            return Append("...");
        }

        public StringBuilderGenerator Period()
        {
            return Append(".");
        }

        public StringBuilderGenerator Colon()
        {
            return Append(":");
        }

        public StringBuilderGenerator Semicolon()
        {
            return Append(";");
        }

        public StringBuilderGenerator At()
        {
            return Append("@");
        }

        public StringBuilderGenerator Question()
        {
            return Append("?");
        }

        public StringBuilderGenerator Exclamation()
        {
            return Append("!");
        }
    }
}