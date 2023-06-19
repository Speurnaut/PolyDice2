namespace PolyDice2.Models
{
    internal class Die
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
                return (Roll() == 1 ? "Heads" : "Tails", "Not available for this die");
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

        public void Next()
        {
            switch(Sides)
            {
                case 2:
                    Sides = 4;
                    break;
                case 4:
                    Sides = 6;
                    break;
                case 6:
                    Sides = 8;
                    break;
                case 8:
                    Sides = 10;
                    break;
                case 10:
                    Sides = 12;
                    break;
                case 12:
                    Sides = 20;
                    break;
                case 20:
                    Sides = 100;
                    break;
                case 100:
                    Sides = 2;
                    break;
                default:
                    Sides = 20;
                    break;
            }
        }

        public void Previous()
        {
            switch (Sides)
            {
                case 2:
                    Sides = 100;
                    break;
                case 4:
                    Sides = 2;
                    break;
                case 6:
                    Sides = 4;
                    break;
                case 8:
                    Sides = 6;
                    break;
                case 10:
                    Sides = 8;
                    break;
                case 12:
                    Sides = 10;
                    break;
                case 20:
                    Sides = 12;
                    break;
                case 100:
                    Sides = 20;
                    break;
                default:
                    Sides = 20;
                    break;
            }
        }
    }
}
