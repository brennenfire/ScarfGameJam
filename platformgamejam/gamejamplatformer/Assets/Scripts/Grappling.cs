using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [SerializeField] float grappleTimer = 3f;
    [SerializeField] float maxDist = 4f;

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
    }

    void Update()
    {
        animation.SetBool("Swing", isGrappling);
        foreach (var gObject in test)
        {
            var dist = Vector2.Distance(transform.position, gObject.transform.position);
            if (dist < maxDist)
            {
                test1 = gObject;
            }
        }
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
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void DontGrapple()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        isGrappling = false;
        grappleTimer = initialTimer;
    }

    private bool ShouldntGrapple()
    {
        return Input.GetMouseButtonUp(0) || grappleTimer <= 0;
    }

    void Grapple()
    {
        lineRenderer.SetPosition(0, test1.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        distanceJoint.connectedAnchor = test1.transform.position;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
        isGrappling = true;
    }

    bool ShouldGrapple()
    {
        return Input.GetMouseButtonDown(0) && grappleTimer > 0 && test1 != null;
    }
}
