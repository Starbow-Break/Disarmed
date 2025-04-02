using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standup;
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    private static bool _firstTime = true;
    
    private IEnumerator Start()
    {
        if (!_firstTime)
        {
            standup.Priority = 0;
            PlayerLoading.LoadDelay();
        }
        
        else
        {
            _firstTime = false;
            PlayerLoading.PlayerSetStop();
            yield return StartCoroutine(StartFadeOut());
            yield return new WaitForSeconds(0.5f);

            CameraSwitcher.instance.SwitchCamera(
                cameraName: "AfterStand Camera",
                afterSwitch: () => SwitchToPlayer());
        }
    }
    
    private void SwitchToPlayer()
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Player Camera", 
            afterSwitch: () => GameStart());
    }
    
    private IEnumerator StartFadeOut()
    {
        yield return fadePanel.FadeOut(fadeDuration);
    }

    private void GameStart()
    {
        PlayerLoading.PlayerSetStart();
    }
}
