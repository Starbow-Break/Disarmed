using System;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private StartSwitchOn ToDialogue;
    [SerializeField] private Door door;

    private Dictionary<SwitchState, Action> stateActions;

    public static Func<SwitchState> onSwitch;
    public static event Action Reset;
    
    private void Awake()
    {
        InitializeStateActions();
    }

    public void TriggerSwitch()
    {
        SwitchEnter();
    }

    private void SwitchEnter()
    {
        var resultState = audioSource.isPlaying ? SwitchState.Again
            : onSwitch?.Invoke() ?? SwitchState.Failed;
        
        if (stateActions.TryGetValue(resultState, out var action))
        {
            action?.Invoke();
            ToDialogue.StartDialogue((int)resultState);
        }
    }

    private void InitializeStateActions()
    {
        stateActions = new Dictionary<SwitchState, Action>()
        {
            { SwitchState.Nodata, FailAction },
            { SwitchState.Success, SuccessAction },
            { SwitchState.Failed, FailAction },
            { SwitchState.Again, AgainAction }
        };
    }
   
    private void SuccessAction()
    {
        door.Open();
    }

    private void FailAction()
    {
        Reset?.Invoke();
    }

    private void AgainAction()
    {
        audioSource.Stop();
        Debug.Log("Again");
    }
}
