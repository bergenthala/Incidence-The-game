using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{

    public GameObject player;

    public CameraController theCamera;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        if(PlayerMovement.instance == null) //if player doesn't exist
        {
            Instantiate(player); //Creates player
            theCamera.target = PlayerMovement.instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
