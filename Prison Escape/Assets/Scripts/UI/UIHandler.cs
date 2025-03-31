using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private TextMeshProUGUI pickUpUIText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private AimUI aimUI;
    [SerializeField, Min(1)] private float hitRange = 3.0f;
    
    private RaycastHit hit;

    private void Start()
    {
        dialogueText.text = "";
    }
    
    void Update()
    {
        // 기존에 감지된 오브젝트의 하이라이트 초기화
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(false);
        }
        
        // 오브젝트 감지를 위한 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * hitRange, Color.red);
        
        // UI 업데이트
        UpdateUI(Physics.Raycast(
            ray,
            out hit,
            hitRange,
            pickableLayerMask
        ));
        
        // 감지된 물체가 있으면 하이라이트
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(true);
        }
    }
    
    // UI 업데이트
    private void UpdateUI(bool isTriggered)
    {
        pickUpUIText.color = new Color(
            pickUpUIText.color.r,
            pickUpUIText.color.g,
            pickUpUIText.color.b,
            isTriggered ? 1.0f : 0.0f
        );
        
        aimUI.SetAimPoint(isTriggered, hit.collider?.GetComponent<IFocusable>() != null);
    }

    public void ChangeDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }
}
