using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "NpcData", menuName = "Happy Toy Shop/NpcData")]
[InlineEditor]
public class NpcData : BaseEntityData
{
    [SerializeField]private RuntimeAnimatorController anim;

    public RuntimeAnimatorController Anim => anim;
}
