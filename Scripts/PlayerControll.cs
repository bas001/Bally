using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {

    int speed = 5;

    // Use this for initialization
    void Start()
    {
        print("start");
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalVelocity = Input.GetAxis("Horizontal");
        float verticalVelocity = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity =
            new Vector2(horizontalVelocity * speed, verticalVelocity * speed);

    }

}
