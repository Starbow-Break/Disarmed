using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Game/New Ingredient")]
public class IngredientData : ScriptableObject
{
    [Tooltip("비밀ID")]
    public string id;
    
    [Tooltip("영어 이름")]
    public string name;
    
    [Tooltip("해당 오브젝트 프리팹")]
    public GameObject prefab;
}
