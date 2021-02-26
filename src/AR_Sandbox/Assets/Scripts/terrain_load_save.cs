using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class terrain_load_save : MonoBehaviour
{
    public TerrainGenerator gen;
    public bool save = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            terrainSave();
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            terrainLoad();
        }
    }

    public void terrainSave()
    {
        // Save
        Debug.Log("saving the heightmap to a file at:" + Application.dataPath + "/Output! Yay!");
        save = false;
        Texture2D output = gen.heightmapFromSensor;
        byte[] b = output.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Output/output.png", b);
    }

    public void terrainLoad()
    {
        // Load
        Debug.Log("Loading the heightmap to a file at:" + Application.dataPath + "! Yay!");
    }


}
