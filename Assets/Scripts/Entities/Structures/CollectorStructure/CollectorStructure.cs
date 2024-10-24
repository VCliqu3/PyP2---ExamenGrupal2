using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorStructure : Structure
{
    [Header("Collector Structure Specifics")]
    [SerializeField] private CollectorStructureSO collectorStructureSO;

    public CollectorStructureSO CollectorStructureSO => collectorStructureSO;

    private void Update()
    {
        HandleGoldCollection();
    }

    private void HandleGoldCollection()
    {

    }
}
