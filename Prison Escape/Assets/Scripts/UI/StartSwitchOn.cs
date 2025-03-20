using System;
using System.Collections;
using UnityEngine;

public class StartSwitchOn : BaseDialogue
{
    public void StartDialogue(int number)
    {
        if (dialogue == null || uiHandler == null)
        {
            Debug.LogError("대사 데이터 또는 UIHandler가 없음");
            return;
        }
        
        uiHandler.ChangeDialogue(dialogue.dialogues[number]);
        StartCoroutine(SetDialogueNULL());
    }

    private IEnumerator SetDialogueNULL()
    {
        yield return new WaitForSeconds(waitTime);
        uiHandler.ChangeDialogue("");
    }
    

}
