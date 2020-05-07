using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALR_PhysicsConfig : MonoBehaviour
{
  	
  	[Tooltip("Quel(s) layer(s) est considéré comme Ground ?")]
    public LayerMask groundMask;
    [Tooltip("Quel(s) layer(s) comporte le Character ?")]
    public LayerMask characterMask;
    [Tooltip("Avec quel(s) layer(s) le Character peut collisionner ?")]
    public LayerMask characterCollisionMask;

    public LayerMask movingPlatformMask;

    //Parameters
    public float gravity = -30f; // La gravity de base // Voir si on peut l'encadrer
    public float airFriction = 0f; // responsable de la fluidité des mouvements dans l'air ?
    public float groundFriction = 0f; // peut être utile pour l'émulation de l'aspet glissant du sol ?
    //public float staggerSpeedFalloff = 50f; // Pour la reprise de contrôle fluide ? ou l'inverse ?


    void Start()
    {
    	// On initialize les différents layerMask nécessaire
    	// On PEUT en ajouter si besoin
    	// Par exemple, un layerMask s'occupant des collectible objects
        if (groundMask == 0) 
        {
            groundMask = LayerMask.GetMask("TempleGround");
        }

        if (characterCollisionMask == 0) 
        {
            characterCollisionMask = LayerMask.GetMask("TempleGround", "Collectibles", "CheckPoint", "Obstacles");
        }

        if (characterMask == 0) 
        {
            characterMask = LayerMask.GetMask("Player");
        }

        /*if(movingPlatformMask == 0)
        {
            movingPlatformMask = LayerMask.GetMask("Player");
        }*/
    }

}
