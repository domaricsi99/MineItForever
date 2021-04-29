// <copyright file="GameLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogicTest
{
    using System.Collections.Generic;
    using System.Linq;
    using GameLogicDll;
    using GameModelDll;
    using GameRepository;
    using NUnit.Framework;

    /// <summary>
    /// Game logic test class.
    /// </summary>
    [TestFixture]
    class GameLogicTest
    {
        MapRepository mapRepo;
        CharacterRepository charRepo;
        GameModel model;

        /// <summary>
        /// Init mock logic.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.charRepo = new CharacterRepository();
            this.mapRepo = new MapRepository();
            this.model = new GameModel();
        }

        [Test]
        public void CreateCharacter()
        {
            this.charRepo.NewCharacter("newTestCharacter");

            Character testChar = this.charRepo.StartGame();

            Assert.AreEqual("newTestCharacter", testChar.Name);
        }

        [Test]
        public void LoadCharacter()
        {
            Character character = this.charRepo.StartGame();

            Assert.AreNotEqual(null, character);
        }

        [Test]
        public void HighscoreXML()
        {
            this.charRepo.NewCharacter("highscoreTestCharacter");
            Character testChar = this.charRepo.StartGame();

            testChar.Score = 6666;

            this.charRepo.DeleteProfile(testChar);
            List<Character> highscores = this.charRepo.LoadHighscore();

            Character highscoreProfile = highscores.First(prof => prof.Name == "highscoreTestCharacter");

            Assert.That(highscoreProfile.Score.Equals(6666));
        }

        [Test]
        public void Movement()
        {
            Character character = this.charRepo.StartGame();

            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(50, 50);

            double charX = character.Area.X;
            movementLogic.MoveCharacter(Direction.Left, 0);

            charX -= 7.5;

            Assert.That(charX.Equals(character.Area.X));
        }

        [Test]
        public void Mining()
        {
            Character character = this.charRepo.StartGame();

            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(412.5, 80);

            movementLogic.MoveCharacter(Direction.Down, 1);

            Assert.NotNull(character.Backpack);
        }

        [Test]
        public void Falling()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(412.5, 60);

            double charposY = character.Area.Y;

            movementLogic.Fall(0);

            charposY += 5;

            Assert.That(charposY.Equals(character.Area.Y));
        }

        [Test]
        public void JumpWhileFalling()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(412.5, 60);

            bool value = movementLogic.CanJumpMethod();

            Assert.AreEqual(false, value);
        }

        [Test]
        public void FallDmg()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            movementLogic.DeadFall(225);

            Assert.That(character.Health.Equals(20));
        }

        [Test]
        public void JumpOnSurface()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(200, 330);

            double charY = character.Area.Y;
            movementLogic.MoveCharacter(Direction.Up, 0);

            charY -= 60;

            Assert.That(charY.Equals(character.Area.Y));
        }

        [Test]
        public void SellItem()
        {
            Character character = this.charRepo.StartGame();
            GameLogic sellLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.Backpack.Add(new Ore()
            {
                Value = 100,
            });
            character.Backpack.Add(new Ore()
            {
                Value = 100,
            });
            character.Backpack.Add(new Ore()
            {
                Value = 100,
            });

            if (character.Backpack.Count != 0)
            {
                sellLogic.SellOreLogic();

                Assert.IsEmpty(character.Backpack);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void BuyPickaxe()
        {
            Character character = this.charRepo.StartGame();
            GameLogic buyLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.Money += 100000;
            character.PickAxLevel = 1;

            int pickLevel = character.PickAxLevel;
            buyLogic.PickaxBuyLogic();

            pickLevel += 1;

            Assert.That(pickLevel.Equals(character.PickAxLevel));
        }

        [Test]
        public void BuyPetrol()
        {
            Character character = this.charRepo.StartGame();
            GameLogic buyLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.Money = 200;
            character.Fuel = 10; // we pay 180 to have max fuel.
            buyLogic.PetrolBuyLogic();
            Assert.That(character.Money.Equals(20));
        }
    }
}
