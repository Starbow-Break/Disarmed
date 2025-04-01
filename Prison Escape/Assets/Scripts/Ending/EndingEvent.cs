using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndingEvent : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;   // FadePanel
    [SerializeField] private float panelFadeDuration;   // 페이드 시간

    [SerializeField] private ToBeContinuedText toBeContinuedText;   // To Be Continued를 띄울 텍스트 
    [SerializeField] private float textFadeDuration;    // 텍스트의 페이드 시간

    [SerializeField] private float fadeInterval;    // 각 페이드마다 시간 간격
    
    [SerializeField] private GameObject player; // 플레이어
    [SerializeField] private PrisonTrap prisonTrap; // 감옥 함정
    [SerializeField] private Transform badEndingSpawnPoint; // 배드 엔딩 스폰 위치

    [SerializeField] private PlayerInput playerInput;   // Player Input
    [SerializeField] SceneLoader sceneLoader;   // Scene Loader
    [SerializeField] private List<GuardController> guardControllers;
    
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
        // 플레이어 위치를 함정 위치로 강제 고정
        PlayerLoading.PlayerSetStop();
        player.transform.position = prisonTrap.transform.position;
        
        // 감옥 내리기
        prisonTrap.Close();
        yield return new WaitUntil(() => prisonTrap.isClosed);
        
        // 플레이어 조작 가능
        PlayerLoading.PlayerSetStart();
        
        // 경비원 출현
        int guardNum = guardControllers.Count;
        foreach (GuardController guardController in guardControllers)
        {
            guardController.OnFinish += () => guardNum--;
            guardController.Play();
        }

        yield return new WaitWhile(() => guardNum > 0);
        
        // FadePanel 페이드 인
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(fadeInterval / 2.0f);
        
        // 감옥 순간 이동
        PlayerLoading.PlayerSetStop();
        player.transform.position = badEndingSpawnPoint.position;
        player.transform.rotation = badEndingSpawnPoint.rotation;
        PlayerLoading.PlayerSetStart();
        
        // FadePanel 페이드 아웃
        yield return new WaitForSeconds(fadeInterval / 2.0f);
        yield return fadePanel.FadeOut(panelFadeDuration);
    }

    private IEnumerator HappyEndingSequence()
    {
        playerInput.enabled = false;
        
        yield return fadePanel.FadeIn(panelFadeDuration);
        yield return new WaitForSeconds(fadeInterval);
        
        yield return toBeContinuedText.FadeIn(textFadeDuration);
        yield return new WaitForSeconds(fadeInterval);
        yield return toBeContinuedText.FadeOut(textFadeDuration);
        yield return new WaitForSeconds(fadeInterval);
        
        playerInput.enabled = true;
        CursorLocker.instance.UnlockCursor();
        sceneLoader.LoadScene("Main");
    }
}
