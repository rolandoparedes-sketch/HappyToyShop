using UnityEngine;

/// <summary>
/// Controla el efecto de flash del shader "Custom/URP/FlashEffect" en el
/// MeshRenderer de este mismo objeto, usando MaterialPropertyBlock (no
/// instancia materiales). Se activa automaticamente en Start.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class FlashEffectController : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Si se deja vacio, usa el MeshRenderer de este mismo objeto")]
    [SerializeField] private MeshRenderer targetRenderer;

    [Header("Nombre de la propiedad del shader")]
    [SerializeField] private string enableFlashProperty = "_EnableFlash";

    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<MeshRenderer>();
        }

        propBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        DeactivateFlash();
    }

    /// <summary>
    /// Activa el flash en todos los materiales del MeshRenderer.
    /// </summary>
    public void ActivateFlash()
    {
        SetFlash(true);
    }

    /// <summary>
    /// Desactiva el flash en todos los materiales del MeshRenderer.
    /// </summary>
    public void DeactivateFlash()
    {
        SetFlash(false);
    }

    private void SetFlash(bool enable)
    {
        if (targetRenderer == null) return;

        int materialCount = targetRenderer.sharedMaterials.Length;

        for (int i = 0; i < materialCount; i++)
        {
            // Traemos el property block actual de ese submesh (por si tenia otros overrides)
            targetRenderer.GetPropertyBlock(propBlock, i);
            propBlock.SetFloat(enableFlashProperty, enable ? 1f : 0f);
            targetRenderer.SetPropertyBlock(propBlock, i);
        }
    }
}