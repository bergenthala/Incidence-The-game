using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
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
            }
            else
            {
                DeactivatePauseMenu();
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
}
