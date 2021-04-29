# AR-Sandbox-for-Construction-Planning

The AR sandbox is meant to augment a physical sandbox with additional information overlaid with a projector. Specifically, this project aims to aid in the visualization of civil and construction engineering concepts both for collaboration and teaching.

**NOTE: This project requires a Kinect V2 sensor to operate.**

# SEE HERE FOR THE UPDATED INSTALL GUIDE FOR TRAFFIC SIMULATION FEATURES

https://github.com/spencjon/AR-Sandbox-for-OSU-Civil-Construction-Engineering/blob/master/Documents/2018_Documents/ARSandbox%20Traffic%20Simulation%20Install%20Guide/ARSandbox_Traffic_Simulation_Install_Guide.pdf

# Installation

This project requires the Unity game engine and the Microsoft Kinect SDK to operate. After installing Unity and the Kinect SDK, run the Unity editor. Open a new project, and navigate to the src directory in the local GitHub repository. Unity should recognize the AR_Sandbox directory contained in the src directory as a Unity project. After opening the project, the program can be run in the editor by pressing the play button, or as an executable by navigating to **File > Build & Run** or pressing **CTRL + B**.

# User Manual

## Navigation

The navigation menu is opened by pressing the **ESC** key. The menu gives the option to change mode, calibrate the system, or quit the application. The mode can also be changed by pressing **Q** for depth mode, **W** for design mode, **E** for cut and fill mode, and **R** for calibration mode.

## System Setup

When the system starts, a number of parameters must be set in order to ensure proper height measurement and display. These parameters are set by opening the navigation menu and pressing **Calibrate** or pressing the **R** key. Begin by dragging the gray control points at each edge of the projection area so that the entire surface of the sandbox is covered by the projection. Clicking and dragging anywhere on the projection area itself will move entire projection area. To align the projected image with the physical features of the sandbox, use the **Arrow Keys** to translate and the **+** and **-** keys to scale it. Finally, set the lowest and highest points on the sand by adjusting the two sliders labeled maximum and minimum height that appear in the lower left-hand corner. Dig a hole in the sand down to the bottom of the sandbox, or to the desired lowest point. Build a hill to the highest desired point. Start with the Minimum Height slider at 0 and lower the Maximum Height slider until the lowest point on the sand turns blue, but before cyan equipotential lines appear. Raise the Minimum Height slider until the highest point on the sand turns red, but before orange equipotential lines appear. Once you have calibrated the system, you can click the **Save** button to save your settings and the **Load** button to load your saved settings.

## Depth Mode

Depth mode is used for displaying strictly the height of the sand using a color gradient. Red areas are the lowest, and blue areas are the highest.

## Design Mode

Design Mode is used for designing a road segment that will be used for cut and fill calculations. When Design Mode is selected, a road will appear on the sand. This road is constrained to the bounds of the sandbox. The path of the road can be changed by clicking and dragging the orange diamond shaped control points. Holding the **Shift** key causes a cross-sectional view of the road and terrain to appear, and allows the height of the control points to be changed by clicking and dragging with the mouse. This cross-sectional view can also be toggled on or off using the **H** key. Points can be added or removed by pressing the **Add Point** or **Remove Point** buttons. Unwanted changes to control point positions can be undone by pressing **CTRL + Z**. You can also press the **Z** key to make the road flat which can be useful when testing the sandbox.


## Saving Terrain

Saving the terrain was a feature that we implemented this last year (2020-2021). This feature was implemented to allow users to save the terrain that they had made while playing around with the sand. The goal of preserving this terrain is so that users will be able to save and then load terrain data into the sandbox as they please. To get the save feature to work, all you have to do is start up the system like you usually would with the instructions above. Play around in the sand and make sure everything is working correctly. Once you have a terrain in the sand that you like and want to save, press escape on the computer keyboard to bring up the mode selection menu like you would normally do. Once in the mode selection menu, choose the button that says "Save Terrain". Once this option is clicked, you will see a popout feature that will allow you to enter some text input. This text input will be the name that you want to call your terrain heightmap that you are currently trying to save. An example of a file name could be "firstTerrain". If nothing is entered, the default terrain name is "output". Once you are happy with the name for your terrain data, just simply click the "Save Terrain" button right below. This will save the heightmap data as a PNG file and store it in this file path: AR-Sandbox-for-OSU-Civil-Construction-Engineering/src/AR_Sandbox/Assets/Output/". You can navigate to this file path and see that your terrain data is saved as a PNG. If both the load and save UI panels are closed, the keyboard key "s" can be used to save terrain, just make sure there was something in the text input. Otherwise, it will be called "output".


## Loading Terrain (incomplete)

WARNING: This feature currently does not work as intended. The values of the desired heightmap must be normalized using the current configuration settings of the sandbox. This way, the max height value, min height value, and all values in between will be the same between the desired terrain and the current terrain, allowing for accurate comparisons. Unfortunately, the group implementing the loading terrain feature did not have enough time to accomplish this.

The loading feature was implemented to complement the saving feature. When the terrain is first loaded into the augmentation, you will see various colors on the sand: blue (too low), green (perfect), and even red (too high). In order for you to match the terrain that was just loaded into the sandbox, you have to move and play around with all of the sand until all of the sand is colored green. When the sand is colored green, you will know that you have successfully matched and recreated the terrain you just loaded in.

First, start by starting the sandbox and making sure everything is working correctly, and going through all of the steps above. Also, make sure that you have already saved a terrain so that you will be able to load a terrain into the sandbox. Now that you are ready to load a terrain, open up the mode selection menu by hitting escape, and then navigate to the button that says "Load Terrain". Now you will be prompted to enter a file name. The file path will look something like this: AR-Sandbox-for-OSU-Civil-Construction-Engineering/src/AR_Sandbox/Assets/Output/example.png where "example.png is the name of your terrain that you are trying to load in. The terrain you are trying to load in must be within the "Output" folder. After you have successfully entered the file name, there are a few other options to configure. You must select if the file type is a PNG or Raw. As of now, the sandbox only saves terrain as PNG and does not currently support the loading of Raw files. The bit mode can also be selected but this only pertains to Raw files which are not currently supported. Lastly, the height and width of the terrain to be loaded must be entered. Now, hit the "Load Terrain" button. You should now see the sandbox color change, and you are now ready to start messing with the sand and make it green. Once you are done, you can press the "Stop Loading" button, changing the colors back to normal viewing. Lastly, if both of the save and load terrain UI panels are closed, you can use "l" to bring back the load mode and display how to change the sand. Pressing "q" will stop this.



## Cut/Fill Mode

Cut/Fill Mode is used to display a table containing information about the road segment such as cut and fill areas and volumes. When design mode is selected, the road segment will be visible. To open the cut/fill table, press the **E** key. The table updates every 5 seconds, and can be scrolled using the horizontal scrollbar to the bottom and vertical scrollbar to the right.
