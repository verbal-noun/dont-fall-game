using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHorizontal : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tower;
    //Speed loss determines how much speed is lost when bouncing, a higher value will have a bouncier result
    public float speedLoss = 0.7f;
    private Collider collider;

    private AudioManager audio;
    void Start()
    {
        tower = FindObjectOfType<PlayerController>().GetTower();
        collider = GetComponent<Collider>();
        audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Sign")
        {
            Vector3 av = tower.GetComponent<Rigidbody>().angularVelocity;

            if (Mathf.Abs(av.y) > 0.1)
            {
                audio.PlayOneShot("Hit");
            }

            tower.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, av.y * -speedLoss, 0);
        }
    }
}

