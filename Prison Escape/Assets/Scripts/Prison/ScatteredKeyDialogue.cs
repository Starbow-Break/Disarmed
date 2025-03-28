using UnityEngine;

public class ScatteredKeyDialogue : BaseDialogue
{
    public void PlayFailDialogue()
    {
        StartCoroutine(DialogueCoroutine());
    }
}
