using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace GameRendererDll
{
    interface IGameRenderer
    {
        public RectangleGeometry RectangleG(double oreX, double oreY);

        public void Draw(DrawingContext ctx, int mapID, string intersectShop, Key k);

        public FormattedText DrawScoreText(DrawingContext ctx, int mapID);

        public FormattedText DrawHealthText(DrawingContext ctx, int mapID);

        public FormattedText DrawPetrolText(DrawingContext ctx, int mapID);

        public FormattedText DrawMoneyText(DrawingContext ctx, int mapID);

        public FormattedText ShopText(DrawingContext ctx, string intersectShop);

        public FormattedText HealthPriceShopText(DrawingContext ctx);

        public FormattedText PetrolPriceShopText(DrawingContext ctx);

        public FormattedText ReturnShopText(DrawingContext ctx);

        public FormattedText EndGameText(DrawingContext ctx);
    }
}
