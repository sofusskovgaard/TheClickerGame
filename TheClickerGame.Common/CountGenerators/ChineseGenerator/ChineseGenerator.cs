using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.ChineseGenerator
{
    public class ChineseGenerator : CountGenerator
    {
        public override string Name => "Chinese gold farmer";

        public override decimal Multiplier => 47;

        public override decimal Price => 12000;

        public override string Description => "Orci varius natoque penatibus et magnis dis parturient montes";
    }
}
