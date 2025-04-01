using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineCamera> cinemachineCameras;

    public static CameraSwitcher instance { get; private set; }
    public static CinemachineBrain cinemachineBrain { get; private set; }
    public static CinemachineCamera activeCamera { get; private set; }

    private readonly int ActivePriority = 10;
    private readonly int InActivePriority = 0;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        cinemachineBrain = Camera.main?.GetComponent<CinemachineBrain>();
        
        foreach (CinemachineCamera cinemachineCamera in cinemachineCameras)
        {
            if (cinemachineCamera.Priority == ActivePriority)
            {
                activeCamera = cinemachineCamera;
                break;
            }
        }
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }

        activeCamera = null;
    }

    private void Update()
    {
        Debug.Log(cinemachineBrain != null || cinemachineBrain.IsBlending);
    }
    
    // 카메라를 cameraName에 해당하는 카메라로 변경
    public void SwitchCamera(string cameraName, Action beforeSwitch = null, Action afterSwitch = null)
    {
        StartCoroutine((SwitchCameraSequence(cameraName, beforeSwitch, afterSwitch)));
    }
    
    public IEnumerator SwitchCameraSequence(string cameraName, Action beforeSwitch, Action afterSwitch)
    {
        // 카메라 전환 전에 작동할 로직 시행
        if (beforeSwitch != null)
        {
            beforeSwitch();
        }
        
        yield return null;
        
        // 카메라 전환
        foreach (CinemachineCamera camera in cinemachineCameras)
        {
            if (camera.name == cameraName)
            {
                camera.Priority = ActivePriority;
                activeCamera = camera;
            }
            else
            {
                camera.Priority = InActivePriority;
            }
        }
        
        // 전환이 완료될 때가지 대기
        yield return new WaitUntil(() => cinemachineBrain.ActiveBlend != null);
        yield return new WaitForSeconds(cinemachineBrain.ActiveBlend.Duration);
        
        // 전환 이후 작동할 로직 시행
        if (afterSwitch != null)
        {
            afterSwitch();
        }
    }
}
