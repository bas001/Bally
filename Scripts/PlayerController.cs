﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text debugText;

    // Update is called once per frame
    void Update()
    {

        if (Input.touchSupported)
        {

            float x = Input.GetTouch(0).position.x;
            float y = Input.GetTouch(0).position.y;
            debugText.text = (int)x + ", " + (int)y;

            GetComponent<Transform>().position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        }
        else
        {
            float horizontalVelocity = Input.GetAxis("Horizontal");
            float verticalVelocity = Input.GetAxis("Vertical");

            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalVelocity * GameConstants.MaxSpeed, verticalVelocity * GameConstants.MaxSpeed);

        }

    }

    void check()
    {
        if (Math.Abs(GetComponent<Transform>().position.x - new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y).x) > 3
    || Math.Abs(GetComponent<Transform>().position.y - new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y).y) > 3)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y), ForceMode2D.Impulse);

        }
    }

}