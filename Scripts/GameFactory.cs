using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{
    private static int width;
    private static int height;
    private static List<GameObject> walls = new List<GameObject>();
    private static System.Random rnd = new System.Random();

    private static Dictionary<string, ColorWrapper> colorDict;
    public static Dictionary<string, ColorWrapper> ColorDict
    {
        get
        {
            return colorDict;
        }
    }

    private static readonly Dictionary<int, string> colorMapping = new Dictionary<int, string>
        {
            {1, "blue"},
            {2, "red"},
            {3, "yellow"},
            {4, "green"}
        };

    public static void Init()
    {
        width = Screen.width;
        height = Screen.height;

        int wallScaleThickness = height / 4;

        GameConstants.Init(height);
        InitColors();

        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height / 2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);

        BallFactory.CreatePlayer(new Vector2(width / 2, height / 2));

        var rightWall = GameObject.Find("rightWall");
        rightWall.gameObject.tag = "grey";
        rightWall.GetComponent<SpriteRenderer>().color = Color.grey;
        rightWall.gameObject.transform.position = new Vector3(width, height / 2);
        rightWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        var downWall = GameObject.Find("downWall");
        rightWall.gameObject.tag = "grey";
        downWall.GetComponent<SpriteRenderer>().color = Color.grey;
        downWall.gameObject.transform.position = new Vector3(width / 2, 0);
        downWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var upWall = GameObject.Find("upWall");
        rightWall.gameObject.tag = "grey";
        upWall.GetComponent<SpriteRenderer>().color = Color.grey;
        upWall.gameObject.transform.position = new Vector3(width / 2, height);
        upWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var leftWall = GameObject.Find("leftWall");
        rightWall.gameObject.tag = "grey";
        leftWall.GetComponent<SpriteRenderer>().color = Color.grey;
        leftWall.gameObject.transform.position = new Vector3(0, height / 2, 0);
        leftWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        walls.Add(rightWall);
        walls.Add(upWall);
        walls.Add(leftWall);
        walls.Add(downWall);

        var canvas = GameObject.Find("Canvas");
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector3(width, height);
        canvas.GetComponent<RectTransform>().position = new Vector3(width / 2, height / 2);

    }

    public static string NextRandomColor()
    {
        return colorMapping[rnd.Next(1, 5)];
    }

    public static Vector2 NextRandomPosition()
    {
        return new Vector2(NextRandomPosition(0, width), NextRandomPosition(0, height));
    }

    public static void ChangeWallColor(string color)
    {
        foreach (var wall in walls)
        {
            wall.gameObject.tag = color;
            wall.GetComponent<SpriteRenderer>().color = ColorDict[color].bright;
        }
        State.WallColor = color;
    }

    private static void InitColors()
    {
        float h, s, v;

        Color.RGBToHSV(Color.blue, out h, out s, out v);
        var blue = Color.HSVToRGB(h, s, v - 0.3f);

        Color.RGBToHSV(Color.red, out h, out s, out v);
        var red = Color.HSVToRGB(h, s, v - 0.4f);

        Color.RGBToHSV(Color.yellow, out h, out s, out v);
        var yellow = Color.HSVToRGB(h, s, v - 0.4f);

        Color.RGBToHSV(Color.green, out h, out s, out v);
        var green = Color.HSVToRGB(h, s, v - 0.4f);

        Color.RGBToHSV(Color.grey, out h, out s, out v);
        var grey = Color.HSVToRGB(h, s, v - 0.4f);

        Color.RGBToHSV(Color.black, out h, out s, out v);
        var brightBlack = Color.HSVToRGB(h, s, v + 0.3f);

        colorDict = new Dictionary<string, ColorWrapper>
        {
            {"blue", new ColorWrapper(name: "blue", dark: blue, bright: Color.blue) },
            {"red", new ColorWrapper(name: "red", dark: red, bright: Color.red) },
            {"yellow", new ColorWrapper(name: "yellow", dark: yellow, bright: Color.yellow) },
            {"green", new ColorWrapper(name: "green", dark: green, bright: Color.green) },
            {"grey", new ColorWrapper(name: "grey", dark: grey, bright: Color.grey) },
            {GameConstants.PLAYER_TAG, new ColorWrapper(name: GameConstants.PLAYER_TAG, dark: Color.black, bright: brightBlack) }
        };
    }

    private static float NextRandomPosition(float min, float max)
    {
        int next = rnd.Next((int)(min * 100), (int)(max * 100));
        return (float)next / 100;
    }

}
