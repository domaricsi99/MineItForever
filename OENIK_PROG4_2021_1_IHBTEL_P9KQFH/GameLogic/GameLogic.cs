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
        Character character;
        Ore newAir;
        bool falling;

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
            this.map = this.mapRepo.DrawMap(character);
            this.ore = this.DrawMap();
            this.newAir = this.mapRepo.MakeAir();
        }

        public GameLogic(GameModel model, MapRepository mapRepo, CharacterRepository charRepo)
        {
            this.model = model;
            this.mapRepo = mapRepo;
            this.charRepo = charRepo;
            this.map = this.mapRepo.DrawMap(this.character);
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
                    this.character.ChangeX(-7.5);
                }
                else if (d == Direction.Right && this.character.Area.Right < Config.Width)
                {
                    this.character.ChangeX(+7.5);
                }
                else if (d == Direction.Up)
                {
                    this.character.ChangeY(-60);
                }
            }
            else if (mapID == 1)
            {
                double moveSize = Movement(d);

                if (d == Direction.Left && this.character.Area.Left > 0)
                {
                    this.character.ChangeX(-moveSize);
                    if (moveSize == 0 && !this.falling)
                    {
                        Mining(d);
                    }
                }
                else if (d == Direction.Right && this.character.Area.Right < Config.Width)
                {
                    this.character.ChangeX(moveSize);
                    if (moveSize == 0 && !this.falling)
                    {
                        Mining(d);
                    }
                }
                else if (d == Direction.Up)
                {
                    if (this.CanJumpMethod())
                    {
                        MapMovementDown();
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MapMovementDown()
        {
            foreach (var item in this.ore)
            {
                item.ChangeY(60);
            }
        }

        public bool CanJumpMethod()
        {
            Ore[,] renderedOres = this.MapPart();

            if (!this.falling)
            {
                Rect predictedChar = new Rect()
                {
                    X = this.character.Area.X,
                    Y = this.character.Area.Y - 60,
                    Size = this.character.Area.Size,
                    Height = this.character.Area.Height,
                    Width = this.character.Area.Width,
                };

                foreach (var item in renderedOres)
                {

                    if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air")
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
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
                        if (this.character.Area.Top >= item.Area.Bottom)
                        {
                            // this.character.SetXY(this.character.Area.X, item.Area.Bottom + 1);
                        }
                        else
                        {
                            canFall = false;
                        }

                        break;
                    }
                }

                if (canFall)
                {
                    foreach (var item2 in ore)
                    {
                        item2.ChangeY(-5);
                    }

                    falling = true;
                }
                else
                {
                    falling = false;
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public double Movement(Direction d)
        {
            Ore[,] renderedOres = this.MapPart();
            double movementRange = 7.5;
            Rect predictedChar = new Rect()
            {
                    X = this.character.Area.X,
                    Y = this.character.Area.Y + 1,
                    Height = this.character.Area.Height - 2,
                    Width = this.character.Area.Width,
            };

            switch (d)
            {
                case Direction.Left:
                    predictedChar.X -= 7.5;
                    foreach (var item in renderedOres)
                    {
                        //if (item.Area.Left <= this.character.Area.Left - 5 && this.character.Area.Left - 5 <= item.Area.Right
                        //    && item.canPass == false && item.Area.Bottom <= this.character.Area.Bottom && this.character.Area.Top <= item.Area.Top)
                        //{
                        //    return false;
                        //}
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air")
                        {
                            movementRange = (predictedChar.X + 7.5) - item.Area.Right - 1;
                            if (movementRange < 2)
                            {
                                return 0;
                            }
                        }
                    }

                    break;
                case Direction.Right:
                    predictedChar.X += 7.5;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air")
                        {
                            movementRange = item.Area.Left - 1 - (predictedChar.X + Config.MinerWidth - 7.5);
                            if (movementRange < 2)
                            {
                                return 0;
                            }
                        }
                    }

                    break;
                default:
                    break;
            }

            return movementRange;
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
            // this.charRepo.SaveMap();
        }

        public Character LoadGame(string name)
        {
            return this.charRepo.LoadGame(name);
        }

        public void Mining(Direction d)
        {
            Ore[,] renderedOres = this.MapPart();
            Rect predictedChar = new Rect()
            {
                X = this.character.Area.X,
                Y = this.character.Area.Y + 1,
                Height = this.character.Area.Height - 2,
                Width = this.character.Area.Width,
            };
            //foreach (var item in renderedOres)
            //{
            //    Point asd = item.Area.TopLeft;
            //    if ((this.character.PickAxLevel >= item.Level && item.OreType != "air") && (this.character.Area.TopRight == () || this.character.Area.TopLeft == item.Area.TopRight))
            //    {
            //        this.character.Backpack.Add(item);
            //        this.character.Score += item.Score;
            //        this.character.Money += item.Value; // Nem itt majd a shopban ha eladtuk

            //        item.OreType = this.newAir.OreType;
            //        item.canPass = this.newAir.canPass;
            //        item.Hurt = this.newAir.Hurt;
            //        item.BreakLevel = this.newAir.BreakLevel;
            //        item.Score = this.newAir.Score;
            //        item.Level = this.newAir.Level;
            //        item.Value = this.newAir.Value;
            //    }
            //}
            switch (d)
            {
                case Direction.Left:
                    predictedChar.X -= 7.5;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && this.character.PickAxLevel >= item.Level)
                        {
                            this.character.Backpack.Add(item);
                            this.character.Score += item.Score;
                            this.character.Money += item.Value; // Nem itt majd a shopban ha eladtuk

                            item.OreType = this.newAir.OreType;
                            item.canPass = this.newAir.canPass;
                            item.Hurt = this.newAir.Hurt;
                            item.BreakLevel = this.newAir.BreakLevel;
                            item.Score = this.newAir.Score;
                            item.Level = this.newAir.Level;
                            item.Value = this.newAir.Value;
                        }
                    }

                    break;
                case Direction.Right:
                    predictedChar.X += 7.5;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && this.character.PickAxLevel >= item.Level)
                        {
                                this.character.Backpack.Add(item);
                                this.character.Score += item.Score;
                                this.character.Money += item.Value; // Nem itt majd a shopban ha eladtuk

                                item.OreType = this.newAir.OreType;
                                item.canPass = this.newAir.canPass;
                                item.Hurt = this.newAir.Hurt;
                                item.BreakLevel = this.newAir.BreakLevel;
                                item.Score = this.newAir.Score;
                                item.Level = this.newAir.Level;
                                item.Value = this.newAir.Value;
                        }
                    }

                    break;
            }
        }
    }
}
