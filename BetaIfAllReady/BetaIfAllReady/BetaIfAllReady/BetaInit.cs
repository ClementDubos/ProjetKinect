using System;
using Microsoft.Kinect;
using System.IO.Ports;

namespace BetaIfAllReady{

	public class BetaInit{

		//Attribut
		KinectSensor sensor ;
		//SerialPort port;
		//Constructeur
		public BetaInit() {

		}

		//Requette
		public KinectSensor getKinectSensor (){
		
			return sensor;
		
		}

		/*public SerialPort getPort (){

			return port;
		}*/


		public void initializ (){
			//port = new SerialPort ("COM3", 9600);
			//port.Open ();
			sensor = KinectSensor.KinectSensors[0];

 
   		// SmoothParamater pour éliminer le bruit
  			TransformSmoothParameters parameters =
			new TransformSmoothParameters{
   			  Correction = 0.5f,
     	      JitterRadius = 0.05f,
              MaxDeviationRadius = 0.05f,
              Prediction = 0.5f,
              Smoothing = 0.5f 
    		};
			sensor.Start();
  			// Ouverture des flux
			sensor.ColorStream.Enable();
			sensor.DepthStream.Enable();
			sensor.SkeletonStream.Enable(parameters);
			sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;

			//Si un squelette est détecter , on lance le Delegate de l'event
			//SkeletonFrameReadyEventArgs


			return;
		}
	}
}

