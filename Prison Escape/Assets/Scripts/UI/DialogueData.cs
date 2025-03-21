using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/New Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogues;
}