using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    private CharacterStats[] playerStats;
    public TextMeshProUGUI[] nameText, hpText, mpText, lvlText, expText, strengthText, DefenseText, MoneyText;
    public Slider[] expSlider;
    public Image[] characterSprite;
    public GameObject[] characterStatsHolder;


    public ItemButton[] itemButtons;
    public string selectedItem;
    public ItemController activeItem;
    public TextMeshProUGUI itemName, itemDescription, useButtonText;

    //public string FileToSave;

    public static PauseMenuController instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isPaused = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                ActivatePauseMenu();
                UpdateStats();
                ShowItems();
                GameManager.instance.gameMenuOpen = true;
            }
            else
            {
                DeactivatePauseMenu();
                GameManager.instance.gameMenuOpen = false;
            }
        }


    }

    public void ActivatePauseMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Debug.Log("Pause");
    }

    public void DeactivatePauseMenu()
    {
        //FindObjectOfType<AudioController>().Play("click");
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Debug.Log("Resume");
    }


    public void Back()
    {
        DeactivatePauseMenu();
        GameManager.instance.gameMenuOpen = false;
    }

    public void UpdateStats() //Accesses TextMeshPro Text and changes it accordingly through the CharacterStats. Set up to use multiple characters if implemented
    {
        playerStats = GameManager.instance.playerstats;

        for (int i = 0; i < playerStats.Length; i++) {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                characterStatsHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].characterName;
                hpText[i].text = "HP:" + playerStats[i].currentHP + "/" + playerStats[i].MaxHP;
                mpText[i].text = "MP:" + playerStats[i].currentMP + "/" + playerStats[i].MaxMP;
                lvlText[i].text = "Level:" + playerStats[i].playerLevel;
                strengthText[i].text = "Strength:" + playerStats[i].attackPower;
                DefenseText[i].text = "Defense:" + playerStats[i].defense;
                MoneyText[i].text = "Money:" + MoneyShort(playerStats[i].currentMoney); //uses MoneyShort Function in player stats
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].EXPToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                //characterSprite[i].sprite = playerStats[i].characterImage;
            }
            else
            {
                characterStatsHolder[i].SetActive(false);
            }
        }

    }

    // Shortens Money text
    public string MoneyShort(int money)
    {
        if (money >= 1000000000) // If >= 1,000,000,000 change to 1B+
        {
            string shorthand = "1B+";
            return shorthand;
        }
        else if (money >= 1000000) // If >= 1,000,000 change to 1M
        {
            string shorthand = decimal.Floor(money / 100000)/10 + "M";
            return shorthand;
        }
        else if (money >= 1000) // If >= 1,000 change to 1K
        {
            string shorthand = decimal.Floor(money / 100)/10 + "K";
            return shorthand;
        }
        else if (money <= 1000) // If <= 1,000 do nothing
        {
            string shorthand = money.ToString();
            return shorthand;
        }
        else if (money <= 0) // If Negative ==> Error Message
        {
            string shorthand = "" + money.ToString();
            Debug.Log("Negative $$$");
            return shorthand;
        }
        return "ERROR: Something Went Wrong"; // Just in Case
    }


    public void ShowItems()
    {

        GameManager.instance.SortItems();

        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {
                //Debug.Log("test1");
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                //Debug.Log("test2");
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(ItemController newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void LoadMenu()
    {
        //Insert the menu scene here
       // FindObjectOfType<AudioController>().Play("click");
        DeactivatePauseMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCredits()
    {
        //insert the credits scene here
        DeactivatePauseMenu();
        Debug.Log("Credits Scene");
    }

    public void SaveGame(string SaveFile1) 
    {
        GameManager.instance.SaveData(SaveFile1);
    }

    public void SaveGame2(string SaveFile2)
    {
       GameManager.instance.SaveData(SaveFile2);
    }

    public void SaveGame3(string SaveFile3)
    {
       GameManager.instance.SaveData(SaveFile3);
    }

    public void LoadGame(string DataToLoad)
    {
        GameManager.instance.LoadData(DataToLoad);
    }



}
