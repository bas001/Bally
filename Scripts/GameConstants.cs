
public class GameConstants {

    public static readonly float BALL_RADIUS = 0.5f;
    public static readonly string PLAYER_TAG= "black";
    public static readonly string NONE_ACTIVE_COLOR = "grey";

    private static float ballScale;
    private static float ballSize;
    private static float playerVelocity;
    private static float minSpeed;

    public static void Init(float referenceSize)
    {
        ballScale = referenceSize / 10;
        ballSize = ballScale * BALL_RADIUS;
        playerVelocity = referenceSize / 200;
        minSpeed = referenceSize / 100;

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
            return playerVelocity;
        }
    }

    public static float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }

}
