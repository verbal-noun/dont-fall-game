using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] sentences;
    
    private TextMeshProUGUI text;
    [SerializeField]
    private int index;
    void Start()
    {
        StartCoroutine(Type());
    }


    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray()){
            text.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    void NextSentence()
    {
        if (index < sentences.Length - 1){
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
    }
}
