using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public CharacterStats[] playerstats;

    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas;
    public bool canSave = true;
    public bool firstSave = false;

    public GameObject overwriteBox;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public ItemController[] referenceItems;

    public bool shouldTransition;
    public int playerLevel;
    public int currentEXP;
    public int currentHP;
    public int currentMP;
    public int currentMoney;
    public int maxHP;
    public int maxMP;

    public GameObject player;
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        player.GetComponent<CharacterStats>();

        DontDestroyOnLoad(gameObject);

        SortItems();

        shouldTransition = true;
        overwriteBox = PauseMenuController.instance.gameObject.transform.GetChild(2).GetChild(2).GetChild(8).gameObject; //gets the overwrite box
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogueActive || fadingBetweenAreas)
        {
            PlayerMovement.instance.canMove = false;
        }
        else
        {
            PlayerMovement.instance.canMove = true;
        }
    }


    public ItemController GetItemDetails(string itemToGet)
    {

        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGet)
            {
                return referenceItems[i];
            }
        }


        return null;
    }

    public void SortItems() //Sort items together moving them up within the inventory
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string ItemToAdd) //Adds item
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == ItemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;
            for (int i = 0; i < itemsHeld.Length; i++)
            {
                if (referenceItems[i].itemName == ItemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }

                if (itemExists)
                {
                    itemsHeld[newItemPosition] = ItemToAdd;
                    numberOfItems[newItemPosition]++;
                }
                else
                {
                    Debug.LogError(ItemToAdd + "Does not Exist");
                }
            }
        }

        PauseMenuController.instance.ShowItems();

    }

    public void RemoveItem(string ItemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == ItemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;
            }
        }
        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            PauseMenuController.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find" + ItemToRemove);
        }

    }

    //NEW SAVE SYSTEM

    private void Save()
    {
        //Location Info
        string currentScene = SceneManager.GetActiveScene().name;
        float playerPositionX = PlayerMovement.instance.transform.position.x;
        float playerPositionY = PlayerMovement.instance.transform.position.y;
        float playerPositonZ = PlayerMovement.instance.transform.position.z;

        //NEW Character Info
        for (int i = 0; i < playerstats.Length; i++)
        {
            if (playerstats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 0);
            }

             playerLevel =  playerstats[i].playerLevel;
             currentEXP = playerstats[i].currentEXP;
             currentHP = playerstats[i].currentHP;
             currentMP = playerstats[i].currentMP;
             currentMoney = playerstats[i].currentMoney;
             maxHP = playerstats[i].MaxHP;
             maxMP = playerstats[i].MaxMP;
        }

      
        string[] contents = new string[]
            {
                "" +currentScene,
                "" +playerPositionX,
                "" +playerPositionY,
                "" +playerPositonZ,
                "" +player.GetComponent<CharacterStats>().playerLevel,
                "" +player.GetComponent<CharacterStats>().currentEXP,
                "" +player.GetComponent<CharacterStats>().currentHP,
                "" +player.GetComponent<CharacterStats>().currentMP,
                "" +player.GetComponent<CharacterStats>().currentMoney,
                "" +maxHP,
                "" +maxMP
            };

    }

    //OLD SAVE SYSTEM
    public void SaveData(string SaveFile)
    {
        //not length but something else
        if (SaveFile != null) //if the savefile exists
        {
            //overwrite message
            overwriteBox.SetActive(true);
            print("overwrite");
            print("savefile: " + SaveFile);
            canSave = false; //make it that you can't save unless you allow to overwrite
        }

        else if (SaveFile == null) //if the SaveFile is a clean safe file.
        {
            firstSave = true;
            print("new save file");
            overwriteBox.SetActive(false);
            canSave = true;           
        }
 
        switch (SaveFile)
        {
            case "SaveFile1":
                
                if (canSave)
                {
                    //Location Info
                    PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name); //Current Scene
                    PlayerPrefs.SetFloat("Player_Position_x", PlayerMovement.instance.transform.position.x); //Player position
                    PlayerPrefs.SetFloat("Player_Position_y", PlayerMovement.instance.transform.position.y);
                    PlayerPrefs.SetFloat("Player_Position_z", PlayerMovement.instance.transform.position.z);

                    //Character info
                    for (int i = 0; i < playerstats.Length; i++)
                    {
                        if (playerstats[i].gameObject.activeInHierarchy)
                        {
                            PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 0);
                        }

                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_Level", playerstats[i].playerLevel);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentExp", playerstats[i].currentEXP);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentHP", playerstats[i].currentHP);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentMP", playerstats[i].currentMP);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentMoney", playerstats[i].currentMoney);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_MaxHP", playerstats[i].MaxHP);
                        PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_MaxMP", playerstats[i].MaxMP);
                    }
                }
                print("saving to SaveFile1");
                break;

            case "SaveFile2":
                if (canSave)
                {
                    //Location Info
                    PlayerPrefs.SetString("Current_Scene2", SceneManager.GetActiveScene().name); //Current Scene
                    PlayerPrefs.SetFloat("Player_Position_x2", PlayerMovement.instance.transform.position.x); //Player position
                    PlayerPrefs.SetFloat("Player_Position_y2", PlayerMovement.instance.transform.position.y);
                    PlayerPrefs.SetFloat("Player_Position_z2", PlayerMovement.instance.transform.position.z);

                    //Character info
                    for (int i = 0; i < playerstats.Length; i++)
                    {
                        if (playerstats[i].gameObject.activeInHierarchy)
                        {
                            PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_active2", 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_active2", 0);
                        }

                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_Level2", playerstats[i].playerLevel);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentExp2", playerstats[i].currentEXP);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentHP2", playerstats[i].currentHP);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentMP2", playerstats[i].currentMP);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentMoney2", playerstats[i].currentMoney);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_MaxHP2", playerstats[i].MaxHP);
                        PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_MaxMP2", playerstats[i].MaxMP);
                    }
                }
                print("saving to SaveFile2");
                break;

            case "SaveFile3":
                if (canSave)
                {
                    //Location Info
                    PlayerPrefs.SetString("Current_Scene3", SceneManager.GetActiveScene().name); //Current Scene
                    PlayerPrefs.SetFloat("Player_Position_x3", PlayerMovement.instance.transform.position.x); //Player position
                    PlayerPrefs.SetFloat("Player_Position_y3", PlayerMovement.instance.transform.position.y);
                    PlayerPrefs.SetFloat("Player_Position_z3", PlayerMovement.instance.transform.position.z);

                    //Character info
                    for (int i = 0; i < playerstats.Length; i++)
                    {
                        if (playerstats[i].gameObject.activeInHierarchy)
                        {
                            PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_active3", 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_active3", 0);
                        }

                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_Level3", playerstats[i].playerLevel);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentExp3", playerstats[i].currentEXP);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentHP3", playerstats[i].currentHP);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentMP3", playerstats[i].currentMP);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentMoney3", playerstats[i].currentMoney);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_MaxHP3", playerstats[i].MaxHP);
                        PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_MaxMP3", playerstats[i].MaxMP);
                    }
                }
                print("saving to SaveFile3");
                break;
        }

        /*
        if (SaveFile == "SaveFile1")
        {
            if (SaveFile != null)
            {
                //overwrite message
                overwriteBox.SetActive(true);

            }
            //Location Info
            PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name); //Current Scene
            PlayerPrefs.SetFloat("Player_Position_x", PlayerMovement.instance.transform.position.x); //Player position
            PlayerPrefs.SetFloat("Player_Position_y", PlayerMovement.instance.transform.position.y);
            PlayerPrefs.SetFloat("Player_Position_z", PlayerMovement.instance.transform.position.z);

            //Character info
            for (int i = 0; i < playerstats.Length; i++)
            {
                if (playerstats[i].gameObject.activeInHierarchy)
                {
                    PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_active", 0);
                }

                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_Level", playerstats[i].playerLevel);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentExp", playerstats[i].currentEXP);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentHP", playerstats[i].currentHP);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentMP", playerstats[i].currentMP);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_CurrentMoney", playerstats[i].currentMoney);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_MaxHP", playerstats[i].MaxHP);
                PlayerPrefs.SetInt("Player_" + playerstats[i].characterName + "_MaxMP", playerstats[i].MaxMP);
            }
        }

        if (SaveFile == "SaveFile2")
        {
            //Location Info
            PlayerPrefs.SetString("Current_Scene2", SceneManager.GetActiveScene().name); //Current Scene
            PlayerPrefs.SetFloat("Player_Position_x2", PlayerMovement.instance.transform.position.x); //Player position
            PlayerPrefs.SetFloat("Player_Position_y2", PlayerMovement.instance.transform.position.y);
            PlayerPrefs.SetFloat("Player_Position_z2", PlayerMovement.instance.transform.position.z);

            //Character info
            for (int i = 0; i < playerstats.Length; i++)
            {
                if (playerstats[i].gameObject.activeInHierarchy)
                {
                    PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_active2", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_active2", 0);
                }

                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_Level2", playerstats[i].playerLevel);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentExp2", playerstats[i].currentEXP);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentHP2", playerstats[i].currentHP);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentMP2", playerstats[i].currentMP);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_CurrentMoney2", playerstats[i].currentMoney);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_MaxHP2", playerstats[i].MaxHP);
                PlayerPrefs.SetInt("Player_2" + playerstats[i].characterName + "_MaxMP2", playerstats[i].MaxMP);
            }
        }

        if (SaveFile == "SaveFile3")
        {
            //Location Info
            PlayerPrefs.SetString("Current_Scene3", SceneManager.GetActiveScene().name); //Current Scene
            PlayerPrefs.SetFloat("Player_Position_x3", PlayerMovement.instance.transform.position.x); //Player position
            PlayerPrefs.SetFloat("Player_Position_y3", PlayerMovement.instance.transform.position.y);
            PlayerPrefs.SetFloat("Player_Position_z3", PlayerMovement.instance.transform.position.z);

            //Character info
            for (int i = 0; i < playerstats.Length; i++)
            {
                if (playerstats[i].gameObject.activeInHierarchy)
                {
                    PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_active3", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_active3", 0);
                }

                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_Level3", playerstats[i].playerLevel);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentExp3", playerstats[i].currentEXP);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentHP3", playerstats[i].currentHP);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentMP3", playerstats[i].currentMP);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_CurrentMoney3", playerstats[i].currentMoney);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_MaxHP3", playerstats[i].MaxHP);
                PlayerPrefs.SetInt("Player_3" + playerstats[i].characterName + "_MaxMP3", playerstats[i].MaxMP);
            }
        }

       */

        //Store inventory data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }

        //Quest info
        QuestManager.instance.SaveQuestData();
    }

    public void DeleteSave()
    {
   //     SaveData() = null;
    }

    public void canoverwriteSave()
    {
        canSave = true;
    }

    public void cantoverwriteSave()
    {
        overwriteBox.SetActive(false);
    }

    public void LoadData(string LoadFile)
    {
        //Location Info
        PlayerMovement.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        //Character Info
        for (int i = 0; i < playerstats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_active") == 0)
            {
                playerstats[i].gameObject.SetActive(false);
            }
            else
            {
                playerstats[i].gameObject.SetActive(true);
            }


            playerstats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_Level");
            playerstats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentExp");
            playerstats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentHP");
            playerstats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentMP");
            playerstats[i].currentMoney = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentMoney");
            playerstats[i].MaxHP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_MaxHP");
            playerstats[i].MaxMP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_MaxMP");
        }

        //Get Inventory Data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }

        //Quest info
        QuestManager.instance.LoadQuestData();

    }

    public void LoadData2(string LoadFile2)
    {
        //Location Info
        PlayerMovement.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        //Character Info
        for (int i = 0; i < playerstats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_active") == 0)
            {
                playerstats[i].gameObject.SetActive(false);
            }
            else
            {
                playerstats[i].gameObject.SetActive(true);
            }


            playerstats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_Level");
            playerstats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentExp");
            playerstats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentHP");
            playerstats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentMP");
            playerstats[i].currentMoney = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_CurrentMoney");
            playerstats[i].MaxHP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_MaxHP");
            playerstats[i].MaxMP = PlayerPrefs.GetInt("Player_" + playerstats[i].characterName + "_MaxMP");
        }
    }
}
