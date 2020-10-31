using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTransition : MonoBehaviour
{
    public GameObject character;
    public Material skybox;

    private float currHeight;
    public float maxHeight = 180f;
    public float threshold1 = 0.3f;
    public float threshold2 = 0.6f;
    public float buffer = 2;


    // Update is called once per frame
    void Start() 
    {
        skybox.SetFloat("_Blend", 0);
    }

    void Update()
    {   
        currHeight = character.transform.position.y;
        //skybox.SetFloat("_Blend", (2.0f*(currHeight/maxHeight)));
        
        if (currHeight > threshold1*maxHeight && currHeight < threshold1*maxHeight + buffer) {
            skybox.SetFloat("_Blend", (currHeight-threshold1*maxHeight)/buffer);
        } else if (currHeight > threshold2*maxHeight && currHeight < threshold2*maxHeight + buffer) {
            skybox.SetFloat("_Blend", 1f+(currHeight-threshold2*maxHeight)/buffer);
        }

    }
}
