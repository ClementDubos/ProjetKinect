using System;
using Kinect.Toolbox;
using Kinect.Toolbox.Voice;
using System.IO.Ports;
using Microsoft.Speech;
using Microsoft.Speech.Synthesis;
using System.Diagnostics;


namespace BetaIfAllReady{

	public class BetaIfAllReadyMain{

		//Attribut





		//Constructeur
		public BetaIfAllReadyMain (){


		}


		//Main
		static void Main (){
		
		BetaInit init = new BetaInit();
		BetaVoice voice = new BetaVoice(init);
		init.initializ();
		VoiceCommander voiceCommander =
		new VoiceCommander("start", "stop", "move");
		voiceCommander.Start(init.getKinectSensor());
        while (true)
        {
            voiceCommander.OrderDetected += voice.voiceCommander_OrderDetected;
        }
		}

	}

}



