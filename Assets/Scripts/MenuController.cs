using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
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
    private int direction = 1;
    //Jumping Power / Distance
    private float jumpPower = 18;
    //Buffer Distance from ground to enable jump
    public float jumpBuffer = 0.1f;
    private Rigidbody rb;
    private bool grounded;
    public float turnSpeed = 10f;
    public float walkSpeed = 3f;

    [SerializeField]
    private GameObject jumpAndLand;

    public float wallBuffer = 1f;
    public bool walkEnabled = true;
    private Animator animator;
    private AudioManager audio;
    private bool btnDownJump = false;
    private bool btnJump = false;
    private bool btnUpJump = false;
    private bool isOnGround = false;
    private Rigidbody trb;

    private float waitTime = 4.5f; 
    private bool playerJump = false; 

    public GameObject GetTower(){
        return tower;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        audio = FindObjectOfType<AudioManager>();
        character = transform.Find("Character").gameObject;
        trb = tower.GetComponent<Rigidbody>();

        Debug.Log("Starting coroutine");
        StartCoroutine(JumpCoroutine());

        powerbar.SetPower(0);
        powerbar.SetMaxValue(maxJumpPower);
        powerbar.SetLowHighColor(maxJumpPower / 3, maxJumpPower * 2 / 3);

        
    }


    void Jump()
    {
        audio.PlayJump(jumpPower/maxJumpPower);
        rb.velocity += Vector3.up * jumpPower;
        //Debug.Log(Input.GetAxis("Horizontal") + "____"  + direction);
        if (Input.GetAxis("Horizontal") == Mathf.Sign(direction)){
            trb.AddTorque(Vector3.up * direction * angularSpeed, ForceMode.VelocityChange);
        }
        animator.SetBool("Grounded", false);
        PlayParticleJumpAndLand();
    }

    void PlayParticleJumpAndLand(){
        GameObject p = Instantiate(jumpAndLand, transform.position, transform.rotation);
        p.transform.parent = tower.transform;
        p.transform.localScale = new Vector3(1,1,1);
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
        animator.SetFloat("Vertical Speed", rb.velocity.y);
        animator.SetFloat("Horizontal Speed", Mathf.Abs(trb.angularVelocity.y));
        animator.SetBool("isCharging", playerJump);

        float h = Input.GetAxis("Horizontal");
        //Debug.Log(CheckWall());

        if (grounded)
        {   
            if (isOnGround && walkEnabled){
                if (!CheckWall() && ((h < 0f  && Mathf.Round(trb.angularVelocity.y) <= 0f) || 
                (h > 0f  && Mathf.Round(trb.angularVelocity.y) >= 0f))){
                    //trb.AddTorque(Vector3.up * h * walkSpeed * 0.001f, ForceMode.VelocityChange);
                    trb.angularVelocity = h * Vector3.up * walkSpeed * 0.1f;
                }
                else {
                    trb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    //Ground check
    bool isGrounded()
    {
        Collider pc = playerCollider;

        Bounds below = new Bounds();
        below.center = new Vector3(pc.bounds.center.x, pc.bounds.center.y - jumpBuffer, pc.bounds.center.z);
        below.extents = pc.bounds.extents;

        bool isHit = Physics.CheckBox(below.center, below.extents, transform.rotation, platformLayerMask);
        //Debug.Log(isGrounded);
        return isHit;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isGrounded() && !CheckWall()){
            isOnGround = true;
        }
        else{
            audio.PlayOneShot("Hit");
        }
    }

    private void OnCollisionExit(Collision other) {
        if (isGrounded() && rb.velocity.y > 0f){
            isOnGround = false;
        }
    }

    bool CheckWall(){
        Collider pc = playerCollider;

        Bounds dir = new Bounds();
        dir.center = new Vector3(pc.bounds.center.x + (wallBuffer * direction), pc.bounds.center.y + jumpBuffer, pc.bounds.center.z);
        dir.extents = pc.bounds.extents;

        bool isHit = Physics.CheckBox(dir.center, dir.extents, transform.rotation, platformLayerMask);
        
        return isHit;
    }
    public void PlayLand(){
        PlayParticleJumpAndLand();
        audio.PlayOneShot("Land");
    }

    IEnumerator JumpCoroutine() {
        
        while(true) {
            // Amount of time to wait before next jump 
            yield return new WaitForSecondsRealtime(waitTime);

            // Initiate jump sequence 
            trb.angularVelocity = Vector3.zero;
            Jump(); 
        }
        
    }
}
