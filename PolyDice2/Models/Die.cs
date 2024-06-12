namespace PolyDice2.Models
{
    public class Die
    {
        public Die(int sides)
        {
            Sides = sides;
        }

        public int Sides { get; set; }
        public string Name {
            get
            {
                if (Sides == 2)
                {
                    return "Coin";
                }

                return $"D{Sides}";
            }
        }
        public string Icon { get { return $"d{Sides}.png"; } }
        public bool ForceSimpleMode { get { return Sides == 2; } }

        public int Roll()
        {
            Random rng = new Random();
            return rng.Next(1, Sides + 1);
        }

        public (string result, string breakdown) Roll(int count = 1, int modifier = 0, RollType rollType = RollType.Normal)
        {
            int total = 0;
            string breakdown = "";

            if (Sides == 2)
            {
                int roll = Roll();
                return (roll == 1 ? "Heads" : "Tails", $"D2({roll})");
            }

            for (int i = 1; i <= count; i++)
            {
                if (i > 1 && i <= count)
                {
                    breakdown += " + ";
                }

                switch (rollType)
                {
                    case RollType.Advantage:
                        int rollA1 = Roll();
                        int rollA2 = Roll();
                        total += Math.Max(rollA1, rollA2);
                        breakdown += $"{Name}a({Math.Max(rollA1, rollA2)}, {Math.Min(rollA1, rollA2)})";
                        break;
                    case RollType.Disadvantage:
                        int rollD1 = Roll();
                        int rollD2 = Roll();
                        total += Math.Min(rollD1, rollD2);
                        breakdown += $"{Name}d({Math.Min(rollD1, rollD2)}, {Math.Max(rollD1, rollD2)})";
                        break;
                    default:
                        int roll = Roll();
                        total += roll;
                        breakdown += $"{Name}({roll})";
                        break;
                }
            }

            total += modifier;

            if (modifier != 0)
            {
                string formattedModifier = modifier >= 0 ? " + " + modifier : " - " + (modifier * -1);
                breakdown += formattedModifier;
            }
            
            return (total.ToString(), breakdown);
        }

        public int Next()
        {
            switch(Sides)
            {
                case 2: return 4;
                case 4: return 6;
                case 6: return 8;
                case 8: return 10;
                case 10: return 12;
                case 12: return 20;
                case 20: return 100;
                case 100: return 2;
                default: return 20;
            }
        }

        public int Previous()
        {
            switch (Sides)
            {
                case 2: return 100;
                case 4: return 2;
                case 6: return 4;
                case 8: return 6;
                case 10: return 8;
                case 12: return 10;
                case 20: return 12;
                case 100: return 20;
                default: return 20;
            }
        }
    }
}
