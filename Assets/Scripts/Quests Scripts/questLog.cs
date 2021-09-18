using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class questLog : MonoBehaviour
{

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    [SerializeField]
    private TextMeshProUGUI questDescription;

    public static questLog instance;

    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddQuest(string quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);
        Debug.Log("Quest Added");

     

        QuestScript qs = go.GetComponent<QuestScript>();
        qs.quest = quest;
    

        go.GetComponent<TextMeshProUGUI>().text = quest;

   
    }


    public void ShowDescription(string quest)
    {
       if (selected != false)
       {
            QuestScript qs = GetComponent<QuestScript>();
            qs.Deselect();
            Debug.Log("quest Deselected");
       }
       else
        {
            Debug.Log("No quest to deselect");
        }

        selected = true;

        questDescription.text = string.Format("{0}", quest);
    }
}
