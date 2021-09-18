using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public string sceneToLoad; //Scene that gets loads, can be changed in inspector in unity

    public string areaTransitionName; //Name of last area transition

    public AreaEntrance theEntrance;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        theEntrance.transitionName = PlayerMovement.instance.areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
    
            if (shouldLoadAfterFade) //Adding small wait time between scene transfer
            {
                waitToLoad -= Time.deltaTime;
                if (waitToLoad <= 0)
                {
                    GameManager.instance.shouldTransition = false;
                    shouldLoadAfterFade = false;
                    SceneManager.LoadScene(sceneToLoad); //Load Scene

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //When an object enters the trigger zone
    {
        if(other.tag == "Player") //when the object with Player tag enters the trigger zone
        {
            if (GameManager.instance.shouldTransition)
            {
                shouldLoadAfterFade = true;
                UIFade.instance.FadeToBlack();
                PlayerMovement.instance.areaTransitionName = areaTransitionName; //Sends the area exit name to the Player script

            }
            //SceneManager.LoadScene(sceneToLoad); //Load scene
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") //when the object with Player tag enters the trigger zone
        {
            GameManager.instance.shouldTransition = true;
        }
    }
}
