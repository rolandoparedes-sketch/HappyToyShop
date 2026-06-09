using UnityEngine;

public enum BuffType
{
    None,
    Speed,
    FastHands,
    WetFloot,
}

public class BuffFactory : MonoBehaviour
{

    public static Buff CreateBuff(BuffType type)
    {
        switch (type)
        {


            case BuffType.None: return null;

            case BuffType.Speed: return new SpeedBuff(5, 8);

            case BuffType.FastHands: return new FastHandsBuff(15, 5);

            case BuffType.WetFloot: return new WetFloorDebuff(5, 5);
                


        }
        return null;
    }
}
