using UnityEngine;

public struct ColorWrapper
{
    public string name;
    public Color dark;
    public Color bright;

    public ColorWrapper(string name, Color dark, Color bright)
    {
        this.name = name;
        this.dark = dark;
        this.bright = bright;
    }
}
