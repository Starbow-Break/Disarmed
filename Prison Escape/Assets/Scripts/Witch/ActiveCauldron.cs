using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActiveCauldron : MonoBehaviour, IItemInteractable
{
    [SerializeField] 
    private AudioSource audioSource;
    
    [SerializeField]
    private List<IngredientData> ingredientList;
    
    private Dictionary<string, IngredientData> ingredients;
    private List<string> inCauldronList;

    // 현재 답이 뿌리(RR) + 초록포션(PS) 이므로 이를 알파벳 순서로 정렬한 문자열
    private const string CorrectAnswer = "PSRR";
    private const int CorrectNumber = 2;
    

    private void Awake()
    {
        inCauldronList = new List<string>();
        
        #region SetDictionary
        
            ingredients = new Dictionary<string, IngredientData>();
            
            foreach (var ingredientData in ingredientList)
            {
                if (ingredientData.prefab == null)
                {
                    Debug.LogError($"prefab이 {ingredientData}에 비어있음");
                    continue;
                }
                
                var ingredientName = ingredientData.prefab.name;
                ingredients.Add(ingredientName, ingredientData);
            }
        
        #endregion
    }

    private void OnEnable()
    {
        ActiveSwitch.Reset += ResetCauldron;
        ActiveSwitch.onSwitch = CalculateResult;
    }

    private void OnDisable()
    {
        ActiveSwitch.Reset -= ResetCauldron;
        if (ActiveSwitch.onSwitch == CalculateResult) ActiveSwitch.onSwitch = null;
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
            return;
        }
        
        audioSource.Play();

        if (ingredients.TryGetValue(ingredient.name, out var foundIngredient))
        {
            inCauldronList?.Add(foundIngredient.id);
        }

        useItem.GetComponent<IUsable>()?.Use(actor);

    }

    private void ResetCauldron()
    {
        inCauldronList.Clear();
    }
    

    public SwitchState CalculateResult()
    {
        if (inCauldronList.Count == 0)
        {
            return SwitchState.Nodata;
        }

        if (inCauldronList.Count != CorrectNumber)
        {
            return SwitchState.Failed;
        }
        
        var sortedList = inCauldronList.OrderBy(ingredient => ingredient).ToList();
        var builder = new StringBuilder();
        
        foreach (var ingredient in sortedList)
        {
            builder.Append(ingredient);
        }
        
        string result = builder.ToString();
        
        if (result == CorrectAnswer)
        {
            return SwitchState.Success;
        }

        return SwitchState.Failed;
    }
}
