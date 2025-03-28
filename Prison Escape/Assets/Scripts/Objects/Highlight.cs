using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class ShaderPropertySettings<T>
{
    public string property;
    public T value;
}

public class Highlight : MonoBehaviour
{
    [Header("Highlight Material")]
    [SerializeField] private Material HighlightMaterial;
    
    [Header("Material Settings")]
    [SerializeField] List<ShaderPropertySettings<float>> floatProperties = new();
    [SerializeField] List<ShaderPropertySettings<Color>> colorProperties = new();

    private new Renderer renderer; // 렌더러
    private Material[] originalMaterials; // 원래 머티리얼들

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalMaterials = renderer.materials;

        if (HighlightMaterial == null)
        {
            Debug.LogError("Outline Materials are not assigned");
            return;
        }

        SetMaterialProperty();
    }

    private void Start()
    {
        // 처음에는 비활성화
        SetHighlight(false);
    }
    
    // 하이라이트 머터리얼의 프로퍼티를 설정
    private void SetMaterialProperty()
    {
        foreach (ShaderPropertySettings<float> floatProperty in floatProperties)
        {
            HighlightMaterial.SetFloat(Shader.PropertyToID(floatProperty.property), floatProperty.value);
        }
        foreach (ShaderPropertySettings<Color> colorProperty in colorProperties)
        {
            HighlightMaterial.SetColor(Shader.PropertyToID(colorProperty.property), colorProperty.value);
        }
    }
    
    // 하이라이트 여부 설정
    public void SetHighlight(bool value)
    {
        if (HighlightMaterial == null)
        {
            return;
        }
        
        if (value)
        {
            Material[] tempMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(tempMaterials, 0);
            tempMaterials[tempMaterials.Length - 1] = HighlightMaterial;
            renderer.materials = tempMaterials;
        }
        else
        {
            renderer.materials = originalMaterials;
        }
    }
}
