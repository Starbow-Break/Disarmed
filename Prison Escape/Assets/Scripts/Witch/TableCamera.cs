using UnityEngine;
using UnityEngine.Splines;

public class TableCamera : MonoBehaviour, IFocusable
{
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Camera_Table", 
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
