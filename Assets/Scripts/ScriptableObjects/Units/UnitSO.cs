using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitSO", menuName = "ScriptableObjects/Entities/NewUnit")]
public class UnitSO : EntitySO
{
    [Range(0, 50)] public int damage;
    [Range(0.2f, 1f)] public float attackSpeed; //Attacks per second
    [Range(0.5f, 5f)] public float projectileSpeed;
    [Space]
    [Range(0.2f, 1f)] public float speed; //Nodes per second
}
