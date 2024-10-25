using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitsUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI unitsText;

    private int currentUnits;
    private int currentCapacity;

    private void OnEnable()
    {
        EntitiesManager.OnAnyAllyEntityAdded += EntitiesManager_OnAnyAllyEntityAdded;
        EntitiesManager.OnAnyAllyEntityRemoved += EntitiesManager_OnAnyAllyEntityRemoved;

        CapacityManager.OnCapacityInitialized += CapacityManager_OnCapacityInitialized;
        CapacityManager.OnCapacityIncreased += CapacityManager_OnCapacityIncreased;
        CapacityManager.OnCapacityDecreased += CapacityManager_OnCapacityDecreased;
    }

    private void OnDisable()
    {
        EntitiesManager.OnAnyAllyEntityAdded -= EntitiesManager_OnAnyAllyEntityAdded;
        EntitiesManager.OnAnyAllyEntityRemoved -= EntitiesManager_OnAnyAllyEntityRemoved;

        CapacityManager.OnCapacityInitialized -= CapacityManager_OnCapacityInitialized;
        CapacityManager.OnCapacityIncreased -= CapacityManager_OnCapacityIncreased;
        CapacityManager.OnCapacityDecreased -= CapacityManager_OnCapacityDecreased;
    }

    private void UpdateUnitsText() => unitsText.text = $"{currentUnits}/{currentCapacity}";


    #region EntitiesManager Subscriptions
    private void EntitiesManager_OnAnyAllyEntityAdded(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        currentUnits = e.allyEntitiesCount;
        UpdateUnitsText();
    }

    private void EntitiesManager_OnAnyAllyEntityRemoved(object sender, EntitiesManager.OnEntityEventArgs e)
    {
        currentUnits = e.allyEntitiesCount;
        UpdateUnitsText();
    }
    #endregion

    #region CapacityManager Subscriptions
    private void CapacityManager_OnCapacityInitialized(object sender, CapacityManager.OnCapacityEventArgs e)
    {
        currentCapacity = e.capacity;
        UpdateUnitsText();
    }

    private void CapacityManager_OnCapacityIncreased(object sender, CapacityManager.OnCapacityEventArgs e)
    {
        currentCapacity = e.capacity;
        UpdateUnitsText();
    }

    private void CapacityManager_OnCapacityDecreased(object sender, CapacityManager.OnCapacityEventArgs e)
    {
        currentCapacity = e.capacity;
        UpdateUnitsText();
    }
    #endregion
}
