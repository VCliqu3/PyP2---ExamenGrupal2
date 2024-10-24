using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefenseStructureSO", menuName = "ScriptableObjects/Entities/Structures/DefenseStructure")]
public class DefenseStructureSO : StructureSO
{
    [Range(0, 50)] public int damage;
    [Range(0.2f, 1f)] public float attackSpeed; //Attacks per second
    [Range(0.5f, 5f)] public float projectileSpeed;
}
