using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField] GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        virtualCam.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        virtualCam.SetActive(false);
    }
}
