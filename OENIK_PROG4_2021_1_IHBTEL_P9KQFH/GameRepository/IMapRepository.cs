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
        public List<Ore> DrawMap(Character character);
    }
}
