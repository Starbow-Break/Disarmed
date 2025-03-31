using UnityEngine;

public class Door : MonoBehaviour, IItemInteractable
{
    [SerializeField] private GameObject needItem;   // 상호작용을 위해 필요한 아이템 (null일 경우 아이템으로 문 열기 불가)
    [SerializeField] private AudioClip openClip;    // 문 열때 소리
    [SerializeField] private AudioClip failClip;    // 문 열기 실패 시 소리
    
    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        // 사용한 아이템과 필요한 아이템이 일치하면
        if (needItem != null && useItem == needItem)
        {
            Open();
            useItem.GetComponent<IUsable>()?.Use(actor);
        }
        else
        {
            OpenFail();
        }
    }

    // 문 열기
    public void Open()
    {
        audioSource?.PlayOneShot(openClip);
        animator?.SetTrigger("Open");

        // 상호작용 불가능하게 기본 레이어로 변경
        LayerMask defaultMask = 1 << LayerMask.GetMask("Default");
        gameObject.layer = defaultMask;
    }

    // 문 열기 실패
    public void OpenFail()
    {
        audioSource?.PlayOneShot(failClip);
    }
}