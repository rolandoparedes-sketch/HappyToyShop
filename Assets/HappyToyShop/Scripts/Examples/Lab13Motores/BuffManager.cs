using UnityEngine;
using System.Collections.Generic;
using System.Collections;


[RequireComponent(typeof(BaseEntity))]
public class BuffManager : MonoBehaviour
{
    public List<Buff> activeBuffs = new();
    private BaseEntity baseEntity;

    private void Awake()
    {
        baseEntity = GetComponent<BaseEntity>();
    }

    public void AddBuff(Buff buff)
    {
        buff.Apply(baseEntity);
        activeBuffs.Add(buff);
        StartCoroutine(RemoveBuff(buff));
    }
    public IEnumerator RemoveBuff(Buff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        
        buff.Remove(baseEntity);
        activeBuffs.Remove(buff);
    }
}
