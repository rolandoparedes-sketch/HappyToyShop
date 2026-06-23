using UnityEngine;

public class VentShock : MonoBehaviour
{
    [Header("Circuit")]
    public float charge = 0f;

    public float maxCharge = 100f;
    public EnemyShadow enemy;
    public float decayRate = 2f;

    private void Update()
    {
        if (charge > 0)
        {
            charge -= decayRate * Time.deltaTime;

            if (charge < 0)
                charge = 0;
        }
    }

    public bool CanShock()
    {
        return charge <= 0;
    }

    public void UseShock()
    {
        charge += 50f;

        if (charge > maxCharge)
            charge = maxCharge;

        if (enemy != null && enemy.isVent)
        {
            enemy.GoIdle();

            Debug.Log("El enemigo ha sido aturdido por el shock de ventilacion");
        }
    }
}