using UnityEngine;
using UnityEngine.Events;

public class UsableBone : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }
    [SerializeField] private GameObject Destroyer;

    public void Use(GameObject actor, GameObject target)
    {
        IItemInteractable item = target.GetComponent<IItemInteractable>();
        if (item != null)
        {
            item.InteractUseItem(actor, gameObject);
        }
        
        if (Destroyer == target)
        {
            actor.GetComponent<EventHandler>()?.SetNullUsable();
            Destroy(gameObject);
        }
    }
}
