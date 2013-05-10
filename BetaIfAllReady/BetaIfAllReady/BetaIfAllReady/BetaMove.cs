using System;
using Microsoft.Kinect;
using System.Linq;
using System.IO.Ports;

namespace BetaIfAllReady{

	public class BetaMove{

		//Attribut
		const int skeletonCount = 6; 
		Skeleton[] allSkeletons;
        float distance;
        int ascii;
        float rightX;
        float rightY;
        float rightZ;
        float leftX;
        float leftY;
        float leftZ;
        float rlX;
        float rlY;
        float rlZ;
        int k;
        float m;
		float newDistance;
		
		private char c;
        Joint rightHand;
        Joint leftHand;
        BetaInit init;
       
		public BetaMove (BetaInit b){
			allSkeletons = new Skeleton[skeletonCount];
			init = b;
			ascii = 0;
            }


        public void kinect_SkeletonFrameReady(object sender,
                                            SkeletonFrameReadyEventArgs e)
        {

            

            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {


                if (frame == null)
                {
                    return;
                }
                else
                {
                    frame.CopySkeletonDataTo(allSkeletons);

                    foreach (Skeleton skele in allSkeletons)
                    {
                        if (skele.TrackingState == SkeletonTrackingState.Tracked)
                        {
                           
                            if (skele != null)
                            {
                                rightHand = skele.Joints[JointType.HandRight];
                                leftHand = skele.Joints[JointType.HandLeft];

                                if (rightHand.TrackingState ==
                                        JointTrackingState.Tracked
                                   && leftHand.TrackingState ==
                                        JointTrackingState.Tracked)
                                {

                                    newDistance = handPos(rightHand, leftHand);

                                    c = (char)(moveTranslate(newDistance));
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


    		
    		rightX = rightHand.Position.X;
    		rightY = rightHand.Position.Y;
    		rightZ = rightHand.Position.Z;
			leftX = leftHand.Position.X;
			leftY = leftHand.Position.Y;
			leftZ = leftHand.Position.Z;

			rlX =(float) Math.Pow((rightX - leftX),2);
			rlY = (float)Math.Pow((rightY - leftY),2);
			rlZ =(float) Math.Pow((rightZ - leftZ),2);
			distance =(float) Math.Sqrt(rlX+rlY+rlZ);
			// Transforme les coordonnées pour simplifer le calcul de Distance
			return distance;

		}

		//traduit la distance en une valeur ascii pour l'Arduino
        public int moveTranslate(float a)
        {



            //si la distance est supérieur a 1 cm
            if (a > 0.2)
            {
                m = a * 100;
                if (m >= 110)
                {
                    ascii = 106;
                    return ascii;
                }
                else
                {
                    k = (int)m / 10;
                    ascii = 96 + k;
                    return ascii;
                }

            }
            else
            {
                //sinon , elle est inferieur a 1
                //et elles sont toute eteinte.
                ascii = 97;
                return ascii;
            }



        }
	}
}