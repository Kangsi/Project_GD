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
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    ///////////////////////
    // Public variables
    //----------------------------------------
    // Public Instance
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
            return instance;
        }
    }
    //---------------------------------------
    // Decleration
    [System.NonSerialized]
    public Quaternion lookRotation;
    public float rotationSpeed = 11f;
    public bool jump; 

    ///////////////////////
    // Private variables
    //---------------------------------------
    // Internal reference
    private static PlayerController instance;
    //---------------------------------------
    // Component reference
    private Animator animator;
    private NavMeshAgent agent;
    private AnimatorStateInfo upperBodyState;
    private AnimatorStateInfo lowerBodyState;
    //---------------------------------------
    // Decleration  
    static int idleState = Animator.StringToHash("LowerBody.Idle");
    static int runState = Animator.StringToHash("LowerBody.Run");
    static int attackState = Animator.StringToHash("LowerBody.Attack1");
    static int heavySwingState = Animator.StringToHash("LowerBody.HeavySwing");
    static int chargeState = Animator.StringToHash("LowerBody.Charge");
    static int poundState = Animator.StringToHash("LowerBody.Pound");
    private int AttackKey;    
    private Vector3 destinationPoint;
    private GameObject destinationTarget;

    ///////////////////////
    // States
    //----------------------------------------
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
            animator.SetInteger("movementState", (int)State);
        }
    }
    #endregion
    //----------------------------------------
    // Behaviour
    #region
    private enum playerState { idle = 0, move = 1, engageEnemy = 2, moveToObject = 3 };
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
    //----------------------------------------
    #region
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        lookRotation = Quaternion.identity;
    }
    void Start()
    {
        EventManager.Instance.AddListener(this, "OnMouseDown");
        EventManager.Instance.AddListener(this, "OnAbilityUse");
        EventManager.Instance.AddListener(this, "OnPlayAttackAnimation");
        EventManager.Instance.AddListener(this, "OnPlayHeavySwingAnimation");
        EventManager.Instance.AddListener(this, "OnPlayPoundAnimation");
        EventManager.Instance.AddListener(this, "OnPlayJumpAnimation");
        EventManager.Instance.AddListener(this, "OnPrimaryAttack");
        EventManager.Instance.AddListener(this, "OnSecondaryAttack");
        EventManager.Instance.AddListener(this, "OnActionbar1");
        EventManager.Instance.AddListener(this, "OnActionbar2");
        EventManager.Instance.AddListener(this, "OnActionbar3");
        EventManager.Instance.AddListener(this, "OnActionbar4");
        EventManager.Instance.AddListener(this, "OnPetAttackOrder");
        EventManager.Instance.AddListener(this, "OnPetFollowOrder");
        EventManager.Instance.AddListener(this, "OnDisplacePlayer");
;    }
	void Update () {
        HandleMovement();
        HandleRotation();
	}      
    void FixedUpdate()
    {
        upperBodyState = animator.GetCurrentAnimatorStateInfo(1);
        lowerBodyState = animator.GetCurrentAnimatorStateInfo(2);
    }
    #endregion

    ///////////////////////
    // Public Funcions
    //---------------------------------------
    

    ///////////////////////
    // Private Funcions    
    //---------------------------------------
    // Behaviour function which processes the state set by the OnMouseDown function
    private void HandleMovement()
    {
        switch(behaviorState)
        {
            case (playerState.idle):
                #region
                agent.Stop();
                animationState = animState.idle;
                break;
                #endregion
            case (playerState.move):
                #region
                if (Vector3.Distance(destinationPoint, transform.position) > 0.1f)
                {                    
                    agent.stoppingDistance = 0.1f;
                    agent.SetDestination(destinationPoint);
                    animationState = animState.moving;
                    // Apply Rotation
                    lookRotation = Quaternion.LookRotation(agent.steeringTarget - transform.position);
                }
                else
                {
                    behaviorState = playerState.idle;
                }
                break;
                #endregion
            case (playerState.engageEnemy):
                #region    
                destinationPoint = destinationTarget.transform.position;
                if (Vector3.Distance(destinationPoint, transform.position) > Player.Instance.attackRange)
                {
                    agent.stoppingDistance = Player.Instance.attackRange-0.2f;
                    agent.SetDestination(destinationPoint);
                    animationState = animState.moving;
                    // Apply Rotation
                    lookRotation = Quaternion.LookRotation(agent.steeringTarget - transform.position);
                }
                else
                {
                    behaviorState = playerState.idle;
                    EventManager.Instance.PostEvent(this, "OnPrimaryAttack");
                }
                break;
                #endregion
            case (playerState.moveToObject):
                #region
                destinationPoint = destinationTarget.GetComponentInChildren<UseableObject>().transform.position;
                if (Vector3.Distance(   new Vector3(destinationPoint.x, 0.0f, destinationPoint.z),
                                        new Vector3(transform.position.x, 0.0f, transform.position.z)) > 0.1f)
                {
                    agent.stoppingDistance = 0.1f;
                    agent.SetDestination(destinationPoint);
                    animationState = animState.moving;
                    // Apply Rotation
                    lookRotation = Quaternion.LookRotation(agent.steeringTarget - transform.position);
                }
                else
                {
                    behaviorState = playerState.idle;
                }
                break;
                #endregion
        }
    }
    //---------------------------------------
    // Function to handle the players rotaton
    private void HandleRotation()
    {
        // Disable rotation on the xz axis
        lookRotation.x = 0.0f;
        lookRotation.z = 0.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    
    ///////////////////////
    // Event Functions
    //---------------------------------------
    // Event function which respondes to the OnMouseDown event.
    private void OnMouseDown()
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
                        behaviorState = playerState.move;
                        break;
                    case "Enemy":
                        behaviorState = playerState.engageEnemy;
                        destinationTarget = hit.collider.gameObject;
                        break;
                    case "Object_Useable":
                        behaviorState = playerState.moveToObject;
                        destinationTarget = hit.collider.gameObject;
                        break;
                }
                destinationPoint = hit.point;
                lookRotation = Quaternion.LookRotation(hit.point - transform.position);
            }
        }
    }
    //---------------------------------------
    // Event function which responds to every ability used event
    private void OnAbilityUse()
    {
        behaviorState = playerState.idle;
    }
    //---------------------------------------
    // Event functions for playing animations
    private void OnPlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
        if (AttackKey < 1) 
            AttackKey++;
        else 
            AttackKey--;
        animator.SetInteger("AttackKey", AttackKey);                
    }
    private void OnPlayHeavySwingAnimation()
    {
        animator.SetTrigger("HeavySwing");
    }
    private void OnPlayPoundAnimation()
    {
        animator.SetTrigger("Pound");
    }
    private void OnPlayChargeAnimation()
    {
        animator.SetTrigger("Charge");
    }
    private void OnPlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
    //---------------------------------------
    // Event functions for processing input events
    private void OnPrimaryAttack()
    {
        Spellbook.Instance.UseSpell("0");
    }
    private void OnSecondaryAttack()
    {
        Spellbook.Instance.UseSpell("1");
    }
    private void OnActionbar1()
    {
        Spellbook.Instance.UseSpell("2");
    }
    private void OnActionbar2()
    {
        Spellbook.Instance.UseSpell("3");
    }

}
