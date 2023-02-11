using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float bounceVelocity = 1f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody = player.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x * bounceVelocity, rigidbody.velocity.y * bounceVelocity);
            }
        }
    }
}
