using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace ProjetKinect{
	//Classe de Gestion de la commande vocale
	public class Voice{


		//Attribut
		bool etat;
        SpeechSynthesizer synth;
		Stopwatch timeElaps;
        SerialPort port;
        Move move;
		Init init;

		//Constructeur
		public Voice (Init b ){
            port = new SerialPort("COM3", 9600);
			init = b;
            etat = false;
            move = new Move(init);
			timeElaps = new Stopwatch();
			timeElaps.Reset();
            synth = new SpeechSynthesizer();
            
		}

		//Methode principale , s'execute quand un ordre est détécté
       public void voiceCommander_OrderDetected(string order){

            switch (order){

				//si "order" = "open"
                case "open":
					//et que le systeme n'a pas été démarré
                    if (!etat){

						//on attribut la valeur true a etat pour
						//signaler l'activation du systeme
                        etat = true;

						//on écrit "a" sur le port USB
                        init.getPort().Write("a");
                        Console.WriteLine("a");
					}

					//on sort de la methode
                    break;

				//Si "order" = "close"
                case "close":

					//Si le systeme a été activé 
                    if (etat){

						//on attribut la valeur false a etat pour
						//signaler l'arret du systeme
                        etat = false;

						//on écrit "z" sur le port USB
                        init.getPort().Write("z");
                        Console.WriteLine("z");
                    }

					//On sort de la méthode
                    break;

				//Si "order" = "move"
                case "move":

					//et si le systeme est actif
                    if (etat){

						//on lance un message vocale
                        synth.Resume();
                        synth.Speak("placez vous devans la kinect");
                        synth.Pause();

						//puis , si un skelette est détecté on active
						//move.kinect_SkeletonFrameReady
                        init.getKinectSensor().SkeletonFrameReady +=
                            move.kinect_SkeletonFrameReady;

                       //temps de gestion de l'éclairage ~ 10 seconde
							//on compte l'execution du kinect_SkeletonFrameReady;
                        while (timeElaps.ElapsedMilliseconds <= 5000){
                        
                        	timeElaps.Start();

                            timeElaps.Stop();
                      	}
						//on remet a 0 le timer
                       	timeElaps.Reset();

						//on désimplémente pour arrété de lancé kinect_SkeletonFrameReady;
						//meme si un squelette est détecté
                        init.getKinectSensor().SkeletonFrameReady -=
                             move.kinect_SkeletonFrameReady;

						//message de fin de gestion par geste
                        synth.Resume();
          			 	synth.Speak("reglage terminé");
           				synth.Pause();
                            
                	}else{
						//gestion impossible si le system est éteint
							//message vocale.
                        synth.Resume();
                        synth.Speak("Kinect éteinte");
                        synth.Pause();
                    }

                    //On sort de la méthode                 
                    break;
                default:
                    break;
            }
	    }  
	}
}

