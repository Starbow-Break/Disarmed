using System;
using UnityEngine;

public class PlayerTrapManager : MonoBehaviour
{
    private void OnEnable()
    {
        ActiveTrap.OnPlayerEnterTrap += TriggerTrap;
    }

    private void OnDisable()
    {
        ActiveTrap.OnPlayerEnterTrap -= TriggerTrap;
    }

    private void TriggerTrap()
    {
        Debug.Log("Trap triggered");
    }
}
