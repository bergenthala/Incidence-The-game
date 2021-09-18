using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool NewGameSelected = false;
    //public AreaEntrance thePlayerPosition;


    public void PlayGame()
    {
        //thePlayerPosition.firstLoad = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        NewGameSelected = true;
    }

    public void ContinueGame()
    {

    }

    public void Options() { 

    }


    public void QuitGame()
    {
        Debug.Log("Quit Successfully");
        Application.Quit();
    }
}
