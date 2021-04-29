// <copyright file="Character.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Character model.
    /// </summary>
    public class Character : Shape
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public List<Ore> Backpack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="health">health.</param>
        /// <param name="fuel">fuel.</param>
        /// <param name="money">money.</param>
        /// <param name="score">score.</param>
        /// <param name="pickAxLevel">pickax level.</param>
        /// <param name="map">map.</param>
        /// <param name="name">name.</param>
        public Character(int health, int fuel, int money, int score, int pickAxLevel, List<string> map, string name)
        {
            this.Health = health;
            this.Fuel = fuel;
            this.Money = money;
            this.Score = score;
            this.PickAxLevel = pickAxLevel;
            this.Map = map;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="health">health.</param>
        /// <param name="fuel">fuel.</param>
        /// <param name="money">money.</param>
        /// <param name="score">score.</param>
        /// <param name="pickAxLevel">pickax level.</param>
        /// <param name="name">name.</param>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        public Character(int health, int fuel, int money, int score, int pickAxLevel, string name, double x, double y, double w, double h)
            : base(x, y, w, h)
        {
            this.Health = health;
            this.Fuel = fuel;
            this.Money = money;
            this.Score = score;
            this.PickAxLevel = pickAxLevel;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        public Character(double x, double y, double w, double h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public int Fuel { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public int PickAxLevel { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public List<string> Map { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public double DX { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public double DY { get; set; }
    }
}
