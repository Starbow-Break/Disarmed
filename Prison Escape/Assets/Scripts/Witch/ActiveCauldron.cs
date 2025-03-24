using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class ActiveCauldron : MonoBehaviour
{
    public static event Action<GameObject> OnCauldron;
    
    [SerializeField] private String[] ingredients;
    [SerializeField] private AudioSource audioSource;
    
    private static string root = "";
    private static string green = "";
    public bool isSelected { get; private set; }
    private int yesitem = 0;
    private int noitem = 1;
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

    private void OnEnable()
    {
        OnCauldron += CauldronEnter;
    }

    private void OnDisable()
    {
        OnCauldron -= CauldronEnter;
    }

    public void TriggerCauldron(GameObject player)
    {
        OnCauldron?.Invoke(player);
    }

    private void CauldronEnter(GameObject player)
    {
        PlayerPickup playerPickup = player.GetComponent<PlayerPickup>();
        if (playerPickup != null)
        {
            GameObject item = playerPickup.inHandItem;

            if (item == null)
            {
                Debug.Log("Item is null");
            }
            else
            {
                audioSource.Play();
                if(!isSelected) isSelected = true;
                Debug.Log("Item is " + item.name);
                if (item.name == root)
                {
                    rootCount++;
                    Debug.Log($"root: {rootCount}");
                }
                else if (item.name == green)
                {
                    greenCount++;
                    Debug.Log($"green: {greenCount}");
                }
            }
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
