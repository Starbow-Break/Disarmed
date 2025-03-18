using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractUseItem : MonoBehaviour
{
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private AimUI aimUI;
    [SerializeField, Min(1)] private float hitRange = 3.0f;
    [SerializeField] private InputActionReference intaractInput;
    [SerializeField] private PlayerItemPickUp itemPickUp;
    
    private RaycastHit hit;

    private void Start()
    {
        intaractInput.action.performed += Interact;
    }

    private void Update()
    {
        // UI 및 하이라이트 초기화
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(false);
            aimUI.SetBigger(false);
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
    
    // 현재 들고 있는 아이템을 사용해서 감지한 오브젝트와 상호작용
    private void Interact(InputAction.CallbackContext context) {
        if (hit.collider != null)
        {
            IItemInteractable itemInteractable = hit.collider.GetComponent<IItemInteractable>();
            if (itemInteractable != null)
            {
                itemInteractable.InteractUseItem(gameObject, itemPickUp.inHandItem);
            }
        }
    }
}
