using UnityEngine;

public class CheatScript : MonoBehaviour {
    public KeyCode specialKey; 
    public KeyCode upArrow; 
    public KeyCode downArrow; 

    public GameObject character; 


    public void Update() {
        if(Input.GetKey(KeyCode.LeftShift)) {
            
            if (Input.GetKey(KeyCode.UpArrow)) {
                this.GetComponent<PlayerController>().enabled = false;
                this.GetComponent<BetterGravity>().enabled = false;
                this.transform.position += Vector3.up;
            }

            if (this.GetComponent<PlayerController>().enabled == false) {
                this.GetComponent<PlayerController>().enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {

            }
        }
    }
}