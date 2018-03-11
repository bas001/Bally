using System.Collections.Generic;
using UnityEngine;

public class ColorWrapper
{

    private static readonly Dictionary<string, Color> colorDict;

    private static readonly Dictionary<string, Color> colorDict2  = new Dictionary<string, Color>
        {
            {"blue", Color.blue},
            {"red", Color.red},
            {"yellow", Color.yellow},
            {"green", Color.green},
            {"grey", Color.grey}
        };

    private static readonly Color BLUE;
    private static readonly Color RED;
    private static readonly Color YELLOW;
    private static readonly Color GREEN;



    static ColorWrapper()
    {
        float h, s, v;

        Color.RGBToHSV(Color.blue, out h, out s, out v);
        BLUE = Color.HSVToRGB(h, s, v - 0.3f);

        Color.RGBToHSV(Color.red, out h, out s, out v);
        RED = Color.HSVToRGB(h, s, v - 0.3f);

        Color.RGBToHSV(Color.yellow, out h, out s, out v);
        YELLOW = Color.HSVToRGB(h, s, v - 0.3f);

        Color.RGBToHSV(Color.green, out h, out s, out v);
        GREEN = Color.HSVToRGB(h, s, v - 0.3f);

        colorDict = new Dictionary<string, Color>
        {
            {"blue", BLUE},
            {"red", RED},
            {"yellow", YELLOW},
            {"green", GREEN},
            {"grey", Color.grey}
        };
    }
    public static Color Get(string color)
    {
        return colorDict[color];
    }

    public static Color Brighter(string color)
    {

        return colorDict2[color];

    }
}
