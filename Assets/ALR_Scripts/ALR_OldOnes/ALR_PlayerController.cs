using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALR_PlayerController : MonoBehaviour
{
    [Header("Ground Movement")]
    public float speed = 5f;
    //public float runMultiplier = 2f;
    public float accelerationTime = 0.5f;
    public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Jump")]
    public float gravity = 9.81f;
    public AnimationCurve jumpCurve = AnimationCurve.Constant(0, 1, 1);
    //public AnimationCurve fallingAcceleration = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float jumpReleaseMultiplier = 3f;
    public int maxJumpsAllowed = 1;                         //ENLEVER CECI

    [Header("Ghost Jump")]
    public float maxGhJumpTimeAllowed = 0.12f;

    [Header("Buffered Jump")]
    public float maxBffJumpTime = 0.12f;

    // POUR LE WALL JUMP + STICK WALL :
    [Header("Wall Jump")]
    public AnimationCurve wJumpCurve = AnimationCurve.Constant(0, 1, 1);
    public float wJumpReleaseMultiplier = 3f;
    public float maxStickWallAllowed = 0.4f;
    public float slideAgainstWall = -0.12f;
    public float wJumpHorizontalForce = 0.18f;
    public float wJumpVerticalForce = 0.26f;
    //FIN ! 

    [Header("Physics")]
    public float movementThreshold = 0.0015f;

    [Header("References")]
    public Transform self;
    public ALR_CharacterRaycaster raycaster;
    public Transform graphicsTransform;
    public SpriteRenderer sprite;
    public Animator animator;
    public AXD_PlayerStatus status;
                                                //RAJOUT DU CODE D'ELIOTT
    public ELC_DontFallAnymore dontFall;

    [System.NonSerialized]
    public bool leftKeyDown, leftKey, leftKeyUp,
        rightKeyDown, rightKey, rightKeyUp,
        jumpKeyDown, jumpKey, jumpKeyUp,
        changeWorldKey, changeWorldKeyDown, changeWorldKeyUp,
        deathKey, deathKeyDown, deathKeyUp;

    [Header("Debug")]                                    //ENLEVER CECI
    public Transform fakeGroundLevel;                    //ENLEVER CECI
    public bool debugMode;                               //ENLEVER CECI

    //Getters
    [System.NonSerialized] public Vector2 movementVector;
    public bool isGrounded { get { return raycaster.flags.below; } }

    // POUR LE WALL JUMP + STICK WALL :
    public bool isAgainstRightWall { get { return raycaster.flags.right; } }
    public bool isAgainstLeftWall { get { return raycaster.flags.left; } }
    // FIN !

    bool isJumping;
    int jumpsAllowedLeft;                           //ENLEVER CECI
    float timeSinceJumped, timeSinceAccelerated;

    // POUR LE GHOST JUMP :
    bool isGhostJumping = false;
    float timeSinceIsFalling;

    //POUR LE BUFFERED JUMP :
    bool isTryingBffJump = false;
    float timeSinceJumpInput;

    // POUR LE WALL JUMP + STICK WALL :
    bool isWallJumping = false;
    bool wJumpToLeft = false;
    bool wJumpToRight = false;
    bool stickToWall = false;
    bool isSlidingAgainstWall = false;
    float timeSinceWallJumped, timeSinceStick;

    void Start()  // PAS DE MODIFICATION ICI
    {
        timeSinceJumped = 0.12f;
        status.LastCheckpoint = self.position;
    }

    void FixedUpdate()   // MODIF ICI WALL JUMP + STICK WALL !
    {
        DeathHandle();
        InputUpdate();
        JumpUpdate();
        // POUR LE STICK WALL : 
        StickWallUpdate();
        // POUR LE WALL JUMP :
        WallJumpUpdate();

        MovementUpdate();
        PostMovementJumpUpdate();
        AnimationUpdate();
    }

    void InputUpdate()   //MODIF ICI WALL JUMP + STICK WALL !
    {

        //Reset movement
        movementVector = Vector2.zero;

        if (deathKey)
        {
            status.HealthPoint = 0;
        }
        //Check if player make horizontal movement
        if (leftKey)
            movementVector.x--;
        else if (rightKey)
            movementVector.x++;

        if(changeWorldKey)
        {
            status.LivingWorld = !status.LivingWorld;
        }

        //Jump detection                                + MODIFICATION WALL JUMP
        if (isGrounded && jumpKeyDown)
        {
            TryJump();
        }

        if(isTryingBffJump)
        {
            timeSinceJumpInput += Time.fixedDeltaTime;

            if (isGrounded && timeSinceJumpInput < maxBffJumpTime)
            {
                StartJump();
                Debug.Log("BUFFERED JUMP !");
                isTryingBffJump = false;
                timeSinceJumpInput = 0f;
            }
        }

        // GHOST JUMP : 
        if(!isGrounded && !isJumping && !isSlidingAgainstWall && !isWallJumping)
        {
            timeSinceIsFalling += Time.fixedDeltaTime;

            if(jumpKeyDown && (timeSinceIsFalling < maxGhJumpTimeAllowed))
            {
                isGhostJumping = true;
                Debug.Log("GHOST JUMP !");
                StartJump();
                timeSinceIsFalling = 0f;
            }

            if(isGrounded && timeSinceIsFalling != 0f)
            {
                isGhostJumping = false;
                timeSinceIsFalling = 0f;
            }

            //BUFFERED JUMP :
            if (!isGrounded && !isSlidingAgainstWall && !isWallJumping && !isJumping)
            {

                if (jumpKeyDown)
                {
                    isTryingBffJump = true;
                }
            }

        }


        // POUR WALL JUMP + STICK LEFT WALL
        if (!isGrounded && isAgainstLeftWall && !isWallJumping)
        {
            if(leftKey)
            stickToWall = true;
            if(jumpKeyDown)
            TryLeftWallJump();
        }

        // POUR WALL JUMP + STICK RIGHT WALL
        if(!isGrounded && isAgainstRightWall && !isWallJumping)
        {
            if(rightKey)
            stickToWall = true;
            if(jumpKeyDown)
            TryRightWallJump();
        }
    }

    void JumpUpdate()
    {
        movementVector.y = gravity * -1f;
        //Debug.Log("movementVector.y :" + movementVector.y);
      
        if (!isJumping)
            return;

        //si on release la touche, le saut se finira plus vite
        float releaseMultiplier = jumpKey ? 1 : jumpReleaseMultiplier;

        //tenir trace de "depuis combien de temps le saut a commencé"
        timeSinceJumped += Time.fixedDeltaTime * releaseMultiplier;

        float gravityMultiplier = jumpCurve.Evaluate(timeSinceJumped);

        movementVector.y *= gravityMultiplier * -1;

        if (timeSinceJumped > jumpCurve.keys[jumpCurve.keys.Length-1].time)
                isJumping = false;
    }  


    // POUR LE STICK WALL :
    void StickWallUpdate() 
    {
        if (!stickToWall)
            return;

        timeSinceStick += Time.fixedDeltaTime;

        if(timeSinceStick < maxStickWallAllowed)
            self.Translate(new Vector3(0,0,0));
        

        else 
        {
            MoveStickWall(new Vector3(0,slideAgainstWall,0));
            isSlidingAgainstWall = true;
        }

        if(isSlidingAgainstWall && isAgainstLeftWall && rightKey)
            stickToWall = false;

        if (isSlidingAgainstWall && isAgainstRightWall && leftKey)
            stickToWall = false;
    }

    // POUR LE WALL JUMP :
    void WallJumpUpdate()
    {
        if (!isWallJumping)
            return;

        float wallJumpReleaseMultiplier = jumpKey ? 1 : wJumpReleaseMultiplier;

        timeSinceWallJumped += Time.fixedDeltaTime * wallJumpReleaseMultiplier;

        float wJumpGravityMultiplier = wJumpCurve.Evaluate(timeSinceWallJumped);

        float wallJumpHorizontalForce = wJumpHorizontalForce;
        float wallJumpVerticalForce =wJumpVerticalForce * wJumpGravityMultiplier;



        if (wJumpToRight == true && !wJumpToLeft)
        {
            Vector3 finalVector = new Vector3(wallJumpHorizontalForce, wallJumpVerticalForce, 0);

            MoveWallJump(finalVector);
        }

        else if (wJumpToLeft == true && !wJumpToRight)
        {
            wallJumpHorizontalForce *= -1f;

            Vector3 finalVector = new Vector3(wallJumpHorizontalForce, wallJumpVerticalForce, 0);

            MoveWallJump(finalVector);
        } 

        if (timeSinceWallJumped > wJumpCurve.keys[wJumpCurve.keys.Length-1].time)
        {
            isWallJumping = false;
            wJumpToLeft = false;
            wJumpToRight = false;
        }
    }

    void PostMovementJumpUpdate() // PAS DE MODIFICATION ICI
    {
        if (isGrounded)
        {
            isJumping = false;
            jumpsAllowedLeft = maxJumpsAllowed;               // ENLEVER CECI
        }
    }

    void MovementUpdate()   // MODIF ICI WALL JUMP + STICK WALL
    {
        if (isWallJumping || stickToWall || dontFall.playerIsInGround)           //CHANGEMENT ELIOTT ICI
            return;

        if (movementVector.x == 0)
            timeSinceAccelerated = 0;
        else
            timeSinceAccelerated += Time.fixedDeltaTime;

        float accelerationMultiplier = 1;
        if (accelerationTime > 0)
            accelerationMultiplier = acceleration.Evaluate(timeSinceAccelerated / accelerationTime);

        float usedSpeed = speed * accelerationMultiplier;
        movementVector.x *= usedSpeed;

        Vector3 finalVector = Time.fixedDeltaTime * movementVector;
        Move(finalVector);
    }

    void AnimationUpdate() // PAS DE MODIFICATION ICI
    {
        animator.SetBool("IsIdle", movementVector.x == 0);
        animator.SetBool("IsMoving", movementVector.x != 0);
        animator.SetBool("IsGrounded", isGrounded);

        if (movementVector.x != 0) 
            graphicsTransform.localScale = new Vector3(
                Mathf.Abs(graphicsTransform.transform.localScale.x) * Mathf.Sign(movementVector.x),
                graphicsTransform.transform.localScale.y,
                graphicsTransform.transform.localScale.z);
    }

    void TryJump() // PAS DE MODIFICATION ICI
    {
        if (!isGrounded)
            return;
        if (jumpsAllowedLeft == 0)
            return;
        jumpsAllowedLeft--;
        StartJump();
    }


    void StartJump()  // PAS DE MODIFICATION ICI
    {
        isJumping = true;
        timeSinceJumped = 0f;
        animator.SetTrigger("Jump");
    }


    // POUR LE WALL JUMP :
    void TryLeftWallJump()
    {
        if (isGrounded)
            return;

        if (!isAgainstLeftWall)
            return;

        //Reset le stick wall
        stickToWall = false;
        timeSinceStick = 0;

        wJumpToRight = true;
        StartWallJump();
    }


    // POUR LE WALL JUMP :
    void TryRightWallJump()
    {
        if (isGrounded)
            return;

        if (!isAgainstRightWall)
            return;

        //Reset le stick wall
        stickToWall = false;
        timeSinceStick = 0;

        wJumpToLeft = true;
        StartWallJump();
    }

    // POUR LE WALL JUMP :
    void StartWallJump()
    {
        isWallJumping = true;
        timeSinceWallJumped = 0f;
        animator.SetTrigger("WallJump");
    }

    void Move(Vector3 movement)     // PAS DE MODIFICATION ICI
    {
        if (movement.x != 0)
            movement.x = raycaster.CastBoxHorizontal(movement.x);
        if (Mathf.Abs(movement.x) < movementThreshold)
            movement.x = 0;

        if (movement.y != 0)
        {
            movement.y = raycaster.CastBoxVertical(movement.y);
        }
        if (Mathf.Abs(movement.y) < movementThreshold)
            movement.y = 0;

        if (movement.x > 0) raycaster.flags.left = false;
        if (movement.x < 0) raycaster.flags.right = false;
        if (movement.y > 0) raycaster.flags.below = false;
        if (movement.y < 0) raycaster.flags.above = false;
        self.Translate(movement);
    }

    // POUR LE STICK WALL : 
     void MoveStickWall(Vector3 sWall)
     {
        if (sWall.x != 0)
            sWall.x = raycaster.CastBoxHorizontal(sWall.x);
        if (Mathf.Abs(sWall.x) < movementThreshold)
            sWall.x = 0;

        if (sWall.y != 0)
            sWall.y = raycaster.CastBoxVertical(sWall.y);
        if (Mathf.Abs(sWall.y) < movementThreshold)
            sWall.y = 0;

        if(sWall.y == 0 )
        {
            stickToWall = false;
            timeSinceStick = 0;
        }

        if (sWall.x > 0) raycaster.flags.left = false;
        if (sWall.x < 0) raycaster.flags.right = false;
        if (sWall.y > 0) raycaster.flags.below = false;
        if (sWall.y < 0) raycaster.flags.above = false;

        self.Translate(sWall);
     }

    // POUR LE WALL JUMP : 
    void MoveWallJump (Vector3 wJump)
    {
        if (wJump.x != 0)
            wJump.x = raycaster.CastBoxHorizontal(wJump.x);
        if (Mathf.Abs(wJump.x) < movementThreshold)
            wJump.x = 0;

        if (wJump.y != 0)
            wJump.y = raycaster.CastBoxVertical(wJump.y);
        if (Mathf.Abs(wJump.y) < movementThreshold)
            wJump.y = 0;

        if(wJump.y == 0 || wJump.x == 0)
        {
            isWallJumping = false;
            wJumpToLeft = false;
            wJumpToRight = false;
        }

        if (wJump.x > 0) raycaster.flags.left = false;
        if (wJump.x < 0) raycaster.flags.right = false;
        if (wJump.y > 0) raycaster.flags.below = false;
        if (wJump.y < 0) raycaster.flags.above = false;
  
        self.Translate(wJump);
    }

    void DeathHandle()
    {
        Debug.Log("HP : " + status.HealthPoint);
        if (status.HealthPoint <= 0)
        {
            Debug.Log("Death");
            self.position = status.LastCheckpoint;
            status.HealthPoint = status.MaxHealthPoint;
        }
    }

}
