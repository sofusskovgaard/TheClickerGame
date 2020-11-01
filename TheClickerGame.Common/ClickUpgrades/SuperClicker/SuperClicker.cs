using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.ClickUpgrades.SuperClicker
{
    public class SuperClicker : ClickUpgrade
    {
        public override string Name => "Super Clicker";

        public override string Description => "Nam tellus ipsum, porta eu urna iaculis, posuere gravida elit.";

        public override decimal Multiplier => 2;

        public override decimal Price => 500;

        public override int SortOrder => 1;
    }
}
