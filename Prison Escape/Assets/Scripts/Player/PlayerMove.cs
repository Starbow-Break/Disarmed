using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerController_TMP playerController;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private GameObject mainCamera;
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 inputDirection = Vector2.zero;
    private float xRotation = 0f;

    private void Start()
    {
        Vector2 StartDirection = new Vector2(transform.eulerAngles.x, transform.eulerAngles.z);
        lookDirection = StartDirection;
        RotateCamera();
    }

    private void OnEnable()
    {
        playerController.OnMoveEvent += MoveDir;
        cameraMove.OnLookEvent += LookDir;
    }
    
    private void OnDisable()
    {
        playerController.OnMoveEvent -= MoveDir;
        cameraMove.OnLookEvent -= LookDir;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        UpdateMoveDirection();
        RotateCamera();
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MoveDir(Vector2 direction)
    {
        inputDirection = direction;
    }

    private void LookDir(Vector2 direction)
    {
        lookDirection = direction;
    }

    private void MovePlayer()
    {
        characterController.Move(moveDirection * (Time.deltaTime * moveSpeed));
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.fixedDeltaTime;
        }
    }
    private void UpdateMoveDirection()
    {
        float previousY = moveDirection.y;
        moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        moveDirection.y = previousY;
    }


    private void RotateCamera()
    {
        transform.Rotate(Vector3.up * lookDirection.x);
        
        xRotation -= lookDirection.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

