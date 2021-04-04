using GameData;
using GameModelDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    public class Repo
    {
        private GameDataBase dbContext;
        private MapRepository mapRepo;
        private GameRepository<Ore> oreRepo;
        private GameRepository<Building> buildingRepo;
        private GameRepository<Character> charRepo;
        private GameRepository<Ladder> ladderRepo;
        private GameRepository<Pickax> pickaxRepo;

        public Repo(GameDataBase dbContext)
        {
            this.dbContext = dbContext;
        }

        public MapRepository GameMapRepository => this.mapRepo;
        public GameRepository<Ore> OreRepository => this.oreRepo;
        public GameRepository<Building> BuildingRepository => this.buildingRepo;
        public GameRepository<Character> CharRepository => this.charRepo;
        public GameRepository<Ladder> LadderRepository => this.ladderRepo;
        public GameRepository<Pickax> PickaxRepository => this.pickaxRepo;

    }
}
