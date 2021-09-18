using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public Transform target;  //camera target

    public float smoothSpeed = 0.125f; // smooth camera movement speed. Smaller the number the faster it follows

    public Tilemap theMap; // the tile map
    public Vector3 offset; //offset of camera smoothing

    private Vector3 bottomLeftLimit; //map min vector points
    private Vector3 topRightLimit; //map max vector points

    private float offsetHeight; //Camera Height Offset
    private float offsetWidth; // Camera Width Offset

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerMovement.instance.transform; //set target to Player

        target = FindObjectOfType<PlayerMovement>().transform; //set target to Player (searches all objects in scene)

        offsetHeight = Camera.main.orthographicSize; //Tell current height of the camera (the screen of the game)
        offsetWidth = offsetHeight * Camera.main.aspect; //Gets the ratio of camera  to get the relative width

        bottomLeftLimit = theMap.localBounds.min + new Vector3(offsetWidth, offsetHeight, 0f); //Finding boundaries of tile map
        topRightLimit = theMap.localBounds.max + new Vector3(-offsetWidth, -offsetHeight, 0f);
        Debug.Log("bounds set to" + theMap.localBounds.min + "and" + theMap.localBounds.max);

        PlayerMovement.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z); //Follows the target position

        //keeps the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z); //Mathf.Clamp takes value and clamps it between 2 points, which sets the boundaries

        //might go over border
        /*Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition; //Camera smoothing*/
    }


}
