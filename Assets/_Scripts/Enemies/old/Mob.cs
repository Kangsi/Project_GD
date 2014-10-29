using UnityEngine;
using System.Collections;


namespace oldFile 
{
    public class Mob : MonoBehaviour {
        
        private CharacterController controller;
        private Player playerPawn;
        private GameObject player;

        public AnimationClip[] anim;
        public string name;
        public float runSpeed;
        public float aggroRange;
        public float attackRange;

        public int maxHealthPoints = 100;
        public int healthPoints;

        private bool isAlive, hasDied;
	
        void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerPawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player = GameObject.FindGameObjectWithTag("Player");
            isAlive = true;
            //healthPoints = maxHealthPoints;
        }

	    void Update () {
            if (isAlive)
               processMotion();

            if (!isAlive && !hasDied)
            {
                die();
            }
	    }

        void processMotion()
        {
            if (inAgrroRange())
            {
                if (!inAttackRange())
                    moveToPlayer();
                else if (inAttackRange())
                {
                    animation.CrossFade(anim[3].name);
                }
            }
            else
            {
                animation.CrossFade(anim[0].name);
            }            
        }

        // Detect if the player is within aggro range
        bool inAgrroRange()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < aggroRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        // Dettect if the player is within attack range
        bool inAttackRange()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Move towards the player
        void moveToPlayer()
        {
            transform.LookAt(player.transform.position);
            controller.SimpleMove(transform.forward * (runSpeed * Time.deltaTime));
            animation.CrossFade(anim[1].name);
        }


        // Set the players opponent to this gameobject upon mouseover
        /*void OnMouseOver()
        {
            Highlight skeleton = gameObject.GetComponentInChildren<Highlight>();
            player.GetComponent<Pawn>().opponent = gameObject;
            skeleton.changeShader();
        }
        void OnMouseExit()
        {
            Highlight skeleton = gameObject.GetComponentInChildren<Highlight>();
            skeleton.revertShader();
        }*/
    
        void dealDamage(int dmg)
        {
            if (inAttackRange())
                playerPawn.takeDamage(dmg);     
        }

        public void takeDamage(int damage)
        {
            healthPoints = (healthPoints - damage);
            animation.CrossFade(anim[4].name);

            if (healthPoints <= 0)
            {
                healthPoints = 0;
                isAlive = false;
            }
        }

        void die()
        {
            // Play our die animation
            animation.CrossFade(anim[5].name);
            hasDied = true;

            // Disable the Character Controller to avoid collision
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);

            if (playerPawn.target != null)
                playerPawn.target = null;
        }

    }
}
