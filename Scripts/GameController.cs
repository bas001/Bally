using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static readonly int NEXT_BALL_TIMEOUT = 2000;
    private static readonly float CIRCLE_RADIUS = 0.5f;

    private static bool isAnyBallInMotion = false;

    private System.Random rnd = new System.Random();
    private Stopwatch sw = new Stopwatch();
    private bool playing = true;

    private float ballScale;
    private GameObject rightWall;
    private GameObject downWall;
    private GameObject upWall;
    private GameObject leftWall;
    private List<GameObject> walls = new List<GameObject>();


    public static void SetIsAnyBallInMotion(bool value)
    {
        isAnyBallInMotion = value;
    }

    // Use this for initialization
    void Start () {
        GameObject.Find("Player").SetActive(false);
        InstantiateGameObjects();
        MinMaxVector.FindMinMax(RetrievePoints(), CIRCLE_RADIUS * ballScale);
        sw.Start();
    }

    private List<Vector2> RetrievePoints()
    {
        List<Vector2> points = new List<Vector2>();
        foreach (var wall in walls)
        {
            Vector3 vector3 = wall.GetComponent<Transform>().position;
            points.Add(new Vector2(vector3.x, vector3.y));
        }

        return points;
    }

    private void InstantiateGameObjects()
    {
        int width = Screen.width;
        int height = Screen.height;

        int wallScaleThickness = height / 4;

        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height/2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);


        rightWall = GameObject.Find("rightWall");
        rightWall.GetComponent<SpriteRenderer>().color = Color.red;
        rightWall.gameObject.tag = "red";
        rightWall.gameObject.transform.position = new Vector3(width, height / 2);
        rightWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        downWall = GameObject.Find("downWall");
        downWall.GetComponent<SpriteRenderer>().color = Color.green;
        downWall.gameObject.tag = "green";
        downWall.gameObject.transform.position = new Vector3(width /2, 0);
        downWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        upWall = GameObject.Find("upWall");
        upWall.GetComponent<SpriteRenderer>().color = Color.yellow;
        upWall.gameObject.tag = "yellow";
        upWall.gameObject.transform.position = new Vector3(width / 2, height);
        upWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, width);

        leftWall = GameObject.Find("leftWall");
        leftWall.GetComponent<SpriteRenderer>().color = Color.blue;
        leftWall.gameObject.tag = "blue";
        leftWall.gameObject.transform.position = new Vector3(0, height / 2, 0);
        leftWall.gameObject.transform.localScale = new Vector3(wallScaleThickness, height);

        walls.Add(rightWall);
        walls.Add(downWall);
        walls.Add(upWall);
        walls.Add(leftWall);

       
        ballScale = height / 25;
        

    }

    // Update is called after the Update of BallController
    void Update()
    {
        if (playing)
        {
            if (!isAnyBallInMotion && sw.ElapsedMilliseconds > NEXT_BALL_TIMEOUT)
            {
                InstantiateRandomBall();
                sw.Reset();
                sw.Start();
            }
        }

        // set to true by BallController
        isAnyBallInMotion = false;
    }

    private void InstantiateRandomBall()
    {
        Vector2 nextPosition;
        bool colliding = true;
        int counter = 0;
        do
        {
            nextPosition = new Vector2(NextRandomPosition(v2 => v2.x), NextRandomPosition(v2 => v2.y));
            colliding = Physics2D.OverlapCircle(nextPosition, CIRCLE_RADIUS * ballScale);
            counter++;
        } while (colliding && counter != MAX_NUMBER_OF_TRYS);

        if(counter == MAX_NUMBER_OF_TRYS)
        {
            print("No space left on table");
            playing = false;
            return;
        }

        var ball = BallFactory.CreateBall();
        ball.GetComponent<SpriteRenderer>().color = NextRandomBallColor();
        
        Instantiate(ball, nextPosition, Quaternion.identity);
    }

    private Color NextRandomBallColor()
    {
        int next = rnd.Next(1, 5);
        switch(next)
        {
            case 1: return Color.blue;
            case 2: return Color.red;
            case 3: return Color.yellow;
            case 4: return Color.green;

        }
        return Color.white;
    }

    private float NextRandomPosition(Func<Vector2,float> GetPart)
    {
        int next = rnd.Next((int)(GetPart(MinMaxVector.min) * 100), (int)(GetPart(MinMaxVector.max) * 100));
        return (float)next / 100;
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
