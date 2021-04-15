using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameModelDll
{
    public class GameModel
    {
        public Character Miner { get; set; }

        public Shape Ground { get; set; }

        /// <summary>
        /// Gets or sets out of the first screen to mine.
        /// </summary>
        public Shape Gate { get; set; }

        /// <summary>
        /// Gets or sets out of shop to first screen.
        /// </summary>
        public Shape MapThreeToOneGate { get; set; }

        /// <summary>
        /// Gets or sets out of mine to first screen.
        /// </summary>
        public Shape MapTwoToOneGate { get; set; }

        public Building PickaxShopHouse { get; set; }

        public Building HealthShopHouse { get; set; }

        public Building PetrolShopHouse { get; set; }

        public Shape SaveButtonRectangle { get; set; }

        public Shape LoadButtonRectangle { get; set; }

        public Shape PickaxShop { get; set; }

        public Shape HealthShop { get; set; }

        public Shape PetrolShop { get; set; }

        public GameModel()
        {
            this.Miner = new Character(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight);
            this.Ground = new Shape(0, Config.Height - Config.GroundHeight, Config.GroundWidth, Config.GroundHeight);
            this.Gate = new Shape(Config.Width - Config.GateWidth, Config.Height - (Config.GroundHeight + Config.GateHeight), Config.GateWidth, Config.GateHeight);
            this.PickaxShopHouse = new Building(10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.HealthShopHouse = new Building(this.PickaxShopHouse.area.Y / 2, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PetrolShopHouse = new Building(this.HealthShopHouse.area.Y - 10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PickaxShop = new Shape((Config.BuildingWidth / 2) - 10, this.PickaxShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);
            this.HealthShop = new Shape(this.HealthShopHouse.Area.X + (Config.BuildingWidth / 2) - 20, this.HealthShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);
            this.PetrolShop = new Shape(this.PetrolShopHouse.Area.X + (Config.BuildingWidth / 2) - 20, this.PetrolShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);
            this.MapThreeToOneGate = new Shape(Config.Width - (Config.GateWidth * 2), Config.Height - (Config.GroundHeight + Config.GateHeight), Config.GateWidth * 2, Config.GateHeight);
            this.MapTwoToOneGate = new Shape(Config.Width - Config.oreWidth, 0, Config.oreWidth, Config.oreHeight);
            this.SaveButtonRectangle = new Shape(Config.Width / 2 + 20, Config.Height / 2 + 20, Config.BuildingWidth, Config.BuildingHeight);
            this.LoadButtonRectangle = new Shape(SaveButtonRectangle.Area.X + Config.BuildingWidth, SaveButtonRectangle.Area.Y + Config.BuildingHeight , Config.BuildingWidth, Config.BuildingHeight);
        }
    }
}
