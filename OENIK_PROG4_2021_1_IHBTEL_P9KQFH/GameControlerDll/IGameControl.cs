// <copyright file="IGameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameControlerDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Game control interface.
    /// </summary>
    public interface IGameControl
    {
        /// <summary>
        /// Game contorl loaded interface.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void GameControl_Loaded(object sender, RoutedEventArgs e);

        /// <summary>
        /// Mouse right click event.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void GameControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e);

        /// <summary>
        /// Mouse left click event.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void GameControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e);

        /// <summary>
        /// Tick timer.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void TickTimer_Tick(object sender, EventArgs e);

        /// <summary>
        /// Wich button to press.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void Win_KeyDown(object sender, KeyEventArgs e);
    }
}
