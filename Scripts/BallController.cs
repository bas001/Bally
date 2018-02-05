using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    bool getDestroyedOnNextHit = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
      if(InMotion())
        {
            getDestroyedOnNextHit = false;
            GameController.SetIsAnyBallInMotion(true);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.collider.transform.tag == gameObject.tag && getDestroyedOnNextHit)
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
        if(GetComponent<Rigidbody2D>().velocity.x < 0.001 && GetComponent<Rigidbody2D>().velocity.y < 0.001)
        {
            return false;
        }
        return true;
    }

}
