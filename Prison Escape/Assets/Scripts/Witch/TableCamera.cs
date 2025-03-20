using UnityEngine;
using UnityEngine.Splines;

public class TableCamera : MonoBehaviour, IFocusable
{
    public void Focus(GameObject actor)
    {
        CameraSwitcher.instance.SwitchCamera("Camera_Table");
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
