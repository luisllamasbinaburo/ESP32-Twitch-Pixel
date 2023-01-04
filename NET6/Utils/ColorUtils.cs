using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ESP32_Twitch_Pixel.Utils
{
    public static class ColorUtils
    {
        public static Color Hex_To_Color(string hex)
        {
           return (Color)new ColorConverter().ConvertFromString(hex);
        }

        public static UInt16 Hex_to_RGB565(string hex)
        {
            var color = Hex_To_Color(hex);
            return RGB888_to_RGB565(color);
        }

        public static UInt16 RGB888_to_RGB565(Color color)
        {
            byte red = color.R;
            byte green = color.G;
            byte blue = color.B;

            var r = red >> 3;
            var g = green >> 2;
            var b = blue >> 3;

            var rgb565 = (ushort)((r << 11) | (g << 5) | b);
            return rgb565;
        }
    }
}