using System;
using UnityEngine;

public class ActiveCauldron : MonoBehaviour
{
    public static event Action<GameObject> OnCauldron;

    private void OnEnable()
    {
        OnCauldron += CauldronEnter;
    }

    private void OnDisable()
    {
        OnCauldron -= CauldronEnter;
    }

    public void TriggerCauldron(GameObject player)
    {
        OnCauldron?.Invoke(player);
    }

    private void CauldronEnter(GameObject player)
    {
        
    }
}
