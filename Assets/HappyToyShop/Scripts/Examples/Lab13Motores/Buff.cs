using UnityEngine;

public abstract class Buff 
{
    public string BuffName;
    public float Amount;
    public float Duration;

    public abstract void Apply(PlayerStats2D entity);
    public abstract void Remove(PlayerStats2D entity);
}
