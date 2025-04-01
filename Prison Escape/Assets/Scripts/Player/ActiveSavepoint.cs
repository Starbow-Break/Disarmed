using System;
using UnityEngine;

public class ActiveSavepoint : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerEnterSavepoint;

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnterSavepoint?.Invoke(gameObject);
    }
}
