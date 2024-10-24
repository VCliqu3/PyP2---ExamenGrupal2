using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStructure : MonoBehaviour
{
    [Header("Defense Structure Specifics")]
    [SerializeField] private DefenseStructureSO defenseStructureSO;
    [SerializeField] private Entity currentTarget;

    public DefenseStructureSO DefenseStructureSO => defenseStructureSO;
    public Entity CurrentTarget => currentTarget;

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {

    }
}
