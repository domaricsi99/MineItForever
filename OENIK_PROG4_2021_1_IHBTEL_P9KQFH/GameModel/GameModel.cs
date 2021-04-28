// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    /// <summary>
    /// All game models.
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
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

            this.ButtonShape = new Shape(550, this.ButtonBackground.Area.Y + 90, Config.ButtonWidth, Config.ButtonHeight);

            this.PetrolButtonShape = new Shape(this.ButtonBackground.Area.X + 40, this.ButtonBackground.Area.Y + 90, Config.ButtonWidth, Config.ButtonHeight);
            this.HealthButtonShape = new Shape(220 + this.PetrolButtonShape.Area.X, this.PetrolButtonShape.Area.Y, Config.ButtonWidth, Config.ButtonHeight);

            this.EndGameButton = new Shape((Config.Width / 2) - (Config.EndGameButtonWidth / 2), Config.Height - Config.EndGameButtonHeight - 20, Config.EndGameButtonWidth, Config.EndGameButtonHeight);
        }

        /// <summary>
        /// Gets or sets ground model.
        /// </summary>
        public Shape Ground { get; set; }

        /// <summary>
        /// Gets or sets out of the first screen to mine.
        /// </summary>
        public Shape Gate { get; set; }

        /// <summary>
        /// Gets or sets pickaxe shop.
        /// </summary>
        public Building PickaxShopHouse { get; set; }

        /// <summary>
        /// Gets or sets health shop.
        /// </summary>
        public Building HealthShopHouse { get; set; }

        /// <summary>
        /// Gets or sets petrol shop.
        /// </summary>
        public Building PetrolShopHouse { get; set; }

        /// <summary>
        /// Gets or sets save button.
        /// </summary>
        public Shape SaveButtonRectangle { get; set; }

        /// <summary>
        /// Gets or sets load button.
        /// </summary>
        public Shape LoadButtonRectangle { get; set; }

        /// <summary>
        /// Gets or sets health shop.
        /// </summary>
        public Shape PickaxShop { get; set; }

        /// <summary>
        /// Gets or sets sell ore shop.
        /// </summary>
        public Shape SellShop { get; set; }

        /// <summary>
        /// Gets or sets petrol and health shop.
        /// </summary>
        public Shape PetrolAndHealthShop { get; set; }

        /// <summary>
        /// Gets or sets button shape.
        /// </summary>
        public Shape ButtonShape { get; set; }

        /// <summary>
        /// Gets or sets petrol button shape.
        /// </summary>
        public Shape PetrolButtonShape { get; set; }

        /// <summary>
        /// Gets or sets health button shape.
        /// </summary>
        public Shape HealthButtonShape { get; set; }

        /// <summary>
        /// Gets or sets button background.
        /// </summary>
        public Shape ButtonBackground { get; set; }

        /// <summary>
        /// Gets or sets end game button.
        /// </summary>
        public Shape EndGameButton { get; set; }
    }
}
