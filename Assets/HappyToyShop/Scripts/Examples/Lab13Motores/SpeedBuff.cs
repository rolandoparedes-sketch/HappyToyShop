using UnityEngine;

public class SpeedBuff : Buff
{
    public SpeedBuff(float duration, float amount)
    {
        BuffName = "SpeedBuff";
        Duration = duration;
        Amount = amount;
    }

    public override void Apply(PlayerStats Stats)
    {
        Stats.moveSpeed += Amount;
    }
    public override void Remove(PlayerStats Stats)
    {
        Stats.moveSpeed -= Amount;
    }


}
