using UnityEngine;
using UnityEditor.Animations;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "NpcData", menuName = "Happy Toy Shop/NpcData")]
[InlineEditor]
public class NpcData : BaseEntityData
{
    [SerializeField]private AnimatorController anim;


    public AnimatorController Anim => anim;
}
