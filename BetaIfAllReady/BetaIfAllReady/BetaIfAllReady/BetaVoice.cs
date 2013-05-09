using System;
using System.IO.Ports;
using System.Diagnostics;


namespace BetaIfAllReady{

	public class BetaVoice{


		//Attribut
		BetaMove move;
		BetaInit init;
		Stopwatch timeElaps;
       
		//Constructeur
		public BetaVoice (BetaInit b ){
			init = b;
			move = new BetaMove(init);
			timeElaps = new Stopwatch();
			timeElaps.Reset();
           
		}

		//Requette




		public void voiceCommander_OrderDetected (string order){

			 

			switch (order) {
				case "start":
                    
					//init.getPort().Write("a");
                    Console.WriteLine("d");
                    
					break;
                        
				case "stop":
                    
                       
                        //init.getPort().Write("z");
                        Console.WriteLine("z");
                    
                    break;
				case "move":
                   
                        while (timeElaps.ElapsedMilliseconds <= 10000)
                        {
                            timeElaps.Start();
                            init.getKinectSensor().SkeletonFrameReady +=
                            move.kinect_SkeletonFrameReady;
                            timeElaps.Stop();
                        }
                        timeElaps.Reset();
                    
   					break;

				default:

					break;
			}
		}	
	}
}

