using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDataBase", menuName = "Happy Toy Shop/NpcDataBase")]
public class NpcDataBase : SerializedScriptableObject
{

    public Dictionary<TypeDialogue, List<string>> typeDialogue = new();

    public Dictionary<int, NpcData> NpcData = new();
    public string GetDialogue(TypeDialogue type)
    {
        if (typeDialogue.TryGetValue(type, out List<string> entities))
        {
            int n = Random.Range(0, entities.Count);
            return entities[n];
        }
        else
        {
            throw new System.Exception("El tipo de dialogo no ha sido asignado aun o no tiene dialogos disponibles");
        }
    }
    public NpcData GetDataNpc()
    {

        int npc = Random.Range(1, NpcData.Count+1);


        if (NpcData.TryGetValue(npc, out NpcData entity))
        {
            return entity;
        }
        else
        {
            throw new System.Exception("El Id del Npc no ha sido asignado aun o no tiene data disponibles");
        }
    }

}
