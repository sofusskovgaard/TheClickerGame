using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.QuoraGenerator
{
    public class QuoraGenerator : CountGenerator
    {
        public override string Name => "Quora";

        public override decimal Multiplier => 44000;

        public override decimal Price => 330000000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}
