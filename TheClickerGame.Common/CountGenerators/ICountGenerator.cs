using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators
{
    public interface ICountGenerator
    {
        string Name { get; }

        decimal Multiplier { get; }

        decimal Price { get; }

        int Quantity { get; set; }
    }
}
