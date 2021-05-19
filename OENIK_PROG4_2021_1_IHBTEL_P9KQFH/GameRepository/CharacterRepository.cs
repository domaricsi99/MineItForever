// <copyright file="CharacterRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using GameModelDll;

    /// <summary>
    /// Character repository.
    /// </summary>
    public class CharacterRepository : IChracterRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRepository"/> class.
        /// </summary>
        public CharacterRepository()
        {
            try
            {
                XDocument.Load("Profiles.xml");
            }
            catch (FileNotFoundException)
            {
                new XDocument(
                    new XElement("CharacterList")).Save("Profiles.xml");
            }

            try
            {
                XDocument.Load("selectedProfile.xml");
            }
            catch (FileNotFoundException)
            {
                new XDocument(new XElement("Character")).Save("selectedProfile.xml");
            }

            try
            {
                XDocument.Load("Highscore.xml");
            }
            catch (FileNotFoundException)
            {
                new XDocument(new XElement("Character")).Save("Highscore.xml");
            }

            try
            {
                XDocument.Load("Map.xml");
            }
            catch (FileNotFoundException)
            {
                MapRepository newMap = new MapRepository();
                List<string> mapList = newMap.RandomMap();
                string mapString = this.MapToXml(mapList);
                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("Map", mapString));
                xdoc.Save("Map.xml");
            }
        }

        /// <summary>
        /// Start game.
        /// </summary>
        /// <returns>Current Character.</returns>
        public Character StartGame()
        {
            XDocument character = XDocument.Load("selectedProfile.xml");
            List<Ore> oreList = this.ListToOre(character.Root.Element("Backpack").Value.Split(';').ToList());
            Character selectedChar = new ()
            {
                Name = character.Root.Element("Name").Value,
                Health = int.Parse(character.Root.Element("Health").Value),
                Fuel = int.Parse(character.Root.Element("Fuel").Value),
                Score = int.Parse(character.Root.Element("Score").Value),
                Money = int.Parse(character.Root.Element("Money").Value),
                PickAxLevel = int.Parse(character.Root.Element("PickLevel").Value),
                Area = new System.Windows.Rect(Config.Width / 2, Config.Height - (Config.GroundHeight + Config.MinerHeight), Config.MinerWidth, Config.MinerHeight),
                Map = character.Root.Element("Map").Value.Split(';').ToList(),
                Backpack = oreList,
            };
            return selectedChar;
        }

        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="character">Current character.</param>
        public void SaveGame(Character character)
        {
            string mapString = this.MapToXml(character.Map);
            string backpackString = this.BackpackToXml(character);
            XDocument selProfile = XDocument.Load("Profiles.xml");
            selProfile.Root.Elements("Character").Elements("Name")
                .Single(profName => profName.Value == character.Name)
                .Parent.Element("Fuel").Value = character.Fuel.ToString();

            selProfile.Root.Elements("Character").Elements("Name")
                .Single(profName => profName.Value == character.Name)
                .Parent.Element("Health").Value = character.Health.ToString();

            selProfile.Root.Elements("Character").Elements("Name")
                .Single(profName => profName.Value == character.Name)
                .Parent.Element("Score").Value = character.Score.ToString();

            selProfile.Root.Elements("Character").Elements("Name")
                .Single(profName => profName.Value == character.Name)
                .Parent.Element("Money").Value = character.Money.ToString();

            selProfile.Root.Elements("Character").Elements("Name")
                .Single(profName => profName.Value == character.Name)
                .Parent.Element("Map").Value = mapString;

            selProfile.Root.Elements("Character").Elements("Name")
                .SingleOrDefault(profName => profName.Value == character.Name)
                .Parent.Element("Backpack").Value = backpackString;

            selProfile.Save("Profiles.xml");
        }

        /// <summary>
        /// Load selected profile.
        /// </summary>
        /// <param name="selectedChar">selected character.</param>
        public void LoadSelectedProfile(Character selectedChar)
        {
            string mapString = this.MapToXml(selectedChar.Map);
            string backpackString = this.BackpackToXml(selectedChar);

            new XDocument(
                    new XElement(
                        "Character",
                        new XElement("Name", selectedChar.Name),
                        new XElement("PickLevel", selectedChar.PickAxLevel),
                        new XElement("Fuel", selectedChar.Fuel),
                        new XElement("Health", selectedChar.Health),
                        new XElement("Score", selectedChar.Score),
                        new XElement("Money", selectedChar.Money),
                        new XElement("Map", mapString),
                        new XElement("Backpack", backpackString))).Save("selectedProfile.xml");
        }

        /// <summary>
        /// Map to xml.
        /// </summary>
        /// <param name="map"> Current map. </param>
        /// <returns>string map.</returns>
        public string MapToXml(List<string> map)
        {
            string mapString = string.Empty;
            for (int i = 0; i < map.Count; i++)
            {
                if (i == map.Count - 1)
                {
                    mapString += map[i];
                }
                else
                {
                    mapString += map[i] + ";";
                }
            }

            return mapString;
        }

        /// <summary>
        /// Backpack to xml.
        /// </summary>
        /// <param name="character">current character.</param>
        /// <returns>string backpack.</returns>
        public string BackpackToXml(Character character)
        {
            string backpackString = string.Empty;
            for (int i = 0; i < character.Backpack.Count; i++)
            {
                if (i == character.Backpack.Count - 1)
                {
                    backpackString += character.Backpack[i].OreType;
                }
                else
                {
                    backpackString += character.Backpack[i].OreType + ";";
                }
            }

            return backpackString;
        }

        /// <summary>
        /// List to ore.
        /// </summary>
        /// <param name="stringList">string list Ore.</param>
        /// <returns>List Ore.</returns>
        public List<Ore> ListToOre(List<string> stringList)
        {
            List<Ore> oreList = new List<Ore>();
            foreach (var item in stringList)
            {
                switch (item)
                {
                    case "1":
                        oreList.Add(new Ore()
                        {
                            Value = 5,
                            Hurt = false,
                            Score = 20,
                            Level = 0,
                            OreType = "dirt",
                            CanPass = false,
                        });
                        break;
                    case "2":
                        oreList.Add(new Ore()
                        {
                            Value = 10,
                            Hurt = false,
                            Score = 100,
                            Level = 1,
                            OreType = "copper",
                            CanPass = false,
                        });
                        break;
                    case "3":
                        oreList.Add(new Ore()
                        {
                            Value = 20,
                            Hurt = false,
                            Score = 200,
                            Level = 2,
                            OreType = "silver",
                            CanPass = false,
                        });
                        break;
                    case "4":
                        oreList.Add(new Ore()
                        {
                            Value = 40,
                            Hurt = false,
                            Score = 400,
                            Level = 3,
                            OreType = "gold",
                            CanPass = false,
                        });
                        break;
                    case "5":
                        oreList.Add(new Ore()
                        {
                            Value = 1,
                            Hurt = false,
                            Score = 50,
                            Level = 3,
                            OreType = "stone",
                            CanPass = false,
                        });
                        break;
                    case "6":
                        oreList.Add(new Ore()
                        {
                            Value = 100,
                            Hurt = false,
                            Score = 1000,
                            Level = 4,
                            OreType = "diamond",
                            CanPass = false,
                        });
                        break;
                    case "7":
                        oreList.Add(new Ore()
                        {
                            Value = 200,
                            Hurt = true,
                            Score = 2000,
                            Level = 4,
                            OreType = "lava",
                            CanPass = false,
                        });
                        break;
                    default:
                        break;
                }
            }

            return oreList;
        }

        /// <summary>
        /// Load all profile.
        /// </summary>
        /// <returns>List Character.</returns>
        public List<Character> LoadAllProfile()
        {
            List<Character> profiles = new List<Character>();

            XDocument xdoc = XDocument.Load("Profiles.xml");

            List<XElement> xelementProfiles = xdoc.Root.Elements("Character").ToList();

            foreach (var item in xelementProfiles)
            {
                profiles.Add(new Character()
                {
                    Name = item.Element("Name").Value,
                    Health = int.Parse(item.Element("Health").Value),
                    Fuel = int.Parse(item.Element("Fuel").Value),
                    Score = int.Parse(item.Element("Score").Value),
                    Money = int.Parse(item.Element("Money").Value),
                    PickAxLevel = int.Parse(item.Element("PickLevel").Value),
                    Area = new System.Windows.Rect(Config.Width / 2, Config.GroundHeight, Config.MinerWidth, Config.MinerHeight), // new System.Windows.Rect(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight)
                    Map = item.Element("Map").Value.Split(';').ToList(),
                    Backpack = new List<Ore>(),
                });
            }

            return profiles;
        }

        /// <summary>
        /// Deletes a profile from the xml.
        /// </summary>
        /// <param name="character"> profile to be deleted. </param>
        public void DeleteProfile(Character character)
        {
            XDocument selProfile = XDocument.Load("Profiles.xml");
            if (selProfile.Root.Elements("Character").Elements("Name")
                      .SingleOrDefault(profName => profName.Value == character.Name) != null)
            {
                XDocument highscore = XDocument.Load("Highscore.xml");
                highscore.Root.Add(new XElement(
                    "Character",
                    new XElement(
                        "Name",
                        character.Name),
                    new XElement(
                        "Score",
                        character.Score)));

                selProfile.Root.Elements("Character").Elements("Name")
                      .SingleOrDefault(profName => profName.Value == character.Name).Parent.Remove();
                selProfile.Save("Profiles.xml");
                highscore.Save("Highscore.xml");
            }
        }

        /// <summary>
        /// Load Highscore.
        /// </summary>
        /// <returns>List Character.</returns>
        public List<Character> LoadHighscore()
        {
            List<Character> profiles = new List<Character>();

            XDocument xdoc = XDocument.Load("Highscore.xml");

            List<XElement> xelementProfiles = xdoc.Root.Elements("Character").ToList();

            foreach (var item in xelementProfiles)
            {
                profiles.Add(new Character()
                {
                    Name = item.Element("Name").Value,
                    Score = int.Parse(item.Element("Score").Value),
                });
            }

            List<Character> sortedHighscore = profiles.OrderByDescending(x => x.Score).ToList();
            return sortedHighscore;
        }

        /// <summary>
        /// Make new Character.
        /// </summary>
        /// <param name="name">Character name.</param>
        public void NewCharacter(string name)
        {
            MapRepository newMap = new MapRepository();
            XDocument xdoc = XDocument.Load("Profiles.xml");
            List<string> mapList = newMap.RandomMap();
            Character character = new Character()
            {
                Name = name,
                PickAxLevel = 0,
                Fuel = 100,
                Health = 100,
                Money = 10,
                Score = 0,
                Map = mapList,
                Backpack = new List<Ore>(),
            };

            string mapString = this.MapToXml(character.Map);

            new XDocument(
                new XElement(
                    "Character",
                    new XElement("Name", character.Name),
                    new XElement("PickLevel", character.PickAxLevel),
                    new XElement("Fuel", character.Fuel),
                    new XElement("Health", character.Health),
                    new XElement("Score", character.Score),
                    new XElement("Money", character.Money),
                    new XElement("Map", mapString),
                    new XElement("Backpack", character.Backpack))).Save("selectedProfile.xml");

            xdoc.Root.Add(new XElement(
                    "Character",
                    new XElement("Name", character.Name),
                    new XElement("PickLevel", character.PickAxLevel),
                    new XElement("Fuel", character.Fuel),
                    new XElement("Health", character.Health),
                    new XElement("Score", character.Score),
                    new XElement("Money", character.Money),
                    new XElement("Map", mapString),
                    new XElement("Backpack", character.Backpack)));
            xdoc.Save("Profiles.xml");
        }
    }
}
