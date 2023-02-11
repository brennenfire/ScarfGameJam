using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Player player;

    int layerMask;
    string interactionButton;

    void Start()
    {
        interactionButton = $"Interact";
        layerMask = LayerMask.GetMask("Lever");
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(player.transform.position, 1f, layerMask);
        if(Input.GetButtonDown(interactionButton)) 
        {
            Debug.Log("interact1");
            if (hit != null)
            {
                Debug.Log("interact2");
                /*
                var obj = GetComponent<IInteract>();
                obj.Interact();
                */
                hit.GetComponent<IInteract>().Interact();
            }
        }
    }
}
