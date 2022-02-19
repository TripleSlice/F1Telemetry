# F1T Installer

In order to import this project the following steps must be followed:

 - Download and install WiX Toolset (https://wixtoolset.org/)
 - Install WiX Toolset extension for your Visual Studio instance (https://marketplace.visualstudio.com/items?itemName=WixToolset.WiXToolset)

To create a new installer:

 - Change the "Version" tag in the first "Product" attribute to be in line with the F1T version
 - Run the project in Release mode like any other project

NOTE: The F1T Installer project will automatically build the F1T project.