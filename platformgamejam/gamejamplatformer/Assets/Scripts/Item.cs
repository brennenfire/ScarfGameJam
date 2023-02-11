using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteract
{
    [SerializeField] bool onlyExamine = true;
    [SerializeField] bool isPickup = false;
    [SerializeField] TMP_Text textBox;
    [SerializeField] Image image;
    [SerializeField] List<string> text;
    bool canExamine = true;
    bool hasRope = false;
    bool hasBigRope = false;
    int textCounter = 0;

    // static script with array of game objects for equipment
    
    void Update()
    {

    }

    public void Interact()
    {
        if (onlyExamine && canExamine)
        {
            /*
            if(textCounter > text.Count)
            {
                return;
            }
            */
            canExamine = false;
            image.enabled = true;
            textBox.text = text[0];
            StartCoroutine(TurnOffBox());
        }

        if(isPickup)
        {
            canExamine = false;
            image.enabled = true;
            textBox.text = text[0];
            StartCoroutine(TurnOffBox());
        }
    }

    IEnumerator TurnOffBox()
    {
        yield return new WaitForSeconds(2.5f);
        textBox.text = text[1];
        yield return new WaitForSeconds(2.5f);
        image.enabled = false;
        if(isPickup)
        {
            gameObject.SetActive(false);
            hasRope = true;
            if(hasRope)
            {
                hasBigRope = true;
            }
        }
        textBox.text = "";
        canExamine = true;
    }
}
