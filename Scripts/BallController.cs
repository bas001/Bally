using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    bool getDestroyedOnNextHit = false;

    // Update is called once per frame
    void Update() {
        if (InMotion())
        {
            GameController.SetIsAnyBallInMotion(true);
        } else
        {
            getDestroyedOnNextHit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.name != "ball")
        {
            return;
        }

        string otherTag = collision.collider.transform.tag;

        if (otherTag == "Player")
        {
            ScoreCount.PlayerCollision(gameObject.tag);
            getDestroyedOnNextHit = true;
        }
        else
        {
            if (otherTag == gameObject.tag)
            {
                if (getDestroyedOnNextHit)
                {
                    ScoreCount.BallDestroyed(gameObject.tag);
                    Destroy(gameObject, 0.00000000001f);
                }
                getDestroyedOnNextHit = true;
            }
            else
            {
                ScoreCount.UnequalBallHit();

            }
        }

    }

    private bool InMotion()
    {
        if (HasNoSpeed(GetComponent<Rigidbody2D>().velocity.x) && HasNoSpeed(GetComponent<Rigidbody2D>().velocity.y))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            return false;
        }

        return true;
    }

    private bool HasNoSpeed(float direction)
    {
        return Math.Abs(direction) < GameConstants.MinSpeed;
    }

}
