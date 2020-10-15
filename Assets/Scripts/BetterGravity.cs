using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterGravity : MonoBehaviour
{

    //Fall Speed
    public float fallMultiplier = 2.5f;
    [SerializeField]
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Credits to https://www.youtube.com/watch?v=7KiK0Aqtmzc
    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
}
