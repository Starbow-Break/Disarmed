using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] Outline outline;

    private bool isEnabled = false;
    private bool dirtyFlag = false;

    private void Start()
    {
        // 처음에는 비활성화
        SetHighlight(false);
    }

    private void Update()
    {
        if (dirtyFlag)
        {
            dirtyFlag = false;
            outline.enabled = isEnabled;
        }
    }

    // 하이라이트 여부 설정
    public void SetHighlight(bool value)
    {
        isEnabled = value;
        dirtyFlag = true;
    }
}