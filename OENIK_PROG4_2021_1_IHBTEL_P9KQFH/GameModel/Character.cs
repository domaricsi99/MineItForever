using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Character : Shape
    {
        public int Health { get; set; }

        public int Fuel { get; set; }

        public int Money { get; set; }

        public int Score { get; set; }

        public int PickAxLevel { get; set; }

        public List<string> Map { get; set; }

        public string Name { get; set; }

        public List<Ore> Backpack;

        public double DX { get; set; }

        public double DY { get; set; }

        public Character()
        {
        }

        public Character(int health, int fuel, int money, int score, int pickAxLevel, List<string> map, string name)
        {
            Health = health;
            Fuel = fuel;
            Money = money;
            Score = score;
            PickAxLevel = pickAxLevel;
            Map = map;
            Name = name;
        }

        public Character(int health, int fuel, int money, int score, int pickAxLevel, List<string> map, string name, double x, double y, double w, double h)
            : base(x, y, w, h)
        {
            Health = health;
            Fuel = fuel;
            Money = money;
            Score = score;
            PickAxLevel = pickAxLevel;
            Map = map;
            Name = name;
        }

        public Character(double x, double y, double w, double h)
            : base (x,y,w,h)
        {
        }
    }
}
