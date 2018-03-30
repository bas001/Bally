using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {

            if (InMotion())
            {
                State.IsAnyBallInMotion = true;
                gameObject.GetComponent<SpriteRenderer>().color = GameFactory.ColorDict[tag].bright;
            }
            else
            {
                if (!State.IsAnyBallInMotion)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = GameFactory.ColorDict[tag].dark;
                    HandleTouchInput();
                }
            }
        }
        else
        {
            HandleKeyboardInput();
        }

    }

    private void HandleKeyboardInput()
    {
        float horizontalVelocity = Input.GetAxis("Horizontal");
        float verticalVelocity = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalVelocity * 1000, verticalVelocity * 1000);
    }

    private void HandleTouchInput()
    {
        var targetX = Input.GetTouch(0).position.x - GetComponent<Rigidbody2D>().position.x;
        var targetY = Input.GetTouch(0).position.y - GetComponent<Rigidbody2D>().position.y;

        GetComponent<Rigidbody2D>().velocity = new Vector2(targetX * GameConstants.MaxSpeed, targetY * GameConstants.MaxSpeed);

    }

    private bool InMotion()
    {
        if (HasNoSpeed(GetComponent<Rigidbody2D>().velocity.x) && HasNoSpeed(GetComponent<Rigidbody2D>().velocity.y))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return false;
        }

        return true;
    }

    private bool HasNoSpeed(float direction)
    {
        return Math.Abs(direction) < GameConstants.MinSpeed;
    }

}
