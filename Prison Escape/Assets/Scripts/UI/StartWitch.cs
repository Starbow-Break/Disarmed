using System.Collections;
using UnityEngine;

public class StartWitch : BaseDialogue
{
    [SerializeField] private MeshCollider switchCollider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayWitchAudio WitchAudioClips;
    public void StartDialogue()
    {
        if (dialogue == null || uiHandler == null)
        {
            Debug.LogError("대사 데이터 또는 UIHandler가 없음");
            return;
        }
        
        switchCollider.enabled = false;
        StartCoroutine(WitchCoroutine());
    }

    private IEnumerator WitchCoroutine()
    {
        int length = dialogue.dialogues.Length;
        audioSource.resource = WitchAudioClips.WitchIncome[0];
        audioSource.Play();
        uiHandler.ChangeDialogue(dialogue.dialogues[0]);

        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 1; i < length; i++)
        {
            if (i % 2 != 0)
            {
                uiHandler.ChangeDialogue(dialogue.dialogues[i]);

                yield return new WaitForSeconds(waitTime);

            }
            else
            {
                audioSource.resource = WitchAudioClips.WitchIncome[i / 2];
                audioSource.Play();
                uiHandler.ChangeDialogue(dialogue.dialogues[i]);
                
                while (audioSource.isPlaying)
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        switchCollider.enabled = true;
        uiHandler.ChangeDialogue("");
    }
}
