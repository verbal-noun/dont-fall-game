using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    private float startTime;
    void Start() {
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || Time.time - startTime > 34.5)
        {
            AudioManager.instance.Play("Theme");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
