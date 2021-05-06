using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;

public class terrain_load_save : MonoBehaviour
{
    [SerializeField]
    Transform SaveTerrainPanel;

    [SerializeField]
    Transform LoadTerrainPanel;

    [SerializeField]
    public TerrainGenerator gen;

    [SerializeField]
    public GameObject saveInputField;

    public GameObject loadFileName;
    public Dropdown loadFileType;
    public Dropdown loadMode;
    public GameObject loadHeight;
    public GameObject loadWidth;

    //This loadedTerrain was used to do RAW files, however there was not time to implement the ability to compare the loaded terrain and current terrain
    //public Terrain loadedTerrain;

    public void Update()
    {
      if (SaveTerrainPanel.gameObject.activeSelf == false && LoadTerrainPanel.gameObject.activeSelf == false) {
        if (Input.GetKeyDown(KeyCode.S))
            terrainSave();
        if (Input.GetKeyDown(KeyCode.L))
            gen.loadTerrain = true;
        if (Input.GetKeyDown(KeyCode.Q))
            gen.loadTerrain = false;
      }
    }

    public void terrainSave()
    {
      // Save
      string filename = "output";

      // Get filename from user
      if (saveInputField.GetComponent<Text>().text != "") {
        filename = saveInputField.GetComponent<Text>().text;
      }

      Texture2D output = gen.heightmapFromSensor;
      byte[] b = output.EncodeToPNG();
      File.WriteAllBytes(Application.dataPath + "/Output/" + filename + ".png", b);

      Debug.Log("Saved the heightmap to a file called " + filename + " at: " + Application.dataPath + "/Output!");
    }

    // User needs to input:
    // The file path
    // The fileType (raw or png)
    // The resolution (height and width) of the heightmap. If bigger than the terrain, it will be cut off
    // The mode (8-bit or 16-bit)
    public void terrainLoad()
    {
        // Load
        string filePath = Application.dataPath + "/Output/" + "output.png";
        string fileType = "PNG";
        string mode = "16-bit";
        // height and width of the heightmap. Needs to be less than the heightmap resolution of the terrrain
        int h = 212;
        int w = 256;

        gen.loadTerrain = true;

        float[,] heightData = new float[h, w];

        // Grab user data from UI
        if (loadFileName.GetComponent<Text>().text != "") {
          filePath = Application.dataPath + "/Output/" + loadFileName.GetComponent<Text>().text;
        }
        fileType = loadFileType.options[loadFileType.value].text;
        mode = loadMode.options[loadMode.value].text;
        if (loadHeight.GetComponent<Text>().text != "") {
          h = int.Parse(loadHeight.GetComponent<Text>().text);
        }
        if (loadWidth.GetComponent<Text>().text != "") {
          w = int.Parse(loadWidth.GetComponent<Text>().text);
        }

        if (fileType == "PNG")
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            var tex = new Texture2D(h, w);
            tex.LoadImage(fileData);

            //Use this if doing terrain, but we are doing heightmaps and textures now
            /*
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    heightData[y, x] = tex.GetPixel(y, x).r;
                }
            }
            */

            gen.loadedHeightmap = tex;
        }

        //This currently does not work because we are using textures now
        //To use this feature, there needs to be a way to color the sand by comparing the loadedTerrain and current terrain
        /*
        if (fileType == "raw")
        {
            using (var file = System.IO.File.OpenRead(filePath))
            using (var reader = new System.IO.BinaryReader(file))
            {
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        float currHeight;

                        // Perform correct conversion based on bit type
                        if (mode == "16-bit")
                        {
                            currHeight = (float)reader.ReadUInt16() / 0xFFFF;
                        }
                        else
                        {
                            currHeight = (float)reader.ReadByte() / 0xFF;
                        }

                        heightData[y, x] = currHeight;
                    }
                }

            }
        }

        loadedTerrain.terrainData.SetHeights(0, 0, heightData);
        */
    }

    public void terrainLoadStop() {
      gen.loadTerrain = false;
    }
}
