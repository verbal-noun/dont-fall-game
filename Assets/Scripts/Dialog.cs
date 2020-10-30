using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Following tutorial from https://www.youtube.com/watch?v=f-oSXg6_AMQ
public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] sentences;
    public float typingSpeed = 0.02f;
    [SerializeField]
    private TextMeshProUGUI text;
    
    private int index;

    public GameObject continueButton;
    public GameObject textbox;
    public void StartDialog()
    {
        if (textbox.activeSelf == true) return;
        textbox.SetActive(true);
        StartCoroutine(Type());
    }

    void Update(){
        if (text.text.Equals(sentences[index])){
            continueButton.SetActive(true);
        }

        if (continueButton.activeSelf && Input.GetKeyDown(KeyCode.E)){
            NextSentence();
        }

    }
    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray()){
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    void NextSentence()
    {
        if (index < sentences.Length - 1){
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else {
            text.text = "";
            continueButton.SetActive(false);
            textbox.SetActive(false);
        }
    }

    public void SetSentences(string[] sentences){
        this.sentences = sentences;
        index = 0;
    }
}
