using UnityEngine;

public class CheatScript : MonoBehaviour {

    public GameObject mountain;
    private bool cheatMode = false;

    private int speed = 40;

    public void FixedUpdate () {

        if (Input.GetKey (KeyCode.LeftShift)) {

            Debug.Log ("Mode activated");
            this.GetComponent<PlayerController> ().enabled = false;
            this.GetComponent<BetterGravity> ().enabled = false;
            this.GetComponent<Rigidbody> ().useGravity = false;

            if (Input.GetKey (KeyCode.UpArrow)) {
                this.transform.position += Vector3.up;
            }

            if (Input.GetKey (KeyCode.DownArrow)) {
                this.transform.position -= Vector3.up;
            }

            if (Input.GetKey (KeyCode.LeftArrow)) {
                mountain.transform.Rotate (Vector3.down * speed * Time.deltaTime);
            }

            if (Input.GetKey (KeyCode.RightArrow)) {
                mountain.transform.Rotate (Vector3.up * speed * Time.deltaTime);
            }

        }

        if (Input.GetKeyUp (KeyCode.LeftShift)) {
            this.GetComponent<PlayerController> ().enabled = true;
            this.GetComponent<BetterGravity> ().enabled = true;
            this.GetComponent<Rigidbody> ().useGravity = true;
        }
    }
}