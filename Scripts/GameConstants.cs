using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {

    public static readonly float BALL_RADIUS = 0.5f;

    private static float ballScale;
    private static float ballSize;
    private static float maxSpeed;
    private static float minSpeed;


    public static float BallSize
    {
        get
        {
            return ballSize;
        }
    }

    public static float BallScale
    {
        get
        {
            return ballScale;
        }
        set
        {
            ballScale = value;
            ballSize = value * BALL_RADIUS;
        }
    }

    public static float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public static float MinSpeed
    {
        get
        {
            return minSpeed;
        }

        set
        {
            minSpeed = value;
        }
    }
}
