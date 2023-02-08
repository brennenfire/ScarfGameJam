using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            BackToStart(player);
        }
    }

    public static void BackToStart(Player player)
    {
        player.GetComponent<Grappling>().DontGrapple();
        player.ResetToStart();
    }
}
