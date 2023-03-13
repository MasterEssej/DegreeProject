using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField] private float cameraDistance;
    
    //Attach camera to player
    //Relative position to player will be (0, cameraDistance, 0) with rotation (90, 0, 0)
    //On camera change relative position: (0, 0, cameraDistance) with rotation (0, 0, 0)
    //Have camera rotate around Y with player (on "world" rotation, not looking around)
}
