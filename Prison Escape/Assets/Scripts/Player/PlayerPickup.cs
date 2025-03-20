using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private Transform pickUpParent;
    public GameObject inHandItem { get; private set; }
    private Quaternion pickUpRotation;

    public void PickUp(IPickable pickable)
    {
        inHandItem = pickable.PickUp();
        inHandItem.transform.SetParent(pickUpParent, false);
        inHandItem.transform.localPosition = Vector3.zero;
    }

    public void ChangeItem(GameObject target)
    {
        if (inHandItem == null || target == null)
            return;
        inHandItem.transform.position = target.transform.position;
        inHandItem.transform.rotation = target.transform.rotation;
        inHandItem.transform.SetParent(target.transform.parent);
    }

    public void CopyItem(GameObject target)
    {
        string targetName = target.name;
        GameObject newItem = Instantiate(target, Vector3.zero, target.transform.localRotation);
        newItem.name = targetName;
        newItem.GetComponent<Highlight>()?.SetHighlight(false);
        IPickable newPickable = newItem.GetComponent<IPickable>();
        PickUp(newPickable);
        inHandItem.transform.localRotation = newItem.transform.rotation;
    }

    public void DestroyinHandItem()
    {
        Destroy(inHandItem);
    }
}
