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

    interface IGameLogic
    {
        public void MoveCharacter(Direction d, int mapID);

        public void MapMovementDownLadder();

        public void MapMovementUpLadder();

        public void MapMovementDown();

        public bool CanJumpMethod();

        public Ore[,] DrawMap();

        public void Fall(int mapID);

        public double Movement(Direction d);

        public void MineGate(int mapID);

        public string IntersectsWithShop();

        public void setCharPosition(double x, double y);

        public Ore[,] MapPart();

        public void SaveGame(Character character);

        public Character LoadGame();

        public void Mining(Direction d);

        public void DropLadder(Point point, int mapID);

        public void PickUpLadder(Point point, int mapID);

        public string HealthBuyLogic();

        public string PetrolBuyLogic();

        public string SellOreLogic();

        public string PickaxBuyLogic();

        public void EndGame();

        public void Damage(Ore ore);

        public void DeadFall(int counter);

        public void Click(Point point, int mapID, string shop);
    }
}
