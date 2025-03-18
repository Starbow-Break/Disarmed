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
            StartCoroutine(InteractSequence(useItem));
        }
    }

    private IEnumerator InteractSequence(GameObject useItem)
    {
        GetComponent<Collider>().enabled = false;
        yield return fadePanel.FadeOut(1.0f);

        foreach (GameObject item in spawningItem)
        {
            item.SetActive(true);
        }
        GetComponent<Renderer>().enabled = false;
        Destroy(useItem);
        yield return new WaitForSeconds(0.5f);
            
        yield return fadePanel.FadeIn(1.0f);
        
        Destroy(gameObject);
    }
}
