namespace TheClickerGame.Common.ClickUpgrades.MarvelousClicker
{
    public class MarvelousClicker : ClickUpgrade
    {
        public override string Name => "Marvelous Clicker";

        public override string Description => "Morbi semper eget sem id ultricies.";

        public override decimal Multiplier => 2;

        public override decimal Price => 5000;

        public override int SortOrder => 3;
    }
}
