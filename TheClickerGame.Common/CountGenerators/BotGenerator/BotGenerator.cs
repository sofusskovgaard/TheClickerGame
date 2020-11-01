using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.BotGenerator
{
    public class BotGenerator : CountGenerator
    {
        public override string Name => "Bot";

        public override decimal Multiplier => 1M;

        public override decimal Price => 100;

        public override string Description => "Aenean nec quam quis lacus commodo efficitur dictum in mi.";
    }
}
