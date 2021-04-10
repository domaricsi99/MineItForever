using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class GameModel
    {
        public Character Miner { get; set; }

        public Shape Ground { get; set; }

        public Shape Gate { get; set; }

        public Building PickaxShopHouse { get; set; }

        public Building HealthShopHouse { get; set; }

        public Building PetrolShopHouse { get; set; }

        public Shape PickaxShop { get; set; }

        public Shape HealthShop { get; set; }

        public Shape PetrolShop { get; set; }

        public GameModel()
        {
            this.Miner = new Character(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight);
            this.Ground = new Shape(0, Config.Height - Config.GroundHeight, Config.GroundWidth, Config.GroundHeight);
            this.Gate = new Shape(Config.Width - Config.GateWidth, Config.Height - (Config.GroundHeight + Config.GateHeight), Config.GateWidth, Config.GateHeight);
            this.PickaxShopHouse = new Building(10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.HealthShopHouse = new Building(PickaxShopHouse.area.Y / 2 , Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PetrolShopHouse = new Building(HealthShopHouse.area.Y - 10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PickaxShop = new Shape(Config.BuildingWidth / 2 - 10, PickaxShopHouse.Area.Y, Config.ShopWidth,Config.ShopHeight);
            this.HealthShop = new Shape(HealthShopHouse.Area.X + Config.BuildingWidth / 2 - 20, HealthShopHouse.Area.Y, Config.ShopWidth, Config.ShopHeight);
            this.PetrolShop = new Shape(PetrolShopHouse.Area.X + Config.BuildingWidth / 2 - 20, PetrolShopHouse.Area.Y, Config.ShopWidth, Config.ShopHeight);
        }
    }
}
