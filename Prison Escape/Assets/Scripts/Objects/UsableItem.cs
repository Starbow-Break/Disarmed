using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UsableItem : PickableItem, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    public void Use(GameObject actor, GameObject target)
    {
        Debug.Log($"Use {gameObject.name}!");
        OnUse?.Invoke();
        Destroy(gameObject);
        actor.GetComponent<EventHandler>()?.SetNullUsable();
    }
}
