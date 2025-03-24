using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] SceneLoader sceneLoader;
    
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
        playerInput.enabled = false;
        
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(0.25f);
        
        CharacterController characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = badEndingSpawnPoint.position;
        player.transform.rotation = badEndingSpawnPoint.rotation;
        characterController.enabled = true;
        
        yield return new WaitForSeconds(0.25f);
        yield return fadePanel.FadeOut(panelFadeDuration);
        
        playerInput.enabled = true;
    }

    private IEnumerator HappyEndingSequence()
    {
        playerInput.enabled = false;
        
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(1.0f);
        
        yield return toBeContinuedText.FadeIn(textFadeDuration);
        yield return new WaitForSeconds(1.0f);
        yield return toBeContinuedText.FadeOut(textFadeDuration);
        yield return new WaitForSeconds(1.0f);
        
        playerInput.enabled = true;
        CursorLocker.instance.UnlockCursor();
        sceneLoader.LoadScene("Main");
    }
}
