using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators
{
    public abstract class CountGenerator : ICountGenerator
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract decimal Multiplier { get; }

        public abstract decimal Price { get; }

        public int Quantity { get; set; }
    }
}
