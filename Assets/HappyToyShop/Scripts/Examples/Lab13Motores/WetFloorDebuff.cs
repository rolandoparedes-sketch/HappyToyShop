using UnityEngine;

public class WetFloorDebuff : Buff
{
    public WetFloorDebuff(float duration, float amount)
    {
        BuffName = "WetFloorDeBuff";
        Duration = duration;
        Amount = amount;
    }

    public override void Apply(PlayerStats2D entity)
    {
        entity.moveSpeed -= Amount;
    }
    public override void Remove(PlayerStats2D entity)
    {
        entity.moveSpeed += Amount;
    }
}
