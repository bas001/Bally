using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 **/
public class ScoreCount
{

    private static int score;
    private static string lastDestroyedTag;
    private static int postiveMultipicator = 1;
    private static int negativeMultipicator = 1;
    private static readonly int POSITIV_SCORE = 2;
    private static readonly int NEGATIV_SCORE = -1;

    public static string Score()
    {
        return score.ToString();
    }

    internal static void PlayerCollision(string tag)
    {
        Reset();
        score += NEGATIV_SCORE;
    }

    internal static void BallDestroyed(string tag)
    {
        if (tag == lastDestroyedTag)
        {
            postiveMultipicator = postiveMultipicator + 5;
        }

        score += POSITIV_SCORE * postiveMultipicator;
        lastDestroyedTag = tag;
    }

    internal static void UnequalBallHit()
    {
        score += NEGATIV_SCORE * negativeMultipicator;
    }

    private static void Reset()
    {
        lastDestroyedTag = null;
        postiveMultipicator = 1;
        negativeMultipicator = 1;
    }

}
