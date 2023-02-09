using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] UnityEvent onDown;
    [SerializeField] UnityEvent onUp;
    [SerializeField] bool startingPosition;
    [SerializeField] bool isTimedLever = false;

    SpriteRenderer sprite;
    string interactionButton;
    bool isUp = true;
    int layerMask;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        interactionButton = $"Interact";
        layerMask = LayerMask.GetMask("Lever");
    }

    void Update()
    {
        var hit = Physics2D.OverlapCircle(player.transform.position, 1f, layerMask);
        if (Input.GetButtonDown(interactionButton) && hit != null)
        {
            if(!isTimedLever) 
            {
                TurnLever();
            }
            else if(isTimedLever)
            {
                StartCoroutine(LeverTimer());
            }
        }
    }

    void TurnLever()
    {
        if(isUp)
        {
            isUp = false;
            onDown.Invoke();
        }
        else if(!isUp)
        {
            isUp = true;
            onUp.Invoke();
        }
    }

    IEnumerator LeverTimer()
    {
        yield return new WaitForSeconds(3f);
        if (startingPosition)
        {
            isUp = true;
            TurnLever();
        }
        else
        {
            isUp = false;
            TurnLever();
        }
    }

    
    public void Test1()
    {
        Debug.Log("now down");
    }
    public void Test2()
    {
        Debug.Log("now up");
    }
    
}
