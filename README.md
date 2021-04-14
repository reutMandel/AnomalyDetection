# Anomaly Detection Flight Simulator

 A desktop application to represnt and detect flight simulator features.
 
## Getting Started

The following instructions will guide you how to download the flight simulator and running this application.
 
# Prerequisites
* ### Flight simulator installation
  * Download the latest flight simulator version for your os from the **https://www.flightgear.org/** page.
  * Add the playback_xml file to **$FG_ROOT/data/Protocol** directory.
   Press the settings button and change to the following settings:
    ```bash
      --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
      --fdm=null 
    ```
  * Run the flight simulator by press the Fly button
 * The project written in c# therefore you will need to download the next Nuget: OxyPlot.Wpf
 

# Operating Instructions
  * Run the application 
  * Click the instructions button
  * Upload xml file, anomaly csv file and the learn csv file to the application
    **please upload csv files with header line**.
  * Upload the specific anomaly algorithm dll - "ANOMALYALGORITHM.dll"
   ** the loading can take a few minutes please wait until the show simulator button enable**
  * Click on the "Show Simulator" to start the application

# Project Structure
  The project implements the architecture design pattern of MVVM. Therefore it consists three main components: 
  * **Model** : implements the logic of the application. Has a externalize interface: IFGModel
            Each class in the model hold the relevant properties and handle with the relevant logic.
  * **ViewModel** : implements the logic of the view. Has a basic class : ViewModel 
            Each view logic part implemneted in differnet view model.
  * **View** : represents the UI of the application. The view divide into several user controls

# Class Diagram
  The class diagram has three layres according to mvvm architecture : View, ViewModel, Model
  In order to show the class diagram please install the ClassDiagram extension of visual studio.
  * link to source class diagram: https://github.com/reutMandel/AnomalyDetection/dev/ClassDiagram.cd
  * link to picture: ![ClassDiagram](https://github.com/reutMandel/AnomalyDetection/blob/dev/ClassDiagram.png)
  
# UI Features
 * Start click buttons:
   * Instrucions buttono
   * Load xml file button
   * Load anomaly and learn csv files
   * Load dll algorithm
   * Show simulator - enable only after the application load all the configuration 
 * Tool bar:
   * Slider - allow to skip to any time in the simulator flight
   * Pause and Continue buttons
   * Speed change - allow to increase and decrease speed
 * Display the throttle, rudder, elevator and aielron values 
 * Display the airspeed, direction altimeter, pitch, roll and yaw properties 
 * Display list of fields name. when a field is selected:
   * Display a graph of the field values 
   * Display a graph of the corrlated field values 
   The X-axis represnts the time in seconds
 * Display list of anomalys : each raw in the list conatins two fields seperated by ":" that has been anomaly   between them and the raw number of the anomaly
 * Display a graph according to the dll algorithm that has been loaded. In addition, we dislpay all the points   that belonging to the last 30 seconds of the flight. 

# Example Video
 * Download the main video from the link:  https://github.com/reutMandel/AnomalyDetection/tree/dev/record1.mp4
 * An additional sort video: https://github.com/reutMandel/AnomalyDetection/tree/dev/record2.mp4
