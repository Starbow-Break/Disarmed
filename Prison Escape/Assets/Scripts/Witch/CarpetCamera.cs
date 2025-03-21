using Unity.Cinemachine;
using UnityEngine;

public class CarpetCamera : MonoBehaviour, IFocusable
{
    [SerializeField] private GameObject targetcamera;
    
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera(targetcamera.name);
        CursorLocker.instance.UnlockCursor();
        actor.GetComponent<PlayerMove>().enabled = false;
    }

    public void UnFocus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera("Player Camera");
        CursorLocker.instance.LockCursor();
        actor.GetComponent<PlayerMove>().enabled = true;
    }
}
