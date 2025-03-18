using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScatteredKey : MonoBehaviour, IItemInteractable
{
    // 상호작용을 위해 필요한 아이템
    [SerializeField] private GameObject needItem;
    [SerializeField] private List<GameObject> spawningItem;
    
    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        // 사용한 아이템과 필요한 아이템이 일치하면
        if (useItem == needItem)
        {
            // TODO : 화면 암전

            foreach (GameObject item in spawningItem)
            {
                item.SetActive(true);
            }
            
            // TODO : 화면 암전 해제
            
            Destroy(useItem);
        }
    }
}
