using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //references to other scripts 

    public GameObject gameManager;
    public GameObject characterStats;
    public GameObject itemController;
    public GameObject pauseMenuController;
    public Text txt;


    //Randomization Vars
   // public Sprite itemPic;
   // private int rand;

    // Start is called before the first frame update
    void Start()
    {
        characterStats = GameObject.Find("Player 1 Info");
  //      rand = Random.Range(0,10); //REMEMBER TO REPLACE WITH ITEM ARRAY LENGTH!!!!
    }

    // Update is called once per frame
    void Update()
    {

        //cash text i guess?
        txt.text = pauseMenuController.GetComponent<PauseMenuController>().MoneyShort(characterStats.GetComponent<CharacterStats>().currentMoney);
    }

}
