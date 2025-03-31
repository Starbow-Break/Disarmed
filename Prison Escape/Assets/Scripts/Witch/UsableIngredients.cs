using UnityEngine;
using UnityEngine.Events;

public class UsableIngredients : PickableItem, IUsable
{
    public void Use(GameObject actor)
    {
        Destroy(gameObject);
    }
}
