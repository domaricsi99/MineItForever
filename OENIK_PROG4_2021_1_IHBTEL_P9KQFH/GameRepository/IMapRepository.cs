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
        bool CanJumpMethod(int predictOreX, int predictOreYLeft, int predictOreBottom, int predictOreYRight, int jumpCount);
        public List<Ore> DrawMap();
    }
}
