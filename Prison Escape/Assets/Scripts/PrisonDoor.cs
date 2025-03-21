using UnityEngine;

public class PrisonDoor : MonoBehaviour, IItemInteractable
{
    // 상호작용을 위해 필요한 아이템
    [SerializeField] private GameObject needItem;
    private Animator animator;
    void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    
    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        // 사용한 아이템과 필요한 아이템이 일치하면
        if (useItem == needItem)
        {
            animator.SetTrigger("Open");
        }
    }
}
