using System.Collections;
using UnityEngine;

public class AfterSecret : BaseDialogue
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DialogueCoroutine());
    }
}
