using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectorStructureSO", menuName = "ScriptableObjects/Entities/Structures/CollectorStructure")]
public class CollectorStructureSO : StructureSO
{
    [SerializeField, Range(0f, 10f)] public int moneyPerCollection;
    [SerializeField, Range(10f, 30f)] public int collectionTime;
}
