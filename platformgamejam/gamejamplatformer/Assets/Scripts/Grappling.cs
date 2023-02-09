using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grappling : MonoBehaviour//, IPointerClickHandler    
{
    [SerializeField] float grappleTimer = 3f;
    [SerializeField] float maxDist = 4f;
    [SerializeField] Transform scarf;
    //[SerializeField] Camera mainCamera;

    EventSystem events;
    Player player;
    new Animator animation;
    public List<GameObject> test;
    GameObject test1;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    float initialTimer;
    public bool isGrappling;

    public bool IsGrappling => isGrappling;

    void Start()
    {
        distanceJoint.enabled = false;
        initialTimer = grappleTimer;
        animation = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        animation.SetBool("Swing", isGrappling);
        CheckClosestPlatform();
        if (isGrappling)
        {
            grappleTimer -= Time.deltaTime;
        }
        if (ShouldGrapple())
        {
            Grapple();
        }
        if (ShouldntGrapple())
        {
            DontGrapple();
        }
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, scarf.position + Vector3.down);
        }
    }

    void CheckClosestPlatform()
    {
        foreach (var gObject in test)
        {
            var dist = Vector2.Distance(transform.position, gObject.transform.position);
            var isBelow = (gObject.transform.position.y - transform.position.y) > 0;
            if (dist < maxDist && isBelow)
            {
                test1 = gObject;
            }
        }
    }

    public void DontGrapple()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        isGrappling = false;
        grappleTimer = initialTimer;
        if (test1 != null)
        {
            test1.GetComponentInChildren<Scarf>().GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    bool ShouldntGrapple()
    {
        return Input.GetMouseButtonUp(0) || grappleTimer <= 0;
    }

    void Grapple()
    {
        //Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, test1.transform.position + Vector3.up);
        lineRenderer.SetPosition(1, scarf.position + (Vector3.down * 2));
        distanceJoint.connectedAnchor = test1.transform.position;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
        isGrappling = true;
        test1.GetComponentInChildren<Scarf>().GetComponent<SpriteRenderer>().enabled = true;
    }

    bool ShouldGrapple()
    {
        return Input.GetMouseButtonDown(0)
            && grappleTimer > 0
            && test1 != null
            && Vector2.Distance(transform.position, test1.transform.position) < maxDist;
    }

    /*
    public void OnPointerClick(PointerEventData eventData)
    {
    }
    */
}
