using GameModelDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    public class MapRepository //: GameRepository<Map>
    {
        List<Ore> map;

        public MapRepository()
        {
        }

        public List<Ore> DrawMap()
        {
            this.map = new List<Ore>();
            int localOreX = Config.oreX;
            int localOreY = Config.oreY;
            string[] fajlsorai = File.ReadAllLines("palya.txt");
            foreach (var item in fajlsorai)
            {
                string[] splitteltsor = item.Split(';');
                foreach (var item2 in splitteltsor)
                {
                    switch (item2)
                    {
                        case "0":
                            map.Add(new Ore()
                            {
                                OREID = 0,
                                Value = 0,
                                Hurt = false,
                                Score = 0,
                                Level = 0,
                                OreType = "air",
                                canPass = true,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "1":
                            map.Add(new Ore()
                            {
                                OREID = 1,
                                Value = 5,
                                Hurt = false,
                                Score = 20,
                                Level = 0,
                                OreType = "dirt",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "2":
                            map.Add(new Ore()
                            {
                                OREID = 2,
                                Value = 10,
                                Hurt = false,
                                Score = 100,
                                Level = 1,
                                OreType = "copper",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "3":
                            map.Add(new Ore()
                            {
                                OREID = 3,
                                Value = 20,
                                Hurt = false,
                                Score = 200,
                                Level = 2,
                                OreType = "silver",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "4":
                            map.Add(new Ore()
                            {
                                OREID = 4,
                                Value = 40,
                                Hurt = false,
                                Score = 400,
                                Level = 3,
                                OreType = "gold",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "5":
                            map.Add(new Ore()
                            {
                                OREID = 5,
                                Value = 1,
                                Hurt = false,
                                Score = 50,
                                Level = 3,
                                OreType = "stone",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "6":
                            map.Add(new Ore()
                            {
                                OREID = 6,
                                Value = 100,
                                Hurt = false,
                                Score = 1000,
                                Level = 4,
                                OreType = "diamond",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "7":
                            map.Add(new Ore()
                            {
                                OREID = 7,
                                Value = 200,
                                Hurt = true,
                                Score = 2000,
                                Level = 4,
                                OreType = "lava",
                                canPass = false,
                            });
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                    }

                    localOreX += 45;
                }

                localOreX = 0;
                localOreY += 45;
            }

            return map;
        }
    }
}
