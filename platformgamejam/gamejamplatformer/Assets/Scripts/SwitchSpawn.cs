using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpawn : MonoBehaviour
{
    [SerializeField] Player playerSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null) 
        {
            return;
        }
        playerSpawn.startingPosition = playerSpawn.transform.position;
    }
}
