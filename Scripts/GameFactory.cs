using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{

    private static int width;
    private static int height;
    private static System.Random rnd = new System.Random();
    private static Dictionary<int, KeyValuePair<string, Color>> colorDictionary = new Dictionary<int, KeyValuePair<string, Color>>
        {
            { 1, new KeyValuePair<string, Color>("blue", Color.blue) },
            { 2, new KeyValuePair<string, Color>("red", Color.red) },
            { 3, new KeyValuePair<string, Color>("yellow", Color.yellow) },
            { 4, new KeyValuePair<string, Color>("green", Color.green) }
        };

    public static void Init()
    {
        width = Screen.width;
        height = Screen.height;

        int wallScaleThickness = height / 4;
        var ballScale = height / 10;

        GameConstants.BallScale = ballScale;
        GameConstants.MaxSpeed = height * 10;
        GameConstants.MinSpeed = height / 100;

        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height / 2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);

        var player = BallFactory.CreatePlayer(new Vector2(width / 2, height / 2), Color.black);

        var rightWall = GameObject.Find("rightWall");
        rightWall.GetComponent<SpriteRenderer>().color = Color.red;
        rightWall.gameObject.tag = "red";
        rightWall.gameObject.transform.position = new Vector3(width, height / 2);
        rightWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        var downWall = GameObject.Find("downWall");
        downWall.GetComponent<SpriteRenderer>().color = Color.green;
        downWall.gameObject.tag = "green";
        downWall.gameObject.transform.position = new Vector3(width / 2, 0);
        downWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var upWall = GameObject.Find("upWall");
        upWall.GetComponent<SpriteRenderer>().color = Color.yellow;
        upWall.gameObject.tag = "yellow";
        upWall.gameObject.transform.position = new Vector3(width / 2, height);
        upWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        var leftWall = GameObject.Find("leftWall");
        leftWall.GetComponent<SpriteRenderer>().color = Color.blue;
        leftWall.gameObject.tag = "blue";
        leftWall.gameObject.transform.position = new Vector3(0, height / 2, 0);
        leftWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        var canvas = GameObject.Find("Canvas");
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector3(width, height);
        canvas.GetComponent<RectTransform>().position = new Vector3(width / 2, height / 2);

    }

    public static KeyValuePair<string, Color> GetRandomColor()
    {
        return colorDictionary[rnd.Next(1, 5)];
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
