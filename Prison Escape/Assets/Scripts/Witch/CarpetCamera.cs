using UnityEngine;

public class CarpetCamera : MonoBehaviour, IFocusable
{
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera("Camera_RedCarpet");
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
