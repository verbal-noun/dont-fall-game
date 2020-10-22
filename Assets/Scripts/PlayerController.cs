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
    private GameObject character;
    private Powerbar powerbar;
    private BoxCollider playerCollider;
    private Rigidbody rigidbody;
    private int direction = 1;
    //Jumping Power / Distance
    private float jumpPower = 0;
    //Buffer Distance from ground to enable jump
    public float jumpBuffer = 0.1f;
    private Rigidbody rb;
    private bool grounded;

    public float turnSpeed = 10f;

    public float walkSpeed = 3f;
    public bool walkEnabled = true;

    private Animator animator;

    private AudioManager audio;

    private bool btnDownJump = false;
    private bool btnJump = false;
    private bool btnUpJump = false;

    private bool isOnGround = false;
    public GameObject GetTower(){
        return tower;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        audio = FindObjectOfType<AudioManager>();
        powerbar = FindObjectOfType<Powerbar>();
        character = transform.Find("Character").gameObject;

        powerbar.SetPower(0);
        powerbar.SetMaxValue(maxJumpPower);
        powerbar.SetLowHighColor(maxJumpPower / 3, maxJumpPower * 2 / 3);
    }

    void Update(){
        btnDownJump = Input.GetButtonDown("Jump");
        btnJump = Input.GetButton("Jump");
        btnUpJump = Input.GetButtonUp("Jump");

    }

    void Jump()
    {
        audio.PlayJump(jumpPower/maxJumpPower);

        rigidbody.velocity += Vector3.up * jumpPower;
        if (Mathf.Sign(Input.GetAxis("Horizontal")) == Mathf.Sign(direction)){
            tower.GetComponent<Rigidbody>().AddTorque(Vector3.up * direction * angularSpeed, ForceMode.VelocityChange);
        }
        animator.SetBool("Grounded", false);
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
        if (direction == 1)
        {
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime * turnSpeed);
        }
        else
        {
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation, Quaternion.LookRotation(Vector3.left), Time.deltaTime * turnSpeed);
        }
    }

    void FixedUpdate()
    {
        UpdateDirection();

        grounded = isGrounded();
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Vertical Speed", rigidbody.velocity.y);
        animator.SetFloat("Horizontal Speed", Mathf.Abs(tower.GetComponent<Rigidbody>().angularVelocity.y));

        animator.SetBool("isCharging", Input.GetButton("Jump"));

        float h = Input.GetAxis("Horizontal");

        if (grounded)
        {   
            if (isOnGround && walkEnabled){
                if (h != 0 && Mathf.Sign(h) == direction){
                    tower.GetComponent<Rigidbody>().AddTorque(Vector3.up * h * walkSpeed * 0.001f, ForceMode.VelocityChange);
                }
                else {
                    tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
            if (btnDownJump)
            {
                tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                jumpPower = 0;
                audio.Play("JumpCharge");

            }
            else if (btnJump)
            {
                tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                jumpPower += Time.deltaTime * jumpChargeSpeed;
                if (jumpPower > maxJumpPower)
                {
                    jumpPower = maxJumpPower;
                }
            }
            else if (btnUpJump || !btnJump)
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


    //Ground check
    bool isGrounded()
    {

        RaycastHit hit;
        Collider pc = playerCollider;
        
        // //Center
        // bool ray1 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray2 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray3 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);

        // //Back
        // bool ray4 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray5 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray6 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);


        // //Front
        // bool ray7 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray8 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        // bool ray9 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);

        bool isHit = Physics.CheckBox(new Vector3(pc.bounds.center.x, pc.bounds.center.y - jumpBuffer, pc.bounds.center.z), pc.bounds.extents, transform.rotation, platformLayerMask);
        //Debug.Log(isGrounded);
        return isHit;
    }

    // private void OnDrawGizmos() {
    //     RaycastHit hit;
    //     Gizmos.color = Color.red;
    //     //bool ray1 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y , pc.bounds.center.z), Vector3.down,  out hit, pc.bounds.extents.y + jumpBuffer);
    //     Gizmos.DrawRay(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y , pc.bounds.center.z), Vector3.down * (1+jumpBuffer));
    //      Gizmos.DrawRay(new Vector3(pc.bounds.center.x, pc.bounds.center.y , pc.bounds.center.z), Vector3.down * (1+jumpBuffer));
    //       Gizmos.DrawRay(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y , pc.bounds.center.z), Vector3.down * (1+jumpBuffer));

    //     //bool ray2 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z),  Vector3.down,  out hit, pc.bounds.extents.y + jumpBuffer);
    //     //bool ray3 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x , pc.bounds.center.y , pc.bounds.center.z), Vector3.down,  out hit, pc.bounds.extents.y + jumpBuffer);

    // }
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collison! Platform!");
        Vector3 av = tower.GetComponent<Rigidbody>().angularVelocity;
        if (isGrounded()){
            isOnGround = true;
            tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else{
            audio.PlayOneShot("Hit");
        }
        
    }

    private void OnCollisionExit(Collision other) {
        if (isGrounded()){
            isOnGround = false;
        }
    }
    public void PlayLand(){
        audio.PlayOneShot("Land");
    }
}
