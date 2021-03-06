﻿using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreText;

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static readonly int NEXT_BALL_TIMEOUT = 2000;

    private Stopwatch ballCreation;
    private bool playing = true;

    // Use this for initialization
    void Start()
    {
        GameFactory.Init();
        ballCreation = Stopwatch.StartNew();
    }

    // Update is called after the Update of BallController
    void Update()
    {
        if (!playing)
        {
            return;
        }

        scoreText.text = ScoreCount.Score();

        if (!State.IsAnyBallInMotion)
        {
            if(ballCreation.ElapsedMilliseconds > NEXT_BALL_TIMEOUT)
            {
                InstantiateRandomBall();
                ballCreation = Stopwatch.StartNew();
            }
            GameFactory.ChangeWallColor(GameConstants.NONE_ACTIVE_COLOR);
            State.ActiveColor = GameConstants.NONE_ACTIVE_COLOR;
        }

        // set to true by BallController
        State.IsAnyBallInMotion = false;

    }

    private void InstantiateRandomBall()
    {
        Vector2? nextPosition = TryFindNextPosition();
        if (nextPosition.HasValue)
        {
            BallFactory.CreateBall(nextPosition.Value, GameFactory.NextRandomColor());
        }
        else
        {
            scoreText.text = ScoreCount.Score() + " GAME OVER";
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
        return !Physics2D.OverlapCircle(pos, GameConstants.BallSize);
    }


}