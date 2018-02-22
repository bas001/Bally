using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static readonly int MAX_NUMBER_OF_TRYS = 50;
    private static readonly int NEXT_BALL_TIMEOUT = 2000;

    private static bool isAnyBallInMotion = false;

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
        Vector2 nextPosition = new Vector2(0,0);
        Vector2? next;
        int counter = 0;
        do
        {
            next = GameFactory.NextRandomPosition();
            if (next.HasValue)
            {
                nextPosition = next.Value;
            }
        } while (!next.HasValue && ++counter != MAX_NUMBER_OF_TRYS);

        if (counter == MAX_NUMBER_OF_TRYS)
        {
            print("No space left on table");
            playing = false;
            return;
        }

        var color = GameFactory.GetRandomColor();
        BallFactory.CreateBall(nextPosition, color.Key, color.Value);

    }

}