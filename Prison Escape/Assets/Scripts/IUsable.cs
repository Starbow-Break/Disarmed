using UnityEngine;
using UnityEngine.Events;

public interface IUsable
{
    public void Use(GameObject actor, GameObject target);
    UnityEvent OnUse { get; }
}

