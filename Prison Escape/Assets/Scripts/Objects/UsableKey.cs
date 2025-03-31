using UnityEngine;
using UnityEngine.Events;

public class UsableKey : PickableItem, IUsable
{
    public void Use(GameObject actor)
    {
        Destroy(gameObject);
    }
}
