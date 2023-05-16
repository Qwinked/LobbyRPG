using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;


public class CameraFollow : NetworkBehaviour
{

    public GameObject cameraTarget;
    public Vector3 offset;
    // Start is called before the first frame update




    // Update is called once per frame
    void LateUpdate()
    {
        
       // cameraTarget.transform.position = transform.position + offset;
    }
}

