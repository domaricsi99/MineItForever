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
        // public Character Miner { get; set; }

        public Shape Ground { get; set; }

        /// <summary>
        /// Gets or sets out of the first screen to mine.
        /// </summary>
        public Shape Gate { get; set; }

        public Building PickaxShopHouse { get; set; }

        public Building HealthShopHouse { get; set; }

        public Building PetrolShopHouse { get; set; }

        public Shape SaveButtonRectangle { get; set; }

        public Shape LoadButtonRectangle { get; set; }

        public Shape PickaxShop { get; set; }

        public Shape SellShop { get; set; }

        public Shape PetrolAndHealthShop { get; set; }

        public Shape ButtonShape { get; set; }

        public Shape PetrolButtonShape { get; set; }

        public Shape HealthButtonShape { get; set; }

        public Shape ButtonBackground { get; set; }

        public GameModel()
        {
            this.Ground = new Shape(0, Config.Height - Config.GroundHeight, Config.GroundWidth, Config.GroundHeight);
            this.Gate = new Shape(Config.Width - Config.GateWidth, Config.Height - (Config.GroundHeight + Config.GateHeight), Config.GateWidth, Config.GateHeight);
            this.PickaxShopHouse = new Building(10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.HealthShopHouse = new Building(this.PickaxShopHouse.area.Y / 2, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PetrolShopHouse = new Building(this.HealthShopHouse.area.Y - 10, Config.Height - (Config.GroundHeight + Config.BuildingHeight), Config.BuildingWidth, Config.BuildingHeight);
            this.PickaxShop = new Shape((Config.BuildingWidth / 2) - 5, this.PickaxShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);
            this.SellShop = new Shape(this.HealthShopHouse.Area.X + (Config.BuildingWidth / 2) - 20, this.HealthShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);
            this.PetrolAndHealthShop = new Shape(this.PetrolShopHouse.Area.X + (Config.BuildingWidth / 2) - 20, this.PetrolShopHouse.Area.Bottom - Config.ShopHeight, Config.ShopWidth, Config.ShopHeight);

            this.ButtonBackground = new Shape(Config.Width / 2, Config.Height / 6, Config.ButtonBgWidth, Config.ButtonBgHeight);

            this.ButtonShape = new Shape(550, ButtonBackground.Area.Y + 100, Config.ButtonWidth, Config.ButtonHeight);

            this.PetrolButtonShape = new Shape(ButtonBackground.Area.X + 40, ButtonBackground.Area.Y + 100, Config.ButtonWidth, Config.ButtonHeight);
            this.HealthButtonShape = new Shape(220 + PetrolButtonShape.Area.X, PetrolButtonShape.Area.Y, Config.ButtonWidth, Config.ButtonHeight);
        }
    }
}
