using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PressStart : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    [SerializeField] private CinemachineCamera startCamera;
    [SerializeField] private CinemachineCamera eyeCamera;
    
    private static bool _firstTime = true;

    private void Start()
    {
        startCamera.Priority = 10;
        eyeCamera.Priority = 0;
    }
    
    public void StartButton(string sceneName)
    {
        Debug.Log("StartButton");

        if (!_firstTime)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        _firstTime = false;
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Eye Camera",
            beforeSwitch: () => StartCoroutine(StartFadeIn()),
            afterSwitch: () => SceneManager.LoadScene(sceneName));
    }

    private IEnumerator StartFadeIn()
    {
        yield return fadePanel.FadeIn(fadeDuration);
    }
}
