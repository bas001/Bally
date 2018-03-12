using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Stopwatch sw = Stopwatch.StartNew();

    // Update is called once per frame
    void Update()
    {
        if (sw.ElapsedMilliseconds > 1000)
        {
            if (Input.touchSupported)
            {
                HandleTouchInput();
            }
            else
            {
                float horizontalVelocity = Input.GetAxis("Horizontal");
                float verticalVelocity = Input.GetAxis("Vertical");
                GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalVelocity * GameConstants.MaxSpeed, verticalVelocity * GameConstants.MaxSpeed);
                if(horizontalVelocity != 0 || verticalVelocity != 0)
                {
                    State.ActiveColor = GameFactory.GetNoneActiveColor();
                }
            }
            sw = Stopwatch.StartNew();
        }

    }

    private void HandleTouchInput()
    {
        var targetX = Input.GetTouch(0).position.x - GetComponent<Rigidbody2D>().position.x;
        var targetY = Input.GetTouch(0).position.y - GetComponent<Rigidbody2D>().position.y;

        var absTargetX = Math.Abs(targetX);
        var absTargetY = Math.Abs(targetY);

        if(absTargetX > 0 || absTargetY > 0 )
        {
            State.ActiveColor = GameFactory.GetNoneActiveColor();
        }

        if (absTargetX < GameConstants.BallSize && absTargetY < GameConstants.BallSize)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(targetX * GameConstants.FollowSpeed, targetY * GameConstants.FollowSpeed);
        }
        else
        {
            if (Math.Abs(targetX) > absTargetY)
            {
                targetY = targetY / absTargetX;
                targetX = targetX / absTargetX;
            }
            else
            {
                targetX = targetX / absTargetY;
                targetY = targetY / absTargetY;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(targetX * GameConstants.MaxSpeed, targetY * GameConstants.MaxSpeed);
        }
    }

}
