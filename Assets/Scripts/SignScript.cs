using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    private Collider collider;
    private bool active = false;
    public Dialog dialog;
    private bool inbox = false;
    public string[] sentences;
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update(){
        if (active && Input.GetKeyDown(KeyCode.E)){
            dialog.SetSentences(sentences);
            dialog.StartDialog();
            active = false;
        }

        if (!dialog.transform.Find("TextBox").gameObject.activeSelf && inbox){
            active = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            active = true;
            inbox = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player"){
            active = false;
            inbox = false;
        }
    }
}
