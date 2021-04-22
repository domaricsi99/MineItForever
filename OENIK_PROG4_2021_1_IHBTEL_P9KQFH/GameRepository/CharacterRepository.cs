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

        /// <summary>
        /// Character name equals file name.
        /// </summary>
        /// <param name="name"></param>
        public Character StartGame()
        {
            XDocument character = XDocument.Load("selectedProfile.xml");
            Character selectedChar = new Character()
            {
                Name = character.Root.Element("Name").Value,
                Health = int.Parse(character.Root.Element("Health").Value),
                Fuel = int.Parse(character.Root.Element("Fuel").Value),
                Score = int.Parse(character.Root.Element("Score").Value),
                Money = int.Parse(character.Root.Element("Money").Value),
                PickAxLevel = int.Parse(character.Root.Element("PickLevel").Value),
                Area = new System.Windows.Rect(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight),
                Map = character.Root.Element("Map").Value.Split(';').ToList(),
                Backpack = new List<Ore>(),
            };
            return selectedChar;
        }

        public void SaveGame(Character character)
        {
            string mapString = mapToXml(character);
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

            selProfile.Save("Profiles.xml");
        }

        public void LoadSelectedProfile(Character selectedChar)
        {
            string mapString = mapToXml(selectedChar);
            new XDocument(
                    new XElement("Character",
                        new XElement("Name", selectedChar.Name),
                        new XElement("PickLevel", selectedChar.PickAxLevel),
                        new XElement("Fuel", selectedChar.Fuel),
                        new XElement("Health", selectedChar.Health),
                        new XElement("Score", selectedChar.Score),
                        new XElement("Money", selectedChar.Money),
                        new XElement("Map", mapString))).Save("selectedProfile.xml");
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
            };

            new XDocument(
                new XElement("Character",
                    new XElement("Name", character.Name),
                    new XElement("PickLevel", character.PickAxLevel),
                    new XElement("Fuel", character.Fuel),
                    new XElement("Health", character.Health),
                    new XElement("Score", character.Score),
                    new XElement("Money", character.Money),
                    new XElement("Map", mapString))).Save("selectedProfile.xml");

            xdoc.Root.Add(new XElement("Character",
                    new XElement("Name", character.Name),
                    new XElement("PickLevel", character.PickAxLevel),
                    new XElement("Fuel", character.Fuel),
                    new XElement("Health", character.Health),
                    new XElement("Score", character.Score),
                    new XElement("Money", character.Money),
                    new XElement("Map", mapString)));
            xdoc.Save("Profiles.xml");
            // SaveGame(character);
        }
    }
}
