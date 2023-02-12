using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RopePole : MonoBehaviour, IInteract
{
    [SerializeField] GameObject smallRope;
    [SerializeField] GameObject bigRope;
    [SerializeField] TMP_Text textBox;
    [SerializeField] TMP_Text endText;
    [SerializeField] Image image;
    [SerializeField] Animator animator;

    public void Interact()
    {
        if(smallRope.activeSelf == false && bigRope.activeSelf == true)
        {
            textBox.text = "This rope is too short.";
            image.enabled = true;
            StartCoroutine(TurnOffBox());
        }
        else if(bigRope.activeSelf == false) 
        {
            animator.SetTrigger("End");
            endText.text = "Thank you for playing this demo.";
            StartCoroutine(TurnOffGame());
        }
        else if(smallRope.activeSelf == true || bigRope.activeSelf == true)
        {
            textBox.text = "It's a rope post.";
            image.enabled = true;
            StartCoroutine(TurnOffBox());
        }
        
    }

    IEnumerator TurnOffBox()
    {
        yield return new WaitForSeconds(2.5f);
        yield return new WaitForSeconds(2.5f);
        image.enabled = false;
        textBox.text = "";
    }

    IEnumerator TurnOffGame()
    {
        yield return new WaitForSeconds(7f);
        Application.Quit();
    }
}
