using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 플레이어 이동을 제어 변수
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float moveSpeed = 5f;
    
    // 플레이어가 현재 이동 여부를 알려줌
    public static event Action<bool> PlayerMoved;
    
    // Vector2로 받은 값을 Vector3로 변환
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputDirection = Vector2.zero;
    
    // 플레이어 이동 여부 판단 지표
    private const float StopSpeed = 0.0f;
    private bool moveState;

    private void Awake()
    {
        moveState = false;
    }
    private void Update()
    {
        UpdateMoveDirection();
        ApplyGravity();
        MovePlayer();
        UpdateWalkState();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        
        inputDirection = moveInput;
    }

    private void UpdateMoveDirection()
    {
        Vector3 newMoveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        
        moveDirection.x = newMoveDirection.x;
        moveDirection.z = newMoveDirection.z;
    }
    
    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.fixedDeltaTime;
        }
    }
    
    private void MovePlayer()
    {
        var move = moveDirection * (Time.deltaTime * moveSpeed);
        
        characterController.Move(move);
    }

    private void UpdateWalkState()
    {
        var xzVelocity = characterController.velocity - characterController.velocity.y * Vector3.up;
        var currentMoveState = xzVelocity.magnitude > StopSpeed;

        if (currentMoveState != moveState)
        {
            PlayerMoved?.Invoke(currentMoveState);
            moveState = currentMoveState;
        }
    }
}

