using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame -> keypress, mouseclick, etc belong here. 
    void Update()
    {
        //jump
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }
    }

    //FixedUpdate is called once every physics update. Unity updates 100x a second.
    private void FixedUpdate() {
        if(jumpKeyWasPressed){
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        }
    }
}
