using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Unity.Mathematics;

public class CubeContrller : NetworkBehaviour
{
    [Header("Base setup")]
    public float walkingSpeed = 7.5f;
    public float runingSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0.0f;
    
    [HideInInspector]
    public bool canMove = true;
    
    [SerializeField]
    private float cameraYOffset = 0.4f;
    
    [Header("Camera")]
    private Camera CubeplayerCamera;
    [SerializeField] private Transform cameraPosition;
    
    [Header("Animator Setup")]
    public Animator animator;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            CubeplayerCamera = Camera.main;
            CubeplayerCamera.transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, cameraPosition.position.z);
            CubeplayerCamera.transform.rotation = cameraPosition.rotation; 
            CubeplayerCamera.transform.SetParent(transform);
        }
        else
        {
            gameObject.GetComponent<CubeContrller>().enabled = false;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        bool isRunning = false;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        float curSpeedX = canMove ? (isRunning ? runingSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runingSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump")&& canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        //움직임 컨트롤러
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
