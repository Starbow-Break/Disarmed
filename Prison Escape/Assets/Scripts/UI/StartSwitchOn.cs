using System;
using System.Collections;
using UnityEngine;

public class StartSwitchOn : BaseDialogue
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayWitchAudio WitchAudioClips;

    private int clicked_again = 3;
    public void StartDialogue(int number)
    {
        if (dialogue == null || uiHandler == null)
        {
            Debug.LogError("대사 데이터 또는 UIHandler가 없음");
            return;
        }
        
        audioSource.resource = WitchAudioClips.WitchSwitch[number];
        Debug.Log(audioSource.resource);
        audioSource.Play();
        uiHandler.ChangeDialogue(dialogue.dialogues[number]);
        
        StartCoroutine(SetDialogueNULL());
    }

    private IEnumerator SetDialogueNULL()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        uiHandler.ChangeDialogue("");
    }
    

}
