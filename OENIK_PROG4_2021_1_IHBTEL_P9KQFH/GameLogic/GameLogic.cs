// <copyright file="GameLogic.cs" company="PlaceholderCompany">
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
    using GameRepository;
    using System.Windows;

    public enum Direction
    {
        Left, Right, Up, Down
    }

    public class GameLogic : IGameLogic
    {
        private GameModel model;
        private MapRepository mapRepo;
        private CharacterRepository charRepo;
        private List<Ore> map;
        Ore[,] ore;
        Character character ;
        Ore newAir;

        int jumpCount = 0; //ugrasok sz�m�t sz�molja
        int maxJump = 2; // max mennyit ugorhatunk

        public event EventHandler RefreshScreen;

        public event EventHandler ChangeScreen;

        public event EventHandler BackToMapOneScreen;

        public event EventHandler ShopScreen;

        public GameLogic(GameModel model, MapRepository mapRepo, CharacterRepository charRepo, Character character)
        {
            this.character = character;
            this.model = model;
            this.mapRepo = mapRepo;
            this.charRepo = charRepo;
            this.map = this.mapRepo.DrawMap(character); // this.mapRepo.DrawMap();
            this.ore = this.DrawMap();
            newAir = mapRepo.MakeAir();
        }

        public GameLogic(GameModel model, MapRepository mapRepo, CharacterRepository charRepo)
        {
            this.model = model;
            this.mapRepo = mapRepo;
            this.charRepo = charRepo;
            this.map = this.mapRepo.DrawMap(character);
            this.ore = this.DrawMap();
        }
        //public GameLogic(Character character)
        //{
        //    this.character = character;
        //}

        public void MoveCharacter(Direction d, int mapID)
        {
            if (mapID == 0)
            {
                if (d == Direction.Left && this.character.Area.Left > 0)
                {
                    this.character.ChangeX(-10);
                }
                else if (d == Direction.Right && this.character.Area.Right < Config.Width)
                {
                    this.character.ChangeX(+10);
                }
                else if (d == Direction.Up)
                {
                    this.character.ChangeY(-60);
                }
            }
            else if (mapID == 1)
            {
                if (d == Direction.Left && this.character.Area.Left > 0)
                {
                    //int predictOreX = (int)((this.model.Miner.Area.Bottom - (Config.MinerHeight / 2)) / Config.oreHeight);
                    //int predictOreY = ((int)this.model.Miner.Area.Left - 8) / Config.oreWidth;

                    //if (!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreY].Area)
                    //    || this.ore[predictOreX, predictOreY].canPass == true)
                    //{
                    //    this.model.Miner.ChangeX(-7.5);
                    //}
                    if (Movement(d))
                    {
                        this.character.ChangeX(-7.5);
                    }
                    else
                    {
                        Mining();
                    }
                }
                else if (d == Direction.Right && this.character.Area.Right < Config.Width)
                {
                    //int predictOreX = (int)((this.model.Miner.Area.Bottom - (Config.MinerHeight / 2)) / Config.oreHeight);
                    //int predictOreY = ((int)this.model.Miner.Area.Right + 8) / Config.oreWidth;
                    //if (predictOreY > Config.Width)
                    //{
                    //    predictOreX = this.ore.GetLength(1);
                    //}

                    //if (predictOreY < 20)
                    //{
                    //    if (!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreY].Area)
                    //        || this.ore[predictOreX, predictOreY].canPass == true)
                    //    {
                    //        this.model.Miner.ChangeX(7.5);
                    //    }
                    //}
                    if (Movement(d))
                    {
                        this.character.ChangeX(7.5);
                    }
                    else
                    {
                        Mining();
                    }
                }
                else if (d == Direction.Up && this.jumpCount < 61)
                {
                    this.jumpCount++;
                    int predictOreX = (int)((this.character.Area.Top - 10) / Config.oreHeight);
                    int predictOreYLeft = (int)(this.character.Area.Left + 1) / Config.oreWidth;
                    int predictOreYRight = (int)(this.character.Area.Right - 1) / Config.oreWidth;
                    int predictOreBottom = (int)(this.character.Area.Bottom + 10) / Config.oreHeight;
                    if (this.CanJumpMethod(predictOreX, predictOreYLeft, predictOreBottom, predictOreYRight, this.jumpCount))
                    {
                        MapJump();
                    }
                    else
                    {
                        jumpCount = 0;
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MapJump()
        {
            foreach (var item2 in ore)
            {
                item2.ChangeY(60);
                this.jumpCount = 60;
            }
        }

        public bool CanJumpMethod(int predictOreX, int predictOreYLeft, int predictOreBottom, int predictOreYRight, int jumpCount)
        {
            bool move = false;
            if ((!(this.character.Area.IntersectsWith(this.ore[predictOreX, predictOreYLeft].Area)
                    && !this.character.Area.IntersectsWith(this.ore[predictOreX, predictOreYRight].Area)
                    && this.ore[predictOreBottom, predictOreYLeft].OreType != "air"
                    && this.ore[predictOreBottom, predictOreYRight].OreType != "air")
                    && jumpCount <= this.maxJump)
                    || ((this.ore[predictOreX, predictOreYLeft].canPass == true
                    && jumpCount <= this.maxJump)
                    && this.ore[predictOreX, predictOreYRight].canPass == true)) // mindig a 2. feltetel fog teljesulni 
            {
                move = true;
            }

            return move;
        }

        public Ore[,] DrawMap()
        {
            int delimeter = 20;
            Ore[,] oreMatrix = new Ore[this.character.Map.Length, delimeter]; // lehet kula
            int counter = 0;
            for (int i = 0; i < oreMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < oreMatrix.GetLength(1); j++)
                {
                    oreMatrix[i, j] = this.map[counter];
                    counter++;
                }
            }

            return oreMatrix;
        }

        public void Fall(int mapID) // csak alattunk
        {
            if (!this.character.Area.IntersectsWith(this.model.Ground.Area) && mapID == 0)
            {
                this.character.ChangeY(5);
            }
            else if (mapID == 1)
            {
                bool canFall = true;
                Ore[,] renderedOres = this.MapPart();
                foreach (var item in renderedOres)
                {
                    if (item.Area.IntersectsWith(this.character.Area) && item.canPass == false)
                    {
                        canFall = false;
                        break;
                    }
                }

                if (canFall)
                {
                    foreach (var item2 in ore)
                    {
                        item2.ChangeY(-5);
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public bool Movement(Direction d)
        {
            bool canGoThrough = true;
            Ore[,] renderedOres = this.MapPart();

            switch (d)
            {
                case Direction.Left:
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.Left <= this.character.Area.Left - 5 && this.character.Area.Left - 5 <= item.Area.Right
                            && item.canPass == false && item.Area.Bottom <= this.character.Area.Bottom && this.character.Area.Top <= item.Area.Top)
                        {
                            return false;
                        }
                    }

                    return true;
                case Direction.Right:
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.Left <= this.character.Area.Right + 5 && this.character.Area.Right + 5 <= item.Area.Right
                            && item.canPass == false && item.Area.Bottom <= this.character.Area.Bottom && this.character.Area.Top <= item.Area.Top)
                        {
                            return false;
                        }
                    }

                    return true;
                default:
                    break;
            }

            return false;
        }

        public void MineGate(int mapID)
        {
            if (this.character.Area.IntersectsWith(this.model.Gate.Area) && mapID == 0)
            {
                this.ChangeScreen?.Invoke(this, EventArgs.Empty);
            }
            else if (this.character.Area.IntersectsWith(this.model.MapThreeToOneGate.Area) && mapID == 3)
            {
                this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
            }
            else if (this.character.Area.IntersectsWith(this.model.MapTwoToOneGate.Area) && mapID == 1)
            {
                this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
            }
            SaveGame(character);
        }

        public bool IntersectsWithShop() // TODO: Eventk�nt
        {
            if (this.character.Area.IntersectsWith(this.model.HealthShop.Area))
            {
                return true;
            }
            else if (this.character.Area.IntersectsWith(this.model.PetrolShop.Area))
            {
                return true;
            }
            else if (this.character.Area.IntersectsWith(this.model.PickaxShop.Area))
            {
                return true;
            }

            return false;
        }

        public void setCharPosition(double x, double y)
        {
            this.character.SetXY(x, y);
        }

        public Ore[,] MapPart()
        {
            Ore[,] renderedOres = new Ore[5, 5];
            int intersectOreX = 2;
            int intersectOreY = 2;

            for (int i = 0; i < ore.GetLength(0); i++) // TODO: WHile megold�s szebb!
            {
                for (int j = 0; j < ore.GetLength(1); j++)
                {
                    if (this.character.Area.IntersectsWith(this.ore[i,j].Area))
                    {
                        intersectOreX = i;
                        intersectOreY = j;
                    }
                }
            }

            if (intersectOreX - 2 < 0)
            {
                intersectOreX = 2;
            }

            if (intersectOreY - 2 < 0)
            {
                intersectOreY = 2;
            }

            if ((intersectOreX + 2) >= this.ore.GetLength(0))
            {
                intersectOreX = this.ore.GetLength(0) - 3;
            }

            if ((intersectOreY + 2) * 45 >= Config.Width)
            {
                intersectOreY = this.ore.GetLength(1) - 3;
            }

            int startingPosY = intersectOreY;
            for (int i = 0; i < renderedOres.GetLength(0); i++)
            {
                for (int j = 0; j < renderedOres.GetLength(1); j++)
                {

                    renderedOres[i, j] = this.ore[intersectOreX - 2, intersectOreY - 2];
                    intersectOreY++;
                }

                intersectOreY = startingPosY;
                intersectOreX++;
            }

            return renderedOres;
        }

        public void SaveGame(Character character)
        {
            this.charRepo.SaveGame(character);
        }

        public Character LoadGame(string name)
        {
            return this.charRepo.LoadGame(name);
        }

        public void Mining ()
        {
            Ore[,] renderedOres = this.MapPart();
            foreach (var item in renderedOres)
            {
                if ((this.character.PickAxLevel >= item.Level && item.OreType != "air") && (this.character.Area.TopRight == item.Area.TopLeft || this.character.Area.TopLeft == item.Area.TopRight))
                {
                    this.character.Backpack.Add(item);
                    this.character.Score += item.Score;
                    this.character.Money += item.Value;

                    item.OreType = this.newAir.OreType;
                    item.canPass = this.newAir.canPass;
                    item.Hurt = this.newAir.Hurt;
                    item.BreakLevel = this.newAir.BreakLevel;
                    item.Score = this.newAir.Score;
                    item.Level = this.newAir.Level;
                    item.Value = this.newAir.Value;
                }
            }
        }
    }
}
