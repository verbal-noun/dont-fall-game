using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 1 means its facing right, -1 facing left
    public GameObject tower;
    public float jumpChargeSpeed = 5f;
    public float maxJumpPower = 10f;
    public LayerMask platformLayerMask;
    public GameObject character;
    private Collider playerCollider;
    private Rigidbody rigidbody;
    private int direction = 1;
    //Jumping Power / Distance
    private float jumpPower = 0;
    //Buffer Distance from ground to enable jump
    public float jumpBuffer = 0.1f;
    private Rigidbody rb;


    //Debug
    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;
    Collider m_Collider;
    RaycastHit m_Hit;
    bool grounded;

    void Start()
    {
        playerCollider = character.GetComponent<Collider>();
        rigidbody = character.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

    }
    void Jump()
    {
        rigidbody.velocity += Vector3.up * jumpPower;
        tower.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 10f * direction,0);
        //AddTorque(Vector3.up * direction * 10f, ForceMode.VelocityChange);
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
        if (isStatic() && grounded)
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
            else if (Input.GetButtonUp("Jump"))
            {
                Jump();
                jumpPower = 0;
            }
        }
        else if (grounded && !isStatic()){
            //tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        Debug.Log("Jumping Power: " + grounded);
    }

    bool isGrounded()
    {

        RaycastHit hit;
        Collider pc = playerCollider;
        // bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f + 0.1f);
        bool isGrounded = Physics.BoxCast(transform.position, pc.bounds.extents * 0.99f, Vector3.down, out hit, transform.rotation, jumpBuffer, platformLayerMask);
        
        m_HitDetect = isGrounded;
        m_Hit = hit;

        //Debug.Log(isGrounded);
        return isGrounded;
    }

    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.tag == "Platform"){
    //         grounded = true;
    //     }
    // }

    // private void OnCollisionStay(Collision other) {
        
    // }
    // private void OnCollisionExit(Collision other) {
    //     if (other.gameObject.tag == "Platform"){
    //         grounded = false;
    //     }
    //}
    bool isStatic(){
        return ApproximateToZero(rigidbody.velocity.x) && ApproximateToZero(rigidbody.velocity.y) && ApproximateToZero(rigidbody.velocity.z);
    }
    bool ApproximateToZero(float x){
        return Mathf.Abs(x) < 0.1f;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float m_MaxDistance = jumpBuffer;
        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, Vector3.down * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, Vector3.down * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_MaxDistance, transform.localScale);
        }
    }
}
