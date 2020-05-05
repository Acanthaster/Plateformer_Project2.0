using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ALR_CustomCharacterController))] 

public class ALR_PlayerInputHandler : MonoBehaviour
{

	private ALR_CustomCharacterController charac;
    private ALR_CharacterData cData;
    private AXD_PlayerStatus pStatus;

    bool checkingOnAir = false;
    bool isbufferedJumping = false;
    bool isGhostJumping = false;
    

    float timeSinceJumpInput;
    float timeCheckGhostJump;

    void Start()
    {
        pStatus = GetComponent<AXD_PlayerStatus>();
        charac = GetComponent<ALR_CustomCharacterController>();
        cData = GetComponent<ALR_CharacterData>();
    }

  
    void Update()
    {
        float translation = Input.GetAxis("Horizontal");
        charac.Walk(translation);

        if(checkingOnAir)
        {
      

            if(charac.collisions.onGround)
            {
                //Debug.Log("Pas dans les airs !");

            } 
            
            else if (!charac.collisions.onGround )
            {
                //Debug.Log("Buffering !");
                checkingOnAir = false;
                isbufferedJumping = true;
            }

         
        }

        if(isbufferedJumping)
        {
            timeSinceJumpInput += Time.fixedDeltaTime;
            //Debug.Log("Time Since Input :" + Time.fixedDeltaTime);

            if(charac.collisions.onGround == true && timeSinceJumpInput <= cData.maxBufferedJump)
            {
                charac.Jump();

                timeSinceJumpInput = 0f;
                isbufferedJumping = false;
                //Debug.Log("Buffered Jump !");

            } 
            
            else if (timeSinceJumpInput > cData.maxBufferedJump)
            {
                timeSinceJumpInput = 0f;
               isbufferedJumping = false;
                //Debug.Log("NO Buffered Jump !");
            }
        }

        if(charac.collisions.below == false)
        {
            //Debug.Log("DANS LES AIRS !");

            timeCheckGhostJump += Time.fixedDeltaTime;
           

            if (timeCheckGhostJump <= cData.maxGhostJump && charac.externalForce.y < 0)
            {
                charac.isGhostJumping = true;
            } 
            
            else if (timeCheckGhostJump > cData.maxGhostJump)
            {
                charac.isGhostJumping = false;
            }

        } 
        
        else
        {
                timeCheckGhostJump = 0;
        }



        if (Input.GetKeyDown("joystick button 0"))
        {
            //Debug.Log("INPUT JUMP !");
            charac.Jump();

           if(checkingOnAir == false && charac.collisions.onGround)
            { 
                checkingOnAir = true;
            }

           if(!charac.collisions.onGround && isbufferedJumping == false)
            {
                checkingOnAir = false;
                isbufferedJumping = true;
            }

            if (isbufferedJumping && !charac.collisions.onGround)
            {
                timeSinceJumpInput = 0f;
               
            }
        }

        if (Input.GetKeyUp("joystick button 0")) 
        {
            //Debug.Log("STOP JUMP ! ");
            charac.EndJump();
         }

        // POUR LE SWITCH DE MONDE

        //World Switch = Right Bumper
        if (Input.GetButtonDown("World Switch"))
        {
            pStatus.ChangeWorld();
        }
    }
}
