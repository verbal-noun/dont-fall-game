using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
         if (other.tag == "exit") {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         }
     }
}
