// <copyright file="IChracterRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using GameModelDll;
    using System.Collections.Generic;

    public interface IChracterRepository
    {
        public Character StartGame();

        public void SaveGame(Character character);

        public void LoadSelectedProfile(Character selectedChar);

        public string mapToXml(Character selectedChar);

        public string backpackToXml(Character character);

        public List<Ore> ListToOre(List<string> stringList);

        public List<Character> LoadAllProfile();

        public List<Character> LoadHighscore();

        public void NewCharacter(string name);
    }
}