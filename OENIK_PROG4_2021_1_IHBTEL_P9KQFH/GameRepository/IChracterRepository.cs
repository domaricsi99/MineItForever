// <copyright file="IChracterRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System.Collections.Generic;
    using GameModelDll;

    /// <summary>
    /// Character Repository.
    /// </summary>
    public interface IChracterRepository
    {
        /// <summary>
        /// Start game.
        /// </summary>
        /// <returns>Current Character.</returns>
        public Character StartGame();

        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="character">Current character.</param>
        public void SaveGame(Character character);

        /// <summary>
        /// Load selected profil.
        /// </summary>
        /// <param name="selectedChar">selected character.</param>
        public void LoadSelectedProfile(Character selectedChar);

        /// <summary>
        /// Map to xml.
        /// </summary>
        /// <param name="map"> Current map. </param>
        /// <returns>string map.</returns>
        public string MapToXml(List<string> map);

        /// <summary>
        /// Backpack to xml.
        /// </summary>
        /// <param name="character">current character.</param>
        /// <returns>string backpack.</returns>
        public string BackpackToXml(Character character);

        /// <summary>
        /// List to ore.
        /// </summary>
        /// <param name="stringList">string list Ore.</param>
        /// <returns>List Ore.</returns>
        public List<Ore> ListToOre(List<string> stringList);

        /// <summary>
        /// Load all profile.
        /// </summary>
        /// <returns>List Character.</returns>
        public List<Character> LoadAllProfile();

        /// <summary>
        /// Load Highscore.
        /// </summary>
        /// <returns>List Character.</returns>
        public List<Character> LoadHighscore();

        /// <summary>
        /// Make new Character.
        /// </summary>
        /// <param name="name">Character name.</param>
        public void NewCharacter(string name);

        /// <summary>
        /// Deletes a profile from the xml.
        /// </summary>
        /// <param name="character"> profile to be deleted. </param>
        public void DeleteProfile(Character character);
    }
}