using System;
using Microsoft.Kinect;
using System.Linq;
using System.IO.Ports;

namespace ProjetKinect{
	//Class de traitement de la commande par gestes.
	public class Move{

		//Attribut
		char c;
		const int skeletonCount = 6; 
       	int k;
		int ascii;
		float m;
		float newDistance;
		float distance;
        float rightX;
        float rightY;
        float rightZ;
        float leftX;
        float leftY;
        float leftZ;
        float rlX;
        float rlY;
        float rlZ;
        Joint rightHand;
        Joint leftHand;
        Init init;
		Skeleton[] allSkeletons;
       
		//Constructeur.
		public Move (Init b){
			init = b;
			allSkeletons = new Skeleton[skeletonCount];
			ascii = 0;
            }

		//Event lorsque le skelette et detecter et le move activer
        public void kinect_SkeletonFrameReady(object sender,
                                            SkeletonFrameReadyEventArgs e){            
			//Recupération de la frame
            using (SkeletonFrame frame = e.OpenSkeletonFrame()){

				//si la frame est vide on quitte
                if (frame == null){
                    return;

                }else{
					//Sinon on recupere les squelettes disponibles dans
						//la frame
                    frame.CopySkeletonDataTo(allSkeletons);

					//pour chaque skelette dans le tableau allSkeletons
                    foreach (Skeleton skele in allSkeletons){

						//Si il est traqué
                        if (skele.TrackingState == SkeletonTrackingState.Tracked){
                           // et non vide
                            if (skele != null){

								//on recupere les deux mains
                                rightHand = skele.Joints[JointType.HandRight];
                                leftHand = skele.Joints[JointType.HandLeft];

								//si les deux mains sont traqué
                                if (rightHand.TrackingState ==
                                        JointTrackingState.Tracked
                                   && leftHand.TrackingState ==
                                        JointTrackingState.Tracked){
								
									//On recupere la distance entre les deux(en metre)
                                    newDistance = handPos(rightHand, leftHand);

									//puis le caractère Ascii correspondant
                                    c = (char)(moveTranslate(newDistance));
								
									//On l'ecrit sur la Console et le port USB
                                    Console.WriteLine(c + "");
                                    init.getPort().Write(""+c);
                                }
                            }  
                        }
					}
                }
            }
        }


			
		



	

		//Methode handPos() qui recupere la distance entre les deux mains
		public float handPos (Joint rightHand , Joint leftHand){

			//On recupere les coordonnées de chaque mains
    		rightX = rightHand.Position.X;
    		rightY = rightHand.Position.Y;
    		rightZ = rightHand.Position.Z;
			leftX = leftHand.Position.X;
			leftY = leftHand.Position.Y;
			leftZ = leftHand.Position.Z;
			//calcule intermédiaire pour la distance
			rlX =(float) Math.Pow((rightX - leftX),2);
			rlY = (float)Math.Pow((rightY - leftY),2);
			rlZ =(float) Math.Pow((rightZ - leftZ),2);
			//Calcule de la distance
			distance =(float) Math.Sqrt(rlX+rlY+rlZ);
			//On retourne la distance
			return distance;

		}

		//traduit la distance en une valeur ascii pour l'Arduino
        public int moveTranslate(float a){

            //si la distance est supérieur a 20 cm
            if (a > 0.2){
				//on recupere la valeur de a en centimetre
                m = a * 100;
				//si elle est plus grande que 110 cm
                if (m >= 110){
					//on retourne la valeur ascii 106
                    ascii = 106;
                    return ascii;
				//Sinon on recupere la valeur correspondante
                }else{
                    k = (int)m / 10;
                    ascii = 96 + k;
                    return ascii;
                }
            }else{
           		//si elle est comprise entre 0 et 20 cm
				//on retourne le code ascii de a
				ascii = 97;
                return ascii;
            }
        }
	}
}