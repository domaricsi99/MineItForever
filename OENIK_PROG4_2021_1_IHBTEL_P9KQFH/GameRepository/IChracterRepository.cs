// <copyright file="IChracterRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using GameModelDll;

    public interface IChracterRepository
    {
        public Character LoadGame(string name);

        public void SaveGame(Character character);
    }
}