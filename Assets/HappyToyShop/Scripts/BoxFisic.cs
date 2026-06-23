using UnityEngine;

public class BoxFisic : MonoBehaviour
{
    [Header("Cantidad de Mini Cajas")]
    public int toys;

    [Header("Prefab Mini Caja")]
    public GameObject miniCajaPrefab;

    public void AbrirCaja()
    {
        Debug.Log("ABRIENDO CAJA");
        for (int i = 0; i < toys; i++)
        {
            Vector3 pos = transform.position +
                new Vector3(
                    Random.Range(-1f, 1f),
                    0.5f,
                    Random.Range(-1f, 1f)
                );

            Instantiate(
                miniCajaPrefab,
                pos,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}
