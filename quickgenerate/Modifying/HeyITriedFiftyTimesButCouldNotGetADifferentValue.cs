using System;

namespace QuickGenerate.Modifying
{
    public class HeyITriedFiftyTimesButCouldNotGetADifferentValue
        : Exception
    {
        public HeyITriedFiftyTimesButCouldNotGetADifferentValue() { }

        public HeyITriedFiftyTimesButCouldNotGetADifferentValue(string message) 
            : base(message) { }
    }
}