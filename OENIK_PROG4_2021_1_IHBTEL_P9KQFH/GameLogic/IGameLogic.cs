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
    using GameModelDll;

    interface IGameLogic
    {
        public void MoveCharacter(Direction d, int mapID);

        public Ore[,] DrawMap();

        public void Fall(int mapID);

        public void MineGate(int mapID);

        public void setCharPosition(double x, double y);

        public bool CanJumpMethod(int predictOreX, int predictOreYLeft, int predictOreBottom, int predictOreYRight, int jumpCount);
    }
}
