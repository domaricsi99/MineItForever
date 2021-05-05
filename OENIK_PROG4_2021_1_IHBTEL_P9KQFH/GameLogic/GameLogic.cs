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
    using System.Windows;
    using GameModelDll;
    using GameRepository;

    /// <summary>
    /// Where we can move to.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Move left.
        /// </summary>
        Left,

        /// <summary>
        /// Move right
        /// </summary>
        Right,

        /// <summary>
        /// Move up.
        /// </summary>
        Up,

        /// <summary>
        /// Move down.
        /// </summary>
        Down,

        /// <summary>
        /// Climb the ladder.
        /// </summary>
        Climb,
    }

    /// <summary>
    /// The logic of the game.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private GameModel model;
        private IMapRepository mapRepo;
        private IChracterRepository charRepo;
        private IShopRepository shopRepo;
        private List<Ore> map;

        private Ore[,] ore;
        private Character character;
        private Ore newAir;
        private Ore newLadder;
        private bool falling;
        private int fallCounter;

        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">models.</param>
        /// <param name="mapRepo">map.</param>
        /// <param name="charRepo">character repo.</param>
        /// <param name="character">character.</param>
        public GameLogic(GameModel model, IMapRepository mapRepo, IChracterRepository charRepo, Character character)
        {
            this.character = character;
            this.model = model;
            this.mapRepo = mapRepo;
            this.charRepo = charRepo;
            this.map = this.mapRepo.StringToOreList(character);
            this.ore = this.DrawMap();
            this.newAir = this.mapRepo.MakeAir();
            this.newLadder = this.mapRepo.MakeLadder();
            this.shopRepo = new ShopRepsitory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">model.</param>
        /// <param name="mapRepo">map.</param>
        /// <param name="charRepo">character.</param>
        public GameLogic(GameModel model, IMapRepository mapRepo, IChracterRepository charRepo)
        {
            this.model = model;
            this.mapRepo = mapRepo;
            this.charRepo = charRepo;
            this.map = this.mapRepo.StringToOreList(this.character);
            this.ore = this.DrawMap();
        }

        /// <summary>
        /// Refresh screen event.
        /// </summary>
        public event EventHandler RefreshScreen;

        /// <summary>
        /// Change screen event.
        /// </summary>
        public event EventHandler ChangeScreen;

        /// <summary>
        /// Back to map one screen event.
        /// </summary>
        public event EventHandler BackToMapOneScreen;

        /// <summary>
        /// End game event.
        /// </summary>
        public event EventHandler EndGameEvent;

        /// <summary>
        /// Main menu event.
        /// </summary>
        public event EventHandler BackToMainMenuEvent;

        /// <summary>
        /// How the character moves.
        /// </summary>
        /// <param name="d">Direction.</param>
        /// <param name="mapID">Which map.</param>
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
                double moveSize = this.Movement(d);

                if (d == Direction.Left && this.character.Area.Left > 0)
                {
                    this.character.ChangeX(-moveSize);
                    if (moveSize == 0 && !this.falling)
                    {
                        this.Mining(d);
                    }
                }
                else if (d == Direction.Right && this.character.Area.Right < Config.Width)
                {
                    this.character.ChangeX(moveSize);
                    if (moveSize == 0 && !this.falling)
                    {
                        this.Mining(d);
                    }
                }
                else if (d == Direction.Up)
                {
                    if (this.CanJumpMethod())
                    {
                        this.MapMovementDown();
                    }
                }
                else if (d == Direction.Down)
                {
                    if (moveSize == 0 && !this.falling)
                    {
                        this.Mining(d);
                    }
                    else if (moveSize == 1)
                    {
                        this.MapMovementUpLadder();
                    }
                }
                else if (d == Direction.Climb)
                {
                    if (moveSize == 1 && !this.falling)
                    {
                        this.MapMovementDownLadder();
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Map as if moving downwards (LADDER).
        /// </summary>
        public void MapMovementDownLadder()
        {
            foreach (var item in this.ore)
            {
                item.ChangeY(5);
            }
        }

        /// <summary>
        /// Map as if moving upwards (LADDER).
        /// </summary>
        public void MapMovementUpLadder()
        {
            foreach (var item in this.ore)
            {
                item.ChangeY(-5);
            }
        }

        /// <summary>
        /// Map as if moving downwards.
        /// </summary>
        public void MapMovementDown()
        {
            foreach (var item in this.ore)
            {
                item.ChangeY(60);
            }
        }

        /// <summary>
        /// See if the jump is possible.
        /// </summary>
        /// <returns>If we can jump back true.</returns>
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

        /// <summary>
        /// Make mine to matrix.
        /// </summary>
        /// <returns>Ore[,] map.</returns>
        public Ore[,] DrawMap()
        {
            int delimeter = 20;
            Ore[,] oreMatrix = new Ore[this.character.Map.Count / 20, delimeter];
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

        /// <summary>
        /// Fall checker.
        /// </summary>
        /// <param name="mapID">Which map we are on.</param>
        public void Fall(int mapID)
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
                    if (item.Area.IntersectsWith(this.character.Area) && item.CanPass == false)
                    {
                        canFall = false;

                        break;
                    }
                    else if (item.Area.IntersectsWith(this.character.Area) && item.OreType == "ladder")
                    {
                        canFall = false;
                    }
                }

                if (canFall)
                {
                    foreach (var item2 in this.ore)
                    {
                        item2.ChangeY(-5);
                    }

                    this.fallCounter += 5;
                    this.falling = true;
                }
                else
                {
                    this.DeadFall(this.fallCounter);
                    this.falling = false;
                    this.fallCounter = 0;
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Movement range.
        /// </summary>
        /// <param name="d">Direction.</param>
        /// <returns>How much more we can move.</returns>
        public double Movement(Direction d)
        {
            Ore[,] renderedOres = this.MapPart();
            double movementRange = 7.5;
            int canPassThrough = 0;
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
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && item.OreType != "ladder" && item.OreType != "gate" && item.OreType != "gate2" && item.OreType != "gate3")
                        {
                            movementRange = (predictedChar.X + 7.5) - item.Area.Right - 1;
                            if (movementRange < 2)
                            {
                                return 0;
                            }
                        }
                        else if ((item.Area.IntersectsWith(predictedChar) && item.OreType == "gate")
                            || (item.Area.IntersectsWith(predictedChar) && item.OreType == "gate2")
                            || (item.Area.IntersectsWith(predictedChar) && item.OreType == "gate3"))
                        {
                            this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    break;
                case Direction.Right:
                    predictedChar.X += 7.5;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && item.OreType != "ladder" && item.OreType != "gate")
                        {
                            movementRange = item.Area.Left - 1 - (predictedChar.X + Config.MinerWidth - 7.5);
                            if (movementRange < 2)
                            {
                                return 0;
                            }
                        }
                        else if (item.Area.IntersectsWith(predictedChar) && item.OreType == "gate")
                        {
                            this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    break;
                case Direction.Down:
                    predictedChar.Y += Config.MinerHeight;
                    foreach (var item in renderedOres)
                    {
                        if ((item.OreType == "ladder" || item.OreType == "air")
                            && item.Area.Left <= predictedChar.Left
                            && item.Area.Right >= predictedChar.Right
                            && item.Area.IntersectsWith(predictedChar))
                        {
                            canPassThrough = 1;
                            return canPassThrough;
                        }
                        else
                        {
                            canPassThrough = 0;
                        }
                    }

                    return canPassThrough;
                case Direction.Climb:
                    predictedChar.Y -= Config.MinerHeight;
                    foreach (var item in renderedOres)
                    {
                        if ((item.OreType == "ladder" || item.OreType == "air")
                            && item.Area.Left <= predictedChar.Left
                            && item.Area.Right >= predictedChar.Right
                            && item.Area.IntersectsWith(predictedChar))
                        {
                            canPassThrough = 1;
                            return canPassThrough;
                        }
                        else
                        {
                            canPassThrough = 0;
                        }
                    }

                    return canPassThrough;
                default:
                    break;
            }

            return movementRange;
        }

        /// <summary>
        /// Mine gate intersect.
        /// </summary>
        /// <param name="mapID">Which map we are on.</param>
        public void MineGate(int mapID)
        {
            if (this.character.Area.IntersectsWith(this.model.Gate.Area) && mapID == 0)
            {
                this.ChangeScreen?.Invoke(this, EventArgs.Empty);
                this.SaveGame(this.character);
            }
        }

        /// <summary>
        /// Miner intersects with shop.
        /// </summary>
        /// <returns>Shop name.</returns>
        public string IntersectsWithShop()
        {
            if (this.character.Area.IntersectsWith(this.model.SellShop.Area))
            {
                return "sell";
            }
            else if (this.character.Area.IntersectsWith(this.model.PetrolAndHealthShop.Area))
            {
                return "petrol;Health";
            }
            else if (this.character.Area.IntersectsWith(this.model.PickaxShop.Area))
            {
                return "pickax";
            }

            return "none";
        }

        /// <summary>
        /// Set character position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void SetCharPosition(double x, double y)
        {
            this.character.SetXY(x, y);
        }

        /// <summary>
        /// What can we see in mine.
        /// </summary>
        /// <returns>5x5 Ore matrix.</returns>
        public Ore[,] MapPart()
        {
            Ore[,] renderedOres = new Ore[5, 5];
            int intersectOreX = 2;
            int intersectOreY = 2;

            for (int i = 0; i < this.ore.GetLength(0); i++)
            {
                for (int j = 0; j < this.ore.GetLength(1); j++)
                {
                    if (this.character.Area.IntersectsWith(this.ore[i, j].Area))
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

        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="character">Character what we save.</param>
        public void SaveGame(Character character)
        {
            List<string> mapStringList = new List<string>();
            foreach (var item in this.map)
            {
                switch (item.OreType)
                {
                    case "air":
                        mapStringList.Add("0");
                        break;
                    case "dirt":
                        mapStringList.Add("1");
                        break;
                    case "copper":
                        mapStringList.Add("2");
                        break;
                    case "silver":
                        mapStringList.Add("3");
                        break;
                    case "gold":
                        mapStringList.Add("4");
                        break;
                    case "stone":
                        mapStringList.Add("5");
                        break;
                    case "diamond":
                        mapStringList.Add("6");
                        break;
                    case "lava":
                        mapStringList.Add("7");
                        break;
                    case "ground2":
                        mapStringList.Add("8");
                        break;
                    case "gate":
                        mapStringList.Add("9");
                        break;
                    case "ladder":
                        mapStringList.Add("10");
                        break;
                    case "gate2":
                        mapStringList.Add("11");
                        break;
                    case "gate3":
                        mapStringList.Add("12");
                        break;
                    default:
                        break;
                }
            }

            this.character.Map = mapStringList;
            this.charRepo.SaveGame(character);
        }

        /// <summary>
        /// Load game.
        /// </summary>
        /// <returns>Current character.</returns>
        public Character LoadGame()
        {
            return this.charRepo.StartGame();
        }

        /// <summary>
        /// Mining logic.
        /// </summary>
        /// <param name="d">Which direction to mine.</param>
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

            switch (d)
            {
                case Direction.Left:
                    predictedChar.X -= 7.5;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && item.OreType != "ladder" && this.character.PickAxLevel >= item.Level)
                        {
                            this.character.Backpack.Add(new Ore()
                            {
                                Score = item.Score,
                                OreType = item.OreType,
                                Value = item.Value,
                            });

                            this.character.Score += item.Score;
                            this.Damage(item);
                            item.OreType = this.newAir.OreType;
                            item.CanPass = this.newAir.CanPass;
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
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && item.OreType != "ladder" && this.character.PickAxLevel >= item.Level)
                        {
                                this.character.Backpack.Add(new Ore()
                                {
                                    Score = item.Score,
                                    OreType = item.OreType,
                                    Value = item.Value,
                                });
                                this.character.Score += item.Score;
                                this.Damage(item);
                                item.OreType = this.newAir.OreType;
                                item.CanPass = this.newAir.CanPass;
                                item.Hurt = this.newAir.Hurt;
                                item.BreakLevel = this.newAir.BreakLevel;
                                item.Score = this.newAir.Score;
                                item.Level = this.newAir.Level;
                                item.Value = this.newAir.Value;
                        }
                    }

                    break;
                case Direction.Down:
                    predictedChar.Y += 45;
                    foreach (var item in renderedOres)
                    {
                        if (item.Area.IntersectsWith(predictedChar) && item.OreType != "air" && item.OreType != "ladder" && this.character.PickAxLevel >= item.Level)
                        {
                            this.character.Backpack.Add(new Ore()
                            {
                                Score = item.Score,
                                OreType = item.OreType,
                                Value = item.Value,
                            });
                            this.character.Score += item.Score;
                            this.Damage(item);
                            item.OreType = this.newAir.OreType;
                            item.CanPass = this.newAir.CanPass;
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

        /// <summary>
        /// Drop one ladder.
        /// </summary>
        /// <param name="point">mouse click coordinate.</param>
        /// <param name="mapID">Which map we are on.</param>
        public void DropLadder(Point point, int mapID)
        {
            if (mapID == 1)
            {
                Ore[,] renderedOres = this.MapPart();
                for (int i = 0; i < renderedOres.GetLength(0); i++)
                {
                    for (int j = 0; j < renderedOres.GetLength(1); j++)
                    {
                        if (renderedOres[i, j].OreType == "air"
                            && renderedOres[i, j].Area.Left <= point.X && renderedOres[i, j].Area.Right >= point.X
                            && renderedOres[i, j].Area.Bottom >= point.Y && renderedOres[i, j].Area.Top <= point.Y)
                        {
                            renderedOres[i, j].OreType = this.newLadder.OreType;
                            renderedOres[i, j].CanPass = this.newLadder.CanPass;
                            renderedOres[i, j].Hurt = this.newLadder.Hurt;
                            renderedOres[i, j].BreakLevel = this.newLadder.BreakLevel;
                            renderedOres[i, j].Score = this.newLadder.Score;
                            renderedOres[i, j].Level = this.newLadder.Level;
                            renderedOres[i, j].Value = this.newLadder.Value;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pick up one ladder.
        /// </summary>
        /// <param name="point">mouse click coordinate.</param>
        /// <param name="mapID">Which map we are on.</param>
        public void PickUpLadder(Point point, int mapID)
        {
            if (mapID == 1)
            {
                Ore[,] renderedOres = this.MapPart();
                for (int i = 0; i < renderedOres.GetLength(0); i++)
                {
                    for (int j = 0; j < renderedOres.GetLength(1); j++)
                    {
                        if (renderedOres[i, j].OreType == "ladder"
                            && renderedOres[i, j].Area.Left <= point.X && renderedOres[i, j].Area.Right >= point.X
                            && renderedOres[i, j].Area.Bottom >= point.Y && renderedOres[i, j].Area.Top <= point.Y)
                        {
                            renderedOres[i, j].OreType = this.newAir.OreType;
                            renderedOres[i, j].CanPass = this.newAir.CanPass;
                            renderedOres[i, j].Hurt = this.newAir.Hurt;
                            renderedOres[i, j].BreakLevel = this.newAir.BreakLevel;
                            renderedOres[i, j].Score = this.newAir.Score;
                            renderedOres[i, j].Level = this.newAir.Level;
                            renderedOres[i, j].Value = this.newAir.Value;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Health buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string HealthBuyLogic()
        {
            int maxHealth = 100 - this.character.Health;

            int cost = maxHealth * 2;
            if (this.character.Money - cost >= 0)
            {
                this.character.Money -= cost;
                this.character.Health = 100;
                this.message = "Your health is 100!";
            }
            else
            {
                this.message = "You don't have enough money!";
            }

            return this.message;
        }

        /// <summary>
        /// Petrol buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string PetrolBuyLogic()
        {
            var maxPetrol = 100 - this.character.Fuel;

            int cost = maxPetrol * 2;
            if (this.character.Money - cost >= 0)
            {
                this.character.Money -= cost;
                this.character.Fuel = 100;
                this.message = "Your petrol tank is full!";
            }
            else
            {
                this.message = "You don't have enough money!";
            }

            return this.message;
        }

        /// <summary>
        /// Sell ores logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string SellOreLogic()
        {
            int money = 0;
            if (this.character.Backpack != null)
            {
                foreach (var item in this.character.Backpack)
                {
                    this.character.Money += item.Value;
                    money += item.Value;
                }
            }

            this.character.Backpack.Clear();

            this.message = $"You got {money}$ !";
            return this.message;
        }

        /// <summary>
        /// Pickax buy logic.
        /// </summary>
        /// <returns>Purchase success rate.</returns>
        public string PickaxBuyLogic()
        {
            List<Pickax> pickaxes = this.shopRepo.PickaxList();
            int currentPickaxLevel = this.character.PickAxLevel;

            foreach (var item in pickaxes)
            {
                if (item.Level == currentPickaxLevel + 1)
                {
                    if (this.character.Money - item.Price >= 0)
                    {
                        this.character.Money -= item.Price;
                        this.character.PickAxLevel = item.Level;
                        this.message = $"Your pickax level is {item.Level}!";
                        break;
                    }
                    else if (this.character.Money - item.Price <= 0)
                    {
                        this.message = "You don't have enough money!";
                        break;
                    }
                }
                else if (currentPickaxLevel == 4)
                {
                    this.message = "You have the highest level of pickax!";
                    break;
                }
            }

            return this.message;
        }

        /// <summary>
        /// End game logic.
        /// </summary>
        public void EndGame()
        {
            if (this.character.Health <= 0 || this.character.Fuel <= 0)
            {
                this.charRepo.DeleteProfile(this.character);
                this.EndGameEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get hurt from lava.
        /// </summary>
        /// <param name="ore">lava.</param>
        public void Damage(Ore ore)
        {
            if (ore.OreType == "lava")
            {
                this.character.Health -= 20;
            }
        }

        /// <summary>
        /// Get hurt from a fall.
        /// </summary>
        /// <param name="counter">How many pixel we fall.</param>
        public void DeadFall(int counter)
        {
            switch (counter)
            {
                case 135:
                    this.character.Health -= 20;
                    break;
                case 180:
                    this.character.Health -= 40;
                    break;
                case 225:
                    this.character.Health -= 80;
                    break;
                case >=270:
                    this.character.Health -= 100;
                    break;
            }
        }

        /// <summary>
        /// Where can we click logic.
        /// </summary>
        /// <param name="point">click coordinate.</param>
        /// <param name="mapID">which map.</param>
        /// <param name="shop">which shop.</param>
        public void Click(Point point, int mapID, string shop)
        {
            if (mapID == 0)
            {
                if (shop == "petrol;Health")
                {
                    if (this.model.HealthButtonShape.Area.Left <= point.X && this.model.HealthButtonShape.Area.Right >= point.X
                   && this.model.HealthButtonShape.Area.Bottom >= point.Y && this.model.HealthButtonShape.Area.Top <= point.Y)
                    {
                        this.HealthBuyLogic();
                    }
                    else if (this.model.PetrolButtonShape.Area.Left <= point.X && this.model.PetrolButtonShape.Area.Right >= point.X
                   && this.model.PetrolButtonShape.Area.Bottom >= point.Y && this.model.PetrolButtonShape.Area.Top <= point.Y)
                    {
                        this.PetrolBuyLogic();
                    }
                }
                else if (shop == "sell"
                   && this.model.ButtonShape.Area.Left <= point.X && this.model.ButtonShape.Area.Right >= point.X
                   && this.model.ButtonShape.Area.Bottom >= point.Y && this.model.ButtonShape.Area.Top <= point.Y)
                {
                    this.SellOreLogic();
                }
                else if (shop == "pickax"
                   && this.model.ButtonShape.Area.Left <= point.X && this.model.ButtonShape.Area.Right >= point.X
                   && this.model.ButtonShape.Area.Bottom >= point.Y && this.model.ButtonShape.Area.Top <= point.Y)
                {
                    this.PickaxBuyLogic();
                }

                this.SaveGame(this.character);
            }
            else if (mapID == 2)
            {
                if (this.model.EndGameButton.Area.Left <= point.X && this.model.EndGameButton.Area.Right >= point.X
                   && this.model.EndGameButton.Area.Bottom >= point.Y && this.model.EndGameButton.Area.Top <= point.Y)
                {
                    this.BackToMainMenuEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
