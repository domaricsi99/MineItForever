// <copyright file="GameLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogicTest
{
    using GameLogicDll;
    using GameModelDll;
    using GameRepository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Game logic test class.
    /// </summary>
    [TestFixture]
    class GameLogicTest
    {
        GameLogic logic;
        MapRepository mapRepo;
        CharacterRepository charRepo;
        GameModel model;
        Ore[,] matrix;

        /// <summary>
        /// Init mock logic.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.charRepo = new CharacterRepository();
            this.mapRepo = new MapRepository();
            this.logic = new GameLogic(this.mapRepo, this.charRepo);
            this.model = new GameModel();
            this.matrix = new Ore[5, 5];
        }

        [Test]
        public void Movement()
        {
            Direction direction = Direction.Left;
            var value = this.logic.Movement(direction);

            Assert.AreEqual(true, value);
        }

        [Test]
        public void JumpWhileFalling()
        {
            bool value = this.logic.CanJumpMethod();

            Assert.AreEqual(true, value);
        }
    }
}
