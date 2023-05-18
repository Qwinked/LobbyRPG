using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public GameObject playerCamera;
    private float cameraOrbitSpeed = 2f;
    private float cameraDistance = 8f;
    private Vector2 cameraRotationLimits = new Vector2(-60f, 60f);
    private float cameraYaw; // Horizontal rotation around the player.
    private float cameraPitch; // Vertical rotation around the player.
    private bool isCameraMovement; // Flag to indicate if the camera is being moved.
    private Vector3 movementDirection; // Direction for player movement.
    private bool isFacingCameraDirection; // Flag to indicate if the player is facing the camera direction.

    
    public override void OnNetworkSpawn()
    {
        /*
        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + "; randomNumber:" + randomNumber.Value);
        };
        */

        // Enables the camera for the local player.
        playerCamera.SetActive(IsLocalPlayer);
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window.
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

        // Camera Orbit
        if (Input.GetMouseButton(0)) {
            isCameraMovement = true;
            cameraYaw += Input.GetAxis("Mouse X") * cameraOrbitSpeed;
            cameraPitch -= Input.GetAxis("Mouse Y") * cameraOrbitSpeed;
            cameraPitch = Mathf.Clamp(cameraPitch, cameraRotationLimits.x, cameraRotationLimits.y);
        }

        Quaternion rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
        Vector3 negDistance = new Vector3(0f, 0f, -cameraDistance);
        Vector3 position = rotation * negDistance + transform.position;

        playerCamera.transform.rotation = rotation;
        playerCamera.transform.position = position;

        // Player Movement
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir += playerCamera.transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDir -= playerCamera.transform.forward;
        if (Input.GetKey(KeyCode.A)) moveDir -= playerCamera.transform.right;
        if (Input.GetKey(KeyCode.D)) moveDir += playerCamera.transform.right;

        moveDir.y = 0f;
        moveDir.Normalize();

        if (Input.GetMouseButtonDown(1)) {
            isFacingCameraDirection = true; // The player is facing the camera direction.
        }

        if (isFacingCameraDirection) {
            movementDirection = playerCamera.transform.forward;
        }
        else {
            movementDirection = moveDir;
        }

        // Player movement based on input.
        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonUp(1)) {
            isFacingCameraDirection = false; // The player is no longer facing the camera direction.
        }
    }

    //Sets the camera offset
    private void LateUpdate()
    {
        // playerCamera.transform.position = transform.position + cameraOffset;
    }
}
