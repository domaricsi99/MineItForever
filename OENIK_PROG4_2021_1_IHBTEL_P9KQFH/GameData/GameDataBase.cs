using GameModelDll;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace GameData
{
    public class GameDataBase : DbContext
    {
        public DbSet<Ore> Ores { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Ladder> Ladders { get; set; }

        public DbSet<Pickax> Pickaxes { get; set; }

        public DbSet<Character> Character { get; set; }

        public DbSet<Map> Map { get; set; }

        public GameDataBase()
        {
            this.Database.EnsureCreated();
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                Value = 0,
                Hurt = false,
                Score = 50,
                Level = 3,
                OreType = "stone",
            };
            Ore copper = new Ore()
            {
                Value = 10,
                Hurt = false,
                Score = 100,
                Level = 1,
                OreType = "copper",
            };
            Ore dirt = new Ore()
            {
                Value = 5,
                Hurt = false,
                Score = 20,
                Level = 0,
                OreType = "dirt",
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
                OreType = "lavaGem",
            };
            Ore air = new Ore()
            {
                Value = 0,
                Hurt = false,
                Score = 0,
                Level = 0,
                OreType = "air",
                canPass = true,
            };
            //Character goblin = new Character()
            //{
            //    Fuel = 200,
            //    Health = 100,
            //    Money = 50,
            //    PickAxLevel = 0,
            //    Score = 0,
            //};
            //Map gameMap = new Map()
            //{
            //     GameMap = File.ReadAllLines("Map.txt"),
            //};

            // todo pickax
            // TODOD: Poison
            
            modelBuilder.Entity<Ore>().HasData(silver, gold, stone, copper, dirt, lavaGem, diamond, air);
            //modelBuilder.Entity<Character>().HasData(goblin);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GameData.mdf;Integrated Security=True");
        }
    }
}
