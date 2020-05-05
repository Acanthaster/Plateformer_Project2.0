using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELC_DontFallAnymore : MonoBehaviour
{
    RaycastHit2D uponTheGroundHit;

    [SerializeField]
    public bool playerIsInGround;


    [SerializeField]
    private float underRayLenght;
    [SerializeField]
    private float underRayPositionY;
    [SerializeField]
    private float underRayPositionX;     /*En faisant varier ces 3 valeurs dans l'inspector, vous pouvez contrôler la longueur, la position en X et la position en Y du raycast
                                         Faites en sorte que ce raycast soit juste au dessus de votre raycast de détection du sol*/

    public LayerMask collisionMask;             /*Si vous en avez un pour détecter vos plateformes dans votre script de déplacement de base, mettez votre LayerMask depuis l'inspector*/
   
    private void FixedUpdate()
    {
  
        if (playerIsInGround == true)
        {
            transform.localPosition = transform.localPosition + (new Vector3(0f, 0.5f, 0f)); //Le joueur montera tant que ce raycast détecte le sol : si il ne le détecte plus, alors le joueur n'est plus dans le sol et se stabilise
            Debug.Log("Go Up ! ");
        }

        InGroundCheck();

    }

    private void InGroundCheck()
    {
        Vector3 startPositionRaycastUponTheGround = new Vector3(transform.position.x - underRayPositionX, transform.position.y - underRayPositionY, transform.position.z); /*Pour déterminer la position de départ du raycast 
                                                                                                                                                                            pour qu'il ait pour référence le centre du player 
                                                                                                                                                                           et que vous puissiez modifier les valeurs plus facilement*/
        /*Debug.DrawRay(startPositionRaycastUponTheGround, transform.TransformDirection(new Vector2(underRayLenght, 0)), Color.green);
        uponTheGroundHit = Physics2D.Raycast(startPositionRaycastUponTheGround, transform.TransformDirection(new Vector2(1f, 0f)), underRayLenght, collisionMask);
        Debug.Log("Yes or not : " + uponTheGroundHit.collider.name);

        if (uponTheGroundHit.collider != null) //Si il détecte quelque chose qui est dans son LayerMask alors il prévient que le joueur est dans le sol.
        {
            playerIsInGround = true;
            Debug.Log("ON GROUND !");
        }
        else
        {
            playerIsInGround = false;
        }*/

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.9f, collisionMask);
        Debug.DrawRay(transform.position, new Vector3(0, transform.position.y - 0.9f, transform.position.z), Color.blue);

        if (hit.collider != null)
        {

             Debug.Log("HIT !");
            playerIsInGround = true;
            Debug.Log("ON GROUND !");
        } else
        {
            playerIsInGround = false;

        }
    }
}
