using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{

    private static int width;
    private static int height;
    private static List<GameObject> walls = new List<GameObject>();

    internal static Color GetColor(string tag)
    {
        return colorDict[tag];
    }

    private static System.Random rnd = new System.Random();
    private static Dictionary<string, Color> colorDict = new Dictionary<string, Color>
    {
        {"blue", Color.blue },
        {"red", Color.red },
        {"yellow", Color.yellow },
        {"green", Color.green },
        {"grey", Color.grey }
    };
    private static Dictionary<int, string> colorMapping = new Dictionary<int, string>
        {
            { 1, "blue"},
            { 2,"red"},
            { 3, "yellow"},
            { 4, "green" }
        };


    public static void Init()
    {
        width = Screen.width;
        height = Screen.height;

        int wallScaleThickness = height / 4;

        GameConstants.Init(height);

        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height / 2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);

        var player = BallFactory.CreatePlayer(new Vector2(width / 2, height / 2), Color.black);

        var rightWall = GameObject.Find("rightWall");
        rightWall.GetComponent<SpriteRenderer>().color = Color.grey;
        rightWall.gameObject.transform.position = new Vector3(width, height / 2);
        rightWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        var downWall = GameObject.Find("downWall");
        downWall.GetComponent<SpriteRenderer>().color = Color.grey;
        downWall.gameObject.transform.position = new Vector3(width / 2, 0);
        downWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var upWall = GameObject.Find("upWall");
        upWall.GetComponent<SpriteRenderer>().color = Color.grey;
        upWall.gameObject.transform.position = new Vector3(width / 2, height);
        upWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var leftWall = GameObject.Find("leftWall");
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

    public static void ChangeWallColor(string color)
    {
        foreach (var wall in walls)
        {
            wall.gameObject.tag = color;
            wall.GetComponent<SpriteRenderer>().color = colorDict[color];
        }
    }

    public static string NextRandomColor()
    {
        return colorMapping[rnd.Next(1, 5)];
    }

    public static Vector2 NextRandomPosition()
    {
        return new Vector2(NextRandomPosition(0, width), NextRandomPosition(0, height));
    }

    private static float NextRandomPosition(float min, float max)
    {
        int next = rnd.Next((int)(min * 100), (int)(max * 100));
        return (float)next / 100;
    }
    
}
