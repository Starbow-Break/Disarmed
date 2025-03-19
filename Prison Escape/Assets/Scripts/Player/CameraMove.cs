using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    
    public event Action<Vector2> OnLookEvent;
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void CallMoveEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector2 adjustedInput = moveInput * mouseSensitivity * Time.deltaTime;
        
        CallMoveEvent(adjustedInput);
    }
}
