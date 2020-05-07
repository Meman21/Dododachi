using System;

namespace Dojodachi.Models
{
    public class Pet
    {
        private int? happiness;
        private int? fullness;
        private int? energy;
        private int? meals;
        // Use getter and setter to type cast values into their proper types
        public int Happiness
        {
            get 
            {
                if (happiness == null)
                    return 20;
                return (int)happiness;
            }
            set {happiness = (int?)value;}
        }
        public int Fullness
        {
            get 
            {
                if (fullness == null)
                    return 20;
                return (int)fullness;
            }
            set {fullness = (int?)value;}
        }
        public int Energy
        {
            get 
            {
                if (energy == null)
                    return 50;
                return (int)energy;
            }
            set {energy = (int?)value;}
        }
        public int Meals
        {
            get 
            {
                if (meals == null)
                    return 3;
                return (int)meals;
            }
            set {meals = (int?)value;}
        }
        public Pet()
        {
            Happiness = 20;
            Fullness = 20;
            Energy = 50;
            Meals = 3;
        }

        // Used to update session with new values
        public Pet(int? Fullness, int? Happiness, int? Meals, int? Energy)
        {
            fullness = Fullness;
            happiness = Happiness;
            meals = Meals;
            energy = Energy;
        }
        public string Feed()
        {
            if (Meals == 0)
                return "You have no meals to eat!";
            Random rInt = new Random();
            int chance = rInt.Next(1,5);
            if (chance == 1)
            {
                Meals --;
                return "Your Dojodachi didn't like that. Fullness +0, Meals -1";
            }
            Meals--;
            int result = rInt.Next(5,11);
            Fullness += result;
            return $"You fed your Dojodachi! Fullness +{result}, Meals -1";
        }

        public string Play()
        {
            if (Energy == 0)
                return "You have no energy!";
            Random rInt = new Random();
            int chance = rInt.Next(1,5);
            if (chance == 1)
            {
                Energy -= 5;
                return "Your Dojodachi didn't feel like playing. Happiness +0, Energy -5";
            }
            Energy -= 5;
            int result = rInt.Next(5,11);
            Happiness += result;
            return $"You played with your Dojodachi! Happiness +{result}, Energy -5";
        }

        public string Work()
        {
            Energy -= 5;
            Random rInt = new Random();
            int result = rInt.Next(1,4);
            Meals += result;
            return $"Your Dojodachi did some work! Meals +{result}, Energy -5";
        }

        public string Sleep()
        {
            Energy += 15;
            Fullness -= 5;
            Happiness -=5;
            return $"Your Dojodachi had a nap! Energy +15, Fullness -5, Happiness -5";
        }

        public bool Won()
        {
            if (Energy >= 100 && Fullness >= 100 && Happiness >= 100)
                return true;
            return false;
        }

        public bool Lost()
        {
            if (Fullness <= 0 || Happiness <= 0)
                return true;
            return false;
        }
    }
}