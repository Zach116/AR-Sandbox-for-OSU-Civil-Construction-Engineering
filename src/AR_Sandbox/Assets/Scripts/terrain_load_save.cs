using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;

public class terrain_load_save : MonoBehaviour
{
    public TerrainGenerator gen;
    public bool save = false;
    public GameObject saveInputField;


    // Should only use keyboard shortcut for save and shoutl be CTRL+S, does not make sense to have a shortcut for load
    /*
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
    */

    public void terrainSave()
    {
      // Save
      string filename = "output";

      // Get filename from user
      if (saveInputField.GetComponent<Text>().text != "") {
        filename = saveInputField.GetComponent<Text>().text;
      }

      save = false;
      Texture2D output = gen.heightmapFromSensor;
      byte[] b = output.EncodeToPNG();
      File.WriteAllBytes(Application.dataPath + "/Output/" + filename + ".png", b);

      Debug.Log("Saved the heightmap to a file called " + filename + ".png at:" + Application.dataPath + "/Output! Yay!");
    }

    public void terrainLoad()
    {
        // Load
        Debug.Log("Loading the heightmap to a file at:" + Application.dataPath + "! Yay!");
    }


}
