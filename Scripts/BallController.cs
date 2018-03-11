﻿using System;
using UnityEngine;

public class BallController : MonoBehaviour
{

    bool isActive = false;

    private static String activeColor;

    // Update is called once per frame
    void Update()
    {
        if (InMotion())
        {
            GameController.SetIsAnyBallInMotion(true);
        }
        else
        {
            isActive = false;
        }
    }


    /*
     * IMPORTANT: If two ball are colliding OnCollisionEnter2D gets called once for every ball!!
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name != "ball")
        {
            return;
        }

        string otherTag = collision.collider.transform.tag;

        if (otherTag == "Player")
        {
            PlayerCollision();
        }
        else
        {
            if (otherTag == gameObject.tag)
            {
                SameColorCollision();
            }
            else
            {
                OtherColorCollision();
            }
        }

    }

    private void OtherColorCollision()
    {
        isActive = false;
    }

    private void SameColorCollision()
    {
        if (isActive)
        {
            Destroy(gameObject, 0.00000000001f);
        }
        isActive = true;
    }

    private void PlayerCollision()
    {
        string color = gameObject.tag;
        if (activeColor == null || activeColor == color)
        {
            isActive = true;
            activeColor = color;
            GameFactory.ChangeWallColor(color);
        }
    }

    private bool InMotion()
    {
        if (HasNoSpeed(GetComponent<Rigidbody2D>().velocity.x) && HasNoSpeed(GetComponent<Rigidbody2D>().velocity.y))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return false;
        }

        return true;
    }

    private bool HasNoSpeed(float direction)
    {
        return Math.Abs(direction) < GameConstants.MinSpeed;
    }

}
