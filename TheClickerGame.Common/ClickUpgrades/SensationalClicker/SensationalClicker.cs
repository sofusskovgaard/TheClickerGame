using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.ClickUpgrades.SensationalClicker
{
    public class SensationalClicker : ClickUpgrade
    {
        public override string Name => "Sensational Clicker";

        public override string Description => "Vestibulum quis scelerisque sapien.";

        public override decimal Multiplier => 2;

        public override decimal Price => 25000;

        public override int SortOrder => 5;
    }
}
