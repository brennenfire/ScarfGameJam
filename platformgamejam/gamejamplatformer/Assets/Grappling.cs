using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public List<GameObject> test;
    GameObject test1;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    [SerializeField] float grappleTimer = 3f;
    float initialTimer;
    bool isGrappling;

    private void Start()
    {
        distanceJoint.enabled = false;
        initialTimer = grappleTimer;
    }

    private void Update()
    {
        foreach(var gObject in test)
        {
            var dist = Vector2.Distance(transform.position, gObject.transform.position);
            if(dist < 4)
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
        if (Input.GetMouseButtonUp(0) || grappleTimer <= 0)
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            isGrappling = false;
            grappleTimer = initialTimer;
        }
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
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
