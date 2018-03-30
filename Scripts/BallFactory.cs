using System;
using UnityEngine;

public class BallFactory
{
    public static GameObject CreateBall(Vector2 position, String tag)
    {
        var ball = Create(position, tag);
        ball = AddCircleColider(ball);
        ball.name = "ball";
        ball.AddComponent<BallController>();
        return ball;
    }

    public static GameObject CreatePlayer(Vector2 position)
    {
        var player = Create(position, GameConstants.PLAYER_TAG);
        player = AddCircleColider(player);
        player.name = "Player";
        player.AddComponent<PlayerController>();
        return player;
    }

    private static GameObject Create(Vector2 position, String tag)
    {
        var ball = new GameObject();

        ball.tag = tag;

        ball.GetComponent<Transform>().localScale = new Vector3(GameConstants.BallScale, GameConstants.BallScale);
        ball.GetComponent<Transform>().position = position;

        var rigidbody2D = ball.AddComponent<Rigidbody2D>();
        rigidbody2D.drag = 0.4f;
        rigidbody2D.angularDrag = 0.05f;
        rigidbody2D.gravityScale = 0;

        var spriteRenderer = ball.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Ball");
        spriteRenderer.color = GameFactory.ColorDict[tag].dark;

        return ball;
    }

    private static GameObject AddCircleColider(GameObject ball)
    {
        var circleCollider2D = ball.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = GameConstants.BALL_RADIUS;
        circleCollider2D.sharedMaterial = new PhysicsMaterial2D { bounciness = 0.3f, friction = 0.01f };
        return ball;
    }

}