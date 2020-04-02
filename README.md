# Universal Controller

## How to run the project

*Run server
    * Create a Mongo database from the existing data, modify __vid__ and __pid__ for device named "Jaco" under _outputData_ in _uc_ collections in the following format:
        ```
        "vid": "<your local USB device vid>"
        "pid": "<your local USB device pid>"
        ```
    * Set the IP Whitelist to your network IP address or 0.0.0.0/0
    * Copy the connection string to _connectionString.txt_ under ./bin/Server folder without any whitespace. 
    * Compile the Server project and build it for debug
    * Run server under administrator mode

* Run desktop UI
    * Run the server under administrator mode
    * Run the desktop UI (UCUI.exe in .\bin folder)
        * Or, alternatively compile the DesktopUI project and build it for debug

## File Structure
This folder contains a single Visual Studio 2019 solution that comprises the a few subprojects in their respective folders. To explore and demonstrate the portability of the project in various platforms, projects are created with different versions of C# frameworks provided by Microsoft.

* JacoDriver
    * This project implements the driver to control the Jaco robotic arm. It implements six different functions for Arm and Wrist mode and four different functions for Finger mode including two addtional features to enter Drinking Mode and to return HOME position. This project is targeted at .Net Standard 2.0 and is cross-platform.

* UCProtocol
    * This project implements internal protocols shared by the universal controller, desktop UI, and are intended for use by plug-in projects as well. It implements internal data representations, IDevice plug-in interface, server core, and internal server definitions. This project is targeted at .Net Standard 2.0 and is cross-platform.

* UCUtility
    * This project implements **WINDOWS-SPECIFIC** components of the universal controller. Currently these include audio playback engine (using NAudio .Net library that uses DirectSound), and a network manager class that executes Windows netsh commands. For the universal controller to work on other platforms, these components have to be re-written for the desired platform. This projectis targeted at .Net Framework 4.6.1.

* Server
    * This project implements the core server of the universal controller. The server interacts with remote database, controls USB and wireless smart devices, receives requests from related local applications. This project is targed at .Net Core 2.1 and is cross-platform.

* USBManager
    * This project implements a **WINDOWS-SPECIFIC** manager application that inspects USB devices and tracks USB events. It is not directly linked to the server in anyway. It communicates with the server via internal http requests and uses message definitions implemented within UCProtocol. This project is targeted at .Net Framework 4.6.1, and it has to be re-written for different platforms. 

* DesktopUI
    * This project implements the **WINDOWS-SPECIFIC** desktop user interface of the universal controller using Windows Presentation Foundation (WPF) framework. It is not directly linked to the server in anyway. It communicates with the server via internal http requests and uses message definitions implemented within UCProtocol. This project is targeted at .Net Framework 4.6.1, and it has to be re-written for different platforms. 

* USBDevices
    * ./JacoInstance folder includes the implementation of the control library used by server to interact with the JacoDriver.
