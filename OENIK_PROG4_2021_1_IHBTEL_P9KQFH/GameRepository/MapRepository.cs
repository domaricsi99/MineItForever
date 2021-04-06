using GameData;
using GameModelDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    public class MapRepository : GameRepository<Map>
    {
        private GameDataBase dbContext;

        public MapRepository(GameDataBase dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Ore GetOne(string name)
        {
            return dbContext.Ores.SingleOrDefault(x => x.OreType == name);
        }

        public List<Ore> DrawMap()
        {
            List<Ore> map = new List<Ore>();
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
                            map.Add(GetOne("air"));
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "1":
                            map.Add(GetOne("dirt"));
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "2":
                            map.Add(GetOne("copper"));
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "3":
                            map.Add(GetOne("silver"));
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "4":
                            map.Add(GetOne("gold"));
                            map.Last().area.X = localOreX;
                            map.Last().area.Y = localOreY;
                            break;
                        case "5":
                            map.Add(GetOne("diamond"));
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
