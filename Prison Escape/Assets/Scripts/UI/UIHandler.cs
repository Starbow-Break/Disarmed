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
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.SetHighlight(false);
            pickUpUIText.color = new Color(
                pickUpUIText.color.r,
                pickUpUIText.color.g,
                pickUpUIText.color.b,
                0.0f
            );
            aimUI.SetBigger(false);
        }
        
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
            pickUpUIText.color = new Color(
                pickUpUIText.color.r,
                pickUpUIText.color.g,
                pickUpUIText.color.b,
                1.0f
            );
            aimUI.SetBigger(true);
        }
    }

    public void ChangeDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }
}
