using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonPlayerCard : MonoBehaviour
{
    public GameObject BackPrefab;
    public GameObject TempSprites;
    public void Summon()
    {
        TempSprites = (Instantiate(BackPrefab) as GameObject);
        TempSprites.transform.parent = transform;
       TempSprites.transform.position = new Vector2(7.5f, 8.5f);

    }
}