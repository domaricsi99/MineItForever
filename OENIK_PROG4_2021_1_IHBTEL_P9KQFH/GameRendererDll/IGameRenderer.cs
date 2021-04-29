// <copyright file="IGameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRendererDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Game renderer interface.
    /// </summary>
    internal interface IGameRenderer
    {
        /// <summary>
        /// Create rectange here due to optimisation.
        /// </summary>
        /// <param name="oreX">x.</param>
        /// <param name="oreY">y.</param>
        /// <returns>rectangle.</returns>
        public RectangleGeometry RectangleG(double oreX, double oreY);

        /// <summary>
        /// Draw the models.
        /// </summary>
        /// <param name="ctx">DrawingContext.</param>
        /// <param name="mapID">which map.</param>
        /// <param name="intersectShop">which shop we are intersect.</param>
        /// <param name="k">which key was pressed.</param>
        public void Draw(DrawingContext ctx, int mapID, string intersectShop, Key k);

        /// <summary>
        /// Draw score to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Score in text.</returns>
        public FormattedText DrawScoreText(DrawingContext ctx, int mapID);

        /// <summary>
        /// Draw health to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Health in text.</returns>
        public FormattedText DrawHealthText(DrawingContext ctx, int mapID);

        /// <summary>
        /// Draw petrol to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Petrol in text.</returns>
        public FormattedText DrawPetrolText(DrawingContext ctx, int mapID);

        /// <summary>
        /// Draw money to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Money in text.</returns>
        public FormattedText DrawMoneyText(DrawingContext ctx, int mapID);

        /// <summary>
        /// Draw shop text to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="intersectShop">which shop.</param>
        /// <returns>Shop in text.</returns>
        public FormattedText ShopText(DrawingContext ctx, string intersectShop);

        /// <summary>
        /// Draw health price to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>Health price in text.</returns>
        public FormattedText HealthPriceShopText(DrawingContext ctx);

        /// <summary>
        /// Draw petrol price to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>Petrol price in text.</returns>
        public FormattedText PetrolPriceShopText(DrawingContext ctx);

        /// <summary>
        /// Draw end game text to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>End game in text.</returns>
        public FormattedText EndGameText(DrawingContext ctx);
    }
}
