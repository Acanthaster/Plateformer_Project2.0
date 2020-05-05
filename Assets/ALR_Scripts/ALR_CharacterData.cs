using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ALR_CustomCharacterController))] 

public class ALR_CharacterData : MonoBehaviour
{
	 // Character's attributes
    [Header("Movement")]
    public float maxSpeed;
    public float accelerationTime;
    public float decelerationTime;

    [Header("Jumping")]
    public float maxJumpHeight;
    public float minJumpHeight;
    public bool advancedAirControl; // on autorise à régler plus finement le déplacement aérien en fonction des deux paramètres ci dessous
    public float airAccelerationTime;
    public float airDecelerationTime;
    public float maxGhostJump;
    public float maxBufferedJump;

    [Header("Wall Sliding/Jumping")]
    public bool canWallSlide;
    public float wallSlideSpeed; 
    public bool canWallJump;
    public float wallJumpSpeed;
    public float maxGhostWallJump;

}
