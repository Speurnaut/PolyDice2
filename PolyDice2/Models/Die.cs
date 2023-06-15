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

        public string Roll()
        {
            Random rng = new Random();
            var result = rng.Next(1, Sides + 1);
            if (Sides == 2)
            {
                return result == 1 ? "Heads" : "Tails";
            }
            return result.ToString();
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
