using UnityEngine;
using UnityEngine.UI;

public class AimUI : MonoBehaviour
{
    [SerializeField, Min(1)] float focusSizeMultiplier = 6.0f;
    [SerializeField] private Sprite interactSprite;
    [SerializeField] private Sprite unfocusableSprite;

    RectTransform aimRect;
    Image aimImage;
    Vector2 aimOriginSize;

    public void Awake()
    {
        aimRect = GetComponent<RectTransform>();
        aimImage = GetComponent<Image>();
        aimOriginSize = aimRect.sizeDelta;
    }

    public void SetAimPoint(bool detect)
    {
        if (detect)
        {
            aimRect.sizeDelta = aimOriginSize * focusSizeMultiplier;
            aimImage.sprite = interactSprite;
        }
        else
        {
            aimRect.sizeDelta = aimOriginSize;
            aimImage.sprite = unfocusableSprite;
        }
    }
}
