using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteract
{
    [SerializeField] bool onlyExamine = true;
    [SerializeField] TMP_Text textBox;
    [SerializeField] Image image;
    [SerializeField] List<string> text;
    bool canExamine = true;
    int textCounter = 0;

    void Update()
    {
        foreach(var obj in text)
        {
            textCounter++;
        }
    }

    public void Interact()
    {
        if (onlyExamine && canExamine)
        {
            canExamine = false;
            image.enabled = true;
            textBox.text = text[text.Count - textCounter++];
            StartCoroutine(TurnOffBox());
        }
    }

    IEnumerator TurnOffBox()
    {
        yield return new WaitForSeconds(5f);
        image.enabled = false;
        textBox.text = "";
        canExamine = true;
    }
}
