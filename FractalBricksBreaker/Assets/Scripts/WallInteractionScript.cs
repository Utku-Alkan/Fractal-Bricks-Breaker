using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Get the ball's Rigidbody2D component and its velocity
            Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 ballVelocity = ballRigidbody.velocity;

            // Calculate the direction of the wall
            Vector2 wallDirection = transform.right;

            // Calculate the angle between the ball's velocity and the wall's direction
            // float angle = Vector2.Angle(ballVelocity, wallDirection);

            // Flip the direction of the wall if the ball approaches from the left side
            if (ballVelocity.x < 0)
            {
                wallDirection *= -1;
            }

            // Calculate the reflected direction of the ball
            Vector2 reflectedDirection = Vector2.Reflect(ballVelocity, wallDirection);

            // Set the reflected direction as the new velocity for the ball
            ballRigidbody.velocity = reflectedDirection;
        }
    }
}
