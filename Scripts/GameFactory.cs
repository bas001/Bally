using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{

    private static List<GameObject> walls = new List<GameObject>();
    private static float ballScale;
    private static Dictionary<int, KeyValuePair<string, Color>> colorDictionary;

    public static List<GameObject> GetWalls()
    {
        return walls;
    }
    public static float GetBallScale()
    {
        return ballScale;
    }
    public static KeyValuePair<string, Color> GetColor(int key)
    {
        return colorDictionary[key];
    }


    public static void Init()
    {
        int width = Screen.width;
        int height = Screen.height;

        int wallScaleThickness = height / 4;
        ballScale = height / 25;

        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height / 2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);

        var player = BallFactory.CreatePlayer(new Vector2(width / 2, height / 2), Color.black);
        player.AddComponent<PlayerControll>();

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

        walls.Add(rightWall);
        walls.Add(downWall);
        walls.Add(upWall);
        walls.Add(leftWall);


        colorDictionary = ColorDictionary(); 
    
    }

    private static Dictionary<int, KeyValuePair<string, Color>> ColorDictionary()
    {
        var dict = new Dictionary<int, KeyValuePair<string, Color>>();
        dict.Add(1, new KeyValuePair<string, Color>("blue", Color.blue));
        dict.Add(2, new KeyValuePair<string, Color>("red", Color.red));
        dict.Add(3, new KeyValuePair<string, Color>("yellow", Color.yellow));
        dict.Add(4, new KeyValuePair<string, Color>("green", Color.green));
        return dict;
    }


}
