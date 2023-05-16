using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public GameObject playerCamera;
    private Vector3 cameraOffset = new Vector3(0,3,-8);
    
    
    public override void OnNetworkSpawn()
    {
        /*
        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + "; randomNumber:" + randomNumber.Value);
        };
        */

        //Enables the camera for the local player and disables all others.
        if (IsLocalPlayer == true)
            
            playerCamera.SetActive(true);
        else
            playerCamera.SetActive(false);
             
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if (!IsOwner) return; //As in the Server Owner

        

        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 100);
        }
        */

        //Player Movement
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    //Sets the camera offset
    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position + cameraOffset;
    }
}
