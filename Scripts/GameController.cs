using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static readonly int NEXT_BALL_TIMEOUT = 2000;

    private static bool isAnyBallInMotion = false;

    private System.Random rnd = new System.Random();
    private Stopwatch sw = new Stopwatch();
    private bool playing = true;

    private CircleCollider2D ball;
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
        InstantiateGameObjects();
        MinMaxVector.FindMinMax(RetrievePoints(), ball);
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
        //screen size
        int width = Screen.width;
        int height = Screen.height;
        
        print(width +", " + height);

        int wallSize = height / 4;


        Camera m_OrthographicCamera = Camera.main;
        m_OrthographicCamera.transform.position = new Vector3(width / 2, height / 2, -10);
        m_OrthographicCamera.orthographic = true;
        m_OrthographicCamera.orthographicSize = height/2 + 1;
        m_OrthographicCamera.rect = new Rect(0, 0, width, height);


        rightWall = GameObject.Find("rightWall");
        rightWall.GetComponent<SpriteRenderer>().color = Color.red;
        rightWall.gameObject.tag = "redBall";
        rightWall.gameObject.transform.position = new Vector3(width, height / 2);
        rightWall.gameObject.transform.localScale = new Vector3(wallSize, height);

        downWall = GameObject.Find("downWall");
        downWall.GetComponent<SpriteRenderer>().color = Color.green;
        downWall.gameObject.tag = "greenBall";
        downWall.gameObject.transform.position = new Vector3(width /2, 0);
        downWall.gameObject.transform.localScale = new Vector3(wallSize, width);

        upWall = GameObject.Find("upWall");
        upWall.GetComponent<SpriteRenderer>().color = Color.yellow;
        upWall.gameObject.tag = "yellowBall";
        upWall.gameObject.transform.position = new Vector3(width / 2, height);
        upWall.gameObject.transform.localScale = new Vector3(wallSize, width);

        leftWall = GameObject.Find("leftWall");
        leftWall.GetComponent<SpriteRenderer>().color = Color.blue;
        leftWall.gameObject.tag = "blueBall";
        leftWall.gameObject.transform.position = new Vector3(0, height / 2, 0);
        leftWall.gameObject.transform.localScale = new Vector3(wallSize, height);

        walls.Add(rightWall);
        walls.Add(downWall);
        walls.Add(upWall);
        walls.Add(leftWall);




        ball = GameObject.FindWithTag("circleCollider").GetComponent<CircleCollider2D>();


    }

    // Update is called after the Update of BallController
    void Update()
    {
        if (playing)
        {
            if (!isAnyBallInMotion && sw.ElapsedMilliseconds > NEXT_BALL_TIMEOUT)
            {
                CreateRandomBall();
                sw.Reset();
                sw.Start();
            }
        }

        // set to true by BallController
        isAnyBallInMotion = false;
    }

    private void CreateRandomBall()
    {
        Vector2 nextPosition;
        bool colliding = true;
        int counter = 0;
        do
        {
            nextPosition = new Vector2(nextRandomPosition(v2 => v2.x), nextRandomPosition(v2 => v2.y));
            colliding = Physics2D.OverlapCircle(nextPosition, ball.radius);
            counter++;
        } while (colliding && counter != MAX_NUMBER_OF_TRYS);

        if(counter == MAX_NUMBER_OF_TRYS)
        {
            print("No space left on table");
            playing = false;
            return;
        }

        Instantiate(Resources.Load(NextRandomBallColor()), nextPosition, Quaternion.identity);
    }

    private String NextRandomBallColor()
    {
        int next = rnd.Next(1, 5);
        switch(next)
        {
            case 1: return "blueBall";
            case 2: return "redBall";
            case 3: return "yellowBall";
            case 4: return "greenBall";

        }
        return "";
    }

    private float nextRandomPosition(Func<Vector2,float> GetPart)
    {
        int next = rnd.Next((int)(GetPart(MinMaxVector.min) * 100), (int)(GetPart(MinMaxVector.max) * 100));
        return (float)next / 100;
    }

    
    private static class MinMaxVector
    {
        public static Vector2 min;
        public static Vector2 max;
    
        public static void FindMinMax(List<Vector2> points, CircleCollider2D ball)
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

            min = new Vector2(minX + ball.radius, minY + ball.radius);
            max = new Vector2(maxX - ball.radius, maxY - ball.radius);
        }

    }


}
