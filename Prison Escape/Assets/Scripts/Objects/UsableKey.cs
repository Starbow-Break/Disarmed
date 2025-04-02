using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class UsableKey : PickableItem, IUsable
{
    private VisualEffect effect;

    private void Awake()
    {
        effect = GetComponentInChildren<VisualEffect>();
    }

    public override GameObject PickUp()
    {
        effect.Stop();
        return base.PickUp();
    }

    public void Use(GameObject actor)
    {
        Destroy(gameObject);
    }
}
