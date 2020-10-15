﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 1 means its facing right, -1 facing left
    public GameObject tower;
    public float jumpChargeSpeed = 5f;
    public float maxJumpPower = 10f;
    public float angularSpeed = 5f;
    public float minJumpThreshold = 1f;
    public LayerMask platformLayerMask;
    public GameObject character;
    public Powerbar powerbar;
    private Collider playerCollider;
    private Rigidbody rigidbody;
    private int direction = 1;
    //Jumping Power / Distance
    private float jumpPower = 0;
    //Buffer Distance from ground to enable jump
    public float jumpBuffer = 0.1f;
    private Rigidbody rb;
    private bool grounded;

    void Awake()
    {
        playerCollider = character.GetComponent<Collider>();
        rigidbody = character.GetComponent<Rigidbody>();
        
        powerbar.SetPower(0);
        powerbar.SetMaxValue(maxJumpPower);
        powerbar.SetLowHighColor(maxJumpPower/3, maxJumpPower * 2 /3 );
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

    }
    void Jump()
    {
        rigidbody.velocity += Vector3.up * jumpPower;
        tower.GetComponent<Rigidbody>().AddTorque(Vector3.up * direction * angularSpeed, ForceMode.VelocityChange);
    }
    void UpdateDirection()
    {
        float facing = Input.GetAxis("Horizontal");
        if (facing > 0)
        {
            direction = 1;
        }
        else if (facing < 0)
        {
            direction = -1;
        }
    }

    void FixedUpdate()
    {

        grounded = isGrounded();
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpPower = 0;
            }
            else if (Input.GetButton("Jump"))
            {
                jumpPower += Time.deltaTime * jumpChargeSpeed;
                if (jumpPower > maxJumpPower)
                {
                    jumpPower = maxJumpPower;
                }
            }
            else if (Input.GetButtonUp("Jump") || !Input.GetButton("Jump"))
            {
                if (jumpPower > minJumpThreshold)
                {
                    Jump();
                }

                jumpPower = 0;
            }
        }
        powerbar.SetPower(jumpPower);
        //Debug.Log("Jumping Power: " + jumpPower);
    }

    bool isGrounded()
    {

        RaycastHit hit;
        Collider pc = playerCollider;
        bool isGrounded = Physics.BoxCast(pc.bounds.center, pc.bounds.extents * 0.99f, Vector3.down, out hit, transform.rotation, jumpBuffer, platformLayerMask);


        //Debug.Log(isGrounded);
        return isGrounded;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collison! Platform!");
        tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // bool isStatic()
    // {
    //     return ApproximateToZero(rigidbody.velocity.x) && ApproximateToZero(rigidbody.velocity.y) && ApproximateToZero(rigidbody.velocity.z);
    // }
    // bool ApproximateToZero(float x)
    // {
    //     return Mathf.Abs(x) < 0.1f;
    // }

}
