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
    private int superJumpsRemaining;
    private int speedBoost;
    private float adjustSpeed;

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

        if(Input.GetKeyDown(KeyCode.D)){
            adjustSpeed += 1;
            if(speedBoost > 0){
                adjustSpeed += 5;
                speedBoost--;
            }
        }
        if(Input.GetKeyDown(KeyCode.A)){
            adjustSpeed = -1;
            if(speedBoost > 0){
                adjustSpeed -= 5;
                speedBoost--;
            }
        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)){
            adjustSpeed = 0;
        }

        //movement
        horizontalInput = Input.GetAxis("Horizontal");
    }

    //FixedUpdate is called once every physics update. Unity updates 100x a second.
    private void FixedUpdate() {
        //handle x of x,y,z with this line first, player can move in air. 
        rigidBodyComponent.velocity = new Vector3(horizontalInput + adjustSpeed, rigidBodyComponent.velocity.y, 0);

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){
            return;
        }

        if(jumpKeyWasPressed){
            float jumpPower = 5f;
            if(superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            //jumpheight
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

            jumpKeyWasPressed = false;
        }
        
    }

    //add coins that double jump height.
    private void OnTriggerEnter(Collider other) {
        //handle coins pickup, coin is layer 9
        if(other.gameObject.layer == 9){
            //other = coin. 
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

        if(other.gameObject.layer == 10){
            //other = speedBoost. 
            Destroy(other.gameObject);
            speedBoost++;
        }
    }
    
}
