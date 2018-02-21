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


    public static void SetIsAnyBallInMotion(bool value)
    {
        isAnyBallInMotion = value;
    }

    // Use this for initialization
    void Start () {
        InstantiateGameObjects();
        MinMaxVector.FindMinMax(RetrievePoints(), CIRCLE_RADIUS * GameFactory.GetBallScale());
        sw.Start();
    }

    private List<Vector2> RetrievePoints()
    {
        List<Vector2> points = new List<Vector2>();
        foreach (var wall in GameFactory.GetWalls())
        {
            Vector3 vector3 = wall.GetComponent<Transform>().position;
            points.Add(new Vector2(vector3.x, vector3.y));
        }

        return points;
    }

    private void InstantiateGameObjects()
    {
        GameFactory.Init();

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
            colliding = Physics2D.OverlapCircle(nextPosition, CIRCLE_RADIUS * GameFactory.GetBallScale() + 1);
            counter++;
        } while (colliding && counter != MAX_NUMBER_OF_TRYS);

        if(counter == MAX_NUMBER_OF_TRYS)
        {
            print("No space left on table");
            playing = false;
            return;
        }

        var color = GameFactory.GetColor(rnd.Next(1, 5));
        BallFactory.CreateBall(nextPosition, color.Key, color.Value);
        
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
