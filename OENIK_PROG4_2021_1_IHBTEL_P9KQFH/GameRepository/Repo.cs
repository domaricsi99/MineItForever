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
        private GameRepository<Map> mapRepo;
        private GameRepository<Ore> oreRepo;
        private GameRepository<Building> buildingRepo;
        private GameRepository<Character> charRepo;
        private GameRepository<Ladder> ladderRepo;
        private GameRepository<Pickax> pickaxRepo;



        public GameRepository<Map> MapRepository => this.mapRepo;
        public GameRepository<Ore> OreRepository => this.oreRepo;
        public GameRepository<Building> BuildingRepository => this.buildingRepo;
        public GameRepository<Character> CharRepository => this.charRepo;
        public GameRepository<Ladder> LadderRepository => this.ladderRepo;
        public GameRepository<Pickax> PickaxRepository => this.pickaxRepo;

    }
}
