﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;

namespace Paint_AvaloniaUI.Models.Extensions
{
    public static class PointerPressedExtensions
    {
        public static Point GetPositionRelative(this PointerEventArgs e)
        {
            try
            {
                return e.GetPosition((Visual)e.Source!);
            }
            catch 
            { }

            return default;
        }
    }
}
