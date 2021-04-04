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
                            break;
                        case "1":
                            map.Add(GetOne("dirt"));
                            break;
                        case "2":
                            map.Add(GetOne("copper"));
                            break;
                        case "3":
                            map.Add(GetOne("silver"));
                            break;
                        case "4":
                            map.Add(GetOne("gold"));
                            break;
                        case "5":
                            map.Add(GetOne("diamond"));
                            break;
                    }
                }
            }

            return map;
        }
    }
}
