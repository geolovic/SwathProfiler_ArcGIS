# SwathProfiler (ArcGIS 10.1 +)

The SwathProfiler is an ArcGIS Add-In to extract swath profiles. It works for ArcGIS version up to 10.1. 

## Instalation
To install them simply double click the Add-In file **SwathProfiler_ArcMap_Addin.esriAddIn**. It is not necessary to keep the Add-In file in the computer, it will be copied to *"<user_directory>\Documents\ArcGIS\AddIns"*.

Once installed they should appear in the Add-In Manager of ArcMap , under *Menu > Customize > Add-In Manager*.

<img src="https://geolovic.github.io/SwathProfiler_ArcGIS/images/swath_addin.jpg" />

To add tool:

1. Go to *Menu > Customize > Customize Mode*. 
2. Select the *Commands tab* and locate the **"Geomorphic Indexes"** category. 
3. Drag and drop the command button to any toolbar of ArcMap

<img src="https://geolovic.github.io/SwathProfiler_ArcGIS/images/swath_command.jpg" />

## Usage

<img src="https://geolovic.github.io/SwathProfiler_ArcGIS/images/swath_inputbox.jpg" />

To use the Add-In simply click on the added button and the program will show an inputbox to select the input parameters:
+ **Line feature class:** Input **line shapefile**. Each line in this shapefile will be considered as a swath base-line
+ **Digital Elevation Model:** Input **raster** with elevations
+ **Strip width:** Total width for the swath. I.e. the resulted swath will be a buffer of *length = (strip width / 2)* centered in each base-line
+ **Step size (optional):** Step size to take elevation points in each profile. By default, it will take 1,5 * DEM cellsize
+ **Number of profiles (optional):** Number of parallel profiles for each swath. By default, it will generate 50 profiles (25 to each side of the base-line).
+ **Only selected:** Only extract Swath profiles for selected features of the line shapefile

Once the profiles are extracted, the main window will display the profiles. 

<img src="https://geolovic.github.io/SwathProfiler_ArcGIS/images/swath_main_window.jpg" />

This main window has the following parts:
1. Swath profile area (with the max, min and mean elevation, Q1 and Q3 quartiles and profile data. 
2. THi profile (see Pérez-Peña et al., 2017 for details)
3. Combo-box to display the extracted swath profiles. The *Autoscale axis* check-box recalculates horizonal and vertical scales to fit each profile within the window. 
4. Check-boxes to select which data display in the main window
5. Buttons to save and export data. The properties button allows to select different display properties.

<img src="https://geolovic.github.io/SwathProfiler_ArcGIS/images/swath_properties.jpg" />

## References
Pérez-Peña, J. V., Al-Awabdeh, M., Azañón, J. M., Galve, J. P., Booth-Rea, G., & Notti, D. (2017). SwathProfiler and NProfiler: Two new ArcGIS Add-ins for the automatic extraction of swath and normalized river profiles. Computers & Geosciences, 104(135), 150. https://doi.org/10.1016/j.cageo.2016.08.008


