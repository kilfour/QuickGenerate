using System;
using System.Linq;
using QuickGenerate.Complex;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class GeneratorOptionsKnowTheEntity
    {
        [Fact(Skip="too soon")]
        public void YesItDoes()
        {
            //var generator =
            //    new DomainGenerator()
            //        .With<SomethingToGenerate>(
            //            opt => opt.For(e => e.Omschrijving,
            //                           new StringBuilderGenerator()
            //                               .Append("Meneer", "Mevrouw")
            //                               .Space()
            //                               .Append("de")
            //                               .Space()
            //                               .Append("afgevaardigde", "medewerker", "chef")
            //                               .Space()
            //                               .Append("van de")
            //                               .Space()
            //                               .Append("minister", "staatssecretaris", "gouverneur")
            //                       ))
            //        .With<SomethingToGenerate>(
            //            opt => opt.AppendCounter(e => e.Afkorting, () => Abbreviate(opt.Entity.Omschrijving)));

            //var something = generator.One<SomethingToGenerate>();
            //Assert.NotNull(something.Omschrijving);
            //Assert.NotNull(something.Afkorting);

            //Console.WriteLine(something.Omschrijving);
            //Console.WriteLine(something.Afkorting);
        }

        private static string Abbreviate(string input)
        {
            return
                input.Split(' ')
                    .Select(s => s.First())
                    .Aggregate("", (s, c) => s + c, s => s);
        }

        public class SomethingToGenerate
        {
            public string Omschrijving { get; set; }
            public string Afkorting { get; set; }
        }
    }
}
