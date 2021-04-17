using GameModelDll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    public class CharacterRepository : IChracterRepository
    {
        public CharacterRepository()
        {

        }
        /// <summary>
        /// Character name equals file name.
        /// </summary>
        /// <param name="name"></param>
        public Character LoadGame(string name)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                string[] fileLines = File.ReadAllLines(name + ".txt");

                string[] map = File.ReadAllLines(name + "Map.txt");

                // 0 - Name, 1 - Health, 2 - Fuel, 3 - PickAxLevel, 4 - Money, 5 - Score

                Character character = new Character(
                    int.Parse(fileLines[1]),
                    int.Parse(fileLines[2]),
                    int.Parse(fileLines[4]),
                    int.Parse(fileLines[5]),
                    int.Parse(fileLines[3]),
                    map, fileLines[0],
                    Config.Width / 2,
                    Config.Height / 2,
                    Config.MinerWidth,
                    Config.MinerHeight
                    );

                character.Backpack = new List<Ore>();
                return character;
            }
        }

        public bool SaveGame (Character character)
        {

            if (character.Name == null)
            {
                return false;
            }
            else
            {
                // 0 - Name, 1 - Health, 2 - Fuel, 3 - PickAxLevel, 4 - Money, 5 - Score
                string[] profile = new string[6];
                profile[0] = character.Name;
                profile[1] = character.Health.ToString();
                profile[2] = character.Fuel.ToString();
                profile[3] = character.PickAxLevel.ToString();
                profile[4] = character.Money.ToString();
                profile[5] = character.Score.ToString();

                // MAYBE character.Name + ".txt"
                File.WriteAllLines(character.Name, profile);

                //MAP
                // MAYBE character.Name + "Map" + ".txt"
                File.WriteAllLines(character.Name + "Map", character.Map);
                return true;
            }
        }

        public void NewCharacter (string name)
        {
            Character character = new Character()
            {
                Name = name,
                PickAxLevel = 0,
                Fuel = 100,
                Health = 100,
                Money = 10,
                Score = 0,
                Map = File.ReadAllLines("palya.txt"),
            };
            SaveGame(character);
        }
    }
}
