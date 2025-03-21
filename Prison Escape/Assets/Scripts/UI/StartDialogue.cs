using System;
using System.Collections;
using UnityEngine;

public class StartDialogue : BaseDialogue
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DialogueCoroutine());
    }
}
