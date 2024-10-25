using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorStructureCollection : MonoBehaviour
{
    [Header("Collector Structure Specifics")]
    [SerializeField] private CollectorStructureSO collectorStructureSO;

    public CollectorStructureSO CollectorStructureSO => collectorStructureSO;

    private float timer;
    private float collectionCooldown;

    public event EventHandler<OnGoldCollectedEventArgs> OnGoldCollected;

    public static event EventHandler<OnAnyGoldCollectedEventArgs> OnAnyGoldCollected;

    public class OnGoldCollectedEventArgs : EventArgs
    {
        public int gold;
    }

    public class OnAnyGoldCollectedEventArgs : EventArgs
    {
        public CollectorStructureCollection collectorStructureCollection;
        public int gold;
    }

    private void Start()
    {
        SetCollectionCooldown();
        ResetTimer();
    }

    private void Update()
    {
        HandleGoldCollection();
    }

    private void HandleGoldCollection()
    {
        if (CollectionOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        CollectGold();
        ResetTimer();
    }

    private void CollectGold()
    {
        OnAnyGoldCollected?.Invoke(this, new OnAnyGoldCollectedEventArgs { collectorStructureCollection = this, gold = collectorStructureSO.moneyPerCollection});
        OnGoldCollected?.Invoke(this, new OnGoldCollectedEventArgs { gold = collectorStructureSO.moneyPerCollection});
    }

    private void SetCollectionCooldown() => collectionCooldown = collectorStructureSO.collectionTime;
    private void ResetTimer() => timer = 0f;
    private void MaxTimer() => timer = collectionCooldown;
    private bool CollectionOnCooldown() => timer < collectionCooldown;
}
