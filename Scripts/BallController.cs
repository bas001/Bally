﻿using System;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private bool isActive;

    // Update is called once per frame
    void Update()
    {
        if (InMotion())
        {
            State.IsAnyBallInMotion = true;
        }
        if(State.IsAnyBallInMotion == false)
        {
            if(isActive)
            {
                Destroy(gameObject, 0.00000000001f);
            }
        }
    }

    /*
     * IMPORTANT: If two balls are colliding OnCollisionEnter2D gets called once for every ball!!
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name != "ball")
        {
            return;
        }

        string otherTag = collision.collider.transform.tag;

        if (otherTag == GameConstants.PLAYER_TAG)
        {
            PlayerCollision();
        }
        else
        {
            if (otherTag == gameObject.tag)
            {
                SameColorCollision(collision.collider);
            }
        }

    }

    private void SetActive()
    {
        isActive = true;
        gameObject.GetComponent<SpriteRenderer>().color = GameFactory.ColorDict[tag].bright;
    }

    private void SetInactive()
    {
        isActive = false;
        gameObject.GetComponent<SpriteRenderer>().color = GameFactory.ColorDict[tag].dark;
    }

    private void SameColorCollision(Collider2D other)
    {
        if (gameObject.tag == State.ActiveColor)
        {
            GameFactory.ChangeWallColor(gameObject.tag);

            SetActive();
            var allColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, GameConstants.BallSize);
            foreach (var collider in allColliders)
            {
                if(collider.gameObject.name == "ball" && gameObject.tag == collider.gameObject.tag)
                {
                    collider.gameObject.SetActive(true);
                }
            }
        }
    }

    private void PlayerCollision()
    {
        string color = gameObject.tag;
        if (State.ActiveColor == GameConstants.NONE_ACTIVE_COLOR || State.ActiveColor == color)
        {
            SetActive();
            State.ActiveColor = color;
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
