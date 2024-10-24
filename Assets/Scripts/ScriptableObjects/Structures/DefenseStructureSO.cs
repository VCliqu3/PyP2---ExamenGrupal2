using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefenseStructureSO", menuName = "ScriptableObjects/Entities/Structures/DefenseStructure")]
public class DefenseStructureSO : StructureSO
{
    [Range(0, 50)] public int damage;
}
