﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Windows.Kinect;
using System;

[RequireComponent(typeof(Mesh))]
public class TerrainGenerator : MonoBehaviour {
	public Texture2D heightmap;
    public Texture2D heightmapFromSensor;
    public Texture2D loadedHeightmap;
    public GameObject depthSourceManager;
	public float scale = 10;				// Size of the resulting mesh
	public float magnitude = 1;				// Maximum height of the resulting mesh
	public float maxHeight; 				// Maximum height value from the sensor
	public float minHeight;                 // Minimum height value from the sensor
    public bool loadTerrain = true; // make this a public bool so that it can be changed from UI

    private Mesh mesh;
	private float spacing;					// The distance between vertices in the mesh
	private Vector3[] vertices;
	private int[] triangles;
	private KinectSensor sensor;
	private CoordinateMapper mapper;
	private DepthSourceManager manager;
	public int frameWidth;					// The width in pixels of a frame of depth data
	public int frameHeight;				// The height in pixels of a frame of depth data

	private const int downsampleSize = 2;	// How much to scale down the depth data. 2 = half resolution

	void Start () {
		sensor = KinectSensor.GetDefault ();

		if (sensor != null) {
			mapper = sensor.CoordinateMapper;
			manager = depthSourceManager.GetComponent<DepthSourceManager> ();
			if (manager == null) {
				return;
			}

			mesh = new Mesh ();		// Initialize mesh
			GetComponent<MeshFilter> ().mesh = mesh;

			FrameDescription frameDesc = sensor.DepthFrameSource.FrameDescription;
			frameWidth = frameDesc.Width;
			frameHeight = frameDesc.Height;

			spacing = scale / frameHeight;

			CreateMesh (frameWidth / downsampleSize, frameHeight / downsampleSize);

			if (!sensor.IsOpen) {
				sensor.Open ();
			}
		}
		else {
			mesh = new Mesh();
			GetComponent<MeshFilter> ().mesh = mesh;
			frameWidth  = 100;
			frameHeight = 100;
			spacing = scale / frameHeight;
			CreateMesh (frameWidth / downsampleSize, frameHeight / downsampleSize);
		}
	}

	void Update () {
		UpdateMesh ();
	}

    // Create a new mesh by generating a set of vertices and triangles
	// x and y are the number of width and height vertices
	void CreateMesh(int x, int y) {
        // Initialize vertex and triangle arrays
        vertices = new Vector3[x * y];
		triangles = new int[(y - 1) * (x - 1) * 6];

		// Populate vertex array
		for (int i = 0; i < y; i++) {
			for (int j = 0; j < x; j++) {
				vertices [j + x * i] = new Vector3 (j * spacing, 0, i * spacing);
			}
		}

		int idx = 0; // index for triangle array

		// Populate triangle array
		for (int i = 0; i < y - 1; i++) {
			for (int j = 0; j < x - 1; j++) {

				triangles [idx++] = j + x * i; 				// '
				triangles [idx++] = j + x * (1 + i);		// :
				triangles [idx++] = 1 + j + x * i;	 		// :'

				triangles [idx++] = j + x * (1 + i);		// .
				triangles [idx++] = 1 + j + x * (1 + i); 	// ..
				triangles [idx++] = 1 + j + x * i; 			// .:
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}

	//update the terrain mesh with height data from Kinect sensor
	public void UpdateMesh() {
        heightmapFromSensor = new Texture2D(frameWidth/downsampleSize, frameHeight/downsampleSize, TextureFormat.RGB24, false);
		spacing = scale / frameHeight;


		if (sensor != null) {
			ushort[] heightData = manager.GetData ();

            // Populate vertex array using sensor data
            for (int i = 0; i < frameHeight / downsampleSize; i++) {
				for (int j = 0; j < frameWidth / downsampleSize; j++) {
                    ushort y = heightData [j * downsampleSize + frameWidth * i * downsampleSize];       // Get Y value from Kinect height data
                    float yNorm = ((float)y - maxHeight) / (minHeight-maxHeight);                     // Normalize height
                    Color height = new Color(yNorm, yNorm, yNorm, 1);
                    heightmapFromSensor.SetPixel(j, i, height);
                    // If we are loading terrain
                    if (loadTerrain)
                    {
                        // if current height is higher than the heightmap.
                        if (heightmapFromSensor.GetPixel(j,i).r > loadedHeightmap.GetPixel(j,i).r)
                        {
                            // show blue to lower.
                            yNorm -= 1.0f;
                        }
                        // if current height is lower than the heightmap.
                        else if (heightmapFromSensor.GetPixel(j, i).r < loadedHeightmap.GetPixel(j, i).r)
                        {
                            // show red to raise.
                            yNorm += 1.0f;
                        }
                        // if the current height is the same as the heightmap.
                    }
                    vertices [j + frameWidth / downsampleSize * i] = new Vector3 (j * spacing, yNorm * magnitude, i * spacing);
                }
            }
        }
        else {
			// Populate vertex array using placeholder heightmap for debugging
			for (int i = 0; i < frameHeight / downsampleSize; i++) {
				for (int j = 0; j < frameWidth / downsampleSize; j++) {
					float y = heightmap.GetPixel (i, j).r;	// Get Y value from heightmap
					vertices [j + frameWidth / downsampleSize * i] = new Vector3 (j * spacing, y * magnitude, i * spacing);
				}
			}
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
	}

	// Get height from heightmap at a given world coordinate
	// Very slow if not using sensor data
	public float GetHeightAtWorldPosition(Vector3 pos) {
		ushort[] heightData;
		if (sensor != null) {
			heightData = manager.GetData ();
		} else {
			Color[] pixelData = heightmap.GetPixels ();
			heightData = new ushort[heightmap.width * heightmap.height];
			for (int i = 0; i < (heightmap.width * heightmap.height); i++) {
				heightData [i] = (ushort)(pixelData [i].r * maxHeight);
			}
		}

		Vector3 modelPos = (pos - transform.position);
		Vector3 texelPos = (modelPos / spacing) * downsampleSize;

		if (texelPos.x >= 0 && texelPos.x < frameWidth && texelPos.z >= 0 && texelPos.z < frameHeight) {
			float y = heightData [((int)texelPos.z) * frameWidth + ((int)texelPos.x)];
			return (((float)y - maxHeight) / (minHeight - maxHeight)) * magnitude;
		} else {
			Debug.LogError ("TerrainGenerator: Request for height data returned 0, world position out of range");
			return 0;
		}
	}

	void OnApplicationQuit() {
		if (mapper != null) {
			mapper = null;
		}

		if (sensor != null) {
			if (sensor.IsOpen) {
				sensor.Close();
			}

			sensor = null;
		}
	}
}
