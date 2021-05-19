// <copyright file="MapRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using GameModelDll;

    /// <summary>
    /// Map repository.
    /// </summary>
    public class MapRepository : IMapRepository
    {
        private List<Ore> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapRepository"/> class.
        /// </summary>
        public MapRepository()
        {
        }

        /// <summary>
        /// Strinng ore list.
        /// </summary>
        /// <param name="character">Current character.</param>
        /// <returns>List Ore.</returns>
        public List<Ore> StringToOreList(Character character)
        {
            this.map = new List<Ore>();
            int localOreX = Config.OreX;
            int localOreY = Config.OreY;
            int counter = 0;
            foreach (string item in character.Map)
            {
                switch (item)
                {
                    case "0":
                        this.map.Add(new Ore()
                        {
                            Value = 0,
                            Hurt = false,
                            Score = 0,
                            Level = 0,
                            OreType = "air",
                            CanPass = true,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "1":
                        this.map.Add(new Ore()
                        {
                            Value = 10,
                            Hurt = false,
                            Score = 20,
                            Level = 0,
                            OreType = "dirt",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "2":
                        this.map.Add(new Ore()
                        {
                            Value = 20,
                            Hurt = false,
                            Score = 100,
                            Level = 1,
                            OreType = "copper",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "3":
                        this.map.Add(new Ore()
                        {
                            Value = 30,
                            Hurt = false,
                            Score = 200,
                            Level = 2,
                            OreType = "silver",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "4":
                        this.map.Add(new Ore()
                        {
                            Value = 45,
                            Hurt = false,
                            Score = 400,
                            Level = 3,
                            OreType = "gold",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
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
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "6":
                        this.map.Add(new Ore()
                        {
                            Value = 150,
                            Hurt = false,
                            Score = 1000,
                            Level = 4,
                            OreType = "diamond",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "7":
                        this.map.Add(new Ore()
                        {
                            Value = 300,
                            Hurt = true,
                            Score = 2000,
                            Level = 4,
                            OreType = "lava",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
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
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
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
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "10":
                        this.map.Add(new Ore()
                        {
                            BreakLevel = 0,
                            CanPass = true,
                            Hurt = false,
                            Level = 0,
                            OreType = "ladder",
                            Score = 0,
                            Value = 0,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "11":
                        this.map.Add(new Ore()
                        {
                            Value = 0,
                            Hurt = false,
                            Score = 0,
                            Level = 99999,
                            OreType = "gate2",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                    case "12":
                        this.map.Add(new Ore()
                        {
                            Value = 0,
                            Hurt = false,
                            Score = 0,
                            Level = 99999,
                            OreType = "gate3",
                            CanPass = false,
                            Area = new Rect(localOreX, localOreY, Config.OreWidth, Config.OreHeight),
                        });
                        break;
                }

                localOreX += 45;
                counter++;
                if (counter >= Config.MapDelimeter)
                {
                            counter = 0;
                            localOreX = 0;
                            localOreY += 45;
                }
            }

            return this.map;
        }

        /// <summary>
        /// Make air.
        /// </summary>
        /// <returns>Air.</returns>
        public Ore MakeAir()
        {
            return new Ore()
            {
                BreakLevel = 0,
                CanPass = true,
                Hurt = false,
                Level = 0,
                OreType = "air",
                Score = 0,
                Value = 0,
            };
        }

        /// <summary>
        /// Make ladder.
        /// </summary>
        /// <returns>Ladder.</returns>
        public Ore MakeLadder()
        {
            return new Ore()
            {
                BreakLevel = 0,
                CanPass = true,
                Hurt = false,
                Level = 0,
                OreType = "ladder",
                Score = 0,
                Value = 0,
            };
        }

        /// <summary>
        /// Make random map.
        /// </summary>
        /// <returns>Random map in string list.</returns>
        public List<string> RandomMap()
        {
            // 0-air, 1-dirt - 50, 2-copper-20, 3-silver -10, 4-gold-10, 5-stone, 6-diamond-5, 7-lava-5, 9-gate
            Random r = new Random();

            List<string> map = new List<string>();

            int size = 2000;

            for (int i = 0; i < size; i++)
            {
                if (i <= 19)
                {
                    map.Add("5");
                }
                else if (i > 19 && i <= 39)
                {
                    map.Add("0");
                }
                else if (i > 39 && i <= 58)
                {
                    if (i == 40)
                    {
                        map.Add("11");
                    }

                    map.Add("0");
                }
                else if (i > 58 && i <= 77)
                {
                    if (i == 59)
                    {
                        map.Add("12");
                    }

                    map.Add("0");
                }
                else if (i > 77 && i <= 81)
                {
                    map.Add("5");
                }
                else
                {
                    int randomNumber = r.Next(0, 101);
                    if (randomNumber <= 50)
                    {
                        map.Add("1");
                    }
                    else if (randomNumber > 50 && randomNumber <= 70)
                    {
                        map.Add("2");
                    }
                    else if (randomNumber > 70 && randomNumber <= 80)
                    {
                        map.Add("3");
                    }
                    else if (randomNumber > 80 && randomNumber <= 90 && i >= size / 4)
                    {
                        map.Add("4");
                    }
                    else if (randomNumber > 90 && randomNumber <= 95 && i >= size / 2)
                    {
                        map.Add("6");
                    }
                    else if (randomNumber > 95 && randomNumber <= 100 && i >= size / 2)
                    {
                        map.Add("7");
                    }
                }
            }

            return map;
        }
    }
}
