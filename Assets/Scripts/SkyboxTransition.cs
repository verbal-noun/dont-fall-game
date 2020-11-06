using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTransition : MonoBehaviour
{
    public GameObject character;
    public Material skybox;
    public GameObject light;
    private Light lightComp;
    private float currHeight;
    public float maxHeight = 180f;
    public float threshold1 = 0.3f;
    public float threshold2 = 0.6f;
    public float buffer = 2;
    public float maxLightIntensity = 0.6f;
    public float minLightIntensity = 0.2f;


    // Update is called once per frame
    void Start() 
    {
        skybox.SetFloat("_Blend", 0);
        
        // Get the light component of our light gameobject
        lightComp = light.GetComponent<Light>();
    }

    void Update()
    {   
        currHeight = character.transform.position.y;
        
        //Interpolate light intensity based on height
        lightComp.intensity = (maxLightIntensity - (maxLightIntensity-minLightIntensity)*(currHeight/maxHeight));
        
        // Fade transition skybox (via blend) at the first threshold height
        if (currHeight > threshold1*maxHeight && currHeight < threshold1*maxHeight + buffer) {
            skybox.SetFloat("_Blend", (currHeight-threshold1*maxHeight)/buffer);

        // Fade transition skybox (via blend) at the second threshold height    
        } else if (currHeight > threshold2*maxHeight && currHeight < threshold2*maxHeight + buffer) {
            skybox.SetFloat("_Blend", 1f+(currHeight-threshold2*maxHeight)/buffer);
        }

    }
}
