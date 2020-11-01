using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.GoogleGenerator
{
    public class GoogleGenerator : CountGenerator
    {
        public override string Name => "Google";

        public override decimal Multiplier => 7800;

        public override decimal Price => 20000000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}
