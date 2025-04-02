using System;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    [SerializeField] private GameObject guard; // 경비병
    [SerializeField] private List<Transform> routePoints; // 경비병의 경로
    [SerializeField] private float speed;

    public bool isPlaying { get; private set; } // 재생 여부
    public bool isFinish { get; private set; }  // 도착 여부
    public event Action OnFinish; // 이동 종료시 발생할 이벤트
    
    private Animator guardAnim; // 경비병의 애니메이터
    private int pointIndex; // 경비병이 지나간 지점 중 가장 나중에 지난 지점의 인덱스 번호
    
    private void Start()
    {
        guardAnim = guard.GetComponent<Animator>();
        Stop();
    }

    private void Update()
    {
        if (isFinish)
        {
            return;
        }
        
        if (pointIndex == routePoints.Count - 1)
        {
            isFinish = true;
            guardAnim.SetTrigger("Finish");
            OnFinish?.Invoke();
            return;
        }
        
        if (isPlaying)
        {
            Move(speed * Time.deltaTime);
        }
    }
    
    // 경비병을 인자로 받은 만큼 움직인다.
    public void Move(float distance)
    {
        while (pointIndex < routePoints.Count - 1)
        {
            // 다음 지점까지 남은 거리
            float distanceNextPoint = (routePoints[pointIndex + 1].position - guard.transform.position).magnitude;
            
            // 남은 이동거리가 다음 지점까지 남은 거리보다 작으면 이동 후 종료
            if (distance < distanceNextPoint)
            {
                guard.transform.position += distance * guard.transform.forward;
                break;
            }
            
            // 남은 이동거리가 다음 지점까지 남은 거리보다 많으면  다음 지점으로 이동 후 회전 변경
            pointIndex++;
            guard.transform.position = routePoints[pointIndex].position;
            guard.transform.rotation = routePoints[pointIndex].rotation;
            distance -= distanceNextPoint;
        }
    }
    
    // 재생
    public void Play()
    {
        guard.SetActive(true);
        isPlaying = true;
    }
    
    // 정지 및 초기 위치로 복귀
    public void Stop()
    {
        guard.transform.position = routePoints[0].position;
        guard.transform.rotation = routePoints[0].rotation;
        guard.SetActive(false);
        isPlaying = false;
        isFinish = false;
        pointIndex = 0;
    }

    #region GIZMOS
    private void OnDrawGizmosSelected()
    {
        DrawRouteGizmo();
        DrawForwardGizmo();
    }

    private void DrawRouteGizmo()
    {
        Gizmos.color = Color.magenta;
        for (int i = 1; i < routePoints.Count; i++)
        {
            Gizmos.DrawLine(routePoints[i - 1].position, routePoints[i].position);
        }
    }

    private void DrawForwardGizmo()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < routePoints.Count; i++)
        {
            Gizmos.DrawLine(routePoints[i].position, routePoints[i].position + routePoints[i].forward);
        }
    }
    #endregion
}
