using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    private Collider collider;
    private bool active = false;
    public Dialog dialog;

    public string[] sentences;
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update(){
        if (active && Input.GetKeyDown(KeyCode.E)){
            dialog.SetSentences(sentences);
            dialog.StartDialog();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            active = true;
        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){
            active = false;
        }
    }
}
