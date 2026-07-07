using UnityEngine;

/// <summary>
/// Lanza un raycast desde el centro de la pantalla (usando la camara asignada).
/// Si golpea un objeto del layer especificado que tenga un FlashEffectController,
/// activa su flash. Cuando el objeto deja de estar en la mira, desactiva el flash.
/// </summary>
public class RaycastFlashTrigger : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Si se deja vacio, usa Camera.main")]
    [SerializeField] private Camera targetCamera;

    [Header("Configuracion del Raycast")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float maxDistance = 100f;

    // FlashEffectController actualmente activado por el raycast
    private FlashEffectController currentFlashController;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        Ray ray = targetCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Siempre filtramos por el layer configurado
        bool didHit = Physics.Raycast(ray, out hit, maxDistance, targetLayer);

        if (didHit)
        {
            FlashEffectController hitController = hit.collider.GetComponentInParent<FlashEffectController>();

            if (hitController == currentFlashController)
            {
                return; // seguimos apuntando al mismo objeto, no hacemos nada
            }

            // Cambiamos de objeto: apagamos el flash del anterior
            if (currentFlashController != null)
            {
                currentFlashController.DeactivateFlash();
            }

            if (hitController != null)
            {
                hitController.ActivateFlash();
            }

            currentFlashController = hitController;
        }
        else
        {
            // No estamos golpeando nada del layer: apagamos el flash anterior, si habia uno
            if (currentFlashController != null)
            {
                currentFlashController.DeactivateFlash();
                currentFlashController = null;
            }
        }
    }

    private void OnDisable()
    {
        if (currentFlashController != null)
        {
            currentFlashController.DeactivateFlash();
            currentFlashController = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (targetCamera == null) return;
        Ray ray = targetCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance);
    }
}