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

    public interface IGameControl
    {
        public void GameControl_Loaded(object sender, RoutedEventArgs e);

        public void TickTimer_Tick(object sender, EventArgs e);

        public void Win_KeyDown(object sender, KeyEventArgs e);
    }
}
