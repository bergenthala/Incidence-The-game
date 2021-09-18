using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue")]
public class Dialogue : ScriptableObject 
{
    [System.Serializable]
    public class info
    {
        public CharacterProfile character;
       // public string name;
        //public Sprite portrait;
        [TextArea(3, 10)]
        public string myText;
    }

    [Header("Insert Dialogue Information Below")]
    public info[] DialogueInfo;
}
