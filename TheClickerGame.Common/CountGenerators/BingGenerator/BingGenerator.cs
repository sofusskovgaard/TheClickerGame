using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.BingGenerator
{
    public class BingGenerator : CountGenerator
    {
        public override string Name => "Bing";

        public override decimal Multiplier => 260;

        public override decimal Price => 130000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}
