using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.ClickUpgrades.UpgradedClicker
{
    public class UpgradedClicker : ClickUpgrade
    {
        public override string Name => "Upgraded Clicker";

        public override string Description => "Mauris suscipit libero non quam commodo scelerisque.";

        public override decimal Multiplier => 2;

        public override decimal Price => 100;

        public override int SortOrder => 0;
    }
}
