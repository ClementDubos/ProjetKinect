Ce projet a été Réalisé dans le cadre d'un projet de Licence deuxieme année EEA à l'université de Rouen.

Il a pour but de commander un module arduino reliée a un systeme de 10 Led en parallele.

Ecrit en C# DotNET 4.0.

Le principe est de pouvoir allumer et éteindre des Leds a partir de mot , et pouvoir modifier le nombre de LED allumer a partir des mains.

Il reconnait trois mots :
Open : allume une Led
Close : éteint les Leds 
Move: active une reconnaissance des geste modifiant le nombre de Led allumer ( de 1 a 10).

La reconnaissance de geste etablie la distance entre les deux mains et envoie l'ordre correspondant au module arduino:

0 à 20 cm => 1 Led d'allumé

20 à 30 => 2 Led d'allumé

30 à 40 => 3 Led d'allumé

40 à 60 => 4 Led d'allumé

60 à 70 => 5 Led d'allumé

70 à 80 => 6 Led d'allumé

80 à 90 => 7 Led d'allumé

90 cm à 1m =>8 Led d'allumé 

1m à 1m10 => 9 Led d'allumé

+1m10 => 10 Led d'allumé
