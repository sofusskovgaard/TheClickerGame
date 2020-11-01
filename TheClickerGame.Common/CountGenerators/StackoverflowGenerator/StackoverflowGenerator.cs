using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.StackoverflowGenerator
{
    public class StackoverflowGenerator : CountGenerator
    {
        public override string Name => "StackOverflow";

        public override decimal Multiplier => 260000;

        public override decimal Price => 5100000000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}
