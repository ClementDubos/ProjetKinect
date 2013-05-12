using System;
using Kinect.Toolbox;
using Kinect.Toolbox.Voice;
using System.IO.Ports;
using Microsoft.Speech;
using Microsoft.Speech.Synthesis;
using System.Diagnostics;


namespace ProjetKinect{
	//Classe principale
	public class MainClass{

		//Constructeur
		public MainClass(){
		}


		//Main , point d'entrée du Programme
		static void main(string[] Args){
			//Instanciation d'un objet Init et Voice => voir Class
			Init init = new Init();
			Voice voice = new Voice(init);
			//on initialise les composants a partir de la méthode initializ
			//de l'objet init
			init.initializ();
			//on instancie un objet VoiceCommander avec les 3 ordres
			//a reconnaitre
			VoiceCommander voiceCommander =
			new VoiceCommander("open", "close", "move");
			//On implémente voiceCommander_OrderDetected par l'Action
			//OrderDetected : si un ordre est reconnu on lance la méthode
       	 	voiceCommander.OrderDetected += 
				voice.voiceCommander_OrderDetected;
			//On démare la reconnaissance vocale.
       	 	voiceCommander.Start(init.getKinectSensor());
			//Boucle d'attente.
        	while (true){
        	}
		}

	}

}



