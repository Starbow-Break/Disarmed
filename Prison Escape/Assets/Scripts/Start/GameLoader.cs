using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private DialogueData startDialogue;
    [SerializeField] private CinemachineCamera standup;
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Image controlGuide;
    [SerializeField] private TextMeshProUGUI startMessage;
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    private static bool _firstTime = true;
    private static int _startCounter = 0;
    
    private IEnumerator Start()
    {
        if (!_firstTime)
        {
            startCanvas.enabled = false;
            standup.Priority = 0;
            PlayerLoading.LoadDelay();
        }
        
        else
        {
            _firstTime = false;
            startCanvas.enabled = true;
            controlGuide.gameObject.SetActive(false);
            
            PlayerLoading.PlayerSetStop();
            startMessage.text = startDialogue.dialogues[_startCounter++];
            
            yield return StartCoroutine(StartFadeOut());
            // yield return new WaitForSeconds(0.5f);

            startMessage.text = "";
            CameraSwitcher.instance.SwitchCamera(
                cameraName: "AfterStand Camera",
                afterSwitch: SwitchToPlayer);
        }
    }
    
    private void SwitchToPlayer()
    {
        startMessage.text = startDialogue.dialogues[_startCounter];
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Player Camera", 
            afterSwitch: GameStart);
    }
    
    private IEnumerator StartFadeOut()
    {
        yield return fadePanel.FadeOut(fadeDuration);
    }

    private void GameStart()
    {
        StartCoroutine(AfterStandUp());
        PlayerLoading.PlayerSetStart();
    }

    private IEnumerator AfterStandUp()
    {
        controlGuide.gameObject.SetActive(true);
        startMessage.text = "";
        yield return new WaitForSeconds(3f);
        
        controlGuide.gameObject.SetActive(false);
    }
}
