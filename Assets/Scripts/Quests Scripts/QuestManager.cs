using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerNames;


    public string[] questMarkerDescriptions;

    public bool[] questMarkerComplete;

    public static QuestManager instance;

    [SerializeField]
    private Quest[] quests;

    public bool recievedQuest = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkerComplete = new bool[questMarkerNames.Length];

    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Q))
       /* {
            MarkQuestComplete("Regain Health");
        }*/
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogError("Quest" + questToFind + "Does Not Exist");
        return 0;
    }

    public bool CheckQuestComplete(string questToCheck) 
    {
        if (GetQuestNumber(questToCheck) != 0 && recievedQuest)
        {
            recievedQuest = false;
            return questMarkerComplete[GetQuestNumber(questToCheck)];
       
        }
        return false;
    
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkerComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();



    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkerComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();

 

    }

    public void UpdateLocalQuestObjects() 
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }

    public void SaveQuestData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData() 
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }

            if (valueToSet == 0)
            {
                questMarkerComplete[i] = false;
            }
            else
            {
                questMarkerComplete[i] = true;
            }
        }



    }




}
