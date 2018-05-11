﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //Need this for calling UI scripts

public class CutAndFillManager : MonoBehaviour {

    // max points for the road
    const int MAXPOINTS = 20;

    // data table arrays
    int[] station       = new int[MAXPOINTS];
    int[] existGrade    = new int[MAXPOINTS];
    int[] propGrade     = new int[MAXPOINTS];
    int[] roadWidth     = new int[MAXPOINTS];
    int[] cutArea       = new int[MAXPOINTS];
    int[] fillArea      = new int[MAXPOINTS];
    int[] cutVolume     = new int[MAXPOINTS];
    int[] fillVolume    = new int[MAXPOINTS];
    int[] adjFillVolume = new int[MAXPOINTS];
    int[] algebraicSum  = new int[MAXPOINTS];
    int[] massOrdinate  = new int[MAXPOINTS];

    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    [SerializeField]
    Text cutText0, //Will assign our cut Text to this variable so we can modify the text it displays.
         cutText1,
         cutText2,
         cutText3,
         cutText4;

    [SerializeField]
    Text fillText0, //Will assign our fill Text to this variable so we can modify the text it displays.
         fillText1,
         fillText2,
         fillText3,
         fillText4;

    public GameObject terrain;
    private TerrainGenerator terrainHeight;

    public GameObject road;
    private Road roadPoint;

    float timer = 0f;
    float waitingTime = 5f;

    void Start()
    {
        UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts

        // get terrain object
        terrain = GameObject.Find("Terrain");
        // get height function from terrain generator
        terrainHeight = terrain.GetComponent<TerrainGenerator>();

        // get road object
        road = GameObject.Find("Road");
        // get point from road
        roadPoint = road.GetComponent<Road>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        UIPanel.gameObject.SetActive(true); //turn on the pause menu
    }

    public void UnPause()
    {
        UIPanel.gameObject.SetActive(false); //turn off pause menu
    }

    void updateStation()
    {
        station[0] = 0;

        for (int i = 0; i < MAXPOINTS; i++)
        {
            station[i] = station[i - 1] + 100;
        }
    }

    void updateExistGrade()
    {

    }

    void updatePropGrade()
    {

    }

    void updateRoadWidth()
    {

    }

    void updateCutArea()
    {

    }

    void updateFillArea()
    {

    }

    void updateCutVolume()
    {

    }

    void updateFillVolume()
    {

    }

    void updateAdjFillVolume()
    {

    }

    void updateAlgebraicSum()
    {

    }

    void updateMassOrdinate()
    {

    }
}
