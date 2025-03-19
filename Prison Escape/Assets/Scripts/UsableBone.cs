using UnityEngine;
using UnityEngine.Events;

public class UsableBone : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }

    public void Use(GameObject actor)
    {
        Destroy(gameObject);
    }
}
