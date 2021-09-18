using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScript : MonoBehaviour
{

    public string quest;

    public string Description;

    private questLog questLogs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<TextMeshProUGUI>().color = Color.red;
        //Add something to check which quest was selected to give the appropriate description

        

        questLog.instance.ShowDescription(quest);
    }

    public void Deselect()
    {
        GameObject go = questLogs.gameObject;
       go.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

}
