using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.ClickUpgrades
{
    public interface IClickUpgrade
    {
        string Name { get; }

        string Description { get; }

        decimal Multiplier { get; }

        decimal Price { get; }
        
        int SortOrder { get; }
    }
}
