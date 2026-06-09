using UnityEngine;

public class WetFloorDebuff : Buff
{
    public WetFloorDebuff(float duration, float amount)
    {
        BuffName = "WetFloorDeBuff";
        Duration = duration;
        Amount = amount;
    }

    public override void Apply(PlayerStats entity)
    {
        entity.moveSpeed -= Amount;
    }
    public override void Remove(PlayerStats entity)
    {
        entity.moveSpeed += Amount;
    }
}
