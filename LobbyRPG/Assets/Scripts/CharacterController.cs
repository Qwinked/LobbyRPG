using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float horizontalInput = 1f;
    private float verticalInput = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move Player
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);
    }
}
