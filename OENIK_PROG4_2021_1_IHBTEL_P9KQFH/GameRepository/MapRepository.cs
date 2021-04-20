using GameModelDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameRepository
{
    public class MapRepository : IMapRepository
    {
        private List<Ore> map;

        public MapRepository()
        {
        }

        public List<Ore> DrawMap(Character character)
        {
            this.map = new List<Ore>();
            int localOreX = Config.oreX;
            int localOreY = Config.oreY;
            string[] fileLines = character.Map;
            foreach (var item in fileLines)
            {
                string[] splitLine = item.Split(';');
                foreach (var item2 in splitLine)
                {
                    switch (item2)
                    {
                        case "0":
                            this.map.Add(new Ore()
                            {
                                Value = 0,
                                Hurt = false,
                                Score = 0,
                                Level = 0,
                                OreType = "air",
                                canPass = true,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "1":
                            this.map.Add(new Ore()
                            {
                                Value = 5,
                                Hurt = false,
                                Score = 20,
                                Level = 0,
                                OreType = "dirt",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "2":
                            this.map.Add(new Ore()
                            {
                                Value = 10,
                                Hurt = false,
                                Score = 100,
                                Level = 1,
                                OreType = "copper",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "3":
                            this.map.Add(new Ore()
                            {
                                Value = 20,
                                Hurt = false,
                                Score = 200,
                                Level = 2,
                                OreType = "silver",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "4":
                            this.map.Add(new Ore()
                            {
                                Value = 40,
                                Hurt = false,
                                Score = 400,
                                Level = 3,
                                OreType = "gold",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "5":
                            this.map.Add(new Ore()
                            {
                                Value = 1,
                                Hurt = false,
                                Score = 50,
                                Level = 3,
                                OreType = "stone",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "6":
                            this.map.Add(new Ore()
                            {
                                Value = 100,
                                Hurt = false,
                                Score = 1000,
                                Level = 4,
                                OreType = "diamond",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "7":
                            this.map.Add(new Ore()
                            {
                                Value = 200,
                                Hurt = true,
                                Score = 2000,
                                Level = 4,
                                OreType = "lava",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "8":
                            this.map.Add(new Ore()
                            {
                                Value = 0,
                                Hurt = false,
                                Score = 0,
                                Level = 99999,
                                OreType = "ground2",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                        case "9":
                            this.map.Add(new Ore()
                            {
                                Value = 0,
                                Hurt = false,
                                Score = 0,
                                Level = 99999,
                                OreType = "gate",
                                canPass = false,
                                area = new Rect(localOreX, localOreY, Config.oreWidth, Config.oreHeight),
                            });
                            break;
                    }

                    localOreX += 45;
                }

                localOreX = 0;
                localOreY += 45;
            }

            return this.map;
        }

        public Ore MakeAir()
        {
            return new Ore()
            {
                BreakLevel = 0,
                canPass = true,
                Hurt = false,
                Level = 0,
                OreType = "air",
                Score = 0,
                Value = 0,
            };
        }

        public Ore MakeLadder()
        {
            return new Ore()
            {
                BreakLevel = 0,
                canPass = true,
                Hurt = false,
                Level = 0,
                OreType = "ladder",
                Score = 0,
                Value = 0,
            };
        }
    }
}
