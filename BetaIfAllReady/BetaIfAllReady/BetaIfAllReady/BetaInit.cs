using System;
using Microsoft.Kinect;
using System.IO.Ports;

namespace ProjetKinect{
	//Class d'initialisation des composant
	public class Init{

		//Attribut
		KinectSensor sensor ;
		SerialPort port;
		//Constructeur
		public Init(){    
		}

		//Requette
		public KinectSensor getKinectSensor (){
			//retourne la kinect pour une utilisation dans d'autre classe
			return sensor;		
		}

        public SerialPort getPort(){
			//retourne le port Com3 pour une utilisation dans d'autre classe
            return port;
        }

		//Methode d'initialisation
		public void initializ (){
			//on associe a port , le port USB COM3
			port = new SerialPort ("COM3", 9600);
			//On ouvre le port pour l'envoie des données
			port.Open ();
			//On recupere la Kinect
			sensor = KinectSensor.KinectSensors[0];

 
   			// SmoothParamater pour reduire les interferances sur le SkeletonStream
            TransformSmoothParameters parameters = new TransformSmoothParameters{
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
			//On demare la capture
			sensor.Start();
  			// Ouverture des flux
			sensor.ColorStream.Enable();
			sensor.DepthStream.Enable();
			sensor.SkeletonStream.Enable(parameters);
			//on ouvre une frame toute les 33 millisecondes
			//soit environ 30 images secondes.
            sensor.SkeletonStream.OpenNextFrame(33);
			//le Tracking du skelette est réalisé par défault:
				//20 joints , quelque soit la position de l'utilisateur.
			sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
			return;
		}
	}
}

