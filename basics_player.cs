using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame -> keypress, mouseclick, etc belong here. 
    void Update()
    {
        //jump
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //FixedUpdate is called once every physics update. Unity updates 100x a second.
    private void FixedUpdate() {
        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){
            return;
        }

        if(jumpKeyWasPressed){
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        //handle x of x,y,z 
        rigidBodyComponent.velocity = new Vector3(
            horizontalInput, 
            rigidBodyComponent.velocity.y, 
            0
        );
    }
    
}
