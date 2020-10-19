using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHorizontal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject tower;
    //Speed loss determines how much speed is lost when bouncing, a higher value will have a bouncier result
    public float speedLoss = 0.7f;
    private Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();    
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        Vector3 av = tower.GetComponent<Rigidbody>().angularVelocity;

        tower.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, av.y * -speedLoss, 0);
    }
}
