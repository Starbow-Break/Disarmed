using UnityEngine;
using UnityEngine.Events;

public class UsableBone : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }
    [SerializeField] private GameObject Destroyer;

    public void Use(GameObject actor)
    {
        if (Destroyer == actor)
        {
            IItemInteractable item = actor.GetComponent<IItemInteractable>();
            if (item != null)
            {
                item.InteractUseItem(actor, gameObject);
            }
            Destroy(gameObject);
        }
    }
}
