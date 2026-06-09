using UnityEngine;
using System.Collections.Generic;
using System.Collections;


[RequireComponent(typeof(PlayerStats))]
public class BuffManager : MonoBehaviour
{
    public List<Buff> activeBuffs = new();
    private PlayerStats Stats;

    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
    }

    public void AddBuff(Buff buff)
    {
        buff.Apply(Stats);
        activeBuffs.Add(buff);
        StartCoroutine(RemoveBuff(buff));
    }
    public IEnumerator RemoveBuff(Buff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        
        buff.Remove(Stats);
        activeBuffs.Remove(buff);
    }
}
