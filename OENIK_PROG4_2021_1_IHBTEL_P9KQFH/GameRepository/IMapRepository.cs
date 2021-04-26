namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModelDll;

    public interface IMapRepository
    {
        public List<Ore> StringToOreList(Character character);

        /// <summary>
        /// Make air.
        /// </summary>
        /// <returns>Air.</returns>
        public Ore MakeAir();

        /// <summary>
        /// Make ladder.
        /// </summary>
        /// <returns>Ladder.</returns>
        public Ore MakeLadder();

        /// <summary>
        /// Make random Map.
        /// </summary>
        /// <returns>Random map.</returns>
        List<string> RandomMap();
    }
}
