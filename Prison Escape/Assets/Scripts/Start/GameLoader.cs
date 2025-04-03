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
    [SerializeField] private Image guideImage;
    [SerializeField] private Sprite guideSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private TextMeshProUGUI guideText;
    [SerializeField] private TextMeshProUGUI startMessage;
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    private static bool _firstTime = true;
    private static int _startCounter = 0;
    
    private IEnumerator Start()
    {
        if (!_firstTime)
        {
            guideImage.sprite = defaultSprite;
            guideImage.gameObject.SetActive(false);
            startCanvas.enabled = false;
            standup.Priority = 0;
            PlayerLoading.LoadDelay();
        }
        
        else
        {
            _firstTime = false;
            startCanvas.enabled = true;
            guideImage.sprite = guideSprite;
            guideImage.gameObject.SetActive(false);
            
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
        guideImage.gameObject.SetActive(true);
        guideText.text = "이동";
        startMessage.text = "";
        yield return new WaitForSeconds(3f);
        
        guideImage.sprite = defaultSprite;
        guideImage.gameObject.SetActive(false);
        guideText.text = "";
    }
}
