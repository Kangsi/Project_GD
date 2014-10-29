// John van den Berg

/*=============================================================================
	PlayerController - Singleton
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The PlayerController class 
 * 
 * The PlayerController handles all player action such as input and locomotion
 * 
 * The HandleInput() method is responsible for processing all player input and
 * taking the corresponding action related to the input. 
 * Input keys are determined by the InputManager library.
 * 
 * The HandleMouseHit() method calculates mouse(0) clicks in world space by 
 * raycasting from the camera's view direction into world space and returns 
 * a ray hit called hit. Hit passes a vector3 and, if collided with, a GameObject.
 * Furthermore the current state of the player is set depending on the object that 
 * got hit by the ray, e.g. a hit on walkable ground results in setting the player's
 * state to moveToPoint.
 * 
 * The HandleMovement() method handles the player's state and sets the mecanim
 * Finite State Machine to the appropriate state depending on the player's state.
 * 
 * Player movement is driven by the run animation itself. When the run animation 
 * plays we use Inverse Kinematic to align the mesh's feet with the ground and with 
 * each step the mesh takes we drive an animation curve to go from 0 to 1, where 1
 * is reached when the step has taken place and falls back to 0 when we are half way
 * to our next step. The curve's value updates the velocity of the player's rigidbody
 * causing it to start moving. By using this method of moving the player object we 
 * ensure that the movement and movementspeed is perfectly in sync with the animation
 * and we enable the player object to be exposed by the unity engine's physics system.
 * 
 * Reference to this class is handled by calling the Instance of this class
 * Reference call: PlayerController.Instance.functionName();
 */
public class PlayerController : MonoBehaviour {

    ///////////////////////
    // Instance
    public static CharacterController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(CharacterController)) as CharacterController;
            return instance;
        }
    }

    ///////////////////////
    // References
    #region
    public Animator animator;    
    private Rigidbody rigidBody;
    private Player player;
    private static CharacterController instance;
    #endregion

    ///////////////////////
    // Variable Declaration
    #region
    public float rotationSpeed = 11f;
    public float movementSpeed = 25f;

    private int AttackChain;
    private Vector3 destinationPoint;
    #endregion

    ///////////////////////
    // Animation
    #region
    private enum animState { idle = 0, moving = 1};
    private animState State = animState.idle;
    private animState animationState
    {
        get { return State; }

        set
        {
            State = value;
            // Set animation state
            animator.SetInteger("animState", (int)State);
        }
    }
    #endregion

    ///////////////////////
    // States
    #region
    private enum playerState { idle = 0, moveToPoint = 1, moveToObject = 2, engageEnemy = 3, moveToEnemy = 4 };
    private playerState pState = playerState.idle;
    private playerState behaviorState
    {
        get { return pState;  }
        set
        {
            pState = value;
        }
    }
    #endregion

    ///////////////////////
    // Unity
    #region
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
        Physics.gravity = new Vector3(0, -10f, 0);
    }
	void Update () {
        HandleInput();
        HandleMovement();
	}    
    void OnAnimatorMove()
    {
        rigidbody.velocity = new Vector3(transform.forward.x * ((movementSpeed) * Time.deltaTime * animator.GetFloat("Runspeed")),
                                        rigidbody.velocity.y,
                                        transform.forward.z * ((movementSpeed) * Time.deltaTime * animator.GetFloat("Runspeed")));
    }
    #endregion
       
    ///////////////////////
    // Methods
    private void HandleInput()
    {
        // Primary attack
        if (Input.GetKeyUp(InputManager.ActionBarButtonPrimary))
        {
            HandleMouseHit();
        }
        // Secondary attack
        if (Input.GetKeyUp(InputManager.ActionBarButtonSecondary))
        {
            player.activeSoul.attacks[1].useAbility();
        }
        // Ability 1
        if (Input.GetKeyUp(InputManager.ActionBarButton1))
        {
            player.generalClass.ability[0].useAbility();
        }
        if (Input.GetKeyUp(InputManager.ActionBarButton2))
        {
            player.addExperience(400);
        }

        //SecondCharacterInput
        #region
        if (Input.GetKeyDown(KeyCode.Q))
        {//2nd Char command MoveTo
            GameObject mouseOverObject = MouseOverObject();
            if (mouseOverObject.tag == "Enemy")
            {//If mouse over enemy
                player.secondCharacter.SetTarget(mouseOverObject);
                player.secondCharacter.SecondCharacterState = Second_Character.MOVETYPE.attackingEnemy;
            }
            else if (mouseOverObject.tag == "Scenery")
            {//If mouse of scenery

            }
            else
            {//Move to position
                Vector3 moveToPosition = HitPoint();
                player.secondCharacter.MoveTo(moveToPosition);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {//2nd Char command 
            player.secondCharacter.SecondCharacterState = Second_Character.MOVETYPE.followingPawn;
        }
        #endregion
    }

    private void HandleMouseHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player")
            {
                switch (hit.collider.tag)
                {
                    case "Ground_Walkable":
                        behaviorState = playerState.moveToPoint;
                        break;
                    case "Enemy":
                        player.target = hit.collider.gameObject;
                        if (player.targetInRange())
                        {
                            behaviorState = playerState.engageEnemy;
                            player.activeSoul.attacks[0].useAbility();
                        }
                        else
                            behaviorState = playerState.moveToEnemy;
                        break;
                    case "Useable_Object":
                        behaviorState = playerState.moveToObject; 
                        player.target = hit.collider.gameObject;                                           
                        break;
                }
                destinationPoint = hit.point;
            }
        }
    }

    public GameObject MouseOverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public Vector3 HitPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player")
            {
                return hit.point;
            }
        }
        return hit.point;
    }

    private void HandleMovement()
    {
        switch(behaviorState)
        {
            case (playerState.idle):
                #region
                animationState = animState.idle;
                break;
                #endregion

            case (playerState.moveToPoint):
                #region                
                if (Vector3.Distance(new Vector3(destinationPoint.x, transform.position.y, destinationPoint.z), 
                                     new Vector3(transform.position.x, transform.position.y, transform.position.z)) > 0.5f)
                {
                    // Apply Rotation
                    Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);

                    // Disable rotation on the xz axis
                    targetRotation.x = 0.0f;
                    targetRotation.z = 0.0f;

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                    // Apply Movement state
                    animationState = animState.moving;
                }
                else 
                    behaviorState = playerState.idle;
                break;
                #endregion
           
            case (playerState.moveToEnemy):
                #region
                // If the enemy is not in range we move towards him
                if (!player.targetInRange())    
                {
                    // Apply Rotation
                    Quaternion targetRotation = Quaternion.LookRotation(player.target.transform.position - transform.position);

                    // Disable rotation on the xz axis
                    targetRotation.x = 0.0f;
                    targetRotation.z = 0.0f;

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                    // Apply Movement state
                    animationState = animState.moving;
                }
                else
                    behaviorState = playerState.idle;
                break;
                #endregion
            case (playerState.engageEnemy):
                #region         
                if (player.targetInRange())
                {
                    // Apply Rotation
                    Quaternion targetRotation = Quaternion.LookRotation(player.target.transform.position - transform.position);

                    // Disable rotation on the xz axis
                    targetRotation.x = 0.0f;
                    targetRotation.z = 0.0f;

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                    // Set our animation to stop moving
                    animationState = animState.idle;
                }
                break;
                #endregion
        }
    }      
    
}
