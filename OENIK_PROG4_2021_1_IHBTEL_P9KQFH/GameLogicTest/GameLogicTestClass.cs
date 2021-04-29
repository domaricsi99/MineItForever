// <copyright file="GameLogicTestClass.cs" company="PlaceholderCompany">
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
    public class GameLogicTestClass
    {
        private MapRepository mapRepo;
        private CharacterRepository charRepo;
        private GameModel model;

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

        /// <summary>
        /// Creates a character and checks if it actually exists.
        /// </summary>
        [Test]
        public void CreateCharacter()
        {
            this.charRepo.NewCharacter("newTestCharacter");

            Character testChar = this.charRepo.StartGame();

            Assert.AreEqual("newTestCharacter", testChar.Name);
        }

        /// <summary>
        /// Loads a new character and checks if it actually exists.
        /// </summary>
        [Test]
        public void LoadCharacter()
        {
            Character character = this.charRepo.StartGame();

            Assert.AreNotEqual(null, character);
        }

        /// <summary>
        /// Checks if highscore XML works properly.
        /// </summary>
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

        /// <summary>
        /// Checks if the character actually changes position.
        /// </summary>
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

        /// <summary>
        /// Tests the mining method.
        /// </summary>
        [Test]
        public void Mining()
        {
            Character character = this.charRepo.StartGame();

            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(412.5, 80);

            movementLogic.MoveCharacter(Direction.Down, 1);

            Assert.NotNull(character.Backpack);
        }

        /// <summary>
        /// Does the character fall if it is in the air?.
        /// </summary>
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

        /// <summary>
        /// Can we jump during a fall?.
        /// </summary>
        [Test]
        public void JumpWhileFalling()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            character.SetXY(412.5, 60);

            bool value = movementLogic.CanJumpMethod();

            Assert.AreEqual(false, value);
        }

        /// <summary>
        /// Checks if fall dmg changes health.
        /// </summary>
        [Test]
        public void FallDmg()
        {
            Character character = this.charRepo.StartGame();
            GameLogic movementLogic = new GameLogic(this.model, this.mapRepo, this.charRepo, character);

            movementLogic.DeadFall(225);

            Assert.That(character.Health.Equals(20));
        }

        /// <summary>
        /// Jumping on surface test.
        /// </summary>
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

        /// <summary>
        /// Selling item actually gets rid of it from the backpack Test.
        /// </summary>
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

        /// <summary>
        /// Buying pickaxe actually increasing pickaxe level.
        /// </summary>
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

        /// <summary>
        /// Filling up petrol actually decrease our money.
        /// </summary>
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
