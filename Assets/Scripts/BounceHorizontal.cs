using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHorizontal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject tower;
    private Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Vector3 av = tower.GetComponent<Rigidbody>().angularVelocity;
        tower.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, av.y * -0.7f, 0);
    }
}
