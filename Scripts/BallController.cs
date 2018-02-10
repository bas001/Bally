using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    bool getDestroyedOnNextHit = false;

    // Use this for initialization
    void Start() {
    }

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

        if (Math.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.05 && Math.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.05)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            return false;
        }

        return true;

    }

}
