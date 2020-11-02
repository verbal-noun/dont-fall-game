using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame ()

    {
        if (SceneManager.GetActiveScene ().buildIndex == 0) {
            AudioManager.instance.Stop ("Theme");
        }
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

    }

    public void QuitGame () {
        Debug.Log ("QUIT!");
        Application.Quit ();
    }

}