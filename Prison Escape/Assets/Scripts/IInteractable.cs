using UnityEngine;

public interface IFocusable
{
    public void Focus(PlayerController player);
    public void UnFocus(PlayerController player);
}
