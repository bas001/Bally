using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    bool gotHit = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.collider.transform.tag == "blueBall" && gotHit)
        {
            Destroy(gameObject, 0.00000000001f);
        }

        if (collision.collider.transform.tag == "blueBall" 
            || collision.collider.transform.tag == "Player")
        {
            gotHit = true;
        }
    
    
    }

}
