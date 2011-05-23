using System;

namespace Evil.Engine
{
    public class Dice : IDice
    {
        private readonly Func<int, int> _randomizer;

        public Dice(Func<int, int> randomizer)
        {
            _randomizer = randomizer;
        }

        public bool RollPercentage(decimal chancePercentage)
        {
            var max = (int) (chancePercentage*100);
            return _randomizer.Invoke(max) <= max;
        }
    }
}
