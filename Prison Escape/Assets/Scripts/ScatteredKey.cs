using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScatteredKey : MonoBehaviour, IItemInteractable
{
    // 상호작용을 위해 필요한 아이템
    [SerializeField] private GameObject needItem;
    [SerializeField] private List<GameObject> spawningItem;
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    [SerializeField] private PlayerInput playerInput;
    
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        // 사용한 아이템과 필요한 아이템이 일치하면
        if (useItem == needItem)
        {
            StartCoroutine(InteractSequence(actor, useItem));
        }
    }

    private IEnumerator InteractSequence(GameObject actor, GameObject useItem)
    {
        // 상호작용하지 옷하게 PlayerInput 비활성화
        playerInput.enabled = false;
        
        // 페이드 아웃
        yield return fadePanel.FadeIn(fadeDuration);
        
        // 추가 오브젝트 스폰하면서 렌더러 비활성화 및 플레이어가 손에 들고 있는 아이템 사용
        foreach (GameObject item in spawningItem)
        {
            item.SetActive(true);
            item.transform.position = actor.transform.position;
        }
        GetComponent<Renderer>().enabled = false;
        
        // 효과음 재생
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        
        // 페이드 인
        yield return fadePanel.FadeOut(fadeDuration);
        
        // PlayerInput 활성화
        playerInput.enabled = true;
        // 해당 오브젝트 파괴
        Destroy(gameObject);
    }
}
