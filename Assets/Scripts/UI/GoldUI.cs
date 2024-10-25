using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI goldText;

    private void OnEnable()
    {
        GoldManager.OnGoldInitialized += GoldManager_OnGoldInitialized;
        GoldManager.OnGoldIncreased += GoldManager_OnGoldIncreased;
        GoldManager.OnGoldDecreased += GoldManager_OnGoldDecreased;
    }

    private void OnDisable()
    {
        GoldManager.OnGoldInitialized -= GoldManager_OnGoldInitialized;
        GoldManager.OnGoldIncreased -= GoldManager_OnGoldIncreased;
        GoldManager.OnGoldDecreased -= GoldManager_OnGoldDecreased;
    }

    private void UpdateGoldText(int gold) => goldText.text = gold.ToString();

    #region GoldManager Subscriptions
    private void GoldManager_OnGoldInitialized(object sender, GoldManager.OnGoldEventArgs e)
    {
        UpdateGoldText(e.gold);
    }
    private void GoldManager_OnGoldIncreased(object sender, GoldManager.OnGoldEventArgs e)
    {
        UpdateGoldText(e.gold);
    }
    private void GoldManager_OnGoldDecreased(object sender, GoldManager.OnGoldEventArgs e)
    {
        UpdateGoldText(e.gold);
    }

    #endregion
}
