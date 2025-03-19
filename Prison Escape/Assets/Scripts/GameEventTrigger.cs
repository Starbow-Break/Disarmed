using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private bool repeat = false; // 반복 트리거 가능 여부
    
    [field: SerializeField]
    public UnityEvent OnTrigger { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger.Invoke();
            if (!repeat)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
