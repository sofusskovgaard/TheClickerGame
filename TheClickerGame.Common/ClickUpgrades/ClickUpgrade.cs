using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.ClickUpgrades
{
    public abstract class ClickUpgrade : IClickUpgrade
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract decimal Multiplier { get; }

        public abstract decimal Price { get; }

        public abstract int SortOrder { get; }
    }
}
