using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewLever : MonoBehaviour, IInteract
{
    [SerializeField] UnityEvent onDown;
    [SerializeField] UnityEvent onUp;
    [SerializeField] Sprite leverDown;
    [SerializeField] bool startingPosition;
    [SerializeField] bool isTimedLever = false;
    [SerializeField] float timer = 3f;

    SpriteRenderer sprite;
    Sprite leverUp;
    bool isUp = true;
    bool currentlyTiming = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        leverUp = sprite.sprite;
    }

    public void Interact()
    {
        if (!isTimedLever)
        {
            TurnLever();
        }
        else if (isTimedLever && currentlyTiming == false)
        {
            StartCoroutine(LeverTimer());
        }
    }

    void TurnLever()
    {
        if (isUp)
        {
            sprite.sprite = leverDown;
            isUp = false;
            onDown.Invoke();
        }
        else if (!isUp)
        {
            sprite.sprite = leverUp;
            isUp = true;
            onUp.Invoke();
        }
    }

    IEnumerator LeverTimer()
    {
        currentlyTiming = true;
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
        currentlyTiming = false;
        TurnLever();
    }
}
