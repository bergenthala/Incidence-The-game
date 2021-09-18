using UnityEngine;

[System.Serializable]
public class Quest
{

    [SerializeField]
    private string title;


    public string questTitle
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
