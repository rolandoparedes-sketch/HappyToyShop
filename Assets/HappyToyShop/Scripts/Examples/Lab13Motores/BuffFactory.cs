using UnityEngine;

public enum BuffType
{
    None,
    Speed,
    Slow,
    Poison,
}

public class BuffFactory : MonoBehaviour
{

    public static Buff CreateBuff(BuffType type)
    {
        switch (type)
        {


            case BuffType.None: return new SpeedBuff(3, 5);

            case BuffType.Speed: return new SpeedBuff(3, 5);

            case BuffType.Slow: return new SpeedBuff(3, 5);

            case BuffType.Poison: return new SpeedBuff(3, 5);
                


        }
        return null;
    }
}
