﻿using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreText;

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static readonly int NEXT_BALL_TIMEOUT = 2000;

    private static bool isAnyBallInMotion = false;

    private int score = 0;
    private Stopwatch sw = new Stopwatch();
    private bool playing = true;

    public static void SetIsAnyBallInMotion(bool value)
    {
        isAnyBallInMotion = value;
    }

    // Use this for initialization
    void Start()
    {
        GameFactory.Init();
        scoreText.text = score.ToString();
        sw.Start();

    }

    // Update is called after the Update of BallController
    void Update()
    {
        if (!playing)
        {
            return;
        }

        if (!isAnyBallInMotion && sw.ElapsedMilliseconds > NEXT_BALL_TIMEOUT)
        {
            InstantiateRandomBall();
            sw.Reset();
            sw.Start();
        }

        // set to true by BallController
        isAnyBallInMotion = false;
    }

    private void InstantiateRandomBall()
    {
        Vector2? nextPosition = TryFindNextPosition();
        if (nextPosition.HasValue)
        {
            var color = GameFactory.GetRandomColor();
            BallFactory.CreateBall(nextPosition.Value, color.Key, color.Value);
        }
        else
        {
            print("No space left on table");
            playing = false;
        }
    }

    private Vector2? TryFindNextPosition()
    {
        for (int i = 0; i < MAX_NUMBER_OF_TRYS; i++)
        {
            var next = GameFactory.NextRandomPosition();
            if(NotColliding(next))
            {
                return next;
            }
        }
        return null;
    }

    private bool NotColliding(Vector2 pos)
    {
        return !Physics2D.OverlapCircle(pos, GameConstants.BALL_RADIUS * GameConstants.BallScale + 1);
    }
}