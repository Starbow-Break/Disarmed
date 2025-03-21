using UnityEngine;
using UnityEngine.Events;

public class UsableIngredients : PickableItem, IUsable
{
    public UnityEvent OnUse { get; }
    [SerializeField] private GameObject Distroyer;

    private void Start()
    {
        if (Distroyer == null)
        {
            Distroyer = GameObject.FindWithTag("Cauldron");
        }
    }
    
    public void Use(GameObject target)
    {
        if (Distroyer == target)
        {
            Destroy(gameObject);
        }
    }
}
