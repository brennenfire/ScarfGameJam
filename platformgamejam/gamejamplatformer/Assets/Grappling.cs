using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public GameObject test;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private void Start()
    {
        distanceJoint.enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            lineRenderer.SetPosition(0, test.transform.position);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint.connectedAnchor = test.transform.position;
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled= false;
        }
        if(distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
