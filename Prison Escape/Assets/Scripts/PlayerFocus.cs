using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private AimUI aimUI;
    [SerializeField, Min(1)] private float hitRange = 3.0f;
    [SerializeField] private InputActionReference focusInput, unFocusInput;

    private RaycastHit hit;
    private GameObject focusedObject;

    private void Start()
    {
        focusInput.action.performed += Focus;
        unFocusInput.action.performed += UnFocus;
    }

    private void Update()
    {
        // UI 및 하이라이트 초기화
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(false);
            aimUI.SetBigger(false);
        }

        // 이미 아이템이 손에 들려 있다면 아이템을 감지하는 로직은 건너 뛴다.
        if(focusedObject != null)
        {
            return;
        }

        // 에임에 들어온 아이템 감지
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * hitRange, Color.red);
        
        if(Physics.Raycast(
            ray,
            out hit, 
            hitRange, 
            pickableLayerMask
        ))
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(true);
            aimUI.SetBigger(true);
        }
    }
    
    // 선택한 오브젝트에 포커스
    private void Focus(InputAction.CallbackContext context) {
        if (hit.collider != null)
        {
            IFocusable focusable = hit.collider.GetComponent<IFocusable>();
            if (focusable != null)
            {
                focusedObject = hit.collider.gameObject;
                focusable.Focus(gameObject);
            }
        }
    }
    
    // 포커스 해제
    private void UnFocus(InputAction.CallbackContext context) {
        if (focusedObject != null)
        {
            focusedObject.GetComponent<IFocusable>()?.UnFocus(gameObject);
            focusedObject = null;
        }
    }
}
