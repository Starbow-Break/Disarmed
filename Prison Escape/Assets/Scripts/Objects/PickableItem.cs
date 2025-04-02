using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    // 아이템이 집힐 때 동작하는 로직
    public virtual GameObject PickUp()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.isKinematic = true;
        }
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        return gameObject;
    }
}


