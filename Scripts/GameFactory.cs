using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{

    private static float ballScale;
    public static float GetBallScale()
    {
        return ballScale;
    }

    private static System.Random rnd = new System.Random();
    private static Dictionary<int, KeyValuePair<string, Color>> colorDictionary = new Dictionary<int, KeyValuePair<string, Color>>
        {
            { 1, new KeyValuePair<string, Color>("blue", Color.blue) },
            { 2, new KeyValuePair<string, Color>("red", Color.red) },
            { 3, new KeyValuePair<string, Color>("yellow", Color.yellow) },
            { 4, new KeyValuePair<string, Color>("green", Color.green) }
        };

    public static KeyValuePair<string, Color> GetRandomColor()
    {
        return colorDictionary[rnd.Next(1, 5)];
    }

    public static Vector2 NextRandomPosition()
    {
        return new Vector2(GameFactory.NextRandomPosition(v2 => v2.x), GameFactory.NextRandomPosition(v2 => v2.y));
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

        var walls = new List<GameObject>();
        walls.Add(rightWall);
        walls.Add(downWall);
        walls.Add(upWall);
        walls.Add(leftWall);

        MinMaxVector.FindMinMax(RetrievePoints(walls), BallFactory.BALL_RADIUS * ballScale);

    }


    public static float NextRandomPosition(Func<Vector2, float> GetPart)
    {
        int next = rnd.Next((int)(GetPart(MinMaxVector.min) * 100), (int)(GetPart(MinMaxVector.max) * 100));
        return (float)next / 100;
    }

    private static List<Vector2> RetrievePoints(List<GameObject> walls)
    {
        List<Vector2> points = new List<Vector2>();
        foreach (var wall in walls)
        {
            Vector3 vector3 = wall.GetComponent<Transform>().position;
            points.Add(new Vector2(vector3.x, vector3.y));
        }

        return points;
    }

    private static class MinMaxVector
    {
        public static Vector2 min;
        public static Vector2 max;

        public static void FindMinMax(List<Vector2> points, float ballRadius)
        {
            float minY = float.MaxValue;
            float minX = float.MaxValue;
            float maxY = float.MinValue;
            float maxX = float.MinValue;

            foreach (var point in points)
            {
                if (point.y < minY)
                {
                    minY = point.y;
                }
                if (point.x < minX)
                {
                    minX = point.x;
                }
                if (point.y > maxY)
                {
                    maxY = point.y;
                }
                if (point.x > maxX)
                {
                    maxX = point.x;
                }

            }

            min = new Vector2(minX + ballRadius, minY + ballRadius);
            max = new Vector2(maxX - ballRadius, maxY - ballRadius);
        }

    }

}
