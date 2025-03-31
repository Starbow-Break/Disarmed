using UnityEngine;
using UnityEngine.Events;

public class UsableBone : PickableItem, IUsable
{
    public void Use(GameObject actor)
    {
        Destroy(gameObject);
    }
}
