using System;
using Microsoft.Kinect;
using System.Linq;
using System.Speech.Synthesis;

namespace BetaIfAllReady{

	public class BetaMove{

		//Attribut
		const int skeletonCount = 6; 
		Skeleton[] allSkeletons;
		private int i;
		private double m;
		private double rlX;
		private double rlY;
		private double rlZ;
		private double distance;
		private double ascii;
		private double rightX;
		private double rightY;
		private double rightZ;
		private double leftX;
		private double leftY;
		private double leftZ;
		private double newDistance;
		private double oldDistance;
		private char c;
		SpeechSynthesizer synthetic ;

		BetaInit init = new BetaInit();

		public BetaMove (BetaInit b){
			allSkeletons = new Skeleton[skeletonCount];
			init = b;
			synthetic = new SpeechSynthesizer();
			ascii = 0;
		}



		public void kinect_SkeletonFrameReady 
			(object sender, SkeletonFrameReadyEventArgs e)
		{ 
			// This is YOUR KinectSensor's SkeletonFrameReady event handler.
			// You do your thing here. When you want to test for a gesture match do this:

			// Recupere la Frame du squelette
			using (SkeletonFrame frame = e.OpenSkeletonFrame ()) {

				//Tableau de squelettes de taille :"nombre de squelette traqué


				// on envoie les donné correspondante au squelette dans le tableau.
				frame.CopySkeletonDataTo (allSkeletons);
				//si il n'y a pas de tracking , on quitte le Delegate.
				if (frame == null) {
					return;
				}
				//pour chaque squelette , si il est traqué, on identifie les mains


				Skeleton skele = (from s in allSkeletons				                  	
                                  where s.TrackingState == 
				                  SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault ();
				if (skele.TrackingState == SkeletonTrackingState.Tracked) {

					// recupere les articulations main droite ,main gauche
					Joint rightHand = skele.Joints [JointType.HandRight];
					Joint leftHand = skele.Joints [JointType.HandLeft];
					//Instanciation d'un objet Move_test
							

					newDistance = handPos (rightHand, leftHand);
					//c recupere le charactere de code ascii correspondant.
					c = (char)(moveTranslate (newDistance));
					//boucle de traitement.


					//ecriture de c sur l'invite de commande
					//init.getPort ().Write (c + "");
                    Console.WriteLine(""+c);

					//recupération des distance.
					oldDistance = newDistance;
					newDistance = handPos (rightHand, leftHand);
					if (newDistance > 1) {
						return;
					}
					//si l'ancienne Distance est differente de la nouvelle
					if (newDistance != oldDistance) {
					//on affecte a c le code ascii de la valeur associé
							// a newDistance par moveTranslate
					c = (char)(moveTranslate (newDistance));
					
						 
					}//Sinon c reste inchangé.
						//on reteste le tracking du squelette



							
				} else {
					synthese ();
					return;

				}
			}
		}


			
		

		public void synthese(){
		
			synthetic.Resume();//relance
		
			synthetic.SetOutputToDefaultAudioDevice();
			synthetic.Speak("you are not in the fields of kinect");
			synthetic.Pause();//met en pause

			return;
		}

	

		//Methode handPos() qui recupere la distance entre les deux mains
		public double handPos (Joint rightHand , Joint leftHand){


    		
    		rightX = rightHand.Position.X;
    		rightY = rightHand.Position.Y;
    		rightZ = rightHand.Position.Z;
			leftX = leftHand.Position.X;
			leftY = leftHand.Position.Y;
			leftZ = leftHand.Position.Z;

			rlX = Math.Pow((rightX - leftX),2);
			rlY = Math.Pow((rightY - leftY),2);
			rlZ = Math.Pow((rightZ - leftZ),2);
			distance = Math.Sqrt(rlX+rlY+rlZ);
			// Transforme les coordonnées pour simplifer le calcul de Distance
			return distance;

		}

		//traduit la distance en une valeur ascii pour l'Arduino
		public double moveTranslate(double a){



			//si la distance est supérieur a 1 cm
			if(a > 10){


				//on teste les cas jusqu'a 1 m
				for( i = 20 ; i <= 100 ; i = i+10){
					m =i/100;
					//si cette valeur est inferieur ou egale a m;
					if(m.CompareTo(a)>= 0){

						//on effectue l'attribution.
						ascii = 96+i/10;
						return ascii ;
					}
				}
				//si elle est supérieur a 1m
				//Les Led sont toute allumé
				ascii = 106;
				return ascii;
			} else {
				//sinon , elle est inferieur a 1
				//et elles sont toute eteinte.
				ascii = 97;
				return ascii;
			}

		}
	}
}