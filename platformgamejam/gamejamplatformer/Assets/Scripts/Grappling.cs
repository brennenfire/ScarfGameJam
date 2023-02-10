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
    public List<GameObject> grappleList;
    GameObject grapple;
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
        if (grapple != null)
        {
            if (ShouldntGrapple())
            {
                DontGrapple();
            }
        }
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, scarf.position + Vector3.down);
        }
    }

    void CheckClosestPlatform()
    {
        foreach (var gObject in grappleList)
        {
            var dist = Vector2.Distance(transform.position, gObject.transform.position);
            var isBelow = (gObject.transform.position.y - transform.position.y) > 0;
            if (dist < maxDist && isBelow && gObject.activeSelf == true)
            {
                grapple = gObject;
            }
        }
    }

    public void DontGrapple()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        isGrappling = false;
        grappleTimer = initialTimer;
        if (grapple != null)
        {
            grapple.GetComponentInChildren<Scarf>().GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    bool ShouldntGrapple()
    {
        return Input.GetMouseButtonUp(0) || grappleTimer <= 0 || grapple.activeSelf == false;
    }

    void Grapple()
    {
        //Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, grapple.transform.position + Vector3.up);
        lineRenderer.SetPosition(1, scarf.position + (Vector3.down * 2));
        distanceJoint.connectedAnchor = grapple.transform.position;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
        isGrappling = true;
        grapple.GetComponentInChildren<Scarf>().GetComponent<SpriteRenderer>().enabled = true;
    }

    bool ShouldGrapple()
    {
        return Input.GetMouseButtonDown(0)
            && grappleTimer > 0
            && grapple != null
            && Vector2.Distance(transform.position, grapple.transform.position) < maxDist;
    }

    /*
    public void OnPointerClick(PointerEventData eventData)
    {
    }
    */
}
