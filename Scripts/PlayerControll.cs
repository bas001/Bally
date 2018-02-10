using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {

    int speed = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchSupported)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            GetComponent<Rigidbody2D>().velocity = new Vector2(touchDeltaPosition.x, touchDeltaPosition.y);
            
        } else
        {
            float horizontalVelocity = Input.GetAxis("Horizontal");
            float verticalVelocity = Input.GetAxis("Vertical");

            GetComponent<Rigidbody2D>().velocity =
                new Vector2(horizontalVelocity * speed, verticalVelocity * speed);
        }


    }

}
