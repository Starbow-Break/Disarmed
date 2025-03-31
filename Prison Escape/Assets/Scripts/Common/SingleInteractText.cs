using UnityEngine;

public class SingleInteractText : InteractText
{
    [SerializeField] private string text; // 사용할 문구
    
    public override string GetText()
    {
        return text;
    }
}
