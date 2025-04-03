using Unity.Cinemachine;
using UnityEngine;

public class CarpetCamera : MonoBehaviour, IFocusable
{
    [SerializeField] private GameObject targetcamera;
    
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: targetcamera.name, 
            beforeSwitch: () => PlayerLoading.PlayerSetStop(),
            afterSwitch: () => PlayerLoading.SetEnableInput(true));
        
        CursorLocker.instance.UnlockCursor();
    }

    public void UnFocus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Player Camera",
            beforeSwitch: () => PlayerLoading.SetEnableInput(false),
            afterSwitch: () => PlayerLoading.PlayerSetStart());
        
        CursorLocker.instance.LockCursor();
    }
}
