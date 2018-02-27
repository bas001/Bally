using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Text text = GameObject.Find("scoreText").GetComponent<Text>();
        if (Input.touchSupported)
        {

            float touchX = Input.GetTouch(0).position.x;
            float touchY = Input.GetTouch(0).position.y;

            var targetX = touchX - GetComponent<Rigidbody2D>().position.x;
            var targetY = touchY - GetComponent<Rigidbody2D>().position.y;

            if (Math.Abs(targetX) < GameConstants.BallSize / 100 && Math.Abs(targetY) < GameConstants.BallSize / 100)
            {
                text.text = "ruhe";
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else
            {
                if (Math.Abs(targetX) > Math.Abs(targetY))
                {
                    targetY = targetY / Math.Abs(targetX);
                    targetX = targetX / Math.Abs(targetX);
                }
                else
                {
                    targetX = targetX / Math.Abs(targetY);
                    targetY = targetY / Math.Abs(targetY);
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(targetX * GameConstants.MaxSpeed, targetY * GameConstants.MaxSpeed);
            }


        }
        else
        {
            float horizontalVelocity = Input.GetAxis("Horizontal");
            float verticalVelocity = Input.GetAxis("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalVelocity * GameConstants.MaxSpeed, verticalVelocity * GameConstants.MaxSpeed);
        }

    }

}
