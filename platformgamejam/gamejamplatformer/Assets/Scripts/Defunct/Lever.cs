using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] UnityEvent onDown;
    [SerializeField] UnityEvent onUp;
    [SerializeField] Sprite leverDown;
    [SerializeField] bool startingPosition;
    [SerializeField] bool isTimedLever = false;
    [SerializeField] float timer = 3f;

    SpriteRenderer sprite;
    Sprite leverUp;
    string interactionButton;
    bool isUp = true;
    int layerMask;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        leverUp = sprite.sprite;
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
            sprite.sprite = leverDown;
            isUp = false;
            onDown.Invoke();
        }
        else if(!isUp)
        {
            sprite.sprite = leverUp;
            isUp = true;
            onUp.Invoke();
        }
    }

    IEnumerator LeverTimer()
    {
        if (startingPosition)
        {
            Debug.Log("temp up");
            sprite.sprite = leverUp;
            isUp = true;
            onUp.Invoke();
        }
        else
        {
            Debug.Log("temp down");
            sprite.sprite = leverDown;
            isUp = false;
            onDown.Invoke();
        }
        var initialSprite = sprite.sprite;
        yield return new WaitForSeconds(timer - 2f);
        yield return new WaitForSeconds(0.5f);
        sprite.sprite = null;
        yield return new WaitForSeconds(0.5f);
        sprite.sprite = initialSprite;
        yield return new WaitForSeconds(0.5f);
        sprite.sprite = null;
        yield return new WaitForSeconds(0.5f);
        sprite.sprite = initialSprite;
        TurnLever();
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
