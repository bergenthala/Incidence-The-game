using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //public GameObject Dialogue1;
    public GameObject dialogueBox;

    public bool Scene1MCHouseInterior;
    public bool Dialogue1Playing;
   

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;

    public float delayTime;
    public float textSpeedMultiplier;

    public bool innerWorld;
    public bool overWorld;

    public Vector3 offset;

    private bool isCurrentlyTyping;
    private string completeText;

    // public Animator animator;
    public Animator dialoguearrowAnimator;

    public Queue<Dialogue.info> dialogueInfo = new Queue<Dialogue.info>(); //FIFO Collection
    //public string[] dialogLines;

    public static DialogueManager instance;

    public DialogueTrigger triggerDialogue;

    public string questToMark;
    public bool markQuestToComplete;
    public bool shouldMarkQuest;

    public GameObject NPCInteractable;
    // Start is called before the first frame update
    void Start()
    {
      
        instance = this;

        Scene1MCHouseInterior = true;
        Dialogue1Playing = true;

        DontDestroyOnLoad(gameObject);

        triggerDialogue = FindObjectOfType<DialogueTrigger>();
    }
    public GameObject FindClosestInteractableNPC()
    {
        GameObject[] npcInteractable;
        npcInteractable = GameObject.FindGameObjectsWithTag("NPCDialogue");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in npcInteractable)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                NPCInteractable = go;
                distance = curDistance;
            }
        }
        return NPCInteractable;        
    }
    // Update is called once per frame
    void Update()
    {
        //when you want to have the dialogue arrow bouncing:
        dialoguearrowAnimator.SetBool("DialoguePlaying", true);

        //when you want to have the dialogue arrow not bouncing:
        dialoguearrowAnimator.SetBool("DialoguePlaying", false);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }
        //Scene 1 MCHouseInterior Dialogues://

        /*  if(Scene1MCHouseInterior == true)
           {
               //Dialogue 1 Xanthe wakes up and speaks

               if (Dialogue1Playing == true)
               {
                   Dialogue1.SetActive(true);
               }

               else if(Dialogue1Playing == false)
               {
                   Dialogue1.SetActive(false);
               }
           }*/

        
        if (PlayerMovement.instance.lastMoveX == 1) // on the right side of the player
        {
            offset = new Vector3(-135, 0, 0); //change offset   
            print("dialogue box is on left side");
        }

        else if (PlayerMovement.instance.lastMoveX == -1) //on the left side of the player
        {
            offset = new Vector3(-20, 0, 0); //change offest
            
            print("dialogue box is on right side");
        }

    }


    public void StartDialogue(Dialogue dialogue) //Enqueue Dialogue
    {
        FindClosestInteractableNPC();
        //animator.SetBool("IsOpen", true);
        Debug.Log("Starting Conversation with" + dialogue.name);
        dialogueBox.SetActive(true);
        dialogueBox.transform.position = dialogueBox.transform.position + offset;


       /* if (PlayerMovement.instance.lastMoveX == 1) //facing right
        {
            //dialogueBox.transform.position = PlayerMovement.instance.gameObject.transform.position;
            dialogueBox.transform.position = dialogueBox.transform.position + offset;
        }

        else if (PlayerMovement.instance.lastMoveX == -1) //facing left
        {
            //dialogueBox.transform.position = PlayerMovement.instance.gameObject.transform.position;
            dialogueBox.transform.position = dialogueBox.transform.position + offset;
        }

        if (overWorld)
        {
            //dialogueBox.transform.position = PlayerMovement.instance.gameObject.transform.position;
            dialogueBox.transform.position = dialogueBox.transform.position + offset;
        }

        */
        PlayerMovement.instance.moveSpeed = 0;
        
              

        dialogueInfo.Clear();

        foreach (Dialogue.info info in dialogue.DialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueInfo.Count == 0)
        {
            Debug.Log("sentence count 0");
            EndDialogue();
            return;
        }

        if (isCurrentlyTyping)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }

        Dialogue.info info = dialogueInfo.Dequeue();
        completeText = info.myText;

        //  nameText.text = info.name;
        nameText.text = info.character.myName;
        dialogueText.text = info.myText;
        // dialoguePortrait.sprite = info.portrait;
        dialoguePortrait.sprite = info.character.myPortrait;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(info));
    } 

    IEnumerator TypeSentence(Dialogue.info info)
    {
        isCurrentlyTyping = true;
        dialogueText.text = "";
        foreach (char letter in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delayTime * textSpeedMultiplier);
            dialogueText.text += letter;
            yield return null;
        }
        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);

        if (shouldMarkQuest)
        {
            shouldMarkQuest = false;
            if (markQuestToComplete)
            {
                QuestManager.instance.MarkQuestComplete(questToMark);
            }
            else
            {
                QuestManager.instance.MarkQuestIncomplete(questToMark);
            }
        }

        PlayerMovement.instance.moveSpeed = PlayerMovement.instance.defaultMoveSpeed;
    }


    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestToComplete = markComplete;

        shouldMarkQuest = true;
    }
}

