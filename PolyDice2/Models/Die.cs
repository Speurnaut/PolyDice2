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

        public (string result, string breakdown) Roll(int count = 1, int modifier = 0)
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

                int roll = Roll();
                total += roll;
                breakdown += $"{Name}({roll})";
            }

            total += modifier;
            string formattedModifier = modifier >= 0 ? " + " + modifier : " - " + (modifier * -1);
            breakdown += formattedModifier;

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
