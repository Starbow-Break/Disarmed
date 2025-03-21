using UnityEngine;
using UnityEngine.Events;

public class UsableKey : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }
    [SerializeField] private GameObject Destroyer;

    public void Use(GameObject actor)
    {
        if (actor == Destroyer)
        {
            IItemInteractable itemInteractable = actor.GetComponent<IItemInteractable>();
            if (itemInteractable != null)
            {
                itemInteractable.InteractUseItem(actor, gameObject);
            }
            Destroy(gameObject);
        }
    }
}
