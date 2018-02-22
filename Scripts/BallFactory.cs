﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory
{

    public static readonly float BALL_RADIUS = 0.5f;

    public static GameObject CreateBall(Vector2 position, String tag, Color color)
    {
        var ball = CreateBall(position, color);
        ball.tag = tag;
        ball.name = "ball";
        ball.AddComponent<BallController>();
        return ball;
    }

    public static GameObject CreatePlayer(Vector2 position, Color color)
    {
        var player = CreateBall(position, color);
        player.tag = "Player";
        player.name = "Player";
        player.AddComponent<PlayerControll>();
        return player;
    }

    private static GameObject CreateBall(Vector2 position, Color color)
    {
        var ball = new GameObject();

        ball.GetComponent<Transform>().localScale = new Vector3(50, 50);
        ball.GetComponent<Transform>().position = position;

        var rigidbody2D = ball.AddComponent<Rigidbody2D>();
        rigidbody2D.drag = 0.4f;
        rigidbody2D.angularDrag = 0.05f;
        rigidbody2D.gravityScale = 0;

        var circleCollider2D = ball.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = BALL_RADIUS;

        circleCollider2D.sharedMaterial = new PhysicsMaterial2D { bounciness = 0.3f, friction = 0.01f };

        var spriteRenderer = ball.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Ball");
        spriteRenderer.color = color;

        return ball;
    }

}