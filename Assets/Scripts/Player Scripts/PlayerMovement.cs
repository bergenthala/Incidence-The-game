using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRB; //Player Rigidbody
    public float moveSpeed; //Player Movespeed Multiplier
    public float defaultMoveSpeed; //Default Player move speed multiplier
    public float sprintSpeed; //Player sprint speed multiplier

    public float lastMoveX;
    public float lastMoveY;

    public Animator playerAnim; //Player Animator

    public static PlayerMovement instance; //Only one player set up

    public string areaTransitionName; //Checks the last used scene switch area

    private Vector3 bottomLeftLimit; //detection of map boundaries
    private Vector3 topRightLimit; //detection of map boundaries

    public bool canMove;
    public bool enterDialogueTrigger;

    public GameObject targetName;

    public AudioClip walk_wood;
    public AudioClip run_wood;
    public AudioClip walk_tiles;
    public AudioClip run_tiles;
    public AudioClip walk_grass;
    public AudioClip run_grass;

    public AudioSource playerAudioSource;

    public float interactionRadius = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject); //Doesn't destroy gameobject when switching scenes

        playerRB = FindObjectOfType<Rigidbody2D>(); //Accessing the Players Rigidbody
        playerAnim = FindObjectOfType<Animator>(); //Accessing the Players Animator
        moveSpeed = defaultMoveSpeed; //sets the movespeed to default
        playerAudioSource = GetComponent<AudioSource>(); //sets the Audio Source component on the player
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed; //Input detection to movement of the Player velocity in Rigidbody

            playerAnim.SetFloat("moveX", Input.GetAxisRaw("Horizontal")); //Animator Parameter Setup for the x axis
            playerAnim.SetFloat("moveY", Input.GetAxisRaw("Vertical")); //Animator Parameter Setup for the y axis
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) //Horizontal/Vertical Controls
        {
            if (canMove)
            {
                playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

                lastMoveX = playerAnim.GetFloat("lastMoveX"); //1 looks to right and -1 is left

                lastMoveY = playerAnim.GetFloat("lastMoveY"); //1 looks up and -1 is down

                //Debug.Log(lastMoveX);
                //Debug.Log(lastMoveY);

                if (SceneManager.GetActiveScene().name == "MCHouseInterior")
                {
                    playerAudioSource.clip = walk_wood;
                }

                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    playerAudioSource.clip = walk_tiles;
                }

                if (SceneManager.GetActiveScene().name == "KingdomOfSpadesTownCenter" || SceneManager.GetActiveScene().name == "KingdomOfSpadesEastSide" || SceneManager.GetActiveScene().name == "Tester")
                {
                    playerAudioSource.clip = walk_grass;
                }
            }
        }


        //keeps the player inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z); //Mathf.Clamp takes value and clamps it between 2 points, which sets the boundaries

        Sprint();


    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight) // Function to set the boundaries
    {
        bottomLeftLimit = bottomLeft + new Vector3(0.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -1f, 0f);

    }



    public void Sprint() //Function where holding "Shift" allows the Player to Sprint
    {
        //Need to implement player input system (Correlates to main menu options/ controls area)

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            moveSpeed = defaultMoveSpeed;
        }
    }


   /* public void CheckForNearbyNPC()
    {
        var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var target = allParticipants.Find(delegate (NPC p)
        {
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
            (p.transform.position - this.transform.position)// is in range?
            .magnitude <= interactionRadius;
        });
        if (target != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
        }



    }*/
}

