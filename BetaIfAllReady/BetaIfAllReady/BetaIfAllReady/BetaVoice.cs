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

		//Methode principale , s'execute quand un ordre est d�t�ct�
       public void voiceCommander_OrderDetected(string order){

            switch (order){

				//si "order" = "open"
                case "open":
					//et que le systeme n'a pas �t� d�marr�
                    if (!etat){

						//on attribut la valeur true a etat pour
						//signaler l'activation du systeme
                        etat = true;

						//on �crit "a" sur le port USB
                        init.getPort().Write("a");
                        Console.WriteLine("a");
					}

					//on sort de la methode
                    break;

				//Si "order" = "close"
                case "close":

					//Si le systeme a �t� activ� 
                    if (etat){

						//on attribut la valeur false a etat pour
						//signaler l'arret du systeme
                        etat = false;

						//on �crit "z" sur le port USB
                        init.getPort().Write("z");
                        Console.WriteLine("z");
                    }

					//On sort de la m�thode
                    break;

				//Si "order" = "move"
                case "move":

					//et si le systeme est actif
                    if (etat){

						//on lance un message vocale
                        synth.Resume();
                        synth.Speak("placez vous devans la kinect");
                        synth.Pause();

						//puis , si un skelette est d�tect� on active
						//move.kinect_SkeletonFrameReady
                        init.getKinectSensor().SkeletonFrameReady +=
                            move.kinect_SkeletonFrameReady;

                       //temps de gestion de l'�clairage ~ 10 seconde
							//on compte l'execution du kinect_SkeletonFrameReady;
                        while (timeElaps.ElapsedMilliseconds <= 5000){
                        
                        	timeElaps.Start();

                            timeElaps.Stop();
                      	}
						//on remet a 0 le timer
                       	timeElaps.Reset();

						//on d�simpl�mente pour arr�t� de lanc� kinect_SkeletonFrameReady;
						//meme si un squelette est d�tect�
                        init.getKinectSensor().SkeletonFrameReady -=
                             move.kinect_SkeletonFrameReady;

						//message de fin de gestion par geste
                        synth.Resume();
          			 	synth.Speak("reglage termin�");
           				synth.Pause();
                            
                	}else{
						//gestion impossible si le system est �teint
							//message vocale.
                        synth.Resume();
                        synth.Speak("Kinect �teinte");
                        synth.Pause();
                    }

                    //On sort de la m�thode                 
                    break;
                default:
                    break;
            }
	    }  
	}
}

