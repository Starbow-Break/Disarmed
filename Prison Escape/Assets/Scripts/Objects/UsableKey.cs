using UnityEngine;
using UnityEngine.Events;

public class UsableKey : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }
    [SerializeField] private GameObject Destroyer;

    public void Use(GameObject actor, GameObject target)
    {
        if (target == Destroyer)
        {
            IItemInteractable itemInteractable = target.GetComponent<IItemInteractable>();
            if (itemInteractable != null)
            {
                itemInteractable.InteractUseItem(actor, gameObject);
            }

            EventHandler eventHandler = actor.GetComponent<EventHandler>();
            eventHandler.SetNullUsable();
            Destroy(gameObject);
        }
    }
}
