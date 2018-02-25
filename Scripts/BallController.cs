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

        if (collision.collider.transform.tag == gameObject.tag && getDestroyedOnNextHit)
        {
            Destroy(gameObject, 0.00000000001f);
        }

        if (collision.collider.transform.tag == gameObject.tag
            || collision.collider.transform.tag == "Player")
        {
            getDestroyedOnNextHit = true;
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
