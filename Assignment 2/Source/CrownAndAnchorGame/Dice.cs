using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownAndAnchorGame
{
    public enum DiceValue
    {
        CROWN, ANCHOR, HEART, DIAMOND, CLUB, SPADE
    }

    public class Dice
    {
		private static readonly Random RANDOM = new Random();
        public static readonly Array VALUES = Enum.GetValues(typeof(DiceValue));

        private static readonly Dictionary<DiceValue, string> VALUE_REPR_MAP =
            new Dictionary<DiceValue, string>() { 
                { DiceValue.CROWN,"Crown"}, 
                { DiceValue.ANCHOR,"Anchor"}, 
                { DiceValue.HEART,"Heart"}, 
                { DiceValue.DIAMOND,"Diamond"}, 
                { DiceValue.CLUB,"Club"}, 
                { DiceValue.SPADE,"Spade"}, 
            };

        public static DiceValue RandomValue
        {
            get
            {
                return (DiceValue)VALUES.GetValue(RANDOM.Next(VALUES.Length));
            }
        }

        public static string stringRepr(DiceValue diceValue)
        {
            return VALUE_REPR_MAP[diceValue];
        }

        private DiceValue currentValue;
        public DiceValue CurrentValue
        {
            get { return currentValue; }
        }

        public string CurrentValueRepr
        {
            get { return VALUE_REPR_MAP[currentValue]; }
        }



        public Dice()
        {
            currentValue = RandomValue;
        }

        public DiceValue roll()
        {
            return RandomValue;
        }

    }
}
