// <copyright file="GameDataBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModelDll;
    using Microsoft.EntityFrameworkCore;

    public class GameDataBase : DbContext
    {

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Ladder> Ladders { get; set; }

        public DbSet<Pickax> Pickaxes { get; set; }

        public DbSet<Character> Character { get; set; }

        public GameDataBase()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Ore> Ores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Ore dirt = new Ore()
            {
                Value = 5,
                Hurt = false,
                Score = 20,
                Level = 0,
                OreType = "dirt",
            };
            Ore air = new Ore()
            {
                Value = 0,
                Hurt = false,
                Score = 0,
                Level = 0,
                OreType = "air",
                CanPass = true,
            };

            Ore copper = new Ore()
            {
                Value = 10,
                Hurt = false,
                Score = 100,
                Level = 1,
                OreType = "copper",
            };
            Ore silver = new Ore()
            {
                Value = 20,
                Hurt = false,
                Score = 200,
                Level = 2,

                OreType = "silver",
            };
            Ore gold = new Ore()
            {
                Value = 40,
                Hurt = false,
                Score = 400,
                Level = 3,
                OreType = "gold",
            };
            Ore stone = new Ore()
            {
                Value = 1,
                Hurt = false,
                Score = 50,
                Level = 3,
                OreType = "stone",
            };
            Ore lavaGem = new Ore()
            {
                Value = 100,
                Hurt = true,
                Score = 1000,
                Level = 4,
                OreType = "lavaGem",
            };
            Ore diamond = new Ore()
            {
                Value = 200,
                Hurt = false,
                Score = 2000,
                Level = 4,
                OreType = "diamond",
            };


            // todo pickax
            // TODOD: Poison
            modelBuilder.Entity<Ore>().HasData(silver, gold, stone, copper, dirt, lavaGem, diamond, air);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GameData.mdf;Integrated Security=True");
        }
    }
}
