using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants {

    public static readonly float BALL_RADIUS = 0.5f;

    private static float ballScale;
    private static float ballSize;
    private static float maxSpeed;
    private static float minSpeed;
    private static float followSpeed;

    public static void Init(float referenceSize)
    {
        ballScale = referenceSize / 10;
        ballSize = ballScale * BALL_RADIUS;
        maxSpeed = referenceSize * 2;
        minSpeed = referenceSize / 100;
        followSpeed = referenceSize / 20;
        followSpeed = 20;

    }

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
    }

    public static float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    public static float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }

    public static float FollowSpeed
    {
        get
        {
            return followSpeed;
        }

    }
}
