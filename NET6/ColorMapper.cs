using System;
using System.Collections.Generic;
using System.Text;

namespace BotNetCore
{
    public static class ColorMapper
    {
        public static Dictionary<string, string> ColorMap = new Dictionary<string, string>()
        {
                {"darkred",        "#6D001A"},
                {"red",            "#BE0039"},
                {"lightred",       "#FF4500"},
                {"orange",         "#FFA800"},
                {"yellow",         "#FFD635"},
                {"paleyellow",     "#FFF8B8"},
                {"darkgreen",      "#00A368"},
                {"green",          "#00CC78"},
                {"lightgreen",     "#7EED56"},
                {"darkteal",       "#00756F"},
                {"teal",           "#009EAA"},
                {"lightteal",      "#00CCC0"},
                {"darkblue",       "#2450A4"},
                {"blue",           "#3690EA"},
                {"lightblue",      "#51E9F4"},
                {"indigo",         "#493AC1"},
                {"periwinkle",     "#6A5CFF"},
                {"lavender",       "#94B3FF"},
                {"darkpurple",     "#811E9F"},
                {"purple",         "#B44AC0"},
                {"palepurple",     "#E4ABFF"},
                {"magenta",        "#DE107F"},
                {"pink",           "#FF3881"},
                {"lightpink",      "#FF99AA"},
                {"darkbrown",      "#6D482F"},
                {"brown",          "#9C6926"},
                {"beige",          "#FFB470"},
                {"black",          "#000000"},
                {"darkgray",       "#515252"},
                {"gray",           "#898D90"},
                {"lightgray",      "#D4D7D9"},
                {"white",          "#FFFFFF"},
        };

        public static bool IsValidColor(string value)
        {
            return ColorMap.ContainsKey(value.ToLower());
        }

        public static bool TryParse(string s, out string result)
        {
            result = TryGetColor(s.ToLower());

            return result != null;
        }

        public static string TryGetColor(string value)
        {
            if (ColorMap.ContainsKey(value.ToLower())) 
                return ColorMap[value.ToLower()];


            return null;
        }
    }
}
