using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    private float startTime;
    public bool skippable = true;
    public float timeTillSkip = 34.5f;

    void Start() {
        startTime = Time.time;
    }

    void Update()
    {   
        // Check if we can skip the cutscene, if not see if the cutscene has finished playing based on set time
        if ((Input.GetKeyDown("space") && skippable) || (Time.time - startTime > timeTillSkip))
        {
            //AudioManager.instance.Play("Theme");
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }
}
