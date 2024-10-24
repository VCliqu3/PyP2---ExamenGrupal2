using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitSO", menuName = "ScriptableObjects/Entities/NewUnit")]
public class UnitSO : EntitySO
{
    [Range(0, 50)] public int damage;
    [Range(1, 3)] public int speed;
}
