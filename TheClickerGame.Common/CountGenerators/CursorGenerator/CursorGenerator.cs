using System;
using System.Collections.Generic;
using System.Text;

namespace TheClickerGame.Common.CountGenerators.CursorGenerator
{
    public class CursorGenerator : CountGenerator
    {
        public override string Name => "Cursor";

        public override string Description => "Fusce imperdiet erat mauris, et scelerisque arcu mattis vel.";

        public override decimal Multiplier => 0.1M;

        public override decimal Price => 15;
    }
}
