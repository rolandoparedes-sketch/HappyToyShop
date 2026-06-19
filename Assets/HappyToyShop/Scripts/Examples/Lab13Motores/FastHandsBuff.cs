using UnityEngine;

public class FastHandsBuff : Buff
{


    public FastHandsBuff(float duration, float amount)
    {
        BuffName = "FastHandsBuff";
        Duration = duration;
        Amount = amount;
    }
    public override void Apply(PlayerStats2D entity)
    {
        entity.wrapSpeed += Amount;
    }
    public override void Remove(PlayerStats2D entity)
    {
        entity.wrapSpeed -= Amount;
    }
}
