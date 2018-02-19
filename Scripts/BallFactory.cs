using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{

    public static GameObject CreateBall()
    {
        var ball = new GameObject();

        ball.GetComponent<Transform>().localScale = new Vector3(50, 50);

        var rigidbody2D = ball.AddComponent<Rigidbody2D>();
        rigidbody2D.drag = 0.4f;
        rigidbody2D.angularDrag = 0.05f;

        var circleCollider2D = ball.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = 0.5f;

        circleCollider2D.sharedMaterial = new PhysicsMaterial2D { bounciness = 0.3f, friction = 0.01f };

        var spriteRenderer = ball.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Ball");

        return ball;
    }
}