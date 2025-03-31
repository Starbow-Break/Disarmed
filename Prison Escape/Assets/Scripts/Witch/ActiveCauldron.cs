using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class ActiveCauldron : MonoBehaviour, IItemInteractable
{
    [SerializeField] private String[] ingredients;
    [SerializeField] private AudioSource audioSource;
    
    private static string root = "";
    private static string green = "";
    public bool isSelected { get; private set; }
    private int rootCount;
    private int greenCount;

    private void Start()
    {
        isSelected = false;
        rootCount = 0;
        greenCount = 0;
        
        root = ingredients[0];
        green = ingredients[1];
    }

    public void InteractUseItem(GameObject actor, GameObject useItem)
    {
        UsableIngredients ingredient = null;
        if (useItem != null)
        {
            ingredient = useItem.GetComponent<UsableIngredients>();
        }

        if (ingredient == null)
        {
            Debug.Log("Item is null");
        }
        else
        {
            audioSource.Play();
            if(!isSelected) isSelected = true;
            Debug.Log("Item is " + useItem.name);
            if (useItem.name == root)
            {
                rootCount++;
                Debug.Log($"root: {rootCount}");
            }
            else if (useItem.name == green)
            {
                greenCount++;
                Debug.Log($"green: {greenCount}");
            }

            useItem.GetComponent<IUsable>()?.Use(actor);
        }
    }

    public void ResetCauldron()
    {
        isSelected = false;
        rootCount = 0;
        greenCount = 0;
    }

    public bool CaculateResult()
    {
        if (rootCount == 1 && greenCount == 1)
        {
            return true;
        }

        return false;
    }
}
