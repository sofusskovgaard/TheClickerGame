using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.IndianGenerator
{
    public class IndianGenerator : CountGenerator
    {
        public override string Name => "Indian from fiverr";

        public override decimal Multiplier => 8;

        public override decimal Price => 1100;

        public override string Description => "Some indian man will help you out for 500 rupees.";
    }
}
