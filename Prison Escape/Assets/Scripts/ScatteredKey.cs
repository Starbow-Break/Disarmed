using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScatteredKey : MonoBehaviour, IItemInteractable
{
    // 상호작용을 위해 필요한 아이템
    [SerializeField] private GameObject needItem;
    [SerializeField] private List<GameObject> spawningItem;
    [SerializeField] private FadePanel fadePanel;
    
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
        // 상호작용하지 옷하게 몰리전만 비활성화
        GetComponent<Collider>().enabled = false;
        
        // 페이드 아웃
        yield return fadePanel.FadeOut(1.0f);
        
        // 추가 오브젝트 스폰하면서 렌더러 비활성화 및 플레이어가 손에 들고 있는 아이템 사용
        foreach (GameObject item in spawningItem)
        {
            item.SetActive(true);
        }
        GetComponent<Renderer>().enabled = false;
        useItem.GetComponent<IUsable>()?.Use(actor);
        
        // 잠시 대기 후 페이드 인
        yield return new WaitForSeconds(0.5f);
        yield return fadePanel.FadeIn(1.0f);
        
        // 해당 오브젝트 파괴
        Destroy(gameObject);
    }
}
