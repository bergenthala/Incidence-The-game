using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue questDialogue;
    public Dialogue normalDialogue;

   //public string[] lines;
    public bool canActivate;
    public bool scenePlayed;
    public bool enterDialogueSpace;

    public bool deactivateOnPlay;

   // public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;

    public string theQuestForDialogue;
    public bool questActive;

    public float cinematicMoveSpeed;

    public GameObject targetName;

    public bool receivedQuest = true;

    public void TriggerQuestDialogue()
    {
        DialogueManager.instance.StartDialogue(questDialogue);
        DialogueManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        questLog.instance.AddQuest(questToMark);
        receivedQuest = true;
    }

    public void TriggerNormalDialogue()
    {
        DialogueManager.instance.StartDialogue(normalDialogue);
    }

    void Update()
    {
  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            questActive = QuestManager.instance.CheckQuestComplete(theQuestForDialogue);
            //canActivate = true;
            if (!questActive && receivedQuest)
            {
                TriggerQuestDialogue();
                receivedQuest = false;
            }
            else
            {
                TriggerNormalDialogue();
            }


            if (enterDialogueSpace)
            {
                CinematicMovement();
            }
            gameObject.SetActive(!deactivateOnPlay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //canActivate = false;
        scenePlayed = true;
    }


    public void CinematicMovement()
    {

        PlayerMovement.instance.transform.position = Vector2.MoveTowards(transform.position, targetName.transform.position, cinematicMoveSpeed);
    }

}
