using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance {  get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Settings")]
    [SerializeField] private int gold;

    public int Gold => gold;

    public static event EventHandler<OnGoldEventArgs> OnGoldInitialized;
    public static event EventHandler<OnGoldEventArgs> OnGoldIncreased;
    public static event EventHandler<OnGoldEventArgs> OnGoldDecreased;

    public class OnGoldEventArgs : EventArgs
    {
        public int gold;
    }

    private void OnEnable()
    {
        CollectorStructureCollection.OnAnyGoldCollected += CollectorStructureCollection_OnAnyGoldCollected;
    }

    private void OnDisable()
    {
        CollectorStructureCollection.OnAnyGoldCollected -= CollectorStructureCollection_OnAnyGoldCollected;
    }

    private void Start()
    {
        InitializeGold();
    }

    private void InitializeGold()
    {
        gold = gameSettingsSO.startingGold;
        OnGoldInitialized?.Invoke(this, new OnGoldEventArgs { gold = gold });
    }

    private void IncreaseGold(int quantity)
    {
        gold += quantity;
        OnGoldIncreased?.Invoke(this, new OnGoldEventArgs { gold = gold });
    }

    private void DecreaseGold(int quantity)
    {
        gold = gold -quantity < 0 ? 0 : gold;
        OnGoldDecreased?.Invoke(this, new OnGoldEventArgs { gold = gold });
    }

    public bool CanPurchase(int price)
    {
        if (price > gold) return false;

        return true;
    }

    #region CollectorStructureCollection Subscriptions
    private void CollectorStructureCollection_OnAnyGoldCollected(object sender, CollectorStructureCollection.OnAnyGoldCollectedEventArgs e)
    {
        throw new NotImplementedException();
    }
    #endregion
}
