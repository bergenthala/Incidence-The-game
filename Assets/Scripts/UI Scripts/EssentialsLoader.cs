using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameManager gameManage;
    public DialogueManager theDialogue;
    public GameObject dialogueManager;


    // Start is called before the first frame update
    void Start()
    {
        if(UIFade.instance == null)
        {
           UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if (PlayerMovement.instance == null)
        {
            PlayerMovement clone = Instantiate(player).GetComponent<PlayerMovement>();
            PlayerMovement.instance = clone;
        }
        if(GameManager.instance == null)
        {
            Instantiate(gameManage);
        }

/*        if(AudioManager.instance == null)
        {
            Instantiate(AudioManager);
        }
        */
        /*  if(DialogueManager.instance == null)
           {
               Instantiate(theDialogue);
           }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
