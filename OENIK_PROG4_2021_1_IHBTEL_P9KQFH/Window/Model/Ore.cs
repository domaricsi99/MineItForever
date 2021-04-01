// <copyright file="Ore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model
{
    using System;

    /// <summary>
    /// Ore.
    /// </summary>
    public class Ore //: Shape
    {
        enum Type
        {
            Silver, Gold, Copper, Diamond, Lava, Stone, Dirt
        }

        enum MiningLevel
        {
            Zero = 0, One = 1, Two = 2, Three = 3
        }

        public bool Hurt { get; set; }

        public int Value { get; set; }

        public int Score { get; set; }

    }
}