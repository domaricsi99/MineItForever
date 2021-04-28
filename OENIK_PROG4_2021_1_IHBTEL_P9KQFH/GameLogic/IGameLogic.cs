// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogicDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using GameModelDll;

    /// <summary>
    /// Game logic interface.
    /// </summary>
    internal interface IGameLogic
    {
        /// <summary>
        /// How the character moves.
        /// </summary>
        /// <param name="d">Direction.</param>
        /// <param name="mapID">Which map.</param>
        public void MoveCharacter(Direction d, int mapID);

        /// <summary>
        /// Map as if moving downwards (LADDER).
        /// </summary>
        public void MapMovementDownLadder();

        /// <summary>
        /// Map as if moving upwards (LADDER).
        /// </summary>
        public void MapMovementUpLadder();

        /// <summary>
        /// Map as if moving downwards.
        /// </summary>
        public void MapMovementDown();

        /// <summary>
        /// See if the jump is possible.
        /// </summary>
        /// <returns>If we can jump back true.</returns>
        public bool CanJumpMethod();

        /// <summary>
        /// Make mine to matrix.
        /// </summary>
        /// <returns>Ore[,] map.</returns>
        public Ore[,] DrawMap();

        /// <summary>
        /// Fall checker.
        /// </summary>
        /// <param name="mapID">Which map we are on.</param>
        public void Fall(int mapID);

        public double Movement(Direction d);

        /// <summary>
        /// Mine gate intersect.
        /// </summary>
        /// <param name="mapID">Which map we are on.</param>
        public void MineGate(int mapID);

        /// <summary>
        /// Miner intersects with shop.
        /// </summary>
        /// <returns>Shop name.</returns>
        public string IntersectsWithShop();

        /// <summary>
        /// Set character position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void SetCharPosition(double x, double y);

        /// <summary>
        /// What can we see in mine.
        /// </summary>
        /// <returns>5x5 Ore matrix.</returns>
        public Ore[,] MapPart();

        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="character">Character what we save.</param>
        public void SaveGame(Character character);

        /// <summary>
        /// Load game.
        /// </summary>
        /// <returns>Current character.</returns>
        public Character LoadGame();

        /// <summary>
        /// Mining logic.
        /// </summary>
        /// <param name="d">Which direction to mine.</param>
        public void Mining(Direction d);

        /// <summary>
        /// Drop one ladder.
        /// </summary>
        /// <param name="point">mouse click coordinate.</param>
        /// <param name="mapID">Which map we are on.</param>
        public void DropLadder(Point point, int mapID);

        /// <summary>
        /// Pick up one ladder.
        /// </summary>
        /// <param name="point">mouse click coordinate.</param>
        /// <param name="mapID">Which map we are on.</param>
        public void PickUpLadder(Point point, int mapID);

        /// <summary>
        /// Health buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string HealthBuyLogic();

        /// <summary>
        /// Petrol buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string PetrolBuyLogic();

        /// <summary>
        /// Sell ores logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string SellOreLogic();

        /// <summary>
        /// Pickax buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string PickaxBuyLogic();

        /// <summary>
        /// End game logic.
        /// </summary>
        public void EndGame();

        /// <summary>
        /// Get hurt from lava.
        /// </summary>
        /// <param name="ore">lava.</param>
        public void Damage(Ore ore);

        /// <summary>
        /// Get hurt from a fall.
        /// </summary>
        /// <param name="counter">How many pixel we fall.</param>
        public void DeadFall(int counter);

        /// <summary>
        /// Where can we click logic.
        /// </summary>
        /// <param name="point">click coordinate.</param>
        /// <param name="mapID">which map.</param>
        /// <param name="shop">which shop.</param>
        public void Click(Point point, int mapID, string shop);
    }
}
