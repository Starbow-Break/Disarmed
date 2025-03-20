using System;
using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
    [SerializeField] private GameObject cauldron;
    public static event Action OnSwitch;
    
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
                cauldronAction.ResetCauldron();

                Debug.Log("Not selected");
            }
            else if (cauldronAction.CaculateResult())
            {
                Debug.Log("Success");
            }
            else
            {
                cauldronAction.ResetCauldron();
                Debug.Log("Failed");
            }
        }
    }
}
