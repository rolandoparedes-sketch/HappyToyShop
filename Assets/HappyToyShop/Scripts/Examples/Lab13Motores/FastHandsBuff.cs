using UnityEngine;

public class FastHandsBuff : Buff
{


    public FastHandsBuff(float duration, float amount)
    {
        BuffName = "FastHandsBuff";
        Duration = duration;
        Amount = amount;
    }
    public override void Apply(PlayerStats entity)
    {
        entity.wrapSpeed += Amount;
    }
    public override void Remove(PlayerStats entity)
    {
        entity.wrapSpeed -= Amount;
    }
}
