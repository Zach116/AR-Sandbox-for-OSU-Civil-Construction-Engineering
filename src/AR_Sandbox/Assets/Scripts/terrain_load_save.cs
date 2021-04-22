using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;

public class terrain_load_save : MonoBehaviour
{
    [SerializeField]
    public TerrainGenerator gen;

    [SerializeField]
    public GameObject saveInputField;

    public bool save = false;

    public Terrain loadedTerrain;

    // Should only use keyboard shortcut for save and shoutl be CTRL+S, does not make sense to have a shortcut for load

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            terrainSave();
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

      save = false;
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
        string filePath = Application.dataPath + "/HeightMaps/" + "testing.raw";
        string fileType = "raw";
        string mode = "8-bit";
        // height and width of the heightmap. Needs to be less than the heightmap resolution of the terrrain
        int h = 700;
        int w = 700;

        float[,] heightData = new float[h, w];

        if (fileType == "png")
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            var tex = new Texture2D(h, w);
            tex.LoadImage(fileData);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    heightData[y, x] = tex.GetPixel(y, x).r;
                }
            }
        }

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
    }
}
