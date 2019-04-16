﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MarkerChangeIntersection : MonoBehaviour
{
    public Material trafficLightMaterial;
    public Material stopSignMaterial;
    public GameObject junctionsParentObject;
    public TraciController traciController;

    private List<GameObject> junctions;
    private MarkerAction markerAction;
    private bool triggerAreasSet;
    private Junction junctionScript;

    void Start()
    {
        markerAction = gameObject.GetComponent<MarkerAction>();
        if (markerAction == null)
        {
            throw new Exception("No Marker Action component found.");
        }

        if (junctionsParentObject == null)
        {
            junctionsParentObject = GameObject.Find("Junctions");
        }

        if (traciController == null)
        {
            traciController = FindObjectOfType<TraciController>();
        }

        junctionScript = junctionsParentObject.GetComponent<Junction>();
        triggerAreasSet = false;
    }
    
    void Update()
    {
        // Wait until we have loaded a network
        if (!triggerAreasSet && junctionsParentObject.transform.childCount > 0)
        {
            junctions = new List<GameObject>(junctionsParentObject.transform.childCount);

            // Populate the list of roads with the child objects of Junctions
            foreach (Transform child in junctionsParentObject.transform)
            {
                // TODO: Do we want to filter the juctions at all? We may want to take out dead ends, for example
                junctions.Add(child.gameObject);
            }

            // Set the trigger actions to the road bounds
            markerAction.AddTriggerAreas(junctions.Select(j => j.GetComponent<MeshRenderer>().bounds));

            triggerAreasSet = true;
        }
    }

    public void SetTrafficLightIntersection(int junctionIndex)
    {
        traciController.SetTrafficLightJunction(junctions[junctionIndex].name);
        
        junctions[junctionIndex].GetComponent<Renderer>().material = trafficLightMaterial;
        Debug.Log($"Set junction {junctions[junctionIndex].name} to traffic light intersection.");
    }

    public void SetStopSignIntersection(int junctionIndex)
    {
        traciController.SetStopSignJunction(junctions[junctionIndex].name);

        junctions[junctionIndex].GetComponent<Renderer>().material = stopSignMaterial;
        Debug.Log($"Set junction {junctions[junctionIndex].name} to stop sign intersection.");
    }
}
