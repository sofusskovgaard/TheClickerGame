using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.YahooGenerator
{
    public class YahooGenerator : CountGenerator
    {
        public override string Name => "Yahoo Answers";

        public override decimal Multiplier => 1400;

        public override decimal Price => 1400000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}