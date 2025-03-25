using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeAttackTimer : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float timelimit; // 제한 시간
    
    [field: SerializeField]
    public UnityEvent OnTimeOver { get; private set; }
    
    // 타이머 재생
    public void Play()
    {
        StartCoroutine(TimeAttackSequence());
    }
    
    // 타이머 중지
    public void Stop()
    {
        StopAllCoroutines();
    }
    
    // 타임 어택 기능을 담당하는 코루틴
    private IEnumerator TimeAttackSequence()
    {
        yield return new WaitForSeconds(timelimit);
        OnTimeOver?.Invoke();
    }
}
