using System;
using System.IO.Ports;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace BetaIfAllReady{

	public class BetaVoice{


		//Attribut
        SpeechSynthesizer synth;
		BetaMove move;
		BetaInit initi;
		Stopwatch timeElaps;
        bool etat;
        SerialPort port;
        
		//Constructeur
		public BetaVoice (BetaInit b ){
            port = new SerialPort("COM3", 9600);
			initi = b;
            etat = false;
            move = new BetaMove(initi);
			timeElaps = new Stopwatch();
			timeElaps.Reset();
            synth = new SpeechSynthesizer();
            
		}

		//Requette



       public void voiceCommander_OrderDetected(string order)
        {



            switch (order)
            {

                case "open":
                    if (!etat)
                    {
                        etat = true;
                        initi.getPort().Write("a");
                        Console.WriteLine("a");

                    }
                    break;
                case "close":
                    if (etat)
                    {
                        etat = false;
                         initi.getPort().Write("z");
                        Console.WriteLine("z");
                    }
                    break;
                case "move":
                    if (etat)
                    {

                        synth.Resume();
                        synth.Speak("placez vous devans la kinect");
                        synth.Pause();
                        initi.getKinectSensor().SkeletonFrameReady +=
                            move.kinect_SkeletonFrameReady;
                       
                            while (timeElaps.ElapsedMilliseconds <= 5000)
                            {
                                timeElaps.Start();

                                timeElaps.Stop();
                            }

                            timeElaps.Reset();
                            initi.getKinectSensor().SkeletonFrameReady -=
                                move.kinect_SkeletonFrameReady;
                            Speaking();
                            
                        }
                    
                    else{
                        synth.Resume();
                        synth.Speak("Kinect éteinte");
                        synth.Pause();
                    }
                                     
                    break;
                default:
                    break;
            }
        }

       public void Speaking()
       {
           synth.Resume();
           synth.Speak("reglage terminé");
           synth.Pause();
       }
	}
}

