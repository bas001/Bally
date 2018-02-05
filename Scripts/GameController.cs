using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static bool isAnyBallInMotion = false;

    private System.Random rnd = new System.Random();
    private Stopwatch sw = new Stopwatch();
    private bool playing = true;

    private GameObject table;
    private CircleCollider2D ball;

    public static void SetIsAnyBallInMotion(bool value)
    {
        isAnyBallInMotion = value;
    }

    // Use this for initialization
    void Start () {
        InstantiateGameObjects();
        MinMaxVector.FindMinMax(table, ball);
        sw.Start();
    }

    private void InstantiateGameObjects()
    {
        table = GameObject.FindWithTag("Table");
        ball = GameObject.FindWithTag("circleCollider").GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set to true by BallController
        isAnyBallInMotion = false;
    }

    private void LateUpdate()
    {
        if(playing)
        {
            if (!isAnyBallInMotion && sw.ElapsedMilliseconds > 100)
            {
                CreateRandomBall();
                sw.Reset();
                sw.Start();
            }
        }
      
    }

    private void CreateRandomBall()
    {
        Vector2 nextPosition;
        bool colliding = true;
        int counter = 0;
        do
        {
            nextPosition = new Vector2(nextRandom(v2 => v2.x), nextRandom(v2 => v2.y));
            colliding = Physics2D.OverlapCircle(nextPosition, ball.radius);
            counter++;
        } while (colliding && counter != MAX_NUMBER_OF_TRYS);

        if(counter == MAX_NUMBER_OF_TRYS)
        {
            print("No space left on table");
            playing = false;
            return;
        }

        Instantiate(Resources.Load("blueBall"), nextPosition, Quaternion.identity);
    }

    private float nextRandom(Func<Vector2,float> GetPart)
    {
        int next = rnd.Next((int)(GetPart(MinMaxVector.min) * 100), (int)(GetPart(MinMaxVector.max) * 100));
        return (float)next / 100;
    }

    
    private static class MinMaxVector
    {
        public static Vector2 min;
        public static Vector2 max;
    
        public static void FindMinMax(GameObject table, CircleCollider2D ball)
        {
            float minY = float.MaxValue;
            float minX = float.MaxValue;
            float maxY = float.MinValue;
            float maxX = float.MinValue;

            Vector2[] points = table.GetComponent<EdgeCollider2D>().points;
            for (int i = 0; i < points.Length; i++)
            {
                float y = points[i].y * table.GetComponent<Transform>().localScale.y;
                float x = points[i].x * table.GetComponent<Transform>().localScale.x;

                if (y < minY)
                {
                    minY = y;
                }
                if (x < minX)
                {
                    minX = x;
                }
                if (y > maxY)
                {
                    maxY = y;
                }
                if (x > maxX)
                {
                    maxX = x;
                }

            }

            min = new Vector2(minX + ball.radius, minY + ball.radius);
            max = new Vector2(maxX - ball.radius, maxY - ball.radius);
        }

    }


}
