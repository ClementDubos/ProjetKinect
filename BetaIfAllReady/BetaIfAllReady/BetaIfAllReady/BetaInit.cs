using System;
using Microsoft.Kinect;
using System.IO.Ports;

namespace BetaIfAllReady{

	public class BetaInit{

		//Attribut
		KinectSensor sensor ;
		SerialPort port;
		//Constructeur
		public BetaInit() {
            
		}

		//Requette
		public KinectSensor getKinectSensor (){
		
			return sensor;
		
		}

        public SerialPort getPort()
        {
            return port;
        }


		public void initializ (){
			port = new SerialPort ("COM3", 9600);
			port.Open ();
			sensor = KinectSensor.KinectSensors[0];

 
   		// SmoothParamater pour éliminer le bruit
            TransformSmoothParameters parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
			sensor.Start();
  			// Ouverture des flux
			sensor.ColorStream.Enable();
			sensor.DepthStream.Enable();
			sensor.SkeletonStream.Enable(parameters);
            sensor.SkeletonStream.OpenNextFrame(33);
			sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;

			//Si un squelette est détecter , on lance le Delegate de l'event
			//SkeletonFrameReadyEventArgs


			return;
		}
	}
}

