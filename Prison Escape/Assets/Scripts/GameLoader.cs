using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    
    private IEnumerator Start()
    {
        PlayerLoading.PlayerSetStop();
        yield return StartCoroutine(StartFadeOut());
        yield return new WaitForSeconds(0.5f);
        
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "AfterStand Camera", 
            afterSwitch: () => SwitchToPlayer());
        
    }
    
    private void SwitchToPlayer()
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Player Camera", 
            afterSwitch: () => PlayerLoading.PlayerSetStart());
    }
    
    private IEnumerator StartFadeOut()
    {
        yield return fadePanel.FadeOut(fadeDuration);
    }

}
