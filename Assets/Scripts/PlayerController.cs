using System.Collections;
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

    [SerializeField]
    private Animator animator;

    private AudioManager audio;

    void Awake()
    {
        playerCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        audio = FindObjectOfType<AudioManager>();

        powerbar.SetPower(0);
        powerbar.SetMaxValue(maxJumpPower);
        powerbar.SetLowHighColor(maxJumpPower / 3, maxJumpPower * 2 / 3);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

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

        grounded = isGrounded();
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Vertical Speed", rigidbody.velocity.y);
        animator.SetFloat("Horizontal Speed", Mathf.Abs(tower.GetComponent<Rigidbody>().angularVelocity.y));

        animator.SetBool("isCharging", Input.GetButton("Jump"));

        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpPower = 0;
                audio.Play("JumpCharge");

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


    //Ground check
    bool isGrounded()
    {

        RaycastHit hit;
        Collider pc = playerCollider;
        
        //Center
        bool ray1 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray2 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray3 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);

        //Back
        bool ray4 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray5 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray6 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z + pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);


        //Front
        bool ray7 = Physics.Raycast(new Vector3(pc.bounds.center.x - pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray8 = Physics.Raycast(new Vector3(pc.bounds.center.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);
        bool ray9 = Physics.Raycast(new Vector3(pc.bounds.center.x + pc.bounds.extents.x, pc.bounds.center.y, pc.bounds.center.z - pc.bounds.extents.z), Vector3.down, out hit, pc.bounds.extents.y + jumpBuffer);


        //Debug.Log(isGrounded);
        return ray1 || ray2 || ray3 || ray4 || ray5 || ray6 || ray7 || ray8 || ray9;
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
        tower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void PlayLand(){
        audio.PlayOneShot("Land");
    }
}
