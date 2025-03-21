using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndingEvent : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float panelFadeDuration;

    [SerializeField] private ToBeContinuedText toBeContinuedText;
    [SerializeField] private float textFadeDuration;
    
    [SerializeField] private GameObject player;
    [SerializeField] private Transform badEndingSpawnPoint;
    
    public void BadEnding()
    {
        StartCoroutine(BadEndingSequence());
    }

    public void HappyEnding()
    {
        StartCoroutine(HappyEndingSequence());
    }

    private IEnumerator BadEndingSequence()
    {
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(0.25f);
        
        CharacterController characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = badEndingSpawnPoint.position;
        player.transform.rotation = badEndingSpawnPoint.rotation;
        characterController.enabled = true;
        
        yield return new WaitForSeconds(0.25f);
        yield return fadePanel.FadeOut(panelFadeDuration);
    }

    private IEnumerator HappyEndingSequence()
    {
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(1.0f);
        
        yield return toBeContinuedText.FadeIn(textFadeDuration);
        yield return new WaitForSeconds(1.0f);
        yield return toBeContinuedText.FadeOut(textFadeDuration);
        
        // TODO : 메인 씬으로 이동
        //SceneManager.LoadScene();
    }
}
