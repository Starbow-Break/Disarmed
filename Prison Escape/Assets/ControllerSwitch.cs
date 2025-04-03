using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitch : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders;
    [SerializeField] private KeyCode openKey;
    [SerializeField] private KeyCode closeKey;

    private void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            ColliderEnable();
        }
        else if (Input.GetKeyDown(closeKey))
        {
            ColliderDisable();
        }
    }

    private void ColliderEnable()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
    }

    private void ColliderDisable()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
