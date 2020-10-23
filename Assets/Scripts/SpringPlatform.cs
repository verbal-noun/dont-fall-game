using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float offset = 5f;
    public float speed = 5f;
    private Rigidbody rb;
    private Vector3 op;
    private Vector3 target;
    private bool playerOn = false;
    void Start(){
        rb = GetComponent<Rigidbody>();
        op = transform.position;
        target = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
    }
    // private void Update() {
    //     if (playerOn){
    //         transform.position = Vector3.Lerp(op, target, speed * Time.deltaTime * 0.1f);
    //     }
    //     else{
    //         transform.localPosition = Vector3.Lerp(target, op, speed * Time.deltaTime * 0.1f);
    //     }
        
    //}
    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag.Equals("Player")){
            Debug.Log("player detected!");
            playerOn = true;
        }  
    } 
    private void OnCollisionExit(Collision other) {
        if (other.transform.tag.Equals("Player")){
            Debug.Log("player exiting!");
            playerOn = false;
        }  
    }
}
