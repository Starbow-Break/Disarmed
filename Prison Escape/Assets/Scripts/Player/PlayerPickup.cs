using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform pickUpParent;
    public GameObject inHandItem { get; private set; }

    public void PickUp(IPickable pickable)
    {
        inHandItem = pickable.PickUp();
        inHandItem.transform.SetParent(pickUpParent.transform, false);
    }

    public void ChangeItem(GameObject target)
    {
        if (inHandItem == null || target == null)
            return;
        inHandItem.transform.position = target.transform.position;
        inHandItem.transform.rotation = target.transform.rotation;
        inHandItem.transform.SetParent(target.transform.parent);
    }
}
