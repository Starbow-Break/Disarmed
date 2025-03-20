using System;
using System.Collections;
using UnityEngine;

public class StartWitch : BaseDialogue
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DialogueCoroutine());
    }
}
