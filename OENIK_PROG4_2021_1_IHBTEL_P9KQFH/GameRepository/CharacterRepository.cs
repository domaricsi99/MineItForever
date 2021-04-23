using GameModelDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameRepository
{
    public class CharacterRepository : IChracterRepository
    {
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
        }

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
                Area = new System.Windows.Rect(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight),
                Map = character.Root.Element("Map").Value.Split(';').ToList(),
                Backpack = oreList,
            };
            return selectedChar;
        }

        public void SaveGame(Character character)
        {
            string mapString = this.mapToXml(character);
            string backpackString = this.backpackToXml(character);
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

        public void LoadSelectedProfile(Character selectedChar)
        {
            string mapString = mapToXml(selectedChar);
            string backpackString = backpackToXml(selectedChar);

            new XDocument(
                    new XElement("Character",
                        new XElement("Name", selectedChar.Name),
                        new XElement("PickLevel", selectedChar.PickAxLevel),
                        new XElement("Fuel", selectedChar.Fuel),
                        new XElement("Health", selectedChar.Health),
                        new XElement("Score", selectedChar.Score),
                        new XElement("Money", selectedChar.Money),
                        new XElement("Map", mapString),
                        new XElement("Backpack", backpackString))).Save("selectedProfile.xml");
        }

        public string mapToXml(Character selectedChar)
        {
            string mapString = string.Empty;
            for (int i = 0; i < selectedChar.Map.Count; i++)
            {
                if (i == selectedChar.Map.Count - 1)
                {
                    mapString += selectedChar.Map[i];
                }
                else
                {
                    mapString += selectedChar.Map[i] + ";";
                }
            }

            return mapString;
        }

        public string backpackToXml(Character character)
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
                            canPass = false,
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
                            canPass = false,
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
                            canPass = false,
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
                            canPass = false,
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
                            canPass = false,
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
                            canPass = false,
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
                            canPass = false,
                        });
                        break;
                    default:
                        break;
                }
            }

            return oreList;
        }

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
                    Area = new System.Windows.Rect(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight),
                    Map = item.Element("Map").Value.Split(';').ToList(),
                    Backpack = new List<Ore>(),
                });
            }

            return profiles;
        }

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

        public void NewCharacter(string name)
        {
            XDocument xdoc = XDocument.Load("Profiles.xml");
            string mapString = XDocument.Load("Map.xml").Element("Map").Value;
            Character character = new Character()
            {
                Name = name,
                PickAxLevel = 0,
                Fuel = 100,
                Health = 100,
                Money = 10,
                Score = 0,
                Map = XDocument.Load("Map.xml").Element("Map").Value.Split(';').ToList(),
                Backpack = new List<Ore>(),
            };

            new XDocument(
                new XElement("Character",
                    new XElement("Name", character.Name),
                    new XElement("PickLevel", character.PickAxLevel),
                    new XElement("Fuel", character.Fuel),
                    new XElement("Health", character.Health),
                    new XElement("Score", character.Score),
                    new XElement("Money", character.Money),
                    new XElement("Map", mapString),
                    new XElement("Backpack", character.Backpack))).Save("selectedProfile.xml");

            xdoc.Root.Add(new XElement("Character",
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
