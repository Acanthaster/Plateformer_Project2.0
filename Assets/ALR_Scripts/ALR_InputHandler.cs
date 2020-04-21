using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALR_InputHandler : MonoBehaviour
{
    [Header("Key Binding")]
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode jump = KeyCode.Space;
    public KeyCode changeWorld = KeyCode.S;
    public KeyCode instantDeath = KeyCode.P;

    public ALR_PlayerController controller;
    void Update()
    {
        controller.leftKey = Input.GetKey(left);
        controller.leftKeyDown = Input.GetKeyDown(left);
        controller.leftKeyUp = Input.GetKeyUp(left);

        controller.rightKey = Input.GetKey(right);
        controller.rightKeyDown = Input.GetKeyDown(right);
        controller.rightKeyUp = Input.GetKeyUp(right);

        controller.jumpKey = Input.GetKey(jump);
        controller.jumpKeyDown = Input.GetKeyDown(jump);
        controller.jumpKeyUp = Input.GetKeyUp(jump);
        
        controller.changeWorldKey = Input.GetKey(changeWorld);
        controller.changeWorldKeyDown = Input.GetKeyDown(changeWorld);
        controller.changeWorldKeyUp = Input.GetKeyUp(changeWorld);

        controller.deathKey = Input.GetKey(instantDeath);
        controller.deathKeyDown = Input.GetKeyDown(instantDeath);
        controller.deathKeyUp = Input.GetKeyUp(instantDeath);
    }
}
