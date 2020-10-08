using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float speed = 25f;
    public float terrainSize = 128f;
    public float startHeight = 40f;
    public CharacterController controller;
    public float rotateSpeed = 1f;
    private float xRotation;
    private float yRotation;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        cam.transform.position = new Vector3(terrainSize/2, startHeight, terrainSize/2);
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {   
        // Movement of camera on its relative x and z axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = cam.transform.right*x+cam.transform.forward*z;
        controller.Move(move*speed*Time.deltaTime);
        
        // Rotation of camera
        xRotation += Input.GetAxis("Mouse X") * rotateSpeed;
        yRotation += Input.GetAxis("Mouse Y") * rotateSpeed;
        cam.transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);

        if (Input.GetKeyDown("space")) {
            cam.transform.position = new Vector3(terrainSize/2, startHeight, terrainSize/2);
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
