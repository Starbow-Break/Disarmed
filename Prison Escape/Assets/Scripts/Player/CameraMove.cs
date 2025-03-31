using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private GameObject eyes;
    
    private Vector2 lookDirection = Vector2.zero;
    private float xRotation = 0f;

    private void LateUpdate()
    {
        RotateCamera();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector2 adjustedInput = moveInput * mouseSensitivity * Time.deltaTime;
        
        lookDirection = adjustedInput;
    }
    private void RotateCamera()
    {
        transform.Rotate(Vector3.up * lookDirection.x);
        
        xRotation -= lookDirection.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        eyes.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
