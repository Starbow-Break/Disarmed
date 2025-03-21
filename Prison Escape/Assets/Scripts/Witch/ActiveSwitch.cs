using System;
using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    [SerializeField] private GameObject cauldron;
    [SerializeField] private StartSwitchOn ToDialogue;
    public static event Action OnSwitch;

    private enum SwitchState
    {
        Nodata,     // 솥에 데이터가 들어오지 않았을 경우
        Success,    // 솥에서 제약에 성공했을 경우
        Failed      // 솥에서 제약에 실패했을 경우
    }
    
    // 여기에서는 이제 cauldron에 들어 있는 값을 가지고 뭔갈 할 예정.
    // 그러니깐 OnSwitch가 발동하면 여기에서 작동이 되는지 debug log 출력하기

    private void OnEnable()
    {
        OnSwitch += SwitchEnter;
    }

    private void OnDisable()
    {
        OnSwitch -= SwitchEnter;
    }

    public void TriggeerSwitch()
    {
        OnSwitch?.Invoke();
    }

    private void SwitchEnter()
    {
        if (cauldron == null)
        {
            Debug.Log("Cauldron is null");
            return;
        }
        
        ActiveCauldron cauldronAction = cauldron.GetComponent<ActiveCauldron>();
        if (cauldronAction != null)
        {
            if (!cauldronAction.isSelected)
            {
                ToDialogue.StartDialogue((int)SwitchState.Nodata);
                cauldronAction.ResetCauldron();
                Debug.Log("Not selected");
            }
            else if (cauldronAction.CaculateResult())
            {
                ToDialogue.StartDialogue((int)SwitchState.Success);
                Debug.Log("Success");
            }
            else
            {
                ToDialogue.StartDialogue((int)SwitchState.Failed);
                cauldronAction.ResetCauldron();
                Debug.Log("Failed");
            }
        }
    }
}
