using System.Collections;
using UnityEngine;

public abstract class BaseDialogue : MonoBehaviour
{
    [SerializeField] protected DialogueData dialogue;
    [SerializeField] protected UIHandler uiHandler;
    [SerializeField] protected float waitTime = 2.5f;
    
    protected void Start()
    {
        CheckuiHandler();
    }
    
    protected void CheckuiHandler()
    {
        if (uiHandler == null)
        {
            // Debug.Log("uiHandler component not found");
            GameObject playerObj = GameObject.FindWithTag($"Player");
            
            if (playerObj != null)
            {
                uiHandler = playerObj.GetComponentInChildren<UIHandler>();
            }
            
            else
            {
                Debug.LogError("플레이어가 없자나 태그 추가해");
                return;
            }
        }
    }

    protected IEnumerator DialogueCoroutine()
    {
        if (dialogue == null || uiHandler == null)
        {
            Debug.LogError("대사 데이터 또는 UIHandler가 없음");
            yield break;
        }
        
        foreach (var d in dialogue.dialogues)
        {
            uiHandler.ChangeDialogue(d);
            yield return new WaitForSeconds(waitTime);
        }
        
        uiHandler.ChangeDialogue("");
        gameObject.SetActive(false);
    } 
}
